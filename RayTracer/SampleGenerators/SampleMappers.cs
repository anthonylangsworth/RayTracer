﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.SampleGenerators
{
    /// <summary>
    /// Semantically shorter access to <see cref="ISampleMapper"/> types. Also more 
    /// efficient due to them effectively being singletons.
    /// </summary>
    public static class SampleMappers
    {
        public static ISampleMapper UnitSquare = new UnitSquareSampleMapper();
        public static ISampleMapper UnitDisk = new UnitDiskSampleMapper();
    }
}
