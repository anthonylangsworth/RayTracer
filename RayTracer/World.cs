using RayTracer.Objects;
using RayTracer.Primitives;
using RayTracer.SampleGenerators;
using RayTracer.Tracers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class World
    {
        public static void Main()
        {
            ConcurrentRandom random = new ConcurrentRandom();
            World world = new World();
            foreach (var entry in new[]
            {
                //new {
                //    Scene = Scene.BuildSingleSphere(),
                //    ViewPlane = new ViewPlane(200, 200, 1, 1, new RegularSampleGenerator(random)),
                //    FileName = "3.18.png"
                //},
                //new {
                //    Scene = Scene.BuildTwoSpheresAndPlane(),
                //    ViewPlane = new ViewPlane(300, 300, 1, 1, new RegularSampleGenerator(random)),
                //    FileName = "3.21.png"
                //},
                new {
                    Scene = Scene.BuildTwoSpheresAndPlane(),
                    ViewPlane = new ViewPlane(300, 300, 1, 1, new MultiJitteredSampleGenerator(random, 16)), // new NRooksSampleGenerator(random, 6)), // new JitteredSampleGenerator(random, 36)), // new RegularSampleGenerator(random)), // 
                    FileName = "4.1.png"
                }
            })
            {
                RGBColor[,] result = world.Render(entry.Scene, entry.ViewPlane);
                world.Save(result, entry.FileName, entry.ViewPlane.Gamma);
            }
        }

        public World()
        {
            Tracer = new Tracer();
            Serializer = new ImageSerializer();
        }

        public Tracer Tracer { get; }

        public ImageSerializer Serializer { get; }

        /// <summary>
        /// Render the <see cref="Scene"/> to the <see cref="ViewPlane"/>.
        /// </summary>
        /// <param name="scene">
        /// The <see cref="Scene"/> to render.
        /// </param>
        /// <param name="viewPlane">
        /// The <see cref="ViewPlane"/> to render to.
        /// </param>
        /// <returns>
        /// A 2D array of <see cref="RGBColor"/>s representing pixels. The rows are returned in 
        /// bottom-up order, not top-down.
        /// </returns>
        public RGBColor[,] Render(Scene scene, ViewPlane viewPlane)
        {
            Vector3D rayDirection = new Vector3D(0, 0, -1);
            const double zw = 100.0;
            RGBColor[,] result = new RGBColor[viewPlane.VerticalResolution, viewPlane.HorizontalResolution];

            Parallel.For(0, viewPlane.VerticalResolution, row => // up
            {
                // Declare here because they are thread local
                double x, y;
                Ray ray;
                for (int column = 0; column < viewPlane.HorizontalResolution; column++) // left to right
                {
                    RGBColor pixelColor = RGBColor.Black;
                    foreach (Point2D samplePoint in viewPlane.SampleGenerator.GetSamplesOnUnitSquare())
                    {
                        x = viewPlane.PixelSize * (column - 0.5 * viewPlane.HorizontalResolution + samplePoint.X);
                        y = viewPlane.PixelSize * (row - 0.5 * viewPlane.VerticalResolution + samplePoint.Y);
                        ray = new Ray(new Point3D(x, y, zw), rayDirection);
                        pixelColor += Tracer.TraceRay(scene, ray);
                    }
                    pixelColor /= viewPlane.SampleGenerator.SamplesPerSet;
                    result[row, column] = pixelColor;
                }
            });

            return result;
        }

        /// <summary>
        /// Serialize the image to a file.
        /// </summary>
        /// <param name="output">
        /// The output from <see cref="Render(Scene, ViewPlane)"/>.
        /// </param>
        /// <param name="fileName">
        /// The path or file name to output to.
        /// </param>
        /// <param name="gamma">
        /// The gamma to use, defaulting to 1.0.
        /// </param>
        public void Save(RGBColor[,] output, string fileName, double gamma = 1.0)
        {
            Serializer.Save(output, fileName, gamma);
        }
    }
}
