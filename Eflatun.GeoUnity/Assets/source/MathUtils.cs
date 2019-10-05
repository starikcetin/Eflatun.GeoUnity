using UnityEngine;

namespace starikcetin.Eflatun.GeoUnity
{
    public static class MathUtils
    {
        /// <summary>
        /// Calculates a new coordinates, that originates from <paramref name="origin"/>,
        /// and walks <paramref name="distance"/> meters in <paramref name="bearing"/> bearing.
        /// Result is in degrees.
        /// </summary>
        /// <param name="origin">Starting point.</param>
        /// <param name="distance">Distance to walk (in meters).</param>
        /// <param name="bearing">Direction in which we are walking (in degrees).</param>
        /// <returns>A new coordinate (in degrees).</returns>
        public static Coordinates CalculateDestination(Coordinates origin, float distance, float bearing)
        {
            bearing *= Mathf.Deg2Rad;
            origin = origin.ToRadians();

            var sinLat = Mathf.Sin(origin.Latitude);
            var cosLat = Mathf.Cos(origin.Latitude);

            var theta = distance / Const.EarthRadius;
            var sinBearing = Mathf.Sin(bearing);
            var cosBearing = Mathf.Cos(bearing);
            var sinTheta = Mathf.Sin(theta);
            var cosTheta = Mathf.Cos(theta);

            var resultLatitude = Mathf.Asin(sinLat * cosTheta + cosLat * sinTheta * cosBearing);
            var resultLongitude = origin.Longitude +
                                  Mathf.Atan2(sinBearing * sinTheta * cosLat,
                                      cosTheta - sinLat * Mathf.Sin(resultLatitude)
                                  );

            // normalize -PI -> +PI radians
            resultLongitude = ((resultLongitude + Const.ThreePi) % Const.TwoPi) - Mathf.PI;

            var result = new Coordinates(resultLatitude, resultLongitude, AngleType.Radians);
            return result.ToDegrees();
        }

        /// <summary>
        /// Calculates the distance between <paramref name="start"/> and <paramref name="end"/> using haversine.
        /// Result is in meters.
        /// </summary>
        /// <param name="start">Start point.</param>
        /// <param name="end">End point.</param>
        /// <returns>The distance in meters between <paramref name="start"/> and <paramref name="end"/>.</returns>
        public static float DistanceBetween(Coordinates start, Coordinates end)
        {
            start = start.ToRadians();
            end = end.ToRadians();

            var delta = Delta(start, end);

            var a = delta.Latitude * delta.Latitude +
                    delta.Longitude * delta.Longitude * Mathf.Cos(start.Latitude) * Mathf.Cos(end.Latitude);

            return Const.EarthRadius * 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        }

        /// <summary>
        /// Returns the delta coordinates between <paramref name="start"/> and <paramref name="end"/>.
        /// Result is in degrees.
        /// </summary>
        /// <param name="start">Start point.</param>
        /// <param name="end">End point.</param>
        /// <returns>The delta coordinates (in degrees) between <paramref name="start"/> and <paramref name="end"/>.</returns>
        public static Coordinates Delta(Coordinates start, Coordinates end)
        {
            start = start.ToRadians();
            end = end.ToRadians();

            var result = new Coordinates(
                Mathf.Sin((end.Latitude - start.Latitude) / 2),
                (end.Longitude - start.Longitude) / 2,
                AngleType.Radians);

            return result.ToDegrees();
        }
    }
}
