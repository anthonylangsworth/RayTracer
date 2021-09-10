using System;
using System.Collections.Generic;
using System.Text;
using RayTracer.Primitives;

namespace RayTracer.Objects
{
    public class Sphere : GeometricObject
    {
        public Sphere(Point3D location, Material material, double radius)
            : base(location, material)
        {
            if (radius <= 0)
            {
                throw new ArgumentException($"{nameof(radius)} must be positive", nameof(radius));
            }

            Radius = radius;
        }

        public double Radius { get; set; }

        public override ShadeRecord? Hit(Ray ray, double tmin)
        {
            throw new NotImplementedException();
        }
    }
}
