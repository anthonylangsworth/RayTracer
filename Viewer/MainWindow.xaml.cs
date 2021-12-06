using RayTracer.SampleGenerators;
using RayTracer.Cameras;
using RayTracer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using Microsoft.Win32;
using System.IO;
using System.Reflection;
using RayTracer.Primitives;

namespace Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly string AppName = "Ray Tracer";

        public MainWindow()
        {
            // Fix reversed menu drop. From https://www.red-gate.com/simple-talk/blogs/wpf-menu-displays-to-the-left-of-the-window/.
            FieldInfo? menuDropAlignmentField = typeof(SystemParameters).GetField("_menuDropAlignment", BindingFlags.NonPublic | BindingFlags.Static);
            Action setAlignmentValue = () => {
                if (SystemParameters.MenuDropAlignment && menuDropAlignmentField != null)
                {
                    menuDropAlignmentField.SetValue(null, false);
                }
            };
            setAlignmentValue();
            SystemParameters.StaticPropertyChanged += (sender, e) => { setAlignmentValue(); };

            // Create world
            ConcurrentRandom random = new ConcurrentRandom();
            //World = new World("4.1",
            //    new Pinhole(new Point3D(0, 0, 200), new Point3D(0, 0, 0), new Vector3D(0, 1, 0), 100),
            //    Scenes.BuildTwoSpheresAndPlane(),
            //    new ViewPlane(600, 600, 1, 1,
            //        new SampleGenerator<Point2D>(SampleAlgorithms.MultiJittered, SampleMappers.UnitSquare, random, 16, 16)));

            // Three spheres
            //World = new World("Three Spheres with Pinhole Camera",
            //    new Pinhole(new Point3D(0, 30, 100), new Point3D(0, 30, 0), new Vector3D(0, 1, 0), 300),
            //    Scenes.BuildThreeSpheresAbovePlane(), 
            //    new ViewPlane(600, 600, 1, 1, 
            //        new UnitSquareMappedSampleGenerator(SampleAlgorithms.MultiJittered, random, 16, 16)));

            // Three spheres with a thin lens camera
            World = new World(
                name: "Three Spheres with Thin Lens Camera",
                camera: new ThinLens(
                    eye: new Point3D(0, 30, 100), 
                    lookAt: new Point3D(0, 30, 0), 
                    up: new Vector3D(0, 1, 0), 
                    lensRadius: 5,
                    viewPlaneDistance: 150,
                    focalPlaneDistance: 150, 
                    zoom: 2, 
                    blur: new UnitDiskMappedSampleGenerator(SampleAlgorithms.MultiJittered, random, 16, 16)
                ),
                scene: Scenes.BuildThreeSpheresAbovePlane(),
                viewPlane: new ViewPlane(
                    horizontalResolution: 600,
                    verticalResolution: 600, 
                    pixelSize: 1, 
                    gamma: 1,
                    antialiasing: new UnitSquareMappedSampleGenerator(SampleAlgorithms.MultiJittered, random, 16, 16)));
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            BitmapSource bitmapSource = new MediaImageSerializer().Serialize(World.Render(), World.ViewPlane.Gamma);
            image.Source = bitmapSource;
            Title = AppName + " - " + World.Name;
        }

        public World World { get; }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = World.Name + ".png";
            saveFileDialog.Filter = "PNG Files|*.png";
            if (saveFileDialog.ShowDialog(this) == true)
            {
                // From https://stackoverflow.com/questions/2900447/how-to-save-a-wpf-bitmapsource-image-to-a-file
                using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate))
                {
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)image.Source));
                    encoder.Save(fileStream);
                }
            }
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
