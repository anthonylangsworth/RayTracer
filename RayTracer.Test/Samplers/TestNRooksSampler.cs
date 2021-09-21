using NUnit.Framework;
using RayTracer.Primitives;
using RayTracer.Samplers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Test.Samplers
{
    internal class TestNRooksSampler
    {
        [Test]
        public void Ctor()
        {
            int testSamplesPerSet = 64;
            int testSampleSets = 2;
            NRooksSampler sampler = new NRooksSampler(testSamplesPerSet, testSampleSets);
            Assert.That(sampler.SamplesPerSet, Is.EqualTo(testSamplesPerSet));
            Assert.That(sampler.SampleSets, Is.EqualTo(testSampleSets));
        }

        [Test]
        public void TestGenerateSamplesOnUnitSquare()
        {
            int testSamplesPerSet = 16;
            NRooksSampler sampler = new NRooksSampler(testSamplesPerSet, 1);
            IEnumerable<Point2D> set = sampler.GetSamplesOnUnitSquare();
            Assert.That(set.Count(), Is.EqualTo(testSamplesPerSet));
            Assert.IsTrue(set.All(p => p.X >= 0 && p.Y < 1 && p.Y >= 0 && p.Y < 1));
        }
    }
}
