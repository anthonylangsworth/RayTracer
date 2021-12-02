using RayTracer.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.SampleGenerators
{
    /// <summary>
    /// Map a point on a unit square to unit square. Yes, this is effectively a NOOP.
    /// </summary>
    internal class UnitSquareSampleMapper : ISampleMapper<Point2D>
    {
        /// <inheritdoc/>
        public Point2D Map(Point2D point)
        {
            return point;
        }
    }
}
