using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Test
{
    internal class TestDrawingImageSerializer
    {
        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(0.5, 0, 0, 128, 0, 0)]
        [TestCase(0, 0.5, 0, 0, 128, 0)]
        [TestCase(0, 0, 0.5, 0, 0, 128)]
        [TestCase(1.0, 1.0, 1.0, 255, 255, 255)]
        public void TestConvertColor(double red, double green, double blue, byte expectedRed, byte expectedGreen, byte expectedBlue)
        {
            Assert.That(
                new DrawingImageSerializer().ConvertColor(new RGBColor(red, green, blue), 1.0),
                Is.EqualTo(Color.FromArgb(expectedRed, expectedGreen, expectedBlue)));
        }
    }
}
