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

namespace Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // Do nothing
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            ConcurrentRandom random = new ConcurrentRandom();
            World world = new World("4.1", Scene.BuildTwoSpheresAndPlane(), new ViewPlane(300, 300, 1, 1, new MultiJitteredSampleGenerator(random, 16)));
            BitmapSource bitmapSource = new MediaImageSerializer().Serialize(world.Render(), world.ViewPlane.Gamma);
            image.Source = bitmapSource;
        }

        private void SaveMenu_Click(object sender, RoutedEventArgs e)
        {
            // World.Save(result, entry.FileName, entry.ViewPlane.Gamma);
        }
    }
}
