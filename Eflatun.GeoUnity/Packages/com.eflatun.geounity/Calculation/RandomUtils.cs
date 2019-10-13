using System;
using Random = UnityEngine.Random;

namespace starikcetin.Eflatun.GeoUnity.Calculation
{
    public static class RandomUtils
    {
        /// <summary>
        /// Generates a random point ON THE BORDER of the circle whose center is <paramref name="center"/> and radius is <paramref name="radius"/>.
        /// Result is in degrees.
        /// </summary>
        /// <param name="center">The center of the circle ON which the points wil be generated.</param>
        /// <param name="radius">The radius of the circle ON which the points wil be generated.</param>
        /// <returns>A random point (in degrees) ON THE BORDER of the circle whose center is <paramref name="center"/> and radius is <paramref name="radius"/>.</returns>
        public static Coordinates OnCircle(Coordinates center, double radius)
        {
            center = center.ToRadians();
            var randomBearing = Random.Range(0f, 1f) * Const.TwoPi * Const.Rad2Deg;
            var result = MathUtils.CalculateDestination(center, radius, randomBearing);
            return result.ToDegrees();
        }

        /// <summary>
        /// Generates a random point INSIDE of the circle whose center is <paramref name="center"/> and radius is <paramref name="radius"/>.
        /// Result is in degrees.
        /// </summary>
        /// <param name="center">The center of the circle IN which the points wil be generated.</param>
        /// <param name="radius">The radius of the circle IN which the points wil be generated.</param>
        /// <returns>A random point (in degrees)  INSIDE of the circle whose center is <paramref name="center"/> and radius is <paramref name="radius"/>.</returns>
        public static Coordinates InCircle(Coordinates center, double radius)
        {
            center = center.ToRadians();
            var rnd = Random.Range(0f, 1f);
            // use square root of random number to avoid high density at the center
            var randomDist = Math.Sqrt(rnd) * radius;
            var result = OnCircle(center, randomDist);
            return result.ToDegrees();
        }
    }
}
