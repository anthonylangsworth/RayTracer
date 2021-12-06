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
    internal class TestJitteredSampleAlgorithm
    {
        [Test]
        public void GenerateSamplesSet()
        {
            uint testSamplesPerSet = 64;
            ISampleAlgorithm sampler = SampleAlgorithms.Jittered;
            IEnumerable<Point2D> set = sampler.GenerateSampleSet(new Random(), testSamplesPerSet);
            Assert.That(set.Count(), Is.EqualTo(testSamplesPerSet));
            Assert.IsTrue(set.All(p => p.X >= 0 && p.Y < 1 && p.Y >= 0 && p.Y < 1));
        }
    }
}
