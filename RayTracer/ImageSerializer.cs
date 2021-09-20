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
        /// The ray tracer output. Note that the rows are return in bottom-up instead
        /// of top-down order.
        /// </param>
        /// <param name="fileName">
        /// The output file name.
        /// </param>
        /// <param name="gamma">
        /// The gamma to use for the colour conversion.
        /// </param>
        public void Save(RGBColor[,] output, string fileName, double gamma)
        {
            Bitmap bitmap = new Bitmap(output.GetLength(0), output.GetLength(1));
            double inverseGamma = 1 / gamma;
            for (int row = 0; row < output.GetLength(0); row++)
            {
                for (int column = 0; column < output.GetLength(1); column++)
                {
                    // Invert the row because rows are returned bottom-up instead of
                    // top down.
                    bitmap.SetPixel(
                        column, 
                        output.GetLength(0) - row - 1, 
                        ConvertColor(output[row, column], inverseGamma)
                    );
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
        /// <param name="gamma">
        /// The inverse gamma to use for the colour conversion.
        /// </param>
        /// <returns>
        /// The converted <see cref="Color"/>.
        /// </returns>
        /// <exception cref="OverflowException">
        /// The <see cref="RGBColor.Red"/>, <see cref="RGBColor.Green"/> or <see cref="RGBColor.Blue"/> values
        /// must be between 0 to 1 inclusive.
        /// </exception>
        public Color ConvertColor(RGBColor rgbColor, double inverseGamma)
        {
            return Color.FromArgb(
                Convert.ToByte(Math.Pow(rgbColor.Red, inverseGamma) * byte.MaxValue),
                Convert.ToByte(Math.Pow(rgbColor.Green, inverseGamma) * byte.MaxValue),
                Convert.ToByte(Math.Pow(rgbColor.Blue, inverseGamma )* byte.MaxValue));
        }
    }
}
