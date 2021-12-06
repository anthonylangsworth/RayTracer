using RayTracer.Primitives;
using RayTracer.SampleGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class ViewPlane
    {
        public ViewPlane(int horizontalResolution, int verticalResolution, double pixelSize, double gamma, UnitSquareMappedSampleGenerator sampler)
        {
            if(horizontalResolution <= 0)
            {
                throw new ArgumentException($"{ nameof(horizontalResolution) } must be positive", nameof(horizontalResolution));
            }
            if (horizontalResolution <= 0)
            {
                throw new ArgumentException($"{ nameof(verticalResolution) } must be positive", nameof(verticalResolution));
            }
            if (pixelSize <= 0)
            {
                throw new ArgumentException($"{ nameof(pixelSize) } must be positive", nameof(pixelSize));
            }
            if (gamma <= 0)
            {
                throw new ArgumentException($"{ nameof(gamma) } must be positive", nameof(gamma));
            }

            HorizontalResolution = horizontalResolution;
            VerticalResolution = verticalResolution;
            PixelSize = pixelSize;
            Gamma = gamma;
            SampleGenerator = sampler;
        }

        public int HorizontalResolution { get; }
        public int VerticalResolution { get; }
        public double PixelSize { get; }
        public double Gamma { get; }
        public SampleGenerator<Point2D> SampleGenerator { get; }
    }
}
