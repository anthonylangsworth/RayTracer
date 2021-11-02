using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class View
    {
        public View(Scene scene, ViewPlane viewPlane, string name)
        {
            Scene = scene;
            ViewPlane = viewPlane; 
            Name = name;
        }

        public Scene Scene { get; }
        public ViewPlane ViewPlane { get; }
        public string Name { get; }
    }
}
