using System;
using doubleVector3 = System.DoubleNumerics.Vector3;

namespace Eflatun.GeoUnity.Projection
{
    public static class ProjectionUtils
    {
        public static double[] CoordinateToArray(Coordinates coordinates)
        {
            return new[] {coordinates.Latitude, coordinates.Longitude, coordinates.Altitude};
        }

        public static Coordinates ArrayToCoordinate(double[] arr)
        {
            return arr.Length == 3
                ? new Coordinates(arr[0], arr[1], arr[2])
                : new Coordinates(arr[0], arr[1]);
        }

        public static double[] VectorToArray(doubleVector3 vector, Vector3AltitudeAxis altitudeAxis)
        {
            switch (altitudeAxis)
            {
                case Vector3AltitudeAxis.Y:
                    return new[] {vector.X, vector.Z, vector.Y};
                case Vector3AltitudeAxis.Z:
                    return new[] {vector.X, vector.Y, vector.Z};
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static doubleVector3 ArrayToVector(double[] arr, Vector3AltitudeAxis altitudeAxis)
        {
            double x2d = arr[0];
            double y2d = arr[1];
            double alt = arr.Length == 3 ? arr[2] : 0;

            switch (altitudeAxis)
            {
                case Vector3AltitudeAxis.Y:
                    return new doubleVector3(x2d, alt, y2d);
                case Vector3AltitudeAxis.Z:
                    return new doubleVector3(x2d, y2d, alt);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
