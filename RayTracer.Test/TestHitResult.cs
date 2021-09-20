using NUnit.Framework;
using NUnit.Framework.Constraints;
using RayTracer.Objects;
using RayTracer.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace RayTracer.Test
{
    public class TestHitResult
    {
        public void Ctor()
        {
            double distance = Math.PI;
            ShadeRecord shadeRecord= new ShadeRecord(new Vector3D(0, 0, 1), new Point3D(0, 0, 2));
            Sphere sphere = new Sphere(Point3D.Origin, Material.White, 10);
            Hit hit = new Hit(distance, shadeRecord, sphere);
            Assert.That(hit.Distance, Is.EqualTo(distance));
            Assert.That(hit.ShadeRecord, Is.EqualTo(shadeRecord));
            Assert.That(hit.GeometricObject, Is.EqualTo(sphere));
        }

        [TestCase(0.1, false)]
        [TestCase(0, false)]
        [TestCase(-0.1, true)]
        public void CtorDistance(double distance, bool expectedException)
        {
            ShadeRecord shadeRecord = new ShadeRecord(new Vector3D(0, 0, 1), new Point3D(0, 0, 2));
            // Will cause the debugger to break here. Lambda attributes, e.g. [DebuggerStepThrough], is only supported in C# 9.0 or later
            // Assert.That(() => new Hit(distance, shadeRecord), expectedException ? (Constraint) Throws.ArgumentException : Throws.Nothing);
        }
    }
}
