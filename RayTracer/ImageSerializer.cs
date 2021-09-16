using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace RayTracer
{
    internal class ImageSerializer
    {
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

        public Color ConvertColor(RGBColor rgbColor)
        {
            return Color.FromArgb(
                Convert.ToByte(rgbColor.Red * 256),
                Convert.ToByte(rgbColor.Green * 256),
                Convert.ToByte(rgbColor.Blue * 256));
        }
    }
}
