using NUnit.Framework;
using RayTracer.Primitives;
using RayTracer.SampleGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Test.SampleGenerators
{
    internal class TestUnitDiskMappedSampleGenerator
    {
        [Test]
        [Repeat(10)]
        public void Map()
        {
            IEnumerable<Point2D> mappedPoints = new UnitDiskMappedSampleGenerator(SampleAlgorithms.Hammersley, new Random(), 64, 1).GetSamples();
            Assert.That(mappedPoints.Select(point => Math.Sqrt(point.X * point.X + point.Y * point.Y)), Is.All.LessThanOrEqualTo(1));
        }
    }
}
