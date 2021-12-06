using RayTracer.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.SampleGenerators
{
    public class HemisphereMappedSampleGenerator: SampleGenerator<Point3D>
    { 
        public HemisphereMappedSampleGenerator(ISampleAlgorithm algorithm, Random random, int samplesPerSet, int sampleSets, double e) 
            : base(algorithm, random, samplesPerSet, sampleSets)
        {
            E = e;
        }

        public double E { get; }

        /// <inheritdoc/>
        protected internal override Point3D Map(Point2D point)
        {
            double cosPhi = Math.Cos(2 * Math.PI * point.X);
            double sinPhi = Math.Sin(2 * Math.PI * point.X);
            double cosTheta = Math.Pow(1 - point.Y, 1 / (E + 1));
            double sinTheta = Math.Sqrt(1 - cosTheta * cosTheta);
            double pu = sinTheta * cosPhi;
            double pv = sinTheta * sinPhi;
            double pw = cosTheta;

            return new Point3D(pu, pv, pw);
        }
    }
}
