using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RayTracer;

namespace RayTracer.Tracers
{
    public class Tracer
    {
        public RGBColor TraceRay(Scene scene, Ray ray)
        {
            RGBColor result;
            Hit? hit = scene.Objects
                            .Select(geometricObject => geometricObject.Hit(ray))
                            .Where(hitResult => hitResult is Hit)
                            .Cast<Hit>()
                            .OrderBy(hit => hit.Distance)
                            .FirstOrDefault();

            if (hit != null)
            {
                // hit.ShadeRecord.
                result = new RGBColor(1.0, 0, 0);
            }
            else
            {
                result = scene.BackgroundColor;
            }

            return result;
        }
    }
}
