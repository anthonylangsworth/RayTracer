using RayTracer.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.SampleGenerators
{
    public class MultiJitteredSampleGenerator: JitteredSampleGenerator
    {
        /// <inheritdoc/>
        public MultiJitteredSampleGenerator(Random random, int samplesPerSet, int sampleSets = 1)
            : base(random, samplesPerSet, sampleSets)
        {
            // Do nothing
        }

        /// <inheritdoc/>
        protected override IEnumerable<Point2D> GenerateSample(Random random)
        {
            int sampleMax = SamplesPerSetSquareRoot;
            Point2D[,] result = new Point2D[sampleMax, sampleMax];

            for (int sampleRow = 0; sampleRow < sampleMax; sampleRow++) // up
            {
                for (int sampleColumn = 0; sampleColumn < sampleMax; sampleColumn++) // left to right
                {
                    result[sampleRow, sampleColumn] = new Point2D(
                        (sampleColumn + ((sampleRow + random.NextDouble()) / sampleMax)) / sampleMax,
                        (sampleRow + ((sampleColumn + random.NextDouble()) / sampleMax)) / sampleMax
                    );
                }
            }

            // See http://graphics.cs.cmu.edu/courses/15-468/lectures/lecture20.pdf for a good description

            //// Shuffle the X values within each row
            //for (int sampleRow = 0; sampleRow < sampleMax; sampleRow++)
            //{
            //    for (int sampleColumn = 0; sampleColumn < sampleMax; sampleColumn++)
            //    {
            //        int swapColumn = random.Next(sampleColumn, sampleMax);
            //        double swapColumnX = result[sampleRow, swapColumn].X;
            //        result[sampleRow, swapColumn] = new Point2D(result[sampleRow, sampleColumn].X, result[sampleRow, swapColumn].Y);
            //        result[sampleRow, sampleColumn] = new Point2D(swapColumnX, result[sampleRow, sampleColumn].Y);
            //    }
            //}

            //// Shuffle the Y values within each column
            //for (int sampleColumn = 0; sampleColumn < sampleMax; sampleColumn++)
            //{
            //    for (int sampleRow = 0; sampleRow < sampleMax; sampleRow++)
            //    {
            //        int swapRow = random.Next(sampleRow, sampleMax);
            //        double swapRowY = result[swapRow, sampleColumn].Y;
            //        result[swapRow, sampleColumn] = new Point2D(result[swapRow, sampleColumn].X, result[sampleRow, sampleColumn].Y);
            //        result[sampleRow, sampleColumn] = new Point2D(result[sampleRow, sampleColumn].X, swapRowY);
            //    }
            //}

            // Shuffle the X values within each row
            //for (int sampleRow = 0; sampleRow < sampleMax; sampleRow++)
            //{
            //    double[] newXs = Enumerable.Range(0, sampleMax).Shuffle(random).Select(index => result[sampleRow, index].X).ToArray();
            //    for (int sampleColumn = 0; sampleColumn < sampleMax; sampleColumn++)
            //    {
            //        result[sampleRow, sampleColumn] = new Point2D(newXs[sampleColumn], result[sampleRow, sampleColumn].Y);
            //    }
            //}

            // Shuffle the Y values within each column
            for (int sampleColumn = 0; sampleColumn < sampleMax; sampleColumn++)
            {
                double[] newYs = Enumerable.Range(0, sampleMax).Shuffle(random).Select(index => result[index, sampleColumn].Y).ToArray();
                for (int sampleRow = 0; sampleRow < sampleMax; sampleRow++)
                {
                    result[sampleRow, sampleColumn] = new Point2D(result[sampleRow, sampleColumn].X, newYs[sampleRow]);
                }
            }

            //for (int row = 0; row < sampleMax; row++)
            //{
            //    for (int column = 0; column < sampleMax; column++)
            //    {
            //        double minX = (sampleMax - column - 1) / (double)sampleMax;
            //        double maxX = (sampleMax - column) / (double)sampleMax;
            //        double minY = (sampleMax - row - 1) / (double)sampleMax;
            //        double maxY = (sampleMax - row) / (double)sampleMax;
            //        Assert.That(
            //            set.Count(p => p.X >= minX && p.X < maxX && p.Y >= minY && p.Y < maxY),
            //            Is.EqualTo(1),
            //            "Zero or multiple points in a cell");
            //    }
            //}

            return result.Cast<Point2D>();
        }
    }
}
