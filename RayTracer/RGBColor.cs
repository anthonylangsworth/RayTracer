using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    /// <summary>
    /// An red/green/blue colour using a floating instead of integer space.
    /// </summary>
    public class RGBColor
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
        public RGBColor(double red, double green, double blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
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
    }
}
