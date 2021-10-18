using RayTracer.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.SampleGenerators
{
    public class HammersleySampleGenerator : SampleGenerator
    {
        /// <inheritdoc/>
        public HammersleySampleGenerator(Random random, int samplesPerSet) 
            : base(random, samplesPerSet, 1)
        {
            // Do nothing
        }

        protected override IEnumerable<Point2D> GenerateSample(Random random)
        {
            return Enumerable.Range(0, SamplesPerSet).Select(i => new Point2D(i / (double) SamplesPerSet, Phi(i)));
        }

        public static double Phi(int j)
        {
            double x = 0.0;
            double f = 0.5;

            while(j > 0)
            {
                x += f * (j % 2);
                j /= 2;
                f *= 0.5;
            }

            return x;
        }
    }
}
