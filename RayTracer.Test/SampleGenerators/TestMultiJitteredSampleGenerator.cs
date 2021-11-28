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
    internal class TestMultiJitteredSampleGenerator
    {
        [Test]
        public void GenerateSamplesOnUnitSquare()
        {
            int testSamplesPerSet = 64;
            MultiJitteredSampleAlgorithm sampler = new MultiJitteredSampleAlgorithm();
            IEnumerable<Point2D> set = sampler.GenerateSampleSet(new Random(), testSamplesPerSet);
            Assert.That(set.Count(), Is.EqualTo(testSamplesPerSet));
            Assert.IsTrue(set.All(p => p.X >= 0 && p.Y < 1 && p.Y >= 0 && p.Y < 1));
        }

        // [Test]
        [Repeat(10)] // Repeat to test randomness
        public void GenerateSamplesOnUnitSquareInCells()
        {
            int resolution = 8;
            MultiJitteredSampleAlgorithm sampler = new MultiJitteredSampleAlgorithm();
            IEnumerable<Point2D> set = sampler.GenerateSampleSet(new Random(), 16);
            for (int row = 0; row < resolution; row++)
            {
                for (int column = 0; column < resolution; column++)
                {
                    double minX = (resolution - column - 1) / (double)resolution;
                    double maxX = (resolution - column) / (double)resolution;
                    double minY = (resolution - row - 1) / (double)resolution;
                    double maxY = (resolution - row) / (double)resolution;
                    Assert.That(
                        set.Count(p => p.X >= minX && p.X <= maxX && p.Y >= minY && p.Y <= maxY),
                        Is.EqualTo(1),
                        "Zero or multiple points in a cell");
                }
            }
        }
    }
}
