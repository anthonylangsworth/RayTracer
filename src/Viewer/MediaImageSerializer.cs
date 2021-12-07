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
                output.GetLength(0), output.GetLength(1), 96, 96, PixelFormats.Rgb24, null);

            byte[] buffer = new byte[output.GetLength(0) * output.GetLength(1) * 3];
            int current = 0;
            double inverseGamma = 1 / gamma;
            // Invert due to rows being bottom up in the output
            for (int row = output.GetLength(0) - 1; row >= 0; row--)
            {
                for (int column = 0; column < output.GetLength(1); column++)
                {
                    buffer[current++] = Convert.ToByte(Math.Pow(output[row, column].Red, inverseGamma) * byte.MaxValue);
                    buffer[current++] = Convert.ToByte(Math.Pow(output[row, column].Green, inverseGamma) * byte.MaxValue);
                    buffer[current++] = Convert.ToByte(Math.Pow(output[row, column].Blue, inverseGamma) * byte.MaxValue);
                }
            }

            writeableBitmap.WritePixels(
                new Int32Rect(0, 0, output.GetLength(0), output.GetLength(1)),
                buffer,
                output.GetLength(0) * 3, // "Stride" means the size of each line in bytes
                0);

            return writeableBitmap;
        }
    }
}
