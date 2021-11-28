using RayTracer.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.SampleGenerators
{
    public class RegularSampleAlgorithm : ISampleAlgorithm
    {
        /// <inheritdoc/>
        public IEnumerable<Point2D> GenerateSampleSet(Random random, int samplesPerSet)
        {
            return new Point2D[] { new Point2D(0.5, 0.5) };
        }
    }
}
