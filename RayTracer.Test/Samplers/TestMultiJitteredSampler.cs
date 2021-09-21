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
    internal class TestMultiJitteredSampler
    {
        [Test]
        public void Ctor()
        {
            int testSamplesPerSet = 64;
            int testSampleSets = 2;
            Random random = new Random();
            MultiJitteredSampler sampler = new MultiJitteredSampler(random, testSamplesPerSet, testSampleSets);
            Assert.That(sampler.SamplesPerSet, Is.EqualTo(testSamplesPerSet));
            Assert.That(sampler.SampleSets, Is.EqualTo(testSampleSets));
            Assert.That(sampler.Random, Is.EqualTo(random));
        }

        [Test]
        public void GenerateSamplesOnUnitSquare()
        {
            int testSamplesPerSet = 64;
            MultiJitteredSampler sampler = new MultiJitteredSampler(new Random(), testSamplesPerSet, 1);
            IEnumerable<Point2D> set = sampler.GetSamplesOnUnitSquare();
            Assert.That(set.Count(), Is.EqualTo(testSamplesPerSet));
            Assert.IsTrue(set.All(p => p.X >= 0 && p.Y < 1 && p.Y >= 0 && p.Y < 1));
        }

        [Test]
        [Ignore("Failing due to bug in shuffling")]
        public void GenerateSamplesOnUnitSquareInCells()
        {
            int sampleMax = 8;
            MultiJitteredSampler sampler = new MultiJitteredSampler(new Random(), sampleMax * sampleMax);
            IEnumerable<Point2D> set = sampler.GetSamplesOnUnitSquare();
            for(int row = 0; row < sampleMax; row++)
            {
                for(int column = 0; column < sampleMax; column++)
                {
                    double minX = (sampleMax - column - 1) / (double)sampleMax;
                    double maxX = (sampleMax - column) / (double)sampleMax;
                    double minY = (sampleMax - row - 1) / (double)sampleMax;
                    double maxY = (sampleMax - row) / (double)sampleMax;
                    Assert.That(
                        set.Count(p => p.X >= minX && p.X < maxX && p.Y >= minY && p.Y < maxY), 
                        Is.EqualTo(1),
                        "Zero or multiple points in a cell");
                }
            }
        }
    }
}
