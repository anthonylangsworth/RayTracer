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
    internal class TestJitteredSampler
    {
        [Test]
        public void Ctor()
        {
            int testSamplesPerSet = 64;
            int testSampleSets = 2;
            JitteredSampler sampler = new JitteredSampler(testSamplesPerSet, testSampleSets);
            Assert.That(sampler.SamplesPerSet, Is.EqualTo(testSamplesPerSet));
            Assert.That(sampler.SampleSets, Is.EqualTo(testSampleSets));
        }

        [Test]
        public void TestGenerateSamplesOnUnitSquare()
        {
            int testSamplesPerSet = 64;
            JitteredSampler sampler = new JitteredSampler(testSamplesPerSet, 1);
            IEnumerable<Point2D> set = sampler.GetSamplesOnUnitSquare();
            Assert.That(set, Has.Length.EqualTo(64));
            Assert.IsTrue(set.All(p => p.X >= 0 && p.Y < 1 && p.Y >= 0 && p.Y < 1));
        }
    }
}
