using RayTracer.Primitives;
using RayTracer.SampleGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Reflection
{
    /// <summary>
    /// A Bidrectional Reflectance Distribution Function (BRDF).
    /// </summary>
    public abstract record BidirectionalReflectanceDistributionFunction
    {
        protected BidirectionalReflectanceDistributionFunction(SampleGenerator<Point2D> sampleGenerator)
        {
            SampleGenerator = sampleGenerator;
        }

        public SampleGenerator<Point2D> SampleGenerator { get; }

        public abstract RGBColor F(ShadeRecord shadeRecord, Vector3D incoming, Vector3D outgoing);
        public abstract RGBColor SampleF(ShadeRecord shadeRecord, Vector3D incoming, Vector3D outgoing);
        public abstract RGBColor Rho(ShadeRecord shadeRecord, Vector3D outgoing);

    }
}
