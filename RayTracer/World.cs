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
            DrawingImageSerializer imageSerializer = new DrawingImageSerializer();
            foreach (World world in new[]
            {
                //new World(
                //    "3.18",
                //    Scene.BuildSingleSphere(),
                //    new ViewPlane(200, 200, 1, 1, new RegularSampleGenerator(random))
                //),
                //new World(
                //    "3.21",
                //    Scene.BuildTwoSpheresAndPlane(),
                //    new ViewPlane(300, 300, 1, 1, new RegularSampleGenerator(random))
                //),
                new World(
                    "4.1",
                    Scene.BuildTwoSpheresAndPlane(),
                    new ViewPlane(300, 300, 1, 1, new MultiJitteredSampleGenerator(random, 16)) // new NRooksSampleGenerator(random, 6)), // new JitteredSampleGenerator(random, 36)), // new RegularSampleGenerator(random)), // 
                )
            })
            {
                imageSerializer.Save(world.Render(), world.Name, world.ViewPlane.Gamma);
            }
        }

        public World(string name, Scene scene, ViewPlane viewPlane)
        {
            Tracer = new Tracer();
            Name = name;
            Scene = scene;
            ViewPlane = viewPlane;
        }

        public Tracer Tracer { get; }
        public string Name { get; }
        public Scene Scene { get; }
        public ViewPlane ViewPlane { get; }

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
        public RGBColor[,] Render()
        {
            Vector3D rayDirection = new Vector3D(0, 0, -1);
            const double zw = 100.0;
            RGBColor[,] result = new RGBColor[ViewPlane.VerticalResolution, ViewPlane.HorizontalResolution];

            Parallel.For(0, ViewPlane.VerticalResolution, row => // up
            {
                // Declare here because they are thread local
                double x, y;
                Ray ray;
                for (int column = 0; column < ViewPlane.HorizontalResolution; column++) // left to right
                {
                    RGBColor pixelColor = RGBColor.Black;
                    foreach (Point2D samplePoint in ViewPlane.SampleGenerator.GetSamplesOnUnitSquare())
                    {
                        x = ViewPlane.PixelSize * (column - 0.5 * ViewPlane.HorizontalResolution + samplePoint.X);
                        y = ViewPlane.PixelSize * (row - 0.5 * ViewPlane.VerticalResolution + samplePoint.Y);
                        ray = new Ray(new Point3D(x, y, zw), rayDirection);
                        pixelColor += Tracer.TraceRay(Scene, ray);
                    }
                    pixelColor /= ViewPlane.SampleGenerator.SamplesPerSet;
                    result[row, column] = pixelColor;
                }
            });

            return result;
        }
    }
}
