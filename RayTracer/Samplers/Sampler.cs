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
        private Point2D[][] _samples;
        int _currentSample;

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
            _samples = GenerateSamples();
            _currentSample = 0;
        }

        public int SamplesPerSet { get; }
        public int SampleSets { get; }

        public IEnumerable<Point2D> GetSamplesOnUnitSquare()
        {
            lock (_samples)
            {
                IEnumerable<Point2D> result = _samples[_currentSample];
                _currentSample = (_currentSample + 1) % SampleSets;
                return result;
            }
        }

        protected abstract Point2D[][] GenerateSamples();
    }
}
