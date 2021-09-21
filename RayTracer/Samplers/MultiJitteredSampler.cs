using RayTracer.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Samplers
{
    public class MultiJitteredSampler: Sampler
    {
        /// <summary>
        /// Create a new <see cref="MultiJitteredSampler"/>.
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
        public MultiJitteredSampler(Random random, int samplesPerSet, int sampleSets = 1)
            : base(random, samplesPerSet, sampleSets)
        {
            // We could save this value but GenerateSample is called from the base class
            // constructor.
            int samplesPerSetSquareRoot = (int)Math.Sqrt(samplesPerSet);
            if (samplesPerSetSquareRoot * samplesPerSetSquareRoot != samplesPerSet)
            {
                throw new ArgumentOutOfRangeException(nameof(samplesPerSet),
                    $"{ nameof(samplesPerSet) } must be a square number");
            }
        }

        /// <inheritdoc/>
        protected override IEnumerable<Point2D> GenerateSample(Random random)
        {
            Point2D[] result = new Point2D[SamplesPerSet];
            int sampleMax = (int)Math.Sqrt(SamplesPerSet);

            for (int sampleRow = 0; sampleRow < sampleMax; sampleRow++) // up
            {
                for (int sampleColumn = 0; sampleColumn < sampleMax; sampleColumn++) // left to right
                {
                    result[sampleRow * sampleMax + sampleColumn] = new Point2D(
                        (sampleColumn + ((sampleColumn + random.NextDouble()) / sampleMax)) / sampleMax,
                        (sampleRow + ((sampleRow + random.NextDouble()) / sampleMax)) / sampleMax
                    );
                }
            }

            return NRooksSampler.ShuffleXAndYCoords(result, random);
        }
    }
}
