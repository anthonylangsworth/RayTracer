using System;
using System.Collections.Generic;

namespace RayTracer.Primitives
{
    /// <summary>
    /// An immutable 3D vector represented by an X, Y and Z coordinate.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This is close to being a struct but it is too large.
    ///     </para>
    ///     <para>
    ///     <list type="number">
    ///         <item>
    ///             <description>
    ///                 Unity's Vector3 class: https://docs.unity3d.com/ScriptReference/Vector3.html 
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 p26 of "Ray Tracing fromm the Ground Up" by Kevin Suffern (2007)
    ///             </description>
    ///         </item>         
    ///     </para>
    ///     <para>
    ///         Avoid overriding "*" for dot or cross products to avoid confusion.
    ///     </para>
    /// </remarks>
    public class Vector3D : IEquatable<Vector3D?>
    {
        /// <summary>
        /// Create a new <see cref="Vector3D"/>.
        /// </summary>
        /// <param name="x">
        /// The x portion.
        /// </param>
        /// <param name="y">
        /// The y portion.
        /// </param>
        /// <param name="z">
        /// The z portion.
        /// </param>
        /// <param name="doubleComparer">
        /// Used to compare <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/>.
        /// </param>
        public Vector3D(double x, double y, double z,
            IEqualityComparer<double>? doubleComparer = null)
        {
            X = x;
            Y = y;
            Z = z;
            DoubleComparer = doubleComparer ?? DoubleEpsilonEqualityComparer.Instance;
        }

        /// <summary>
        /// X portion.
        /// </summary>
        public double X { get; }

        /// <summary>
        /// Y portion.
        /// </summary>
        public double Y { get; }

        /// <summary>
        /// Z portion.
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

        public bool Equals(Vector3D? other)
        {
            // TODO: Consider epsilon
            return !(other is null) &&
                   DoubleComparer.Equals(X, other.X) &&
                   DoubleComparer.Equals(Y, other.Y) &&
                   DoubleComparer.Equals(Z, other.Z);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public static bool operator ==(Vector3D left, Vector3D right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector3D left, Vector3D right)
        {
            return !(left == right);
        }

        public override string? ToString()
        {
            return $"x: {X}, y: {Y}, z: {Z}";
        }

        public static Vector3D operator +(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector3D operator -(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static Vector3D operator *(double a, Vector3D v)
        {
            return new Vector3D(a * v.X, a * v.Y, a * v.Z);
        }

        public static Vector3D operator *(Vector3D v, double a)
        {
            return new Vector3D(a * v.X, a * v.Y, a * v.Z);
        }

        public static Vector3D operator /(Vector3D v, double a)
        {
            return new Vector3D(v.X / a, v.Y / a, v.Z / a);
        }

        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public double SquaredLength()
        {
            return X * X + Y * Y + Z * Z;
        }

        public double Dot(Vector3D v)
        {
            return X * v.X + Y * v.Y + Z * v.Z;
        }

        public Vector3D Cross(Vector3D v)
        {
            return new Vector3D(Y * v.Z - Z * v.Y, Z * v.X - X * v.Z, X * v.Y - Y * v.X);
        }

        public static Vector3D operator -(Vector3D v)
        {
            return new Vector3D(-v.X, -v.Y, -v.Z);
        }

        /// <summary>
        /// Return the vector with a length of 1. If the vector's length is zero, return a vector of 0, 0, 0.
        /// </summary>
        /// <returns>
        /// The normalized vector.
        /// </returns>
        public Vector3D Normalize()
        {
            double length = Length();
            Vector3D result = length != 0 ? this / length : new Vector3D(0, 0, 0);
            return result;
        }
    }
}
