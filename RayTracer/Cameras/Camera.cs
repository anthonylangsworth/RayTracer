using RayTracer.Primitives;

namespace RayTracer.Cameras
{
    public abstract class Camera
    {
        public const double DefaultExposureTime = 1;

        /// <summary>
        /// Create a new <see cref="Camera"/>.
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
        /// <param name="exposureTime">
        /// The time taken for an exposure, with 1.0 being a normal exposure and the default.
        /// </param>
        protected Camera(Point3D eye, Point3D lookat, Vector3D up, double exposureTime = DefaultExposureTime)
        {
            Eye = eye;
            Lookat = lookat;
            ExposureTime = exposureTime;
            Up = up.Normalize();

            ViewWAxis = (eye - lookat).Normalize();
            ViewUAxis = up.Cross(ViewWAxis).Normalize();
            ViewVAxis = ViewWAxis.Cross(ViewUAxis).Normalize();
        }

        /// <summary>
        /// Render the <see cref="Scene"/> to the <see cref="ViewPlane"/>.
        /// </summary>
        /// <param name="scene">
        /// The <see cref="Scene"/> to render.
        /// </param>
        /// <param name="viewPlane">
        /// The <see cref="ViewPlane"/> to render to.
        /// </param>
        /// <returns>
        /// A 2D array of <see cref="RGBColor"/>s representing pixels. The rows are returned in 
        /// bottom-up order, not top-down.
        /// </returns>
        public abstract RGBColor[,] Render(World world);

        public Point3D Eye { get; }
        public Point3D Lookat { get; }
        public double ExposureTime { get; }
        public Vector3D Up { get; }
        public Vector3D ViewUAxis { get; }
        public Vector3D ViewVAxis { get; }
        public Vector3D ViewWAxis { get; }
    }
}