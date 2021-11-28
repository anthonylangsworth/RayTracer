using RayTracer.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.SampleGenerators
{
    /// <summary>
    /// Map a point on a unit square to a point on a unit disk using Shirley's mapping.
    /// </summary>
    internal class UnitDiskSampleMapper : ISampleMapper
    {
        /// <inheritdoc/>
        public Point2D Map(Point2D point)
        {
            double x;
            double y;
            double r;
            double phi;

            // Map from [0, 1] to [-1, 1]
            x = 2 * point.X - 1;
            y = 2 * point.Y - 1;

            if (x > -y)
            {
                if (x > y)
                {
                    r = x;
                    phi = y / x;
                }
                else
                {
                    r = y;
                    phi = 2 - x / y;
                }
            }
            else
            {
                if (x < y)
                {
                    r = -x;
                    phi = 4 + y / x;
                }
                else
                {
                    r = -y;
                    if (y != 0)
                    {
                        phi = 6 - x / y;
                    }
                    else
                    {
                        phi = 0;
                    }
                }
            }

            phi *= Math.PI / 4;

            return new Point2D(r * Math.Cos(phi), r * Math.Sin(phi));
        }
    }
}
