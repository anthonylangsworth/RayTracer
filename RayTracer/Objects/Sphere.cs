using System;
using System.Collections.Generic;
using System.Text;
using RayTracer.Primitives;

namespace RayTracer.Objects
{
    /// <summary>
    /// A sphere.
    /// </summary>
    public class Sphere : GeometricObject
    {
        /// <summary>
        /// Create a sphere.
        /// </summary>
        /// <param name="location">
        /// The centre point.
        /// </param>
        /// <param name="material">
        /// The <see cref="Material"/> used to colour or shade the sphere.
        /// </param>
        /// <param name="radius">
        /// The sphere's radius in world units. This must be positive.
        /// </param>
        /// <exception cref="ArgumentException">
        /// <paramref name="radius"/> must be positive.
        /// </exception>
        public Sphere(Point3D location, Material material, double radius)
            : base(location, material)
        {
            if (radius <= 0)
            {
                throw new ArgumentException($"{nameof(radius)} must be positive", nameof(radius));
            }

            Radius = radius;
        }

        /// <summary>
        /// The sphere's radius in world units.
        /// </summary>
        public double Radius { get; set; }

        /// <inheritdoc/>
        public override bool Hit(Ray ray, out double tmin, out ShadeRecord shadeRecord)
        {
            throw new NotImplementedException();
        }
    }
}
