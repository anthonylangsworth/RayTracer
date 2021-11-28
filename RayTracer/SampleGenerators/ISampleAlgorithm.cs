using RayTracer.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.SampleGenerators
{
    /// <summary>
    /// Create a set of <see cref="Point2D"/> used for anti-aliasing and similar techniques.
    /// </summary>
    public interface ISampleAlgorithm
    {
        /// <summary>
        /// Generate a sample set.
        /// </summary>
        /// <param name="random">
        /// A <see cref="Random"/> to use.
        /// </param>
        /// <param name="samplesPerSet">
        /// The number of samples to generate in the set.
        /// </param>
        /// <returns>
        /// A sample set of <see cref="Point2D"/> in the range [0, 1].
        /// </returns>
        public abstract IEnumerable<Point2D> GenerateSampleSet(Random random, int samplesPerSet);
    }
}