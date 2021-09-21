using RayTracer.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public static class Helpers
    {
        /// <summary>
        /// Perform a Fisher-Yates-Durstenfeld shuffle (see https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle#The_modern_algorithm).
        /// </summary>
        /// <param name="source">
        /// The source collection.
        /// </param>
        /// <param name="random">
        /// A <see cref="Random"/>.
        /// </param>
        /// <remarks>
        /// Copied from https://stackoverflow.com/questions/5807128/an-extension-method-on-ienumerable-needed-for-shuffling.
        /// </remarks>
        /// <returns>
        /// A shuffled enumerable.
        /// </returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random random)
        {
            var buffer = source.ToList();
            for (int i = 0; i < buffer.Count; i++)
            {
                int j = random.Next(i, buffer.Count);
                yield return buffer[j];
                buffer[j] = buffer[i];
            }
        }
    }
}
