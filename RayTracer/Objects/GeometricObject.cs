using System;
using System.Collections.Generic;
using System.Text;
using RayTracer.Primitives;

namespace RayTracer.Objects
{
    public abstract class GeometricObject
    {
        protected GeometricObject(Point3D location, Material material)
        {
            if (material == null)
            {
                throw new ArgumentNullException(nameof(material));
            }

            Location = location;
            Material = material;
        }

        public Point3D Location { get; }

        public Material Material { get; }

        public abstract bool Hit(Ray ray, out double tmin, out ShadeRecord shadeRecord);
    }
}
