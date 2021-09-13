using RayTracer.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Objects
{
    /// <summary>
    /// A 3D flat, infinite plane.
    /// </summary>
    public class Plane : GeometricObject, IEquatable<Plane?>
    {
        /// <summary>
        /// Create a Plane.
        /// </summary>
        /// <param name="location">
        /// A point on the plane.
        /// </param>
        /// <param name="material">
        /// The material used to colour or shade the plane.
        /// </param>
        /// <param name="normal">
        /// A normal to the plane, specifying its orientation at <paramref name="location"/>.
        /// </param>
        public Plane(Point3D location, Material material, Vector3D normal)
            : base(location, material)
        {
            Normal = normal;
        }

        /// <summary>
        /// A normal to the plane, specifying its orientation at <see cref="Location"/>.
        /// </summary>
        public Vector3D Normal { get; }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Plane);
        }

        public bool Equals(Plane? other)
        {
            return other != null &&
                   Location.Equals(other.Location) &&
                   Material.Equals(other.Material) &&
                   Normal.Equals(other.Normal);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Location, Material, Normal);
        }

        public override string ToString()
        {
            return $"Point { Location } Normal { Normal }";
        }

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
