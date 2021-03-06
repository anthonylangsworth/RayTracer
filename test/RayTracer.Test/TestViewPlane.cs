using NUnit.Framework;
using RayTracer.Primitives;
using RayTracer.SampleGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Test
{
    internal class TestViewPlane
    {
        public void Ctor()
        {
            int testHorizontalResolution = 100;
            int testVerticalResolution = 200;
            double testPixelSize = 1.0;
            double testGamma = 2.0;
            UnitSquareMappedSampleGenerator testSampleGenerator = new UnitSquareMappedSampleGenerator(SampleAlgorithms.Regular, new Random(), 1, 1);
            ViewPlane viewPlane = new ViewPlane(testHorizontalResolution, testVerticalResolution, testPixelSize, testGamma, testSampleGenerator);
            Assert.That(viewPlane.HorizontalResolution, Is.EqualTo(testHorizontalResolution));
            Assert.That(viewPlane.VerticalResolution, Is.EqualTo(testVerticalResolution));
            Assert.That(viewPlane.PixelSize, Is.EqualTo(testPixelSize));
            Assert.That(viewPlane.Gamma, Is.EqualTo(testGamma));
            Assert.That(viewPlane.AntiAliasing, Is.EqualTo(testSampleGenerator));
        }
    }
}
