using RayTracer.Primitives;
using RayTracer.Samplers;
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

namespace SamplerViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GenerateSamplerPlot(this, EventArgs.Empty);
        }

        public Sampler? Sampler { get; set; }
        
        private void CreateSamplePlot(Canvas canvas, Sampler sampler)
        {
            double extent = 5 * 96; // plot is 5 inches wide and high
            canvas.Children.Clear();
            DrawAxes(canvas, extent);
            DrawSamplerPoints(canvas, sampler, extent);
        }

        private void DrawAxes(Canvas canvas, double extent)
        {
            Line horizontalAxis = new()
            {
                X1 = 0,
                Y1 = extent,
                X2 = extent,
                Y2 = extent,
                Stroke = Brushes.Black
            };
            canvas.Children.Add(horizontalAxis);
            Canvas.SetLeft(horizontalAxis, 0);
            Canvas.SetTop(horizontalAxis, 0);

            Line verticalAxis = new()
            {
                X1 = 0,
                Y1 = 0,
                X2 = 0,
                Y2 = extent,
                Stroke = Brushes.Black
            };
            canvas.Children.Add(verticalAxis);
            Canvas.SetLeft(verticalAxis, 0);
            Canvas.SetTop(verticalAxis, 0);
        }

        private void DrawSamplerPoints(Canvas canvas, Sampler sampler, double extent)
        {
            foreach(Point2D point2D in sampler.GetSamplesOnUnitSquare())
            {
                double diameter = 10;
                Ellipse dot = new Ellipse
                {
                    Height = diameter,
                    Width = diameter,
                    Fill = Brushes.Black
                };
                canvas.Children.Add(dot);
                Canvas.SetLeft(dot, point2D.X * extent - diameter / 2);
                Canvas.SetTop(dot, extent - point2D.Y * extent - diameter / 2); // Invert due top down drawing coordinate system
            }
        }

        private void FillPointsListBox(ListBox listbox, Sampler sampler)
        {
            listbox.Items.Clear();
            foreach (Point2D point2D in sampler.GetSamplesOnUnitSquare())
            {
                ListBoxItem listBoxItem = new ListBoxItem();
                listBoxItem.Content = point2D.ToString();
                listbox.Items.Add(listBoxItem);
            }
        }

        private void GenerateSamplerPlot(object sender, EventArgs e)
        {
            int samplesPerSet = Convert.ToInt32(((ComboBoxItem) samplesPerSetCombo.SelectedValue).Content);
            string? samplerName = Convert.ToString(((ComboBoxItem)samplersCombo.SelectedValue).Content);
            switch(samplerName)
            {
                case "Regular":
                    Sampler = new RegularSampler(new Random());
                    break;
                case "Jittered":
                    Sampler = new JitteredSampler(new Random(), samplesPerSet);
                    break;
                case "Multi-Jittered":
                    Sampler = new MultiJitteredSampler(new Random(), samplesPerSet);
                    break;
                case "n-Rooks":
                    Sampler = new NRooksSampler(new Random(), samplesPerSet);
                    break;
                default:
                    MessageBox.Show($"Unknown sampler name: '{samplerName}'");
                    Sampler = null;
                    break;
            }

            if (Sampler != null)
            {
                CreateSamplePlot(samplerCanvas, Sampler);
                FillPointsListBox(pointsListBox, Sampler);
            }
        }
    }
}
