using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    /// <summary>
    /// An red/green/blue colour using a floating instead of integer space.
    /// </summary>
    public class RGBColor : IEquatable<RGBColor?>
    {
        /// <summary>
        /// Create an <see cref="RGBColor"/>.
        /// </summary>
        /// <param name="red">
        /// The red portion of the color.
        /// </param>
        /// <param name="green">
        /// The green portion of the color.
        /// </param>
        /// <param name="blue">
        /// The blue portion of the color.
        /// </param>
        /// <param name="doubleComparer">
        /// Used to compare <see cref="Red"/>, <see cref="Green"/> and <see cref="Blue"/>.
        /// </param>
        public RGBColor(double red, double green, double blue,
            IEqualityComparer<double>? doubleComparer = null)
        {
            Red = red;
            Green = green;
            Blue = blue;
            DoubleComparer = doubleComparer ?? DoubleEpsilonEqualityComparer.Instance;
        }

        /// <summary>
        /// The red portion of the color.
        /// </summary>
        public double Red { get; }

        /// <summary>
        /// The green portion of the color.
        /// </summary>
        public double Green { get; }

        /// <summary>
        /// The blue portion of the color.
        /// </summary>
        public double Blue { get; }

        /// <summary>
        /// Used to compare <see cref="Red"/>, <see cref="Green"/> and <see cref="Blue"/>.
        /// </summary>
        public IEqualityComparer<double> DoubleComparer { get; }

        public override bool Equals(object? obj)
        {
            return Equals(obj as RGBColor);
        }

        public bool Equals(RGBColor? other)
        {
            return other != null &&
                   DoubleComparer.Equals(Red, other.Red) &&
                   DoubleComparer.Equals(Green, other.Green) &&
                   DoubleComparer.Equals(Blue, other.Blue);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Red, Green, Blue);
        }

        public override string ToString()
        {
            return $"R { Red }, G { Green }, B { Blue }";
        }

        public RGBColor Power(double d)
        {
            return new RGBColor(Math.Pow(Red, d), Math.Pow(Green, d), Math.Pow(Blue, d));
        }

        public static RGBColor operator +(RGBColor color1, RGBColor color2)
        {
            return new RGBColor(color1.Red + color2.Red, color1.Green + color2.Green, color1.Blue + color2.Blue);
        }

        public static RGBColor operator *(double d, RGBColor color)
        {
            return new RGBColor(d * color.Red, d * color.Green, d * color.Blue);
        }

        public static RGBColor operator *(RGBColor color, double d)
        {
            return new RGBColor(color.Red * d, color.Green * d, color.Blue * d);
        }

        public static RGBColor operator /(RGBColor color, double d)
        {
            return new RGBColor(color.Red / d, color.Green / d, color.Blue / d);
        }

        public static RGBColor operator *(RGBColor color1, RGBColor color2)
        {
            return new RGBColor(color1.Red * color2.Red, color1.Green * color2.Green, color1.Blue * color2.Blue);
        }

        public static bool operator ==(RGBColor? left, RGBColor? right)
        {
            return EqualityComparer<RGBColor>.Default.Equals(left, right);
        }

        public static bool operator !=(RGBColor? left, RGBColor? right)
        {
            return !(left == right);
        }

        public readonly static RGBColor Black = new RGBColor(0, 0, 0);
        public readonly static RGBColor White = new RGBColor(1, 1, 1);
        public readonly static RGBColor BrightRed = new RGBColor(1, 0, 0);
        public readonly static RGBColor BrightGreen = new RGBColor(0, 1, 0);
        public readonly static RGBColor BrightBlue = new RGBColor(0, 0, 1);
        public readonly static RGBColor DarkRed = new RGBColor(0.3, 0, 0);
        public readonly static RGBColor DarkGreen = new RGBColor(0, 0.3, 0);
        public readonly static RGBColor DarkBlue = new RGBColor(0, 0, 0.3);
        public readonly static RGBColor Magenta = new RGBColor(1, 0, 1);
        public readonly static RGBColor Yellow = new RGBColor(1, 1, 0);
        public readonly static RGBColor Cyan = new RGBColor(0, 1, 1);
    }
}
