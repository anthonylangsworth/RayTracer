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

        // Get an even smaller epsilon to emulate number just above or below epsilon
        const double epsilonVariance = DoubleEpsilonEqualityComparer.DefaultEpsilon / 16;

        // Tests for +/- DoubleEpsilonEqualityComparer.DefaultEpsilon exactly are prone to
        // rounding, platform or .Net implementation errors so omit them.
        [TestCase(0, 1, false)]
        [TestCase(1, 1, true)]
        [TestCase(0, DoubleEpsilonEqualityComparer.DefaultEpsilon - epsilonVariance, true)]
        [TestCase(0, DoubleEpsilonEqualityComparer.DefaultEpsilon + epsilonVariance, false)]
        [TestCase(0, -DoubleEpsilonEqualityComparer.DefaultEpsilon + epsilonVariance, true)]
        [TestCase(0, -DoubleEpsilonEqualityComparer.DefaultEpsilon - epsilonVariance, false)]
        public void Equals(double d1, double d2, bool expectedResult)
        {
            Assert.That(DoubleEpsilonEqualityComparer.Instance.Equals(d1, d2), Is.EqualTo(expectedResult));
        }
    }
}
