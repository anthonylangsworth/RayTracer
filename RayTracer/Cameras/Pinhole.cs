using RayTracer.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Cameras
{
    /// <summary>
    /// A Pinhole or default camera.
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
        /// The zoom level. Defaults to <see cref="DefaultZoom"/>.
        /// </param>
        /// <param name="exposureTime">
        /// The time taken for an exposure. Defaults to <see cref="DefaultExposureTime"/>.
        /// </param>
        public Pinhole(Point3D eye, Point3D lookat, Vector3D up, double viewPlaneDistance, 
            double zoom = DefaultZoom, double exposureTime = DefaultExposureTime) 
            : base(eye, lookat, up, exposureTime)
        {
            ViewPlaneDistance = viewPlaneDistance;
            Zoom = zoom;
        }

        /// <inheritdoc/>
        public override RGBColor[,] Render(World world)
        {
            // int depth = 0;
            RGBColor[,] result = new RGBColor[world.ViewPlane.VerticalResolution, world.ViewPlane.HorizontalResolution];

            Parallel.For(0, world.ViewPlane.VerticalResolution, row => // up
            {
                // Declare here because they are thread local
                double x, y;
                Ray ray;
                for (int column = 0; column < world.ViewPlane.HorizontalResolution; column++) // left to right
                {
                    RGBColor pixelColor = RGBColor.Black;
                    foreach (Point2D samplePoint in world.ViewPlane.SampleGenerator.GetSamples())
                    {
                        x = world.ViewPlane.PixelSize * (column - 0.5 * world.ViewPlane.HorizontalResolution + samplePoint.X);
                        y = world.ViewPlane.PixelSize * (row - 0.5 * world.ViewPlane.VerticalResolution + samplePoint.Y);
                        ray = new Ray(Eye, GetRayDirection(x, y, this));
                        pixelColor += world.Tracer.TraceRay(world.Scene, ray);
                    }
                    pixelColor /= world.ViewPlane.SampleGenerator.SamplesPerSet;
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
        /// <param name="camera">
        /// The <see cref="Pinhole"/> camera used.
        /// </param>
        /// <returns>
        /// </returns>
        public static Vector3D GetRayDirection(double x, double y, Pinhole camera)
        {
            Vector3D direction;

            if (camera.Eye.X == camera.LookAt.X
                && camera.Eye.Z == camera.LookAt.Z)
            {
                if(camera.Eye.Y < camera.LookAt.Y)
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

                direction = x * camera.ViewUAxis
                    + y * camera.ViewVAxis
                    - camera.ViewPlaneDistance * camera.ViewWAxis;
            }

            return direction.Normalize();
        }

        public double ViewPlaneDistance { get; }
        public double Zoom { get; }
    }
}
