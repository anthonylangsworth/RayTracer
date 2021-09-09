using System;
using System.Collections.Generic;

namespace RayTracer.Primitives
{
    /// <summary>
    /// An immutable 3D vector represented by an X, Y and Z coordinate.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This is a struct instead of a class due to its small size (technically over the 16 byte rule of thumb, however), immutability and use as a single value. It is inspired by:
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
    public struct Vector3D : IEquatable<Vector3D>
    {
        public Vector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double X { get; }

        public double Y { get; }

        public double Z { get; }

        public override bool Equals(object? obj)
        {
            return obj is Vector3D d && Equals(d);
        }

        public bool Equals(Vector3D other)
        {
            // TODO: Consider epsilon
            return X == other.X &&
                   Y == other.Y &&
                   Z == other.Z;
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
