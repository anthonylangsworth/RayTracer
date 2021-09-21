using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Test
{
    internal class TestHelpers
    {
        public void Shuffle()
        {
            int[] a = new int[] { 1, 2, 3 };
            IEnumerable<int> b = a.Shuffle(new Random());
            Assert.That(a, Is.EquivalentTo(b));
        }
    }
}
