using NUnit.Framework;
using System.Collections.Generic;
using RayTracer.Primitives;

namespace RayTracer.Test
{
    public class Tests
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
            yield return new TestCaseData(1, 2, 3, 1, 2, 3, true);
            yield return new TestCaseData(1, 2, 3, 4, 2, 3, false);
            yield return new TestCaseData(1, 2, 3, 1, 4, 3, false);
            yield return new TestCaseData(1, 2, 3, 1, 2, 4, false);
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
            Assert.IsTrue(vector1 + vector2 == vector3);
        }

        [TestCase(5, 6, 7, 1, 3, 5, 4, 3, 2)]
        public void MinusOperator(double x1, double y1, double z1, double x2, double y2, double z2, double x3, double y3, double z3)
        {
            Vector3D vector1 = new Vector3D(x1, y1, z1);
            Vector3D vector2 = new Vector3D(x2, y2, z2);
            Vector3D vector3 = new Vector3D(x3, y3, z3);
            Assert.IsTrue(vector1 - vector2 == vector3);
        }

        public void MultiplyDoubleOperator()
        {

        }
    }
}