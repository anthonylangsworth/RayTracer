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
        public RegularSampler() 
            : base(1, 1)
        {
            // Do nothing
        }

        protected override Point2D[][] GenerateSamples(Random random)
        {
            Point2D[][] result = new Point2D[1][];
            result[0] = new Point2D[] { new Point2D(0.5, 0.5) };
            return result;
        }
    }
}
