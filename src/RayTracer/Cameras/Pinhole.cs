using RayTracer.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Cameras
{
    /// <summary>
    /// A pinhole camera. See Chapter 9 "A Practical Viewing System".
    /// </summary>
    public class Pinhole : Camera
    {
        public const double DefaultZoom = 1;

        /// <summary>
        /// Create a new <see cref="Pinhole"/> camera.
        /// </summary>
        /// <param name="eye">
        /// The camera source point.
        /// </param>
        /// <param name="lookat">
        /// Where the camera looks.
        /// </param>
        /// <param name="up">
        /// The orientation of the camera.
        /// </param>
        /// <param name="viewPlaneDistance">
        /// Project the scene on a plane this distance away from the camera.
        /// </param>
        /// <param name="zoom">
        /// The zoom level. Defaults to <see cref="DefaultZoom"/>. Cannot be zero.
        /// </param>
        /// <param name="exposureTime">
        /// The time taken for an exposure. Defaults to <see cref="DefaultExposureTime"/>.
        /// </param>
        /// <exception cref="ArgumentException">
        /// <paramref name="zoom"/> cannot be zero.
        /// </exception>
        public Pinhole(Point3D eye, Point3D lookat, Vector3D up, double viewPlaneDistance, 
            double zoom = DefaultZoom, double exposureTime = DefaultExposureTime) 
            : base(eye, lookat, up, exposureTime)
        {
            if(zoom == 0)
            {
                throw new ArgumentException($"{ nameof(zoom) } cannot be zero", nameof(zoom));
            }

            ViewPlaneDistance = viewPlaneDistance;
            Zoom = zoom;
        }

        /// <inheritdoc/>
        public override RGBColor[,] Render(World world)
        {
            RGBColor[,] result = new RGBColor[world.ViewPlane.VerticalResolution, world.ViewPlane.HorizontalResolution];
            double pixelSize = world.ViewPlane.PixelSize / Zoom;

            Parallel.For(0, world.ViewPlane.VerticalResolution, row => // up
            {
                for (int column = 0; column < world.ViewPlane.HorizontalResolution; column++) // left to right
                {
                    RGBColor pixelColor = RGBColors.Black;
                    foreach (Point2D samplePoint in world.ViewPlane.AntiAliasing.GetSamples())
                    {
                        Point2D point = new Point2D(
                            pixelSize * (column - 0.5 * world.ViewPlane.HorizontalResolution + samplePoint.X),
                            pixelSize * (row - 0.5 * world.ViewPlane.VerticalResolution + samplePoint.Y)
                        );

                        Ray ray = new Ray(Eye, GetRayDirection(point));

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
        protected internal Vector3D GetRayDirection(Point2D point)
        {
            Vector3D direction;

            if (Eye.X == LookAt.X
                && Eye.Z == LookAt.Z)
            {
                if(Eye.Y < LookAt.Y)
                {
                    direction = new Vector3D(0, 1, 0);
                }
                else
                {
                    direction = new Vector3D(0, -1, 0);
                }
            }
            else
            {
                direction = point.X * ViewUAxis
                    + point.Y * ViewVAxis
                    - ViewPlaneDistance * ViewWAxis;
            }

            return direction.Normalize();
        }

        public double ViewPlaneDistance { get; }
        public double Zoom { get; }
    }
}
