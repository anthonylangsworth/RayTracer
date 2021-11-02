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

namespace Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ConcurrentRandom random = new ConcurrentRandom();
            Views = new[]
            {
                //new View(
                //    Scene.BuildSingleSphere(),
                //    new ViewPlane(200, 200, 1, 1, new RegularSampleGenerator(random)),
                //    "3.18"
                //),
                //new View(
                //    Scene.BuildTwoSpheresAndPlane(),
                //    new ViewPlane(300, 300, 1, 1, new RegularSampleGenerator(random)),
                //    "3.21"
                //),
                new View(
                    Scene.BuildTwoSpheresAndPlane(),
                    new ViewPlane(300, 300, 1, 1, new MultiJitteredSampleGenerator(random, 16)), // new NRooksSampleGenerator(random, 6)), // new JitteredSampleGenerator(random, 36)), // new RegularSampleGenerator(random)), // 
                    "4.1"
                )
            };

            // RGBColor[,] result = World.Render(view.Scene, view.ViewPlane);
        }

        public IEnumerable<View> Views{ get; }

        private void SaveMenu_Click(object sender, RoutedEventArgs e)
        {
            // World.Save(result, entry.FileName, entry.ViewPlane.Gamma);
        }
    }
}
