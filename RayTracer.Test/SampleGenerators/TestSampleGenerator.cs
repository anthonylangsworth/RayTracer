using NUnit.Framework;
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
            ISampleAlgorithm sampleAlgorithm = new RegularSampleAlgorithm();
            SampleGenerator sampleGenerator = new SampleGenerator(sampleAlgorithm, random, testSamplesPerSet, testSampleSets);
            Assert.That(sampleGenerator.Algorithm, Is.EqualTo(sampleAlgorithm));
            Assert.That(sampleGenerator.SamplesPerSet, Is.EqualTo(testSamplesPerSet));
            Assert.That(sampleGenerator.SampleSets, Is.EqualTo(testSampleSets));
            Assert.That(sampleGenerator.Random, Is.EqualTo(random));
        }

        public void CtorZeroSamplePerSet()
        {
            Assert.That(
                () => new SampleGenerator(new RegularSampleAlgorithm(), new Random(), 0, 1),
                Throws.ArgumentException.With.Property("Message").EqualTo("samplesPerSet must be positive"));
        }


        public void CtorZeroSampleSets()
        {
            Assert.That(
                () => new SampleGenerator(new RegularSampleAlgorithm(), new Random(), 1, 0),
                Throws.ArgumentException.With.Property("Message").EqualTo("sampleSets must be positive"));
        }
    }
}
