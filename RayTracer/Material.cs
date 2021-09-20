using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    /// <summary>
    /// A simple material class, soon to be subclassed.
    /// </summary>
    public class Material
    {
        /// <summary>
        /// Create a solid color material.
        /// </summary>
        /// <param name="color">
        /// The color.
        /// </param>
        public Material(RGBColor color)
        {
            Color = color;
        }

        /// <summary>
        /// The material's color.
        /// </summary>
        public RGBColor Color { get; }

        public readonly static Material Red = new Material(RGBColor.BrightRed);
        public readonly static Material Green = new Material(RGBColor.BrightGreen);
        public readonly static Material Blue = new Material(RGBColor.BrightBlue);
        public readonly static Material Magenta = new Material(RGBColor.Magenta);
        public readonly static Material Yellow = new Material(RGBColor.Yellow);
        public readonly static Material Cyan = new Material(RGBColor.Cyan);
        public readonly static Material Black = new Material(RGBColor.Black);
        public readonly static Material White = new Material(RGBColor.White);
    }
}
