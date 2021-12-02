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
            ISampleMapper<Point2D> sampleMapper = SampleMappers.UnitSquare;
            SampleGenerator<Point2D> sampleGenerator = new SampleGenerator<Point2D>(sampleAlgorithm, sampleMapper, random, testSamplesPerSet, testSampleSets);
            Assert.That(sampleGenerator.Algorithm, Is.EqualTo(sampleAlgorithm));
            Assert.That(sampleGenerator.Mapper, Is.EqualTo(sampleMapper));
            Assert.That(sampleGenerator.SamplesPerSet, Is.EqualTo(testSamplesPerSet));
            Assert.That(sampleGenerator.SampleSets, Is.EqualTo(testSampleSets));
            Assert.That(sampleGenerator.Random, Is.EqualTo(random));
        }

        public void CtorZeroSamplePerSet()
        {
            Assert.That(
                () => new SampleGenerator<Point2D>(SampleAlgorithms.Regular, SampleMappers.UnitSquare, new Random(), 0, 1),
                Throws.ArgumentException.With.Property("Message").EqualTo("samplesPerSet must be positive"));
        }


        public void CtorZeroSampleSets()
        {
            Assert.That(
                () => new SampleGenerator<Point2D>(SampleAlgorithms.Regular, SampleMappers.UnitSquare, new Random(), 1, 0),
                Throws.ArgumentException.With.Property("Message").EqualTo("sampleSets must be positive"));
        }
    }
}
