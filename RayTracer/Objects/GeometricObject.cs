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
        /// <param name="distance">
        /// Receives the distance to the ray origin of the closest intersection to the 
        /// <paramref name="ray"/>'s origin. The result is undefined if there is no intersection.
        /// </param>
        /// <param name="shadeRecord">
        /// Receives a <see cref="ShadeRecord"/> with details on how to colour or shade that point.
        /// The result is undefined if there is no intersection.
        /// </param>
        /// <returns>
        /// <c>true</c> if the ray intersects the object, <c>false</c> if not.
        /// </returns>
        public abstract bool Hit(Ray ray, out double distance, out ShadeRecord? shadeRecord);
    }
}
