using NUnit.Framework;
using RayTracer.SampleGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Test.SampleGenerators
{
    internal class TestHammersleySampleGenerator
    {
        [TestCase(0, 0)]
        [TestCase(1, 0.5)]
        [TestCase(2, 0.25)]
        [TestCase(3, 0.75)]
        [TestCase(4, 0.125)]
        public void Phi(int i, double expectedPhi)
        {
            Assert.That(HammersleySampleGenerator.Phi(i), Is.EqualTo(expectedPhi));
        }
    }
}
