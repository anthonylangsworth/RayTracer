using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    /// <summary>
    /// Override <see cref="Random"/> to provide thread-safety.
    /// </summary>
    /// <remarks>
    /// See https://docs.microsoft.com/en-us/dotnet/api/system.random?f1url=%3FappId%3DDev16IDEF1%26l%3DEN-US%26k%3Dk(System.Random);k(DevLang-csharp)%26rd%3Dtrue&view=net-5.0
    /// for discussion and details. Not all members need to be overridden.
    /// </remarks>
    internal class ConcurrentRandom: Random
    {
        private readonly object _lock = new object();

        /// <inheritdoc/>
        public ConcurrentRandom()
            : base()
        {
            // Do nothing
        }

        /// <inheritdoc/>
        public ConcurrentRandom(int seed)
            : base(seed)
        {
            // Do nothing
        }

        /// <inheritdoc/>
        protected override double Sample()
        {
            lock (_lock)
            {
                return base.Sample();
            }
        }

        /// <inheritdoc/>
        public override int Next(int minValue, int maxValue)
        {
            lock (_lock)
            {
                return base.Next(minValue, maxValue);
            }
        }

        /// <inheritdoc/>
        public override int Next()
        {
            lock (_lock)
            {
                return base.Next();
            }
        }

        /// <inheritdoc/>
        public override void NextBytes(byte[] buffer)
        {
            lock (_lock)
            {
                base.NextBytes(buffer);
            }
        }
    }
}
