using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public static class RGBColors
    {
        public readonly static RGBColor Black = new RGBColor(0, 0, 0);
        public readonly static RGBColor White = new RGBColor(1, 1, 1);
        public readonly static RGBColor BrightRed = new RGBColor(1, 0, 0);
        public readonly static RGBColor BrightGreen = new RGBColor(0, 1, 0);
        public readonly static RGBColor BrightBlue = new RGBColor(0, 0, 1);
        public readonly static RGBColor DarkRed = new RGBColor(0.3, 0, 0);
        public readonly static RGBColor DarkGreen = new RGBColor(0, 0.3, 0);
        public readonly static RGBColor DarkBlue = new RGBColor(0, 0, 0.3);
        public readonly static RGBColor Magenta = new RGBColor(1, 0, 1);
        public readonly static RGBColor Yellow = new RGBColor(1, 1, 0);
        public readonly static RGBColor Cyan = new RGBColor(0, 1, 1);
    }
}
