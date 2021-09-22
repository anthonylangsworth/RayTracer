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
        /// <summary>
        /// Create a new <see cref="MultiJitteredSampleGenerator"/>.
        /// </summary>
        /// <param name="random">
        /// The random number generator to use.
        /// </param>
        /// <param name="samplesPerSet">
        /// The number of samples per set. This must be a positive square 
        /// number (e.g. 4, 9, 16, 25, 36, ...).
        /// </param>
        /// <param name="sampleSets">
        /// The number of sample sets, which defaults to 1. This must
        /// be positive.
        /// </param>
        /// <exception cref="ArgumentException">
        /// All arguments must be positive.
        /// </exception>
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

            // Shuffle the X values within each row
            for (int sampleRow = 0; sampleRow < sampleMax; sampleRow++)
            {
                double[] newXs = Enumerable.Range(0, sampleMax).Shuffle(random).Select(index => result[sampleRow, index].X).ToArray();
                for (int sampleColumn = 0; sampleColumn < sampleMax; sampleColumn++)
                {
                    result[sampleRow, sampleColumn] = new Point2D(newXs[sampleColumn], result[sampleRow, sampleColumn].Y);
                }
            }

            // Shuffle the Y values within each column
            for (int sampleColumn = 0; sampleColumn < sampleMax; sampleColumn++)
            {
                double[] newYs = Enumerable.Range(0, sampleMax).Shuffle(random).Select(index => result[index, sampleColumn].Y).ToArray();
                for (int sampleRow = 0; sampleRow < sampleMax; sampleRow++)
                {
                    result[sampleRow, sampleColumn] = new Point2D(result[sampleRow, sampleColumn].X, newYs[sampleRow]);
                }
            }

            return result.Cast<Point2D>();
        }
    }
}
