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
    }
}
