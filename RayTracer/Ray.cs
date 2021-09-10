using System;
using System.Collections.Generic;
using System.Text;
using RayTracer.Primitives;

namespace RayTracer
{
    public class Ray : IEquatable<Ray?>
    {
        public Ray(Point3D origin, Vector3D direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Point3D Origin { get; }

        public Vector3D Direction { get; }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Ray);
        }

        public bool Equals(Ray? other)
        {
            return other != null &&
                   EqualityComparer<Point3D>.Default.Equals(Origin, other.Origin) &&
                   EqualityComparer<Vector3D>.Default.Equals(Direction, other.Direction);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Origin, Direction);
        }

        public static bool operator ==(Ray? left, Ray? right)
        {
            return EqualityComparer<Ray>.Default.Equals(left, right);
        }

        public static bool operator !=(Ray? left, Ray? right)
        {
            return !(left == right);
        }
    }
}
