using RayTracer.Primitives;
using RayTracer.SampleGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Cameras
{
    /// <summary>
    /// A lens-based camera. See Chapter 10 "Depth of Field".
    /// </summary>
    public class ThinLens : Camera
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eye"></param>
        /// <param name="lookAt"></param>
        /// <param name="up"></param>
        /// <param name="lensRadius"></param>
        /// <param name="viewPlaneDistance"></param>
        /// <param name="focalPlaneDistance"></param>
        /// <param name="zoom"></param>
        /// <param name="blur"></param>
        /// <param name="exposureTime"></param>
        /// <exception cref="ArgumentException"></exception>
        public ThinLens(Point3D eye, Point3D lookAt, Vector3D up, double lensRadius,
            double viewPlaneDistance, double focalPlaneDistance, double zoom,
            UnitDiskMappedSampleGenerator blur, double exposureTime = DefaultExposureTime) 
            : base(eye, lookAt, up, exposureTime)
        {
            if(viewPlaneDistance == 0)
            {
                throw new ArgumentException($"{ nameof(viewPlaneDistance) } cannot be zero", nameof(viewPlaneDistance));
            }
            if (zoom == 0)
            {
                throw new ArgumentException($"{ nameof(zoom) } cannot be zero", nameof(zoom));
            }

            LensRadius = lensRadius;
            ViewPlaneDistance = viewPlaneDistance;
            FocalPlaneDistance = focalPlaneDistance;
            Zoom = zoom;
            Blur = blur;
        }

        public double LensRadius { get; }
        public double ViewPlaneDistance { get; }
        public double FocalPlaneDistance { get; }
        public double Zoom { get; }
        public UnitDiskMappedSampleGenerator Blur { get; }

        public override RGBColor[,] Render(World world)
        {
            if(world.ViewPlane.AntiAliasing.Samples.Count() != Blur.Samples.Count())
            {
                throw new InvalidOperationException($"Sample count of { nameof(ViewPlane) }.{ nameof(ViewPlane.AntiAliasing) } and { nameof(Blur) } must be equal");
            }

            RGBColor[,] result = new RGBColor[world.ViewPlane.VerticalResolution, world.ViewPlane.HorizontalResolution];
            double pixelSize = world.ViewPlane.PixelSize / Zoom;

            Parallel.For(0, world.ViewPlane.VerticalResolution, row => // up
            {
                for (int column = 0; column < world.ViewPlane.HorizontalResolution; column++) // left to right
                {
                    RGBColor pixelColor = RGBColors.Black;
                    foreach ((Point2D viewPlaneSample, Point2D cameraSample) in world.ViewPlane.AntiAliasing.GetSamples().Zip(Blur.GetSamples()))
                    {
                        Point2D pixelPoint = new Point2D(
                            pixelSize * (column - world.ViewPlane.HorizontalResolution / 2 + viewPlaneSample.X),
                            pixelSize * (row - world.ViewPlane.VerticalResolution / 2 + viewPlaneSample.Y)
                        );

                        Point2D lensPoint = cameraSample * LensRadius;

                        Ray ray = new Ray(
                            Eye + lensPoint.X * ViewUAxis + lensPoint.Y * ViewVAxis, 
                            GetRayDirection(pixelPoint, lensPoint)
                        );

                        pixelColor += world.Tracer.TraceRay(world.Scene, ray, 0);
                    }
                    pixelColor /= world.ViewPlane.AntiAliasing.SamplesPerSet;
                    pixelColor *= ExposureTime;
                    result[row, column] = pixelColor;
                }
            });

            return result;
        }

        /// <summary>
        /// Create a <see cref="Vector3D"/> representing the ray's direction.
        /// </summary>
        /// <param name="x">
        /// The ray's starting x coordinate in the view plane.
        /// </param>
        /// <param name="y">
        /// The ray's starting y coordinate in the view plane.
        /// </param>
        /// <returns>
        /// </returns>
        protected internal Vector3D GetRayDirection(Point2D pixelPoint, Point2D lensPoint)
        {
            Point2D p = new Point2D(
                pixelPoint.X * FocalPlaneDistance / ViewPlaneDistance,
                pixelPoint.Y * FocalPlaneDistance / ViewPlaneDistance);

            Vector3D direction = (p.X - lensPoint.X) * ViewUAxis
                 + (p.Y - lensPoint.Y) * ViewVAxis
                 - FocalPlaneDistance * ViewWAxis;

            return direction.Normalize();
        }
    }
}
