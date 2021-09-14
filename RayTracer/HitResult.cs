using System;
using System.Collections.Generic;
using System.Text;
using RayTracer.Objects;

namespace RayTracer
{
    /// <summary>
    /// The result of a <see cref="GeometricObject.Hit"/> method. Use <see cref="Hit"/> or <see cref="Miss"/>
    /// to indicate the result.
    /// </summary>
    public abstract class HitResult
    {
        // No members
    }

    /// <summary>
    /// When a ray intersects or hits the <see cref="GeometricObject"/>.
    /// </summary>
    public class Hit: HitResult
    {
        /// <summary>
        /// Create a <see cref="HitResult"/>.
        /// </summary>
        /// <param name="distance">
        /// The distance to the ray origin of the closest intersection to the 
        /// <paramref name="ray"/>'s origin. This must be positive.
        /// </param>
        /// <param name="shadeRecord">
        /// A <see cref="ShadeRecord"/> with details on how to colour or shade that point.
        /// </param>
        /// <exception cref="ArgumentException">
        /// <paramref name="distance"/> must be positive.
        /// </exception>
        public Hit(double distance, ShadeRecord shadeRecord)
        {
            if (distance < 0)
            {
                throw new ArgumentException($"{ nameof(distance) } must be positive for a hit", nameof(distance));
            }

            Distance = distance;
            ShadeRecord = shadeRecord;
        }

        /// <summary>
        /// The distance along the ray the hit occurs.
        /// </summary>
        public double Distance { get; }

        /// <summary>
        /// The <see cref="ShadeRecord"/> used to generate the colour of the pixel.
        /// </summary>
        public ShadeRecord ShadeRecord { get; }
    }

    /// <summary>
    /// When a ray does not intersect or misses the <see cref="GeometricObject"/>.
    /// </summary>
    public class Miss: HitResult
    {
        // No members
    }
}
