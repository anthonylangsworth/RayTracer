using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace RayTracer
{
    /// <summary>
    /// Compare two <c>double<c>s, allowing a slight variance or "epsilon".
    /// </summary>
    /// <remarks>
    /// The problem with redefining equality to allow variance is we lose transitivity. 
    /// For example, take three doubles a, b and c. a == b and b == c but a != c 
    /// if abs(a - c) > epsilon. Another problem is where two doubles may be equal 
    /// but have different hash values.
    /// 
    /// However, there is a legitimate need to handle small errors common to floating
    /// point mathematics, so this should be used with care.
    /// 
    /// See https://stackoverflow.com/questions/11258962/iequalitycomparerdouble-with-a-tolerance-how-to-implement-gethashcode
    /// and https://docs.unity3d.com/ScriptReference/Mathf.Epsilon.html for discussion.
    /// </remarks>
    public class DoubleEpsilonEqualityComparer: IEqualityComparer<double>
    {
        /// <summary>
        /// The default allowable variance. Use the same value as he Unity framework.
        /// </summary>
        public const double DefaultEpsilon = 1e-5;

        public DoubleEpsilonEqualityComparer(double epsilon = DefaultEpsilon)
        {
            if(epsilon < 0)
            {
                throw new ArgumentException($"{nameof(epsilon)} must be positive", nameof(epsilon));
            }

            Epsilon = epsilon;
        }

        /// <summary>
        /// Prevent unnecessary duplicate instances
        /// </summary>
        public static readonly DoubleEpsilonEqualityComparer Instance = new DoubleEpsilonEqualityComparer();

        public double Epsilon { get; }

        public bool Equals(double x, double y)
        {
            return Math.Abs(x - y) < Epsilon;
        }

        public int GetHashCode([DisallowNull] double obj)
        {
            // Do not use this for hashing. 
            throw new NotImplementedException();
        }
    }
}
