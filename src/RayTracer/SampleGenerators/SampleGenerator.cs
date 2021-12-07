using RayTracer.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.SampleGenerators
{
    /// <summary>
    /// Generate a set of samples used for anti-aliasing and similar techniques.
    /// </summary>
    public abstract class SampleGenerator<T>
        where T : class
    {
        /// <summary>
        /// Create a new <see cref="SampleGenerator"/>.
        /// </summary>
        /// <param name="algorithm">
        /// The sample generation algorithm.
        /// </param>
        /// <param name="random">
        /// The random number generator to use. This must be threadsafe.
        /// </param>
        /// <param name="samplesPerSet">
        /// The number of samples per set. This must be positive.
        /// </param>
        /// <param name="sampleSets">
        /// The number of sample sets. This must be positive.
        /// </param>
        /// <exception cref="ArgumentException">
        /// All arguments must be positive.
        /// </exception>
        public SampleGenerator(ISampleAlgorithm algorithm, Random random, uint samplesPerSet, uint sampleSets)
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
            Algorithm = algorithm;
            SamplesPerSet = samplesPerSet;
            SampleSets = sampleSets;
            Samples = Enumerable.Range(0, (int) sampleSets).Select(i => algorithm.GenerateSampleSet(Random, samplesPerSet).Select(point => Map(point))).ToArray();
        }

        public Random Random { get; }
        public ISampleAlgorithm Algorithm { get; }
        public uint SamplesPerSet { get; }
        public uint SampleSets { get; }
        public IEnumerable<T>[] Samples { get; }

        /// <summary>
        /// Get a random sample set, mapped to a unit square.
        /// </summary>
        /// <returns>
        /// The sample set.
        /// </returns>
        public IEnumerable<T> GetSamples()
        {
            return Samples[Random.Next() % SampleSets];
        }

        /// <summary>
        /// Map a point on a unit square to another distribution.
        /// </summary>
        /// <remarks>
        /// This method is protected internal to allow easy testing.
        /// </remarks>
        /// <returns>
        /// The mapped point.
        /// </returns>
        protected internal abstract T Map(Point2D point);
    }
}
