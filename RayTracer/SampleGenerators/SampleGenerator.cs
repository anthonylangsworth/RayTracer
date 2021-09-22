using RayTracer.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.SampleGenerators
{
    /// <summary>
    /// Generate "subpixels" to render within a pixel to get a better image.`
    /// </summary>
    public abstract class SampleGenerator
    {
        private readonly IEnumerable<Point2D>[] _samples;

        /// <summary>
        /// Create a new <see cref="Sampler"/>.
        /// </summary>
        /// <param name="random">
        /// The random number generator to use.
        /// </param>
        /// <param name="samplesPerSet">
        /// The number of samples per set. This must be positive.
        /// </param>
        /// <param name="sampleSets">
        /// The number of sample sets, which defaults to 1. This must
        /// be positive.
        /// </param>
        /// <exception cref="ArgumentException">
        /// All arguments must be positive.
        /// </exception>
        protected SampleGenerator(Random random, int samplesPerSet, int sampleSets)
        {
            if (samplesPerSet <= 0)
            {
                throw new ArgumentException($"{ nameof(samplesPerSet)} must be positive", nameof(samplesPerSet));
            }
            if (sampleSets <= 0)
            {
                throw new ArgumentException($"{ nameof(sampleSets)} must be positive", nameof(sampleSets));
            }
            Random = random;
            SamplesPerSet = samplesPerSet;
            SampleSets = sampleSets;
            _samples = Enumerable.Range(0, sampleSets).Select(i => GenerateSample(Random)).ToArray();
        }

        public Random Random { get; }
        public int SamplesPerSet { get; }
        public int SampleSets { get; }

        /// <summary>
        /// Get a random next sample set.
        /// </summary>
        /// <returns>
        /// The sample set.
        /// </returns>
        public IEnumerable<Point2D> GetSamplesOnUnitSquare()
        {
            return _samples[Random.Next() % SampleSets];
        }

        protected abstract IEnumerable<Point2D> GenerateSample(Random random);
    }
}
