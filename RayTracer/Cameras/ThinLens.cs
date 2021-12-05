using RayTracer.Primitives;
using RayTracer.SampleGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Cameras
{
    internal class ThinLens : Camera
    {
        public ThinLens(Point3D eye, Point3D lookAt, Vector3D up, double lensRadius,
            double viewPlaneDistance, double focalPlaneDistance, double zoom,
            SampleGenerator<Point2D> sampleGenerator, double exposureTime = DefaultExposureTime) 
            : base(eye, lookAt, up, exposureTime)
        {
            LensRadius = lensRadius;
            ViewPlaneDistance = viewPlaneDistance;
            FocalPlaneDistance = focalPlaneDistance;
            Zoom = zoom;
            SampleGenerator = sampleGenerator;
        }

        public double LensRadius { get; }
        public double ViewPlaneDistance { get; }
        public double FocalPlaneDistance { get; }
        public double Zoom { get; }
        public SampleGenerator<Point2D> SampleGenerator { get; }

        public override RGBColor[,] Render(World world)
        {
            throw new NotImplementedException();
        }
    }
}
