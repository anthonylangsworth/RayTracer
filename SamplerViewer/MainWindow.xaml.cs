using RayTracer.Primitives;
using RayTracer.SampleGenerators;
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

        public SampleGenerator? SampleGenerator { get; set; }
        
        private void CreateSamplePlot(Canvas canvas, SampleGenerator sampleGenerator, int sqrtSamplesPerSet)
        {
            double extent = 5 * 96; // plot is 5 inches wide and high
            canvas.Children.Clear();
            DrawAxes(canvas, extent, sqrtSamplesPerSet);
            DrawSamplerPoints(canvas, sampleGenerator, extent);
        }

        private void DrawAxes(Canvas canvas, double extent, int sqrtSamplesPerSet)
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

            for(int i = 1; i < sqrtSamplesPerSet; i++)
            {
                Line guide = new()
                {
                    X1 = 0,
                    Y1 = (sqrtSamplesPerSet - i) / (double) sqrtSamplesPerSet * extent,
                    X2 = extent,
                    Y2 = (sqrtSamplesPerSet - i) / (double) sqrtSamplesPerSet * extent,
                    Stroke = Brushes.LightGray
                };
                canvas.Children.Add(guide);
                Canvas.SetLeft(guide, 0);
                Canvas.SetTop(guide, 0);
            }

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

            for (int i = 1; i < sqrtSamplesPerSet; i++)
            {
                Line guide = new()
                {
                    X1 = (sqrtSamplesPerSet - i) / (double)sqrtSamplesPerSet * extent,
                    Y1 = 0,
                    X2 = (sqrtSamplesPerSet - i) / (double)sqrtSamplesPerSet * extent,
                    Y2 = extent,
                    Stroke = Brushes.LightGray
                };
                canvas.Children.Add(guide);
                Canvas.SetLeft(guide, 0);
                Canvas.SetTop(guide, 0);
            }
        }

        private void DrawSamplerPoints(Canvas canvas, SampleGenerator sampleGenerator, double extent)
        {
            double diameter = 10;
            // int index = 1;
            foreach (Point2D point2D in sampleGenerator.GetSamplesOnUnitSquare())
            {
                //Label dot = new Label()
                //{
                //    Content = index++.ToString(),
                //    HorizontalAlignment = HorizontalAlignment.Center
                //};
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

        private void FillPointsListBox(ListBox listbox, SampleGenerator sampleGenerator)
        {
            listbox.Items.Clear();
            foreach (Point2D point2D in sampleGenerator.GetSamplesOnUnitSquare())
            {
                ListBoxItem listBoxItem = new ListBoxItem();
                listBoxItem.Content = point2D.ToString();
                listbox.Items.Add(listBoxItem);
            }
        }

        private void GenerateSamplerPlot(object sender, EventArgs e)
        {
            int samplesPerSet = Convert.ToInt32(((ComboBoxItem) samplesPerSetCombo.SelectedValue).Content);
            string? samplerName = Convert.ToString(((ComboBoxItem) samplersCombo.SelectedValue).Content);
            switch(samplerName)
            {
                case "Regular":
                    SampleGenerator = new RegularSampleGenerator(new Random());
                    break;
                case "Jittered":
                    SampleGenerator = new JitteredSampleGenerator(new Random(), samplesPerSet);
                    break;
                case "Multi-Jittered":
                    SampleGenerator = new MultiJitteredSampleGenerator(new Random(), samplesPerSet);
                    break;
                case "n-Rooks":
                    SampleGenerator = new NRooksSampleGenerator(new Random(), samplesPerSet);
                    break;
                default:
                    MessageBox.Show($"Unknown sampler name: '{samplerName}'");
                    SampleGenerator = null;
                    break;
            }

            if (SampleGenerator != null)
            {
                CreateSamplePlot(samplerCanvas, SampleGenerator, (int) Math.Sqrt(samplesPerSet));
                FillPointsListBox(pointsListBox, SampleGenerator);
            }
        }
    }
}
