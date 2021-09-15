using RayTracer.Objects;
using RayTracer.Primitives;
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
            world.Render(scene, 200, 200);
        }

        public Scene Build()
        {
            return new Scene(
                new Camera(), 
                new[] 
                { 
                    new Sphere(new Point3D(0, 0, 0), new Material(), 1) 
                }, 
                new LightSource[0]);
        }

        public void Render(Scene scene, int hres, int vres)
        {
            ViewPlane viewPlane = new ViewPlane(100, 100, 1, 1);
        }
    }
}
