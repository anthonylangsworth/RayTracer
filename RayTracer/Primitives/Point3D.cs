using System;
using System.Collections.Generic;

namespace RayTracer.Primitives
{
    /// <summary>
    /// An immutable 3D point represented by an X, Y and Z coordinate.
    /// </summary>
    public class Point3D : IEquatable<Point3D?>
    {
        /// <summary>
        /// Create a new <see cref="Point3D"/>.
        /// </summary>
        /// <param name="x">
        /// The x coordinate.
        /// </param>
        /// <param name="y">
        /// The y coordinate.
        /// </param>
        /// <param name="z">
        /// The z coordinate.
        /// </param>
        /// <param name="doubleComparer">
        /// Used to compare <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/>.
        /// </param>
        public Point3D(double x, double y, double z,
            IEqualityComparer<double>? doubleComparer = null)
        {
            X = x;
            Y = y;
            Z = z;
            DoubleComparer = doubleComparer ?? DoubleEpsilonEqualityComparer.Instance;
        }

        /// <summary>
        /// X coordinate.
        /// </summary>
        public double X { get; }

        /// <summary>
        /// Y coordinate.
        /// </summary>
        public double Y { get; }

        /// <summary>
        /// Z coordinate.
        /// </summary>
        public double Z { get; }

        /// <summary>
        /// Used to compare <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/>.
        /// </summary>
        public IEqualityComparer<double> DoubleComparer { get; }

        public override bool Equals(object? obj)
        {
            return obj is Vector3D d && Equals(d);
        }

        public bool Equals(Point3D? other)
        {
            return !(other is null) &&
                   DoubleComparer.Equals(X, other.X) &&
                   DoubleComparer.Equals(Y, other.Y) &&
                   DoubleComparer.Equals(Z, other.Z);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public static bool operator ==(Point3D left, Point3D right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Point3D left, Point3D right)
        {
            return !(left == right);
        }

        public override string? ToString()
        {
            return $"x: {X}, y: {Y}, z: {Z}";
        }

        public static Point3D operator +(Point3D point, Vector3D vector)
        {
            return new Point3D(point.X + vector.X, point.Y + vector.Y, point.Z + vector.Z);
        }

        public static Point3D operator +(Vector3D vector, Point3D point)
        {
            return new Point3D(point.X + vector.X, point.Y + vector.Y, point.Z + vector.Z);
        }

        public static Vector3D operator -(Point3D p1, Point3D p2)
        {
            return new Vector3D(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        }

        public static Point3D operator -(Point3D p, Vector3D v)
        {
            return new Point3D(p.X - v.X, p.Y - v.Y, p.Z - v.Z);
        }

        public double Distance(Point3D point)
        {
            return Math.Sqrt(
                (X - point.X) * (X - point.X) +
                (Y - point.Y) * (Y - point.Y) +
                (Z - point.Z) * (Z - point.Z)
            );
        }

        public static readonly Point3D Origin = new Point3D(0, 0, 0);

        public static Point3D operator *(double d, Point3D point)
        {
            return new Point3D(d * point.X, d * point.Y, d * point.Z);
        }

        public static Point3D operator *(Point3D point, double d)
        {
            return new Point3D(d * point.X, d * point.Y, d * point.Z);
        }
    }
}
