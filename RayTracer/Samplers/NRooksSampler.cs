using RayTracer.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Samplers
{
    internal class NRooksSampler : Sampler
    {
        /// <inheritdoc/>
        public NRooksSampler(int samplesPerSet, int sampleSets = 1) 
            : base(samplesPerSet, sampleSets)
        {
            // Do nothing
        }

        /// <inheritdoc/>
        protected override IEnumerable<Point2D> GenerateSample(Random random)
        {
            Point2D[] result = new Point2D[SamplesPerSet];
            for (int point = 0; point < SamplesPerSet; point++)
            {
                result[point] = new Point2D(
                    (point + random.NextDouble()) / SamplesPerSet,
                    (point + random.NextDouble()) / SamplesPerSet
                );
            }
            return ShuffleXAndYCoords(result, random);
        }

        /// <summary>
        /// Shuffle the X and Y coordinates in <see cref="points"/>.
        /// </summary>
        /// <param name="points">
        /// The <see cref="Point2D"/>s to shuffle.
        /// </param>
        /// <param name="random">
        /// A <see cref="Random"/>.
        /// </param>
        /// <returns>
        /// Shuffled points.
        /// </returns>
        public static IEnumerable<Point2D> ShuffleXAndYCoords(IEnumerable<Point2D> points, Random random)
        {
            return points.Select(point => point.X).Shuffle(random)
                         .Zip(points.Select(point => point.Y).Shuffle(random))
                         .Select(zipped => new Point2D(zipped.First, zipped.Second));
        }
    }
}
