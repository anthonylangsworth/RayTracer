using System;
using System.Collections.Generic;
using System.Text;
using RayTracer.Objects;

namespace RayTracer
{
    public class Scene
    {
        public Scene(Camera camera, IEnumerable<GeometricObject> objects, IEnumerable<LightSource> lightSources, RGBColor backgroundColor)
        {
            Camera = camera;
            BackgroundColor = backgroundColor;
            Objects = new HashSet<GeometricObject>(objects);
            LightSources = new HashSet<LightSource>(lightSources);
        }

        public Camera Camera { get; }
        public RGBColor BackgroundColor { get; }
        public ISet<GeometricObject> Objects { get; }
        public ISet<LightSource> LightSources { get; }
    }
}
