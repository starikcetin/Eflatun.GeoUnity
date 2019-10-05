using System;
using GeoCoordinatePortable;
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
        public static Coordinates CalculateDestination(Coordinates origin, double distance, double bearing)
        {
            bearing *= Const.Deg2Rad;
            origin = origin.ToRadians();

            var sinLat = Math.Sin(origin.Latitude);
            var cosLat = Math.Cos(origin.Latitude);

            var theta = distance / Const.EarthRadius;
            var sinBearing = Math.Sin(bearing);
            var cosBearing = Math.Cos(bearing);
            var sinTheta = Math.Sin(theta);
            var cosTheta = Math.Cos(theta);

            var resultLatitude = Math.Asin(sinLat * cosTheta + cosLat * sinTheta * cosBearing);
            var resultLongitude = origin.Longitude +
                                  Math.Atan2(sinBearing * sinTheta * cosLat,
                                      cosTheta - sinLat * Math.Sin(resultLatitude)
                                  );

            // normalize -PI -> +PI radians
            resultLongitude = ((resultLongitude + Const.ThreePi) % Const.TwoPi) - Math.PI;

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
        public static double DistanceBetween(Coordinates start, Coordinates end)
        {
            var startGC = (GeoCoordinate) start;
            var endGC = (GeoCoordinate) end;

            return startGC.GetDistanceTo(endGC);
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
                Math.Sin((end.Latitude - start.Latitude) / 2),
                (end.Longitude - start.Longitude) / 2,
                AngleType.Radians);

            return result.ToDegrees();
        }

        public static double BearingBetween(Coordinates start, Coordinates end)
        {
            start = start.ToRadians();
            end = end.ToRadians();

            var y = Math.Sin(end.Longitude - start.Longitude) * Math.Cos(end.Latitude);
            var x = Math.Cos(start.Latitude) * Math.Sin(end.Latitude) -
                Math.Sin(start.Latitude) * Math.Cos(end.Latitude) * Math.Cos(end.Longitude - start.Longitude);
            var result = Math.Atan2(y, x) * Const.Rad2Deg;

            return NormalizeDegrees(result);
        }

        public static double NormalizeDegrees(double degrees)
        {
            while (degrees < 0)
            {
                degrees += 360;
            }

            return degrees % 360;
        }

        public static double NormalizeRadians(double radians)
        {
            while (radians < -Math.PI)
            {
                radians += Math.PI;
            }

            return radians % Math.PI;
        }

        public static bool Approx(double a, double b, double maxError, out double error)
        {
            error = Math.Abs(a - b);
            return error < maxError;
        }
    }
}
