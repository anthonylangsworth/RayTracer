using NUnit.Framework;
using System.Collections.Generic;
using RayTracer.Primitives;

namespace RayTracer.Test.Primitives
{
    public class TestPoint2D
    {
        [TestCase(1, 2, 3)]
        public void Ctor(double x, double y, double z)
        {
            Point2D point = new Point2D(x, y);
            Assert.That(point, Has.Property("X").EqualTo(x));
            Assert.That(point, Has.Property("Y").EqualTo(y));
        }

        public static IEnumerable<TestCaseData> EqualTestData()
        {
            return new[] {
                new TestCaseData(1, 2, 1, 2, true),
                new TestCaseData(1, 2, 4, 2, false),
                new TestCaseData(1, 2, 1, 4, false),
                new TestCaseData(3, 5, 1, 2, false)
            };
        }

        [TestCaseSource(nameof(EqualTestData))]
        public void Equals(double x1, double y1, double x2, double y2, bool expectedResult)
        {
            Point2D point1 = new Point2D(x1, y1);
            Point2D point2 = new Point2D(x2, y2);
            Assert.IsTrue(expectedResult ? point1.Equals(point2) : !point1.Equals(point2));
        }

        [TestCaseSource(nameof(EqualTestData))]
        public void EqualsOpertor(double x1, double y1, double x2, double y2, bool expectedResult)
        {
            Point2D point1 = new Point2D(x1, y1);
            Point2D point2 = new Point2D(x2, y2);
            Assert.IsTrue(expectedResult ? point1 == point2 : !(point1 == point2));
        }

        [TestCaseSource(nameof(EqualTestData))]
        public void NotEqualsOpertor(double x1, double y1, double x2, double y2, bool expectedResult)
        {
            Point2D point1 = new Point2D(x1, y1);
            Point2D point2 = new Point2D(x2, y2);
            Assert.IsFalse(expectedResult ? point1 != point2 : !(point1 != point2));
        }

        [Test]
        public void Origin()
        {
            Assert.That(Point2D.Origin, Is.EqualTo(new Point2D(0, 0)));
        }
    }
}
