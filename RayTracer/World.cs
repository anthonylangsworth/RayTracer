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
            Scene scene = world.Build();
            ViewPlane viewPlane = new ViewPlane(200, 200, 1, 1);
            RGBColor[,] result = world.Render(scene, viewPlane);
            world.Save(result, "image.png");
        }

        public World()
        {
            Tracer = new Tracer();
            Serializer = new ImageSerializer();
        }

        public Tracer Tracer { get; }

        public ImageSerializer Serializer { get; }

        public Scene Build()
        {
            return new Scene(
                new Camera(), 
                new[] 
                { 
                    new Sphere(new Point3D(0, 0, 0), new Material(), 20) 
                }, 
                new LightSource[0],
                new RGBColor(0, 0, 0));
        }

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

        public void Save(RGBColor[,] output, string fileName)
        {
            Serializer.Save(output, fileName);
        }
    }
}
