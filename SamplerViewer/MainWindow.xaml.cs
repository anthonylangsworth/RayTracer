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
    internal enum DotType
    {
        Dot,
        Digit
    }

    internal enum SampleProjection
    {
        UnitSquare,
        UnitDisk
    }

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

        private void CreateSamplePlot(Canvas canvas, SampleGenerator sampleGenerator, int sqrtSamplesPerSet, DotType dotType, SampleProjection sampleProjection)
        {
            const double pixelsPerInch = 96; // Cannot find this constant in WPF
            double extent = 6 * pixelsPerInch; // plot is 6 inches wide and high
            canvas.Children.Clear();
            try
            {
                if (sampleProjection == SampleProjection.UnitDisk)
                {
                    DrawCircle(canvas, extent);
                }
                else
                {
                    DrawAxes(canvas, extent, sqrtSamplesPerSet);
                }

                DrawSampleGeneratorPoints(canvas, sampleGenerator, extent, dotType, sampleProjection);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                canvas.Children.Clear();
            }
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

        private void DrawCircle(Canvas canvas, double extent)
        {
            Ellipse ellipse = new()
            {
                Height = extent,
                Width = extent,
                Stroke = Brushes.LightGray
            };
            canvas.Children.Add(ellipse);
            Canvas.SetLeft(ellipse, 0);
            Canvas.SetTop(ellipse, 0);
        }

        private void DrawSampleGeneratorPoints(Canvas canvas, SampleGenerator sampleGenerator, double extent, DotType dotType, SampleProjection sampleProjection)
        {
            double diameter;
            switch(dotType)
            {
                case DotType.Dot:
                    diameter = 10;
                    break;
                case DotType.Digit:
                    // Getting this from dot.ActualHeight is not possible at this rendering stage
                    diameter = 16; 
                    break;
                default:
                    throw new ArgumentException($"Unknown DotType: '{dotType}'", nameof(dotType));
            }

            IEnumerable<Point2D> samples;
            Func<Point2D, Point2D> pointTransform;
            switch(sampleProjection)
            {
                case SampleProjection.UnitSquare:
                    samples = sampleGenerator.GetSamplesOnUnitSquare();
                    pointTransform = p => p;
                    break;
                case SampleProjection.UnitDisk:
                    samples = sampleGenerator.GetSamplesOnUnitDisk();
                    pointTransform = p => new Point2D(p.X / 2 + 0.5, p.Y / 2 + 0.5);
                    break;
                default:
                    throw new ArgumentException($"Unknown sample projection: '{sampleProjection}'", nameof(sampleProjection));
            }

            int index = 0;
            foreach (Point2D point2D in samples.Select(pointTransform))
            {
                FrameworkElement dot;
                if (dotType == DotType.Digit)
                {
                    dot = new Label()
                    {
                        Content = index++.ToString(),
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                }
                else if (dotType == DotType.Dot)
                {
                    dot = new Ellipse()
                    {
                        Height = diameter,
                        Width = diameter,
                        Fill = Brushes.Black
                    };
                }
                else
                {
                    throw new ArgumentException($"Unknown DotType: '{dotType}'", nameof(dotType));
                }

                canvas.Children.Add(dot);
                Canvas.SetLeft(dot, point2D.X * extent - diameter / 2);
                Canvas.SetTop(dot, extent - point2D.Y * extent - diameter / 2); // Invert due top down drawing coordinate system
            }
        }

        private void FillPointsListBox(ListBox listbox, SampleGenerator sampleGenerator)
        {
            listbox.Items.Clear();
            int i = 0;
            foreach (Point2D point2D in sampleGenerator.GetSamplesOnUnitSquare())
            {
                ListBoxItem listBoxItem = new ListBoxItem();
                listBoxItem.Content = i++ + ": " + point2D.ToString();
                listbox.Items.Add(listBoxItem);
            }
        }

        private void GenerateSamplerPlot(object sender, EventArgs e)
        {
            int samplesPerSet = Convert.ToInt32(((ComboBoxItem) samplesPerSetCombo.SelectedValue).Content);

            string? samplerName = Convert.ToString(((ComboBoxItem) samplersCombo.SelectedValue).Content);
            SampleGenerator? sampleGenerator;
            switch(samplerName)
            {
                case "Regular":
                    sampleGenerator = new RegularSampleGenerator(new Random());
                    break;
                case "Jittered":
                    sampleGenerator = new JitteredSampleGenerator(new Random(), samplesPerSet);
                    break;
                case "Multi-Jittered":
                    sampleGenerator = new MultiJitteredSampleGenerator(new Random(), samplesPerSet);
                    break;
                case "n-Rooks":
                    sampleGenerator = new NRooksSampleGenerator(new Random(), samplesPerSet);
                    break;
                case "Hammersley":
                    sampleGenerator = new HammersleySampleGenerator(new Random(), samplesPerSet);
                    break;
                default:
                    throw new InvalidOperationException($"Unknown sampler name: '{samplerName}'");
            }

            string? dotTypeName = Convert.ToString(((ComboBoxItem)dotTypeCombo.SelectedValue).Content);
            DotType dotType = DotType.Dot;
            switch (dotTypeName) // Nicer than Enum.Parse
            {
                case "Dot":
                    dotType = DotType.Dot;
                    break;
                case "Digit":
                    dotType = DotType.Digit;
                    break;
                default:
                    throw new InvalidOperationException($"Unknown dot type: '{dotTypeName ?? "(null)"}'");
            }

            string? projectionName = Convert.ToString(((ComboBoxItem)projectionCombo.SelectedValue).Content);
            SampleProjection sampleProjection = SampleProjection.UnitSquare;
            switch (projectionName) // Nicer than Enum.Parse
            {
                case "Square":
                    sampleProjection = SampleProjection.UnitSquare;
                    break;
                case "Disk":
                    sampleProjection = SampleProjection.UnitDisk;
                    break;
                default:
                    throw new InvalidOperationException($"Unknown projection: '{projectionName ?? "(null)"}'");
            }

            if (sampleGenerator != null)
            {
                CreateSamplePlot(samplerCanvas, sampleGenerator, (int) Math.Sqrt(samplesPerSet), dotType, sampleProjection);
                FillPointsListBox(pointsListBox, sampleGenerator);
            }
        }
    }
}
