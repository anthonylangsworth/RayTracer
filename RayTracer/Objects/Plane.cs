using RayTracer.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Objects
{
    public class Plane: GeometricObject
    {
        public Plane(Point3D point, Material material, Vector3D normal)
            : base(point, material)
        {
            Normal = normal;
        }

        public Vector3D Normal { get; }

        public override bool Hit(Ray ray, out double tmin, out ShadeRecord shadeRecord)
        {
            
        }
    }
}
