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
        public SampleGenerator(ISampleAlgorithm algorithm, Random random, int samplesPerSet, int sampleSets)
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

            // Use Lazy evaluation to allow derived constructors to complete
            // before GenerateSample is called.
            SamplesOnUnitSquare = new Lazy<IEnumerable<Point2D>[]>(() => Enumerable.Range(0, sampleSets).Select(i => algorithm.GenerateSampleSet(Random, samplesPerSet)).ToArray(), true);
            SamplesOnUnitDisk = new Lazy<IEnumerable<Point2D>[]>(() => SamplesOnUnitSquare.Value.Select(samples => samples.Select(point => MapSquareToDisk(point))).ToArray(), true);
        }

        public Random Random { get; }
        public ISampleAlgorithm Algorithm { get; }
        public int SamplesPerSet { get; }
        public int SampleSets { get; }
        public Lazy<IEnumerable<Point2D>[]> SamplesOnUnitSquare { get; }
        public Lazy<IEnumerable<Point2D>[]> SamplesOnUnitDisk { get; }

        /// <summary>
        /// Get a random sample set, mapped to a unit square.
        /// </summary>
        /// <returns>
        /// The sample set.
        /// </returns>
        public IEnumerable<Point2D> GetSamplesOnUnitSquare()
        {
            return SamplesOnUnitSquare.Value[Random.Next() % SampleSets];
        }

        /// <summary>
        /// Get a random sample set, mapped to a unit disk.
        /// </summary>
        /// <returns>
        /// The sample set.
        /// </returns>
        public IEnumerable<Point2D> GetSamplesOnUnitDisk()
        {
            return SamplesOnUnitDisk.Value[Random.Next() % SampleSets];
        }

        /// <summary>
        /// Map a point on a unit square to a point on a unit disk using Shirley's mapping.
        /// </summary>
        /// <returns>
        /// The mapped point.
        /// </returns>
        private Point2D MapSquareToDisk(Point2D point)
        {
            double x;
            double y;
            double r;
            double phi;

            // Map from [0, 1] to [-1, 1]
            x = 2 * point.X - 1;
            y = 2 * point.Y - 1;

            if(x > -y)
            {
                if(x > y)
                {
                    r = x;
                    phi = y / x;
                }
                else
                {
                    r = y;
                    phi = 2 - x / y;
                }
            }
            else
            {
                if (x < y)
                {
                    r = -x;
                    phi = 4 + y / x;
                }
                else
                {
                    r = -y;
                    if(y != 0)
                    {
                        phi = 6 - x / y;
                    }
                    else
                    {
                        phi = 0;
                    }
                }
            }

            phi *= Math.PI / 4;

            return new Point2D(r * Math.Cos(phi), r * Math.Sin(phi));
        }

        private Point2D MapSquareToHemisphere(double e)
        {
            return new Point2D(0, 0);
        }
    }
}
