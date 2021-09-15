using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Test
{
    internal class TestDoubleEpsilonEqualityComparer
    {
        public void TestCtor()
        {
            double epsilon = 1;
            DoubleEpsilonEqualityComparer comparer = new DoubleEpsilonEqualityComparer(epsilon);
            Assert.That(comparer.Epsilon, Is.EqualTo(epsilon));
        }

        [TestCase(-double.Epsilon, true)]
        [TestCase(0, false)]
        [TestCase(double.Epsilon, false)]
        [TestCase(DoubleEpsilonEqualityComparer.DefaultEpsilon, false)]
        public void TestCtor(double epsilon, bool expectedException)
        {
            if (expectedException)
            {
                // Debugger will stop here otherwise
                // Assert.Throws<ArgumentException>(() => new DoubleEpsilonEqualityComparer(epsilon));
            }
            else
            {
                Assert.DoesNotThrow(() => new DoubleEpsilonEqualityComparer(epsilon));
            }
        }

        [TestCase(1, 2, false)]
        [TestCase(1, 1, true)]
        [TestCase(1, 1 + DoubleEpsilonEqualityComparer.DefaultEpsilon - double.Epsilon, true)]
        [TestCase(1, 1 + DoubleEpsilonEqualityComparer.DefaultEpsilon, false)]
        [TestCase(1, 1 - DoubleEpsilonEqualityComparer.DefaultEpsilon + double.Epsilon, true)]
        [TestCase(1, 1 - DoubleEpsilonEqualityComparer.DefaultEpsilon, false)]
        public void Equals(double d1, double d2, bool expectedResult)
        {
            Assert.That(DoubleEpsilonEqualityComparer.Instance.Equals(d1, d2), Is.EqualTo(expectedResult));
        }
    }
}
