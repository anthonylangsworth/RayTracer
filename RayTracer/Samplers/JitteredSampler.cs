using RayTracer.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Samplers
{
    public class JitteredSampler : Sampler
    {
        /// <summary>
        /// Create a new <see cref="JitteredSampler"/>.
        /// </summary>
        /// <param name="random">
        /// The random number generator to use.
        /// </param>
        /// <param name="samplesPerSet">
        /// The number of samples per set. This should be a square 
        /// number (e.g. 4, 9, 16, 25, 36, ...) and must be positive.
        /// </param>
        /// <param name="sampleSets">
        /// The number of sample sets, which defaults to 1. This must
        /// be positive.
        /// </param>
        /// <exception cref="ArgumentException">
        /// All arguments must be positive.
        /// </exception>
        public JitteredSampler(Random random, int samplesPerSet, int sampleSets = 1) 
            : base(random, samplesPerSet, sampleSets)
        {
            // Do nothing
        }

        /// <inheritdoc/>
        protected override IEnumerable<Point2D> GenerateSample(Random random)
        {
            int sampleMax = (int) Math.Sqrt(SamplesPerSet);
            Point2D[] result = new Point2D[SamplesPerSet];

            for (int sampleRow = 0; sampleRow < sampleMax; sampleRow++) // up
            {
                for (int sampleColumn = 0; sampleColumn < sampleMax; sampleColumn++) // left to right
                {
                    result[sampleRow * sampleMax + sampleColumn] = new Point2D(
                        (sampleColumn + random.NextDouble()) / sampleMax,
                        (sampleRow + random.NextDouble()) / sampleMax
                    );
                }
            }

            return result;
        }
    }
}
