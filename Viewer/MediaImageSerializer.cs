using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace RayTracer
{
    /// <summary>
    /// Save an RGBColor array as <see cref="System.Drawing.Bitmap"/>.
    /// </summary>
    public class MediaImageSerializer
    {
        /// <summary>
        /// Convert the RGBColor array to a <see cref="Bitmap"/>.
        /// </summary>
        /// <param name="output">
        /// The ray tracer output. Note that the rows are return in bottom-up instead
        /// of top-down order.
        /// </param>
        /// <param name="gamma">
        /// The gamma to use for the colour conversion.
        /// </param>
        /// <returns>
        /// The <see cref="Bitmap"/> from <paramref name="output"/>.
        /// </returns>
        public BitmapSource Serialize(RGBColor[,] output, double gamma)
        {
            WriteableBitmap writeableBitmap = new WriteableBitmap(
                output.GetLength(0), output.GetLength(1), 96, 96, PixelFormats.Rgba128Float, null);

            float[] buffer = new float[output.GetLength(0) * output.GetLength(1) * 4];
            int current = 0;
            for (int row = 0; row < output.GetLength(0); row++)
            {
                for (int column = 0; column < output.GetLength(1); column++)
                {
                    buffer[current++] = 0;
                    buffer[current++] = (float) output[row, column].Red;
                    buffer[current++] = (float) output[row, column].Green;
                    buffer[current++] = (float) output[row, column].Blue;
                }
            }

            writeableBitmap.WritePixels(
                new Int32Rect(0, 0, output.GetLength(0), output.GetLength(1)),
                buffer,
                output.GetLength(0) * 4 * sizeof(float), // "Stride" means the size of each line in bytes
                0);

            return writeableBitmap;
        }

        ///// <summary>
        ///// Convert an <see cref="RGBColor"/> to <see cref="Color"/>.
        ///// </summary>
        ///// <param name="rgbColor">
        ///// The <see cref="RGBColor"/> to convert.
        ///// </param>
        ///// <param name="gamma">
        ///// The inverse gamma to use for the colour conversion.
        ///// </param>
        ///// <returns>
        ///// The converted <see cref="Color"/>.
        ///// </returns>
        ///// <exception cref="OverflowException">
        ///// The <see cref="RGBColor.Red"/>, <see cref="RGBColor.Green"/> or <see cref="RGBColor.Blue"/> values
        ///// must be between 0 to 1 inclusive.
        ///// </exception>
        //public Color ConvertColor(RGBColor rgbColor, double inverseGamma)
        //{
        //    return Color.FromArgb(
        //        Convert.ToByte(Math.Pow(rgbColor.Red, inverseGamma) * byte.MaxValue),
        //        Convert.ToByte(Math.Pow(rgbColor.Green, inverseGamma) * byte.MaxValue),
        //        Convert.ToByte(Math.Pow(rgbColor.Blue, inverseGamma )* byte.MaxValue));
        //}
    }
}
