using RayTracer.Primitives;

namespace RayTracer
{
    public class ShadeRecord
    {
        public ShadeRecord(Vector3D normal, Point3D localHitpoint)
        {
            Normal = normal;
            LocalHitpoint = localHitpoint;
        }

        public Vector3D Normal { get; }
        public Point3D LocalHitpoint { get; }
    }
}