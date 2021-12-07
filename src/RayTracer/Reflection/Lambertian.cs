using RayTracer.Primitives;
using RayTracer.SampleGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Reflection
{
    public class Lambertian : BRDF
    {
        /// <summary>
        /// Create a new <see cref="Lambertian"/> BRDF.
        /// </summary>
        /// <param name="sampleGenerator">
        /// 
        /// </param>
        /// <param name="diffuseCoefficient">
        /// 
        /// </param>
        /// <param name="diffuseColor">
        /// </param>
        /// <exception cref="ArgumentException">
        /// 
        /// </exception>
        public Lambertian(SampleGenerator<Point2D> sampleGenerator, double diffuseCoefficient, RGBColor diffuseColor)
            : base(sampleGenerator)
        {
            if(diffuseCoefficient < 0.0 || diffuseCoefficient > 1.0)
            {
                throw new ArgumentException($"{ nameof(diffuseCoefficient) } must be between 0 and 1 inclusive", nameof(diffuseCoefficient));
            }

            DiffuseCoefficient = diffuseCoefficient;
            DiffuseColor = diffuseColor;
        }

        /// <summary>
        /// kd in the book. The about of light reflected.
        /// </summary>
        public double DiffuseCoefficient { get; }

        /// <summary>
        /// cd in the book. Filters the colour reflected.
        /// </summary>
        public RGBColor DiffuseColor { get; }

        public override RGBColor F(ShadeRecord shadeRecord, Vector3D incoming, Vector3D outgoing)
        {
            return DiffuseCoefficient * DiffuseColor / Math.PI;
        }

        public override RGBColor Rho(ShadeRecord shadeRecord, Vector3D outgoing)
        {
            return DiffuseCoefficient * DiffuseColor;
        }

        public override RGBColor SampleF(ShadeRecord shadeRecord, Vector3D incoming, Vector3D outgoing)
        {
            throw new NotImplementedException();
        }
    }
}
