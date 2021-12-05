using System;
using System.Collections.Generic;
using System.Text;
using RayTracer.Cameras;
using RayTracer.Objects;
using RayTracer.Primitives;

namespace RayTracer
{
    public class Scene
    {
        public Scene(IEnumerable<GeometricObject> objects, IEnumerable<LightSource> lightSources, RGBColor backgroundColor)
        {
            BackgroundColor = backgroundColor;
            Objects = new HashSet<GeometricObject>(objects);
            LightSources = new HashSet<LightSource>(lightSources);
        }

        public RGBColor BackgroundColor { get; }
        public ISet<GeometricObject> Objects { get; }
        public ISet<LightSource> LightSources { get; }

        public static Scene BuildSingleSphere()
        {
            return new Scene(
                new[]
                {
                    new Sphere(new Point3D(0, 0, 0), new Material(RGBColor.BrightRed), 85)
                },
                new LightSource[0],
                RGBColor.Black);
        }

        public static Scene BuildTwoSpheresAndPlane()
        {
            return new Scene(
                new GeometricObject[]
                {
                    new Sphere(new Point3D(0, -25, 0), new Material(RGBColor.BrightRed), 80),
                    new Sphere(new Point3D(0, 30, 0), new Material(RGBColor.Yellow), 60),
                    new Plane(new Point3D(0, 0, 0), new Material(RGBColor.DarkGreen), new Vector3D(0, 1, 1))
                },
                new LightSource[0],
                RGBColor.Black);
        }

        public static Scene BuildThreeSpheresAbovePlane()
        {
            return new Scene(
                new GeometricObject[]
                {
                    new Sphere(new Point3D(30, 30, 30), new Material(RGBColor.BrightRed), 15),
                    new Sphere(new Point3D(0, 30, 0), new Material(RGBColor.Yellow), 15),
                    new Sphere(new Point3D(-30, 30, -30), new Material(RGBColor.BrightBlue), 15),
                    new Plane(new Point3D(0, 0, 0), new Material(RGBColor.DarkGreen), new Vector3D(0, 1, 0))
                },
                new LightSource[0],
                RGBColor.Black);
        }
    }
}
