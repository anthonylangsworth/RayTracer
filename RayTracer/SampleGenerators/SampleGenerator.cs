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
            Samples = new Lazy<IEnumerable<Point2D>[]>(() => Enumerable.Range(0, sampleSets).Select(i => GenerateSample(Random)).ToArray(), true);
        }

        public Random Random { get; }
        public int SamplesPerSet { get; }
        public int SampleSets { get; }
        public Lazy<IEnumerable<Point2D>[]> Samples { get; }

        /// <summary>
        /// Get a random next sample set, mapped to a unit square.
        /// </summary>
        /// <returns>
        /// The sample set.
        /// </returns>
        public IEnumerable<Point2D> GetSamplesOnUnitSquare()
        {
            return Samples.Value[Random.Next() % SampleSets];
        }

        /// <summary>
        /// Get a random next sample set, mapped to a unit disk.
        /// </summary>
        /// <returns>
        /// The sample set.
        /// </returns>
        public IEnumerable<Point2D> GetSamplesOnUnitDisk()
        {
            return GetSamplesOnUnitSquare().Select(p => new Point2D(Math.Cos(2 * Math.PI * p.X), Math.Sin(2 * Math.PI * p.Y)));
        }

        /// <summary>
        /// Called lazily on first access.
        /// </summary>
        /// <param name="random">
        /// A <see cref="Random"/> to use.
        /// </param>
        /// <returns>
        /// A sample set.
        /// </returns>
        protected abstract IEnumerable<Point2D> GenerateSample(Random random);
    }
}
