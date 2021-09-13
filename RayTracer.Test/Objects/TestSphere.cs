using NUnit.Framework;
using RayTracer.Objects;
using RayTracer.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer.Test.Objects
{
    public class TestSphere
    {
        [Test]
        public void Ctor()
        {
            Point3D point = new Point3D(1, 2, 3);
            Material material = new Material();
            double radius = 2;

            Sphere sphere = new Sphere(point, material, radius);

            Assert.That(sphere.Location, Is.EqualTo(point));
            Assert.That(sphere.Material, Is.EqualTo(material));
            Assert.That(sphere.Radius, Is.EqualTo(radius));
        }

        public static IEnumerable<TestCaseData> HitTestCases()
        {
            return new[]
            {
                new TestCaseData(
                    new Sphere(new Point3D(0, 0, 0), new Material(), 1),
                    new Ray(new Point3D (0, 0, 2), new Vector3D(0, 0, -1)),
                    true,
                    1
                ),
                new TestCaseData(
                    new Sphere(new Point3D(0, 0, 0), new Material(), 1),
                    new Ray(new Point3D (0, 0, 2), new Vector3D(1, 0, 0)),
                    false,
                    double.NaN
                )
            };
        }

        [TestCaseSource(nameof(HitTestCases))]
        public void Hit(Sphere sphere, Ray ray, bool expectedHit, double expectedDistance)
        {
            bool hit = sphere.Hit(ray, out double distance, out ShadeRecord shadeRecord);
            Assert.That(hit, Is.EqualTo(expectedHit));
            if (expectedHit)
            {
                Assert.That(distance, Is.EqualTo(expectedDistance));
            }
        }
    }
}
