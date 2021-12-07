using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.SampleGenerators
{
    /// <summary>
    /// Semantically shorter access to <see cref="ISampleAlgorithm"/> types. Also more 
    /// efficient due to them effectively being singletons.
    /// </summary>
    public static class SampleAlgorithms
    {
        public static ISampleAlgorithm Hammersley = new HammersleySampleAlgorithm();
        public static ISampleAlgorithm Jittered = new JitteredSampleAlgorithm();
        public static ISampleAlgorithm MultiJittered = new MultiJitteredSampleAlgorithm();
        public static ISampleAlgorithm NRooks = new NRooksSampleAlgorithm();
        public static ISampleAlgorithm Regular = new RegularSampleAlgorithm();
    }
}
