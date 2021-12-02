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
    internal class TestUnitDiskSampleMapper
    {
        [Test]
        [Repeat(10)]
        public void Map()
        {
            IEnumerable<Point2D> mappedPoints = SampleAlgorithms.Hammersley.GenerateSampleSet(new Random(), 64).Select(point => SampleMappers.UnitDisk.Map(point));
            Assert.That(mappedPoints.Select(point => Math.Sqrt(point.X * point.X + point.Y * point.Y)), Is.All.LessThanOrEqualTo(1));
        }
    }
}
