using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Test
{
    public class TestRGBColor
    {
        public void Ctor()
        {
            double red = 0.1;
            double green = 0.2;
            double blue = 0.3;
            RGBColor color = new RGBColor(red, green, blue);
            Assert.That(color.Red, Is.EqualTo(red));
            Assert.That(color.Green, Is.EqualTo(green));
            Assert.That(color.Blue, Is.EqualTo(blue));
        }
    }
}
