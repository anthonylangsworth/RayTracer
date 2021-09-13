using RayTracer.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Objects
{
    public class Plane : GeometricObject
    {
        public Plane(Point3D point, Material material, Vector3D normal)
            : base(point, material)
        {
            Normal = normal;
        }

        public Vector3D Normal { get; }

        /// <inheritdoc/>
        public override bool Hit(Ray ray, out double distance, out ShadeRecord? shadeRecord)
        {
            distance = (Location - ray.Origin).Dot(Normal) / (ray.Direction.Dot(Normal));
            bool result;

            if (distance > DoubleEpsilonEqualityComparer.DefaultEpsilon)
            {
                shadeRecord = new ShadeRecord(Normal, ray.Origin + distance * ray.Direction);
                result = true;
            }
            else
            {
                shadeRecord = null;
                result = false;
            }

            return result;
        }
    }
}
