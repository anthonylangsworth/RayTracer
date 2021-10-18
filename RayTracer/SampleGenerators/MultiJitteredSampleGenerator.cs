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
            int resolution = SamplesPerSetSquareRoot;
            Point2D[,] result = GeneratePoints(random, resolution);

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

            //result = ShuffleXValuesInRows(random, result);
            //result = ShuffleYValuesInColumns(random, result);
            result = ShufflePoints(random, result);
            return result.Cast<Point2D>();
        }

        public static Point2D[,] GeneratePoints(Random random, int resolution)
        {
            // See http://graphics.cs.cmu.edu/courses/15-468/lectures/lecture20.pdf for a good description

            Point2D[,] result = new Point2D[resolution, resolution];
            for (int sampleRow = 0; sampleRow < resolution; sampleRow++) // up
            {
                for (int sampleColumn = 0; sampleColumn < resolution; sampleColumn++) // left to right
                {
                    result[sampleRow, sampleColumn] = new Point2D(
                        (sampleColumn + ((sampleRow + random.NextDouble()) / resolution)) / resolution,
                        (sampleRow + ((sampleColumn + random.NextDouble()) / resolution)) / resolution
                    );
                }
            }
            return result;
        }

        public static Point2D[,] ShufflePoints(Random random, Point2D[,] points)
        {
            Point2D[,] result = new Point2D[points.GetLength(0), points.GetLength(1)];
            double[][] newXs = new double[points.GetLength(0)][]; // Row first, like points
            double[][] newYs = new double[points.GetLength(1)][]; // Column first

            for (int row = 0; row < points.GetLength(0); row++)
            {
                newXs[row] = Enumerable.Range(0, points.GetLength(1)).Shuffle(random).Select(column => points[row, column].X).ToArray();
            }
            for (int column = 0; column < points.GetLength(1); column++)
            {
                newYs[column] = Enumerable.Range(0, points.GetLength(0)).Shuffle(random).Select(row => points[row, column].Y).ToArray();
            }

            for (int sampleRow = 0; sampleRow < points.GetLength(0); sampleRow++)
            {
                for (int sampleColumn = 0; sampleColumn < points.GetLength(1); sampleColumn++)
                {
                    result[sampleRow, sampleColumn] = new Point2D(newXs[sampleRow][sampleColumn], newYs[sampleColumn][sampleRow]);
                }
            }

            return result;
        }

        public static Point2D[,] ShuffleXValuesInRows(Random random, Point2D[,] points)
        {
            Point2D[,] result = new Point2D[points.GetLength(0), points.GetLength(1)];
            for (int sampleRow = 0; sampleRow < points.GetLength(0); sampleRow++)
            {
                double[] newXs = Enumerable.Range(0, points.GetLength(1)).Shuffle(random).Select(column => points[sampleRow, column].X).ToArray();
                for (int sampleColumn = 0; sampleColumn < points.GetLength(1); sampleColumn++)
                {
                    result[sampleRow, sampleColumn] = new Point2D(newXs[sampleColumn], points[sampleRow, sampleColumn].Y);
                }
            }
            return result;
        }

        public static Point2D[,] ShuffleYValuesInColumns(Random random, Point2D[,] points)
        {
            Point2D[,] result = new Point2D[points.GetLength(0), points.GetLength(1)];
            for (int sampleColumn = 0; sampleColumn < points.GetLength(1); sampleColumn++)
            {
                double[] newYs = Enumerable.Range(0, points.GetLength(0)).Shuffle(random).Select(row => points[row, sampleColumn].Y).ToArray();
                for (int sampleRow = 0; sampleRow < points.GetLength(0); sampleRow++)
                {
                    result[sampleRow, sampleColumn] = new Point2D(points[sampleRow, sampleColumn].X, newYs[sampleRow]);
                }
            }
            return result;
        }
    }
}
