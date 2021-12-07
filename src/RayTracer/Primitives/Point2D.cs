using System;
using System.Collections.Generic;

namespace RayTracer.Primitives
{
    /// <summary>
    /// An immutable 2D point represented by an X and Y coordinate.
    /// </summary>
    public class Point2D : IEquatable<Point2D?>
    {
        /// <summary>
        /// Create a new <see cref="Point2D"/>.
        /// </summary>
        /// <param name="x">
        /// The x coordinate.
        /// </param>
        /// <param name="y">
        /// The y coordinate.
        /// </param>
        /// <param name="doubleComparer">
        /// Used to compare <see cref="X"/> and <see cref="Y"/>.
        /// </param>
        public Point2D(double x, double y,
            IEqualityComparer<double>? doubleComparer = null)
        {
            X = x;
            Y = y;
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
        /// Used to compare <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/>.
        /// </summary>
        public IEqualityComparer<double> DoubleComparer { get; }

        public override bool Equals(object? obj)
        {
            return obj is Vector3D d && Equals(d);
        }

        public bool Equals(Point2D? other)
        {
            return !(other is null) &&
                   DoubleComparer.Equals(X, other.X) &&
                   DoubleComparer.Equals(Y, other.Y);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(Point2D left, Point2D right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Point2D left, Point2D right)
        {
            return !(left == right);
        }

        public static Point2D operator *(Point2D left, double d)
        {
            return new Point2D(left.X * d, left.Y * d);
        }

        public override string? ToString()
        {
            return $"x: {X}, y: {Y}";
        }

        public static readonly Point2D Origin = new Point2D(0, 0);
    }
}
