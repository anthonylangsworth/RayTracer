using NUnit.Framework;
using RayTracer.Cameras;
using RayTracer.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Test.Cameras
{
    internal class TestPinhole
    {
        [Test]
        public void Ctor()
        {
            Point3D eye = new Point3D(100, 100, 100);
            Point3D lookat = new Point3D(0, 0, 0);
            Vector3D up = new Vector3D(0, 1, 0);
            double viewPlaneDistance = 100;
            Pinhole pinhole = new Pinhole(eye, lookat, up, viewPlaneDistance);
            Assert.That(pinhole.Eye, Is.EqualTo(eye));
            Assert.That(pinhole.LookAt, Is.EqualTo(lookat));
            Assert.That(pinhole.Up.Normalize(), Is.EqualTo(up.Normalize()));
            Assert.That(pinhole.Zoom, Is.EqualTo(Pinhole.DefaultZoom));
            Assert.That(pinhole.ExposureTime, Is.EqualTo(Camera.DefaultExposureTime));
        }

        [Test]
        public void CtorEqualEyeAndLookAt()
        {
            Point3D point = new Point3D(100, 100, 100);
            Vector3D up = new Vector3D(0, 1, 0);
            double viewPlaneDistance = 100;
            Assert.That(() => new Pinhole(point, point, up, viewPlaneDistance),
                Throws.ArgumentException.With.Property("Message").EqualTo("eye cannot be the same as lookAt (Parameter 'eye')"));
        }


        [Test]
        public void CtorZeroZoom()
        {
            Point3D eye = new Point3D(100, 100, 100);
            Point3D lookAt = new Point3D(0, 0, 0);
            Vector3D up = new Vector3D(0, 1, 0);
            double viewPlaneDistance = 100;
            Assert.That(() => new Pinhole(eye, lookAt, up, viewPlaneDistance, 0),
                Throws.ArgumentException.With.Property("Message").EqualTo("zoom cannot be zero (Parameter 'zoom')"));
        }

        [Test]
        [TestCaseSource(nameof(GetRayDirectionTestData))]
        public void GetRayDirection(double x, double y, Pinhole pinholeCamera, Vector3D expectedResult)
        {
            Assert.That(pinholeCamera.GetRayDirection(x, y), Is.EqualTo(expectedResult.Normalize()));
        }

        public static IEnumerable<TestCaseData> GetRayDirectionTestData()
        {
            return new[] {
                new TestCaseData(1, 1, 
                    new Pinhole(new Point3D(0, 0, 100), new Point3D(0, 0, 0), new Vector3D(0, 1, 0), 100), 
                    new Vector3D(1, 1, -100)),
                new TestCaseData(1, 1,
                    new Pinhole(new Point3D(0, 0, 0), new Point3D(0, 50, 0), new Vector3D(0, 1, 0), 100),
                    new Vector3D(0, 1, 0)),
                new TestCaseData(1, 1,
                    new Pinhole(new Point3D(0, 0, 0), new Point3D(0, -50, 0), new Vector3D(0, 1, 0), 100),
                    new Vector3D(0, -1, 0))
            };
        }
    }
}
