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
            Location = location;
            Material = material;
        }

        /// <summary>
        /// Usually the centre point in world coordinates.
        /// </summary>
        public Point3D Location { get; }

        /// <summary>
        /// The material describing how to colour or shade hits.
        /// </summary>
        public Material Material { get; }

        /// <summary>
        /// Intersect a ray with this object. If there are multiple intersections, return details 
        /// of the closest to the <paramref name="ray"/>'s origin.
        /// </summary>
        /// <param name="ray">
        /// The <see cref="Ray"/> to intersect.
        /// </param>
        /// <returns>
        /// A <see cref="RayTracer.Hit"/> or <see cref="RayTracer.Miss"/> indicating whether the ray
        /// hit or missed the object.
        /// </returns>
        public abstract HitResult Hit(Ray ray);
    }
}
