using NUnit.Framework;
using System.Collections.Generic;
using RayTracer.Primitives;

namespace RayTracer.Test.Primitives
{
    public class TestVector3D
    {
        [TestCase(1, 2, 3)]
        public void Ctor(double x, double y, double z)
        {
            Vector3D vector = new Vector3D(x, y, z);
            Assert.That(vector, Has.Property("X").EqualTo(x));
            Assert.That(vector, Has.Property("Y").EqualTo(y));
            Assert.That(vector, Has.Property("Z").EqualTo(z));
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
            Vector3D vector1 = new Vector3D(x1, y1, z1);
            Vector3D vector2 = new Vector3D(x2, y2, z2);
            Assert.IsTrue(expectedResult ? vector1.Equals(vector2) : !vector1.Equals(vector2));
        }

        [TestCaseSource(nameof(EqualTestData))]
        public void EqualsOpertor(double x1, double y1, double z1, double x2, double y2, double z2, bool expectedResult)
        {
            Vector3D vector1 = new Vector3D(x1, y1, z1);
            Vector3D vector2 = new Vector3D(x2, y2, z2);
            Assert.IsTrue(expectedResult ? vector1 == vector2 : !(vector1 == vector2));
        }

        [TestCaseSource(nameof(EqualTestData))]
        public void NotEqualsOpertor(double x1, double y1, double z1, double x2, double y2, double z2, bool expectedResult)
        {
            Vector3D vector1 = new Vector3D(x1, y1, z1);
            Vector3D vector2 = new Vector3D(x2, y2, z2);
            Assert.IsFalse(expectedResult ? vector1 != vector2 : !(vector1 != vector2));
        }

        [TestCase(1, 2, 3, 1, 2, 3, 2, 4, 6)]
        public void PlusOperator(double x1, double y1, double z1, double x2, double y2, double z2, double x3, double y3, double z3)
        {
            Vector3D vector1 = new Vector3D(x1, y1, z1);
            Vector3D vector2 = new Vector3D(x2, y2, z2);
            Vector3D vector3 = new Vector3D(x3, y3, z3);
            Assert.That(vector1 + vector2, Is.EqualTo(vector3));
        }

        [TestCase(5, 6, 7, 1, 3, 5, 4, 3, 2)]
        public void MinusOperator(double x1, double y1, double z1, double x2, double y2, double z2, double x3, double y3, double z3)
        {
            Vector3D vector1 = new Vector3D(x1, y1, z1);
            Vector3D vector2 = new Vector3D(x2, y2, z2);
            Vector3D vector3 = new Vector3D(x3, y3, z3);
            Assert.That(vector1 - vector2, Is.EqualTo(vector3));
        }

        public static IEnumerable<TestCaseData> MultiplyDoubleTestData()
        {
            return new[] {
                new TestCaseData(2, 1, 2, 3, 2, 4, 6)
            };
        }

        [TestCaseSource(nameof(MultiplyDoubleTestData))]
        public void MultiplyDoubleFirstOperator(double a, double x1, double y1, double z1, double x2, double y2, double z2)
        {
            Vector3D vector1 = new Vector3D(x1, y1, z1);
            Vector3D vector2 = new Vector3D(x2, y2, z2);
            Assert.That(a * vector1, Is.EqualTo(vector2));
        }

        [TestCaseSource(nameof(MultiplyDoubleTestData))]
        public void MultiplyDoubleSecondOperator(double a, double x1, double y1, double z1, double x2, double y2, double z2)
        {
            Vector3D vector1 = new Vector3D(x1, y1, z1);
            Vector3D vector2 = new Vector3D(x2, y2, z2);
            Assert.That(vector1 * a, Is.EqualTo(vector2));
        }

        [TestCase(4, 8, 12, 4, 1, 2, 3)]
        public void MultiplyDivideDoubleOperator(double x1, double y1, double z1, double a, double x2, double y2, double z2)
        {
            Vector3D vector1 = new Vector3D(x1, y1, z1);
            Vector3D vector2 = new Vector3D(x2, y2, z2);
            Assert.That(vector1 / a, Is.EqualTo(vector2));
        }

        [TestCase(1, 0, 0, 1)]
        [TestCase(0, 1, 0, 1)]
        [TestCase(0, 0, 1, 1)]
        [TestCase(-1, 0, 0, 1)]
        [TestCase(0, -1, 0, 1)]
        [TestCase(0, 0, -1, 1)]
        [TestCase(1, 1, 1, 1.7320508075688772)]
        [TestCase(-1, -1, -1, 1.7320508075688772)]
        public void Length(double x, double y, double z, double expectedLength)
        {
            Vector3D vector = new Vector3D(x, y, z);
            Assert.That(vector.Length, Is.EqualTo(expectedLength));
        }

        [TestCase(1, 0, 0, 1)]
        [TestCase(0, 1, 0, 1)]
        [TestCase(0, 0, 1, 1)]
        [TestCase(-1, 0, 0, 1)]
        [TestCase(0, -1, 0, 1)]
        [TestCase(0, 0, -1, 1)]
        [TestCase(1, 1, 1, 3)]
        [TestCase(-1, -1, -1, 3)]
        public void SquaredLength(double x, double y, double z, double expectedSquaredLength)
        {
            Vector3D vector = new Vector3D(x, y, z);
            Assert.That(vector.SquaredLength, Is.EqualTo(expectedSquaredLength));
        }

        [TestCase(1, 2, 3, 2, 3, 4, 20)]
        [TestCase(1, 2, 3, 2, 3, -4, -4)]
        public void Dot(double x1, double y1, double z1, double x2, double y2, double z2, double expectedDotProduct)
        {
            Vector3D vector1 = new Vector3D(x1, y1, z1);
            Vector3D vector2 = new Vector3D(x2, y2, z2);
            Assert.That(vector1.Dot(vector2), Is.EqualTo(expectedDotProduct));
        }

        // See https://www.whitman.edu/mathematics/calculus_late_online/section14.04.html
        [TestCase(2, 3, 4, 5, 6, 7, -3, 6, -3)]
        [TestCase(1, 1, 1, 1, 2, 3, 1, -2, 1)]
        [TestCase(1, 0, 2, -1, -2, 4, 4, -6, -2)]
        public void Cross(double x1, double y1, double z1, double x2, double y2, double z2, double x3, double y3, double z3)
        {
            Vector3D vector1 = new Vector3D(x1, y1, z1);
            Vector3D vector2 = new Vector3D(x2, y2, z2);
            Vector3D vector3 = new Vector3D(x3, y3, z3);
            Assert.That(vector1.Cross(vector2), Is.EqualTo(vector3));
        }

        [TestCase(1, 2, 3, -1, -2, -3)]
        public void UnaryMinus(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            Vector3D vector1 = new Vector3D(x1, y1, z1);
            Vector3D vector2 = new Vector3D(x2, y2, z2);
            Assert.That(-vector1, Is.EqualTo(vector2));
        }

        [TestCase(0, 0, 0)]
        [TestCase(1, 2, 3)]
        [TestCase(-1, -2, -3)]
        public void Normalize(double x1, double y1, double z1)
        {
            Vector3D vector1 = new Vector3D(x1, y1, z1);
            Vector3D normalizedVector = vector1.Normalize();
            double newLength = normalizedVector.Length();
            if (newLength == 0)
            {
                Assert.That(normalizedVector, Is.EqualTo(new Vector3D(0, 0, 0)));
            }
            else
            {
                Assert.That(newLength, Is.EqualTo(1));
            }
        }
    }
}