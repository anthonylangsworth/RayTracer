using NUnit.Framework;
using RayTracer.Objects;
using RayTracer.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Test.Objects
{
    public class TestPlane
    {
        [Test]
        public void Ctor()
        {
            Point3D point = new Point3D(1, 2, 3);
            Material material = new Material();
            Vector3D normal = new Vector3D(0, 0, 1); // Up the Z axis

            Plane plane = new Plane(point, material, normal);

            Assert.That(plane.Location, Is.EqualTo(point));
            Assert.That(plane.Material, Is.EqualTo(material));
            Assert.That(plane.Normal, Is.EqualTo(normal));
        }

        public static IEnumerable<TestCaseData> HitTestCases()
        {
            return new[]
            {
                new TestCaseData(
                    new Plane(new Point3D(0, 0, 0), new Material(), new Vector3D(0, 0, 1)),
                    new Ray(new Point3D (0, 0, 1), new Vector3D(0, 0, -1)),
                    true,
                    1
                ),
                new TestCaseData(
                    new Plane(new Point3D(0, 0, 0), new Material(), new Vector3D(0, 0, 1)),
                    new Ray(new Point3D (0, 0, 2), new Vector3D(0, 0, -1)),
                    true,
                    2
                ),
                new TestCaseData(
                    new Plane(new Point3D(0, 0, 0), new Material(), new Vector3D(0, 0, 1)),
                    new Ray(new Point3D (0, 0, -2), new Vector3D(0, 0, 1)),
                    true,
                    2
                ),
                new TestCaseData(
                    new Plane(new Point3D(0, 0, 0), new Material(), new Vector3D(0, 0, 1)),
                    new Ray(new Point3D (0, 0, 1), new Vector3D(1, 0, 0)), // Parallel with Plane
                    false,
                    double.NaN
                ),
                new TestCaseData(
                    new Plane(new Point3D(0, 0, 0), new Material(), new Vector3D(0, 0, 1)),
                    new Ray(new Point3D (0, 0, 1), new Vector3D(0, 0, 1)),
                    false,
                    double.NaN
                )
            };
        }

        [TestCaseSource(nameof(HitTestCases))]
        public void Hit(Plane plane, Ray ray, bool expectedHit, double expectedDistance)
        {
            bool hit = plane.Hit(ray, out double distance, out ShadeRecord shadeRecord);
            Assert.That(hit, Is.EqualTo(expectedHit));
            if (expectedHit)
            {
                Assert.That(distance, Is.EqualTo(expectedDistance));
            }
        }
    }
}
