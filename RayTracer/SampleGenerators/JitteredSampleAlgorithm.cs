using RayTracer.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.SampleGenerators
{
    internal class JitteredSampleAlgorithm : ISampleAlgorithm
    {
        /// <inheritdoc/>
        public IEnumerable<Point2D> GenerateSampleSet(Random random, uint samplesPerSet)
        {
            Point2D[] result = new Point2D[samplesPerSet];
            int sampleMax = (int) Math.Sqrt(samplesPerSet);

            for (int sampleRow = 0; sampleRow < sampleMax; sampleRow++) // up
            {
                for (int sampleColumn = 0; sampleColumn < sampleMax; sampleColumn++) // left to right
                {
                    yield return new Point2D(
                        (sampleColumn + random.NextDouble()) / sampleMax,
                        (sampleRow + random.NextDouble()) / sampleMax
                    );
                }
            }
        }
    }
}
