using static System.Math;
using static Eflatun.GeoUnity.Calculation.Const;
using static Eflatun.GeoUnity.MathExtended;

namespace Eflatun.GeoUnity.Calculation
{
    public static class MathUtils
    {
        /// <summary>
        /// Calculates a new coordinates, that originates from <paramref name="origin"/>,
        /// and walks <paramref name="distance"/> meters in <paramref name="bearing"/> bearing.
        /// Result is in degrees.
        /// Altitude is untouched (same as origin).
        /// </summary>
        /// <param name="origin">Starting point.</param>
        /// <param name="distance">Distance to walk (in meters).</param>
        /// <param name="bearing">Direction in which we are walking (in degrees).</param>
        /// <returns>A new coordinate (in degrees).</returns>
        public static Coordinates CalculateDestination(Coordinates origin, double distance, double bearing)
        {
            bearing = NormalizeDegrees(bearing);
            bearing *= Deg2Rad;
            origin = origin.ToRadians();

            var sinLat = Sin(origin.Latitude);
            var cosLat = Cos(origin.Latitude);
            var theta = distance / EarthRadius;

            var resultLatitude = Asin(sinLat * Cos(theta) + cosLat * Sin(theta) * Cos(bearing));
            var resultLongitude = origin.Longitude +
                                  Atan2(Sin(bearing) * Sin(theta) * cosLat,
                                      Cos(theta) - sinLat * Sin(resultLatitude));

            // normalize -PI -> +PI radians
            resultLongitude = NormalizeRadians(resultLongitude);

            var result = new Coordinates(resultLatitude, resultLongitude, origin.Altitude, AngleType.Radians);
            return result.ToDegrees();
        }

        /// <summary>
        /// Calculates the distance between <paramref name="start"/> and <paramref name="end"/> using haversine.
        /// Result is in meters.
        /// </summary>
        /// <param name="start">Start point.</param>
        /// <param name="end">End point.</param>
        /// <returns>The distance in meters between <paramref name="start"/> and <paramref name="end"/>.</returns>
        public static double DistanceBetween(Coordinates start, Coordinates end)
        {
            var startGC = start.ToGeoCoordinate();
            var endGC = end.ToGeoCoordinate();

            return startGC.GetDistanceTo(endGC);
        }

        /// <summary>
        /// Returns the bearing between <paramref name="start"/> and <paramref name="end"/>.
        /// Result is in degrees.
        /// </summary>
        /// <param name="start">Start point.</param>
        /// <param name="end">End point.</param>
        /// <returns>The bearing in degrees between <paramref name="start"/> and <paramref name="end"/>.</returns>
        public static double BearingBetween(Coordinates start, Coordinates end)
        {
            start = start.ToRadians();
            end = end.ToRadians();

            var y = Sin(end.Longitude - start.Longitude) * Cos(end.Latitude);
            var x = Cos(start.Latitude) * Sin(end.Latitude) -
                    Sin(start.Latitude) * Cos(end.Latitude) * Cos(end.Longitude - start.Longitude);
            var result = Atan2(y, x) * Rad2Deg;

            return NormalizeDegrees(result);
        }
    }
}
