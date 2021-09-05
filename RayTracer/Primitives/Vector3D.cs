using System;
using System.Collections.Generic;

namespace RayTracer.Primitives
{
    /// <summary>
    /// A 3D vector represented by an X, Y and Z coordinate.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is a struct instead of a class due to its small size (technically over the 16 byte rule of thumb, however), immutability and use as a single value. It is inspired by:
    /// </para>
    /// <para>
    /// <list type="number">
    /// <item>
    /// <description>
    /// Unity's Vector3 class: https://docs.unity3d.com/ScriptReference/Vector3.html 
    /// </description>
    /// </item>
    /// </para>
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
            return $"x: {X}, y: {Y}, z:{Z}";
        }
    }
}
