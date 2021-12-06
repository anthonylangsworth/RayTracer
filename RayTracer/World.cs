using RayTracer.Cameras;
using RayTracer.Objects;
using RayTracer.Primitives;
using RayTracer.SampleGenerators;
using RayTracer.Tracers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class World
    {
        public World(string name, Camera camera, Scene scene, ViewPlane viewPlane)
        {
            Tracer = new Tracer();
            Camera = camera;
            Name = name;
            Scene = scene;
            ViewPlane = viewPlane;
        }

        public Tracer Tracer { get; }
        public Camera Camera { get; }
        public string Name { get; }
        public Scene Scene { get; }
        public ViewPlane ViewPlane { get; }

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
        public RGBColor[,] Render()
        {
            return Camera.Render(this);
        }
    }
}
