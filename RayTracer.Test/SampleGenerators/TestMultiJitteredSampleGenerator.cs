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
        public void Ctor()
        {
            int testSamplesPerSet = 64;
            int testSampleSets = 2;
            Random random = new Random();
            MultiJitteredSampleGenerator sampler = new MultiJitteredSampleGenerator(random, testSamplesPerSet, testSampleSets);
            Assert.That(sampler.SamplesPerSet, Is.EqualTo(testSamplesPerSet));
            Assert.That(sampler.SampleSets, Is.EqualTo(testSampleSets));
            Assert.That(sampler.Random, Is.EqualTo(random));
        }

        [Test]
        public void GenerateSamplesOnUnitSquare()
        {
            int testSamplesPerSet = 64;
            MultiJitteredSampleGenerator sampler = new MultiJitteredSampleGenerator(new Random(), testSamplesPerSet, 1);
            IEnumerable<Point2D> set = sampler.GetSamplesOnUnitSquare();
            Assert.That(set.Count(), Is.EqualTo(testSamplesPerSet));
            Assert.IsTrue(set.All(p => p.X >= 0 && p.Y < 1 && p.Y >= 0 && p.Y < 1));
        }

        [Test]
        [Ignore("Failing due to bug in shuffling")]
        public void GenerateSamplesOnUnitSquareInCells()
        {
            int resolution = 8;
            for (int times = 0; times < 10; times++)
            {
                MultiJitteredSampleGenerator sampler = new MultiJitteredSampleGenerator(new Random(), resolution * resolution);
                IEnumerable<Point2D> set = sampler.GetSamplesOnUnitSquare();
                for (int row = 0; row < resolution; row++)
                {
                    for (int column = 0; column < resolution; column++)
                    {
                        double minX = (resolution - column - 1) / (double)resolution;
                        double maxX = (resolution - column) / (double)resolution;
                        double minY = (resolution - row - 1) / (double)resolution;
                        double maxY = (resolution - row) / (double)resolution;
                        Assert.That(
                            set.Count(p => p.X >= minX && p.X < maxX && p.Y >= minY && p.Y < maxY),
                            Is.EqualTo(1),
                            "Zero or multiple points in a cell");
                    }
                }
            }
        }

        [Test]
        public void ShuffleYValuesInColumns()
        {
            Nonrandom nonrandom = new Nonrandom(); // Fixed values
            const int resolution = 4;
            Point2D[,] result = MultiJitteredSampleGenerator.GeneratePoints(nonrandom, resolution);
            result = MultiJitteredSampleGenerator.ShuffleYValuesInColumns(nonrandom, result);

            for (int sampleColumn = 0; sampleColumn < resolution; sampleColumn++)
            {
                var expectedYs = Enumerable.Range(0, resolution).Select(row =>
                    (row + ((sampleColumn + Nonrandom.Value) / resolution)) / resolution);
                var actualYs = Enumerable.Range(0, resolution).Select(row => result[row, sampleColumn].Y);
                Assert.That(actualYs, Is.EquivalentTo(expectedYs));

                var expectedXs = Enumerable.Range(0, resolution).Select(row =>
                    (sampleColumn + ((row + Nonrandom.Value) / resolution)) / resolution);
                var actualXs = Enumerable.Range(0, resolution).Select(row => result[row, sampleColumn].X);
                Assert.That(actualXs, Is.EqualTo(expectedXs));
            }
        }

        [Test]
        public void ShuffleXValuesInRows()
        {
            Nonrandom nonrandom = new Nonrandom(); // Fixed values
            const int resolution = 4;
            Point2D[,] result = MultiJitteredSampleGenerator.GeneratePoints(nonrandom, resolution);
            result = MultiJitteredSampleGenerator.ShuffleXValuesInRows(nonrandom, result);

            for (int sampleRow = 0; sampleRow < resolution; sampleRow++)
            {
                var expectedYs = Enumerable.Range(0, resolution).Select(column =>
                    (sampleRow + ((column + Nonrandom.Value) / resolution)) / resolution);
                var actualYs = Enumerable.Range(0, resolution).Select(column => result[sampleRow, column].Y);
                Assert.That(actualYs, Is.EqualTo(expectedYs));

                var expectedXs = Enumerable.Range(0, resolution).Select(column =>
                    (column + ((sampleRow + Nonrandom.Value) / resolution)) / resolution);
                var actualXs = Enumerable.Range(0, resolution).Select(column => result[sampleRow, column].X);
                Assert.That(actualXs, Is.EquivalentTo(expectedXs));
            }
        }
    }
}
