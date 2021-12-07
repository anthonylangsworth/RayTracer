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

        [TestCase(0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.5, 0.7, 0.9)]
        public void PlusOperator(double red1, double green1, double blue1, double red2, double green2, double blue2, 
            double expectedRed, double expectedGreen, double expectedBlue)
        {
            RGBColor color1 = new RGBColor(red1, green1, blue1);
            RGBColor color2 = new RGBColor(red2, green2, blue2);
            RGBColor expectedColor = new RGBColor(expectedRed, expectedGreen, expectedBlue);
            Assert.That(color1 + color2, Is.EqualTo(expectedColor));
        }

        public static IEnumerable<TestCaseData> MultiplyOperatorTestData()
        {
            return new[]
            {
                new TestCaseData(1, new RGBColor(1, 2, 3), new RGBColor(1, 2, 3)),
                new TestCaseData(2, new RGBColor(1, 2, 3), new RGBColor(2, 4, 6))
            };
        }

        [TestCaseSource(nameof(MultiplyOperatorTestData))]
        public void MultiplyOperatorDoubleColor(double d, RGBColor color, RGBColor expectedResult)
        {
            Assert.That(d * color, Is.EqualTo(expectedResult));
        }

        [TestCaseSource(nameof(MultiplyOperatorTestData))]
        public void MultiplyOperatorColorDouble(double d, RGBColor color, RGBColor expectedResult)
        {
            Assert.That(color * d, Is.EqualTo(expectedResult));
        }

        [TestCase(0.2, 0.4, 0.8, 2, 0.1, 0.2, 0.4)]
        public void DivideOperatorColorDouble(double red, double green, double blue, double d, double expectedRed, double expectedGreen, double expectedBlue)
        {
            RGBColor color = new RGBColor(red, green, blue);
            RGBColor expectedColor = new RGBColor(expectedRed, expectedGreen, expectedBlue);
            Assert.That(color / d, Is.EqualTo(expectedColor));
        }

        [TestCase(1, 2, 3, 4, 5, 6, 4, 10, 18)]
        public void MultiplyOperatorColorColor(double red1, double green1, double blue1, double red2, double green2, double blue2, 
            double expectedRed, double expectedGreen, double expectedBlue)
        {
            RGBColor color1 = new RGBColor(red1, green1, blue1);
            RGBColor color2 = new RGBColor(red2, green2, blue2);
            RGBColor expectedColor = new RGBColor(expectedRed, expectedGreen, expectedBlue);
            Assert.That(color1 * color2, Is.EqualTo(expectedColor));
        }

        [TestCase(0.2, 0.4, 0.8, 2, 0.04, 0.16, 0.64)]
        public void Power(double red, double green, double blue, double d, double expectedRed, double expectedGreen, double expectedBlue)
        {
            RGBColor color = new RGBColor(red, green, blue);
            RGBColor expectedColor = new RGBColor(expectedRed, expectedGreen, expectedBlue);
            Assert.That(color.Power(d), Is.EqualTo(expectedColor));
        }
    }
}
