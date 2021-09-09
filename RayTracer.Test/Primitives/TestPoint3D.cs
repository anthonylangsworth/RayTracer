using NUnit.Framework;
using System.Collections.Generic;
using RayTracer.Primitives;

namespace RayTracer.Test.Primitives
{
    public class TestPoint3D
    {
        [TestCase(1, 2, 3)]
        public void Ctor(double x, double y, double z)
        {
            Point3D point = new Point3D(x, y, z);
            Assert.That(point, Has.Property("X").EqualTo(x));
            Assert.That(point, Has.Property("Y").EqualTo(y));
            Assert.That(point, Has.Property("Z").EqualTo(z));
        }

        public static IEnumerable<TestCaseData> EqualTestData()
        {
            return new[] {
                new TestCaseData(1, 2, 3, 1, 2, 3, true),
                new TestCaseData(1, 2, 3, 4, 2, 3, false),
                new TestCaseData(1, 2, 3, 1, 4, 3, false),
                new TestCaseData(1, 2, 3, 1, 2, 4, false)
            };
        }

        [TestCaseSource(nameof(EqualTestData))]
        public void Equals(double x1, double y1, double z1, double x2, double y2, double z2, bool expectedResult)
        {
            Point3D point1 = new Point3D(x1, y1, z1);
            Point3D point2 = new Point3D(x2, y2, z2);
            Assert.IsTrue(expectedResult ? point1.Equals(point2) : !point1.Equals(point2));
        }

        [TestCaseSource(nameof(EqualTestData))]
        public void EqualsOpertor(double x1, double y1, double z1, double x2, double y2, double z2, bool expectedResult)
        {
            Point3D point1 = new Point3D(x1, y1, z1);
            Point3D point2 = new Point3D(x2, y2, z2);
            Assert.IsTrue(expectedResult ? point1 == point2 : !(point1 == point2));
        }

        [TestCaseSource(nameof(EqualTestData))]
        public void NotEqualsOpertor(double x1, double y1, double z1, double x2, double y2, double z2, bool expectedResult)
        {
            Point3D point1 = new Point3D(x1, y1, z1);
            Point3D point2 = new Point3D(x2, y2, z2);
            Assert.IsFalse(expectedResult ? point1 != point2 : !(point1 != point2));
        }
    }
}
