using RayTracer.SampleGenerators;
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

namespace Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
            World = new World("4.1", Scene.BuildTwoSpheresAndPlane(), new ViewPlane(300, 300, 1, 1, new SampleGenerator(SampleAlgorithms.MultiJittered, random, 16, 16)));
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            BitmapSource bitmapSource = new MediaImageSerializer().Serialize(World.Render(), World.ViewPlane.Gamma);
            image.Source = bitmapSource;
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
