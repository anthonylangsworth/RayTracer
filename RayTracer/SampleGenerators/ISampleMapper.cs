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
    public interface ISampleMapper<T>
        where T : class
    {
        /// <summary>
        /// Map a point on a unit square to another distribution.
        /// </summary>
        /// <returns>
        /// The mapped point.
        /// </returns>
        public T Map(Point2D point);
    }
}
