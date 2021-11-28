using RayTracer.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.SampleGenerators
{
    public class HammersleySampleAlgorithm : ISampleAlgorithm
    {
        /// <inheritdoc/>
        public IEnumerable<Point2D> GenerateSampleSet(Random random, int samplesPerSet)
        {
            return Enumerable.Range(0, samplesPerSet).Select(i => new Point2D(i / (double) samplesPerSet, Phi(i)));
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
