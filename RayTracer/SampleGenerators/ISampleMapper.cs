using RayTracer.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.SampleGenerators
{
    /// <summary>
    /// Map a point on a unit square to another distribution.
    /// </summary>
    public interface ISampleMapper
    {
        /// <summary>
        /// Map a point on a unit square to another distribution.
        /// </summary>
        /// <returns>
        /// The mapped point.
        /// </returns>
        public Point2D Map(Point2D point);
    }
}
