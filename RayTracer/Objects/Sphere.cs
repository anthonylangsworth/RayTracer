using System;
using System.Collections.Generic;
using System.Linq;
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
        public double Radius { get; }
             
        /// <inheritdoc/>
        public override HitResult Hit(Ray ray)
        {
            Vector3D temp = ray.Origin - Location; // Translate to origin for simpler calculations
            double a = ray.Direction.Dot(ray.Direction);
            double b = 2 * temp.Dot(ray.Direction);
            double c = temp.Dot(temp) - Radius * Radius;
            double disc = b * b - 4 * a * c;
            HitResult result = new Miss();

            if (disc > 0)
            {
                double e = Math.Sqrt(disc);
                double denominator = 2 * a;

                double t = new[]
                    {
                        (-b - e) / denominator, // Smaller root
                        (-b + e) / denominator, // Larger root
                    }.FirstOrDefault(t => t > DoubleEpsilonEqualityComparer.DefaultEpsilon);

                if (t != default(double)) // default(double) == 0.0 which is less than DoubleEpsilonEqualityComparer.DefaultEpsilon
                {
                    result = new Hit(
                            t,
                            new ShadeRecord(
                                (temp + t * ray.Direction) / Radius,
                                ray.Origin + t * ray.Direction
                            )
                       );
                }
            }

            return result;
        }
    }
}
