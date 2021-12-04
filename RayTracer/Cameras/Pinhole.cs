﻿using RayTracer.Primitives;
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
    internal class Pinhole : Camera
    {
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
        /// The zoom level, which defaults to 1.
        /// </param>
        /// <param name="exposureTime">
        /// The time taken for an exposure, with 1 being a normal exposure. The default is 1.
        /// </param>
        public Pinhole(Point3D eye, Point3D lookat, Vector3D up, double viewPlaneDistance, double zoom = 1, double exposureTime = 1) 
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
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="pinholeCamera"></param>
        /// <returns></returns>
        public static Vector3D GetRayDirection(double x, double y, Pinhole pinholeCamera)
        {
            Vector3D direction = x * pinholeCamera.ViewUAxis 
                + y * pinholeCamera.ViewVAxis 
                - pinholeCamera.ViewPlaneDistance * pinholeCamera.ViewWAxis;
            direction.Normalize();
            return direction;
        }

        public double ViewPlaneDistance { get; }
        public double Zoom { get; }
    }
}