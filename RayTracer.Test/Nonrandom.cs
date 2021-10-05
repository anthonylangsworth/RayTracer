using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Test
{
    /// <summary>
    /// A partial implementation of <see cref="Random"/> that always returns <see cref="Value"/>.
    /// </summary>
    internal class Nonrandom: Random
    {
        /// <summary>
        /// The value always returned from <see cref="Next"/>.
        /// </summary>
        public readonly static double Value = 0.5;

        /// <inheritdoc/>
        public Nonrandom()
            : base()
        {
            // Do nothing
        }

        /// <inheritdoc/>
        protected override double Sample()
        {
            return 0.5;
        }

        /// <inheritdoc/>
        public override int Next(int minValue, int maxValue)
        {
            return Convert.ToInt32((maxValue - minValue) * Value) + minValue;
        }

        /// <inheritdoc/>
        public override int Next()
        {
            return Next(0, int.MaxValue);
        }

        /// <inheritdoc/>
        public override void NextBytes(byte[] buffer)
        {
            throw new NotImplementedException();
        }
    }
}
