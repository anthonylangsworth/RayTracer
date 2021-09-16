using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace RayTracer
{
    /// <summary>
    /// Save an RGBColor array as a PNG.
    /// </summary>
    public class ImageSerializer
    {
        /// <summary>
        /// Save an RGBColor array as a PNG.
        /// </summary>
        /// <param name="output">
        /// The ray tracer output.
        /// </param>
        /// <param name="fileName">
        /// The output file name.
        /// </param>
        public void Save(RGBColor[,] output, string fileName)
        {
            Bitmap bitmap = new Bitmap(output.GetLength(0), output.GetLength(1));
            for (int row = 0; row < output.GetLength(0); row++)
            {
                for (int column = 0; column < output.GetLength(1); column++)
                {
                    // TODO: Check y axis
                    bitmap.SetPixel(column, row, ConvertColor(output[row, column]));
                }
            }
            bitmap.Save(Path.ChangeExtension(fileName, "png"), ImageFormat.Png);
        }

        /// <summary>
        /// Convert an <see cref="RGBColor"/> to <see cref="Color"/>.
        /// </summary>
        /// <param name="rgbColor">
        /// The <see cref="RGBColor"/> to convert.
        /// </param>
        /// <returns>
        /// The converted <see cref="Color"/>.
        /// </returns>
        /// <exception cref="OverflowException">
        /// The <see cref="RGBColor.Red"/>, <see cref="RGBColor.Green"/> or <see cref="RGBColor.Blue"/> values
        /// must be between 0 to 1 inclusive.
        /// </exception>
        public Color ConvertColor(RGBColor rgbColor)
        {
            // TODO: Consider gamma. 
            return Color.FromArgb(
                Convert.ToByte(rgbColor.Red * Byte.MaxValue),
                Convert.ToByte(rgbColor.Green * Byte.MaxValue),
                Convert.ToByte(rgbColor.Blue * Byte.MaxValue));
        }
    }
}
