using RayTracer.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Samplers
{
    public class RegularSampler : Sampler
    {
        /// <summary>
        /// Create a <see cref="RegularSampler"/>.
        /// </summary>
        /// <param name="random">
        /// The random number generator (unused).
        /// </param>
        public RegularSampler(Random random) 
            : base(random, 1, 1)
        {
            // Do nothing
        }

        protected override IEnumerable<Point2D> GenerateSample(Random random)
        {
            return new Point2D[] { new Point2D(0.5, 0.5) };
        }
    }
}
