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

        public static IEnumerable<TestCaseData> PlusTestData()
        {
            return new[] {
                new TestCaseData(1, 2, 3, 1, 2, 3, 2, 4, 6)
            };
        }

        [TestCaseSource(nameof(PlusTestData))]
        public void PlusOperatorPointVector(double x1, double y1, double z1, double x2, double y2, double z2, double x3, double y3, double z3)
        {
            Point3D point1 = new Point3D(x1, y1, z1);
            Vector3D vector = new Vector3D(x2, y2, z2);
            Point3D point2 = new Point3D(x3, y3, z3);
            Assert.That(point1 + vector, Is.EqualTo(point2));
        }

        [TestCaseSource(nameof(PlusTestData))]
        public void PlusOperatorVectorPoint(double x1, double y1, double z1, double x2, double y2, double z2, double x3, double y3, double z3)
        {
            Point3D point1 = new Point3D(x1, y1, z1);
            Vector3D vector = new Vector3D(x2, y2, z2);
            Point3D point2 = new Point3D(x3, y3, z3);
            Assert.That(vector + point1, Is.EqualTo(point2));
        }

        public static IEnumerable<TestCaseData> MinusTestData()
        {
            return new[] {
                new TestCaseData(5, 6, 7, 1, 3, 5, 4, 3, 2)
            };
        }

        [TestCaseSource(nameof(MinusTestData))]
        public void MinusOperatorPointVector(double x1, double y1, double z1, double x2, double y2, double z2, double x3, double y3, double z3)
        {
            Point3D point1 = new Point3D(x1, y1, z1);
            Vector3D vector = new Vector3D(x2, y2, z2);
            Point3D point2 = new Point3D(x3, y3, z3);
            Assert.That(point1 - vector, Is.EqualTo(point2));
        }

        [TestCaseSource(nameof(MinusTestData))]
        public void MinusOperatorPointPoint(double x1, double y1, double z1, double x2, double y2, double z2, double x3, double y3, double z3)
        {
            Point3D point1 = new Point3D(x1, y1, z1);
            Point3D point2 = new Point3D(x2, y2, z2);
            Vector3D vector = new Vector3D(x3, y3, z3);
            Assert.That(point1 - point2, Is.EqualTo(vector));
        }

        [TestCase(1, 0, 0, 0, 0, 0, 1)]
        [TestCase(0, 1, 0, 0, 0, 0, 1)]
        [TestCase(0, 0, 1, 0, 0, 0, 1)]
        [TestCase(3, 4, 0, 0, 0, 0, 5)]
        public void Distance(double x1, double y1, double z1, double x2, double y2, double z2, double expectedDistance)
        {
            Point3D point1 = new Point3D(x1, y1, z1);
            Point3D point2 = new Point3D(x2, y2, z2);
            Assert.That(point1.Distance(point2), Is.EqualTo(expectedDistance));
        }

        [Test]
        public void Origin()
        {
            Assert.That(Point3D.Origin, Is.EqualTo(new Point3D(0, 0, 0)));
        }

        public static IEnumerable<TestCaseData> MultiplyTestData()
        {
            return new[]
            {
                new TestCaseData(0, 1, 2, 3, 0, 0, 0),
                new TestCaseData(1, 1, 2, 3, 1, 2, 3),
                new TestCaseData(2, 1, 2, 3, 2, 4, 6)
            };
        }

        [TestCaseSource(nameof(MultiplyTestData))]
        public void MultiplyDoublePoint(double d, double x, double y, double z, double expectedX, double expectedY, double expectedZ)
        {
            Assert.That(d * new Point3D(x, y, z), Is.EqualTo(new Point3D(expectedX, expectedY, expectedZ)));
        }

        [TestCaseSource(nameof(MultiplyTestData))]
        public void MultiplyPointDouble(double d, double x, double y, double z, double expectedX, double expectedY, double expectedZ)
        {
            Assert.That(new Point3D(x, y, z) * d, Is.EqualTo(new Point3D(expectedX, expectedY, expectedZ)));
        }
    }
}
