using RayTracer.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Samplers
{
    /// <summary>
    /// Generate "subpixels" to render within a pixel to get a better image.`
    /// </summary>
    public abstract class Sampler
    {
        private readonly IEnumerable<Point2D>[] _samples;
        readonly Random _random;

        /// <summary>
        /// Create a new <see cref="Sampler"/>.
        /// </summary>
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
        protected Sampler(int samplesPerSet, int sampleSets)
        {
            if (samplesPerSet <= 0)
            {
                throw new ArgumentException($"{ nameof(samplesPerSet)} must be positive", nameof(samplesPerSet));
            }
            if (sampleSets <= 0)
            {
                throw new ArgumentException($"{ nameof(sampleSets)} must be positive", nameof(sampleSets));
            }

            SamplesPerSet = samplesPerSet;
            SampleSets = sampleSets;
            _random = new Random();
            _samples = Enumerable.Range(0, sampleSets).Select(i => GenerateSample(_random)).ToArray();
        }

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
            // _random is not threadsafe.
            lock (_random)
            {
                return _samples[_random.Next() % SampleSets];
            }
        }

        protected abstract IEnumerable<Point2D> GenerateSample(Random random);
    }
}
