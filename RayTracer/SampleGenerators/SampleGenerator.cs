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
    public class SampleGenerator
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
        public SampleGenerator(ISampleAlgorithm algorithm, ISampleMapper mapper, Random random, int samplesPerSet, int sampleSets)
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
            Mapper = mapper;
            SamplesPerSet = samplesPerSet;
            SampleSets = sampleSets;
            Samples = Enumerable.Range(0, sampleSets).Select(i => algorithm.GenerateSampleSet(Random, samplesPerSet).Select(point => mapper.Map(point))).ToArray();
        }

        public Random Random { get; }
        public ISampleAlgorithm Algorithm { get; }
        public ISampleMapper Mapper { get; }
        public int SamplesPerSet { get; }
        public int SampleSets { get; }
        public IEnumerable<Point2D>[] Samples { get; }

        /// <summary>
        /// Get a random sample set, mapped to a unit square.
        /// </summary>
        /// <returns>
        /// The sample set.
        /// </returns>
        public IEnumerable<Point2D> GetSamples()
        {
            return Samples[Random.Next() % SampleSets];
        }
    }
}
