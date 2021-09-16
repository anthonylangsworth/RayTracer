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
            World world = new World(200, 200);
            Scene scene = world.Build();
            RGBColor[,] result = world.Render(scene);
            world.Save(result, "image.png");
        }

        public World(int horizontalResolution, int verticalResolution)
        {
            // TODO: Pass in the ViewPlane?
            ViewPlane = new ViewPlane(horizontalResolution, verticalResolution, 1, 1);
            Tracer = new Tracer();
            Serializer = new ImageSerializer();
        }

        public ViewPlane ViewPlane { get; }

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

        public RGBColor[,] Render(Scene scene)
        {
            Vector3D rayDirection = new Vector3D(0, 0, -1);
            const double zw = 100.0;
            double x, y;
            Ray ray;

            RGBColor[,] result = new RGBColor[ViewPlane.VerticalResolution, ViewPlane.HorizontalResolution];

            for(int row = 0; row < ViewPlane.VerticalResolution; row++)
            {
                for(int column = 0; column < ViewPlane.HorizontalResolution; column++)
                {
                    x = ViewPlane.PixelSize * (column - 0.5 * (ViewPlane.HorizontalResolution - 1.0));
                    y = ViewPlane.PixelSize * (row - 0.5 * (ViewPlane.VerticalResolution - 1.0));
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
