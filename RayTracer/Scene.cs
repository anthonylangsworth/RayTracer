using System;
using System.Collections.Generic;
using System.Text;
using RayTracer.Objects;

namespace RayTracer
{
    public class Scene
    {
        public Scene(Camera camera, IEnumerable<GeometricObject> objects, IEnumerable<LightSource> lightSources)
        {
            if (camera == null) 
            {
                throw new ArgumentNullException(nameof(camera));
            }
            if (objects == null)
            {
                throw new ArgumentNullException(nameof(objects));
            }
            if (lightSources == null)
            {
                throw new ArgumentNullException(nameof(lightSources));
            }

            Camera = camera;
            Objects = new HashSet<GeometricObject>(objects);
            LightSources = new HashSet<LightSource>(lightSources);
        }

        public Camera Camera { get; }

        public HashSet<GeometricObject> Objects { get; }

        public HashSet<LightSource> LightSources { get; }
    }
}
