using RayTracer.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.SampleGenerators
{
    /// <summary>
    /// Map a point on a unit square to unit square. Yes, this is effectively a NOOP.
    /// </summary>
    public class UnitSquareMappedSampleGenerator : SampleGenerator<Point2D>
    {
        /// <inheritdoc/>
        public UnitSquareMappedSampleGenerator(ISampleAlgorithm algorithm, Random random, uint samplesPerSet, uint sampleSets) 
            : base(algorithm, random, samplesPerSet, sampleSets)
        {
            // Do nothing
        }

        /// <inheritdoc/>
        protected internal override Point2D Map(Point2D point)
        {
            return point;
        }
    }
}
