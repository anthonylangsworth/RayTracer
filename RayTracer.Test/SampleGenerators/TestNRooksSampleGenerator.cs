using NUnit.Framework;
using RayTracer.Primitives;
using RayTracer.SampleGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Test.Samplers
{
    internal class TestNRooksSampleGenerator
    {
        [Test]
        public void TestGenerateSamplesOnUnitSquare()
        {
            uint testSamplesPerSet = 16;
            Random random = new Random();
            NRooksSampleAlgorithm sampleAlgorithm = new NRooksSampleAlgorithm();
            IEnumerable<Point2D> set = sampleAlgorithm.GenerateSampleSet(random, testSamplesPerSet);
            Assert.That(set.Count(), Is.EqualTo(testSamplesPerSet));
            Assert.IsTrue(set.All(p => p.X >= 0 && p.Y < 1 && p.Y >= 0 && p.Y < 1));
        }
    }
}
