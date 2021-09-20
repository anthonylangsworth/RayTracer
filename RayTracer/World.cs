using RayTracer.Objects;
using RayTracer.Primitives;
using RayTracer.Tracers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    internal class World
    {
        public static void Main()
        {
            World world = new World();
            ViewPlane viewPlane = new ViewPlane(300, 300, 1, 1);
            foreach (var entry in new[]
            {
                new {
                    Scene = world.BuildSingleSphere(),
                    ViewPlane = new ViewPlane(200, 200, 1, 1),
                    FileName = "3.18.png"
                },
                new {
                    Scene = world.BuildTwoSpheresAndPlane(),
                    ViewPlane = new ViewPlane(300, 300, 1, 1),
                    FileName = "3.21.png"
                }
            })
            {
                RGBColor[,] result = world.Render(entry.Scene, entry.ViewPlane);
                world.Save(result, entry.FileName, viewPlane.Gamma);
            }
        }

        public World()
        {
            Tracer = new Tracer();
            Serializer = new ImageSerializer();
        }

        public Tracer Tracer { get; }

        public ImageSerializer Serializer { get; }

        public Scene BuildSingleSphere()
        {
            return new Scene(
                new Camera(), 
                new[] 
                { 
                    new Sphere(new Point3D(0, 0, 0), new Material(RGBColor.BrightRed), 85) 
                }, 
                new LightSource[0],
                RGBColor.Black);
        }

        public Scene BuildTwoSpheresAndPlane()
        {
            return new Scene(
                new Camera(),
                new GeometricObject[]
                {
                    new Sphere(new Point3D(0, -25, 0), new Material(RGBColor.BrightRed), 80),
                    new Sphere(new Point3D(0, 30, 0), new Material(RGBColor.Yellow), 60),
                    new Plane(new Point3D(0, 0, 0), new Material(RGBColor.DarkGreen), new Vector3D(0, 1, 1))
                },
                new LightSource[0],
                RGBColor.Black);
        }

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
            double x, y;
            Ray ray;

            RGBColor[,] result = new RGBColor[viewPlane.VerticalResolution, viewPlane.HorizontalResolution];

            for(int row = 0; row < viewPlane.VerticalResolution; row++)
            {
                for(int column = 0; column < viewPlane.HorizontalResolution; column++)
                {
                    x = viewPlane.PixelSize * (column - 0.5 * (viewPlane.HorizontalResolution - 1.0));
                    y = viewPlane.PixelSize * (row - 0.5 * (viewPlane.VerticalResolution - 1.0));
                    ray = new Ray(new Point3D(x, y, zw), rayDirection);
                    result[row, column] = Tracer.TraceRay(scene, ray);
                }
            }

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
        /// The gamem to use, defaulting to 1.0.
        /// </param>
        public void Save(RGBColor[,] output, string fileName, double gamma = 1.0)
        {
            Serializer.Save(output, fileName, gamma);
        }
    }
}
