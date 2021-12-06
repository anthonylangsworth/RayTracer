using NUnit.Framework;
using RayTracer.Primitives;
using RayTracer.SampleGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Test.SampleGenerators
{
    internal class TestSampleGenerator
    {
        public void Ctor()
        {
            int testSamplesPerSet = 64;
            int testSampleSets = 2;
            Random random = new Random();
            ISampleAlgorithm sampleAlgorithm = SampleAlgorithms.Regular;
            SampleGenerator<Point2D> sampleGenerator = new UnitSquareMappedSampleGenerator(sampleAlgorithm, random, testSamplesPerSet, testSampleSets);
            Assert.That(sampleGenerator.Algorithm, Is.EqualTo(sampleAlgorithm));
            Assert.That(sampleGenerator.SamplesPerSet, Is.EqualTo(testSamplesPerSet));
            Assert.That(sampleGenerator.SampleSets, Is.EqualTo(testSampleSets));
            Assert.That(sampleGenerator.Random, Is.EqualTo(random));
        }

        public void CtorZeroSamplePerSet()
        {
            Assert.That(
                () => new UnitSquareMappedSampleGenerator(SampleAlgorithms.Regular, new Random(), 0, 1),
                Throws.ArgumentException.With.Property("Message").EqualTo("samplesPerSet must be positive"));
        }


        public void CtorZeroSampleSets()
        {
            Assert.That(
                () => new UnitSquareMappedSampleGenerator(SampleAlgorithms.Regular, new Random(), 1, 0),
                Throws.ArgumentException.With.Property("Message").EqualTo("sampleSets must be positive"));
        }
    }
}
