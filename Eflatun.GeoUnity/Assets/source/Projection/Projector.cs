using System;
using System.Device.Location;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;
using ICoordinateTransformation = ProjNet.CoordinateSystems.Transformations.ICoordinateTransformation;

namespace starikcetin.Eflatun.GeoUnity.Projection
{
    /// <summary>
    /// Performs forward and inverse projections between two coordinate systems.
    /// </summary>
    public class Projector
    {
        private readonly ICoordinateTransformation _forwardTransform;
        private readonly ICoordinateTransformation _inverseTransform;

        public Projector(CoordinateSystem fromCs, CoordinateSystem toCs)
        {
            CoordinateTransformationFactory ctfac = new CoordinateTransformationFactory();
            _forwardTransform = ctfac.CreateFromCoordinateSystems(fromCs, toCs);
            _inverseTransform = ctfac.CreateFromCoordinateSystems(toCs, fromCs);
        }

        private double[] _Project(double[] pointInWgs84)
        {
            return _forwardTransform.MathTransform.Transform(pointInWgs84);
        }

        private double[] _InverseProject(double[] pointInWebMercator)
        {
            return _inverseTransform.MathTransform.Transform(pointInWebMercator);
        }

        public GeoCoordinate Project(GeoCoordinate pointInWgs84)
        {
            return _Transform(pointInWgs84, _Project);
        }

        public GeoCoordinate InverseProject(GeoCoordinate pointInWebMercator)
        {
            return _Transform(pointInWebMercator, _InverseProject);
        }

        private static GeoCoordinate _Transform(GeoCoordinate from, Func<double[], double[]> transformFunction)
        {
            double[] fromArr = {from.Latitude, from.Longitude, from.Altitude};
            double[] toArr = transformFunction.Invoke(fromArr);

            double lat = toArr[0];
            double lon = toArr[1];

            if (toArr.Length == 3)
            {
                double alt = toArr[2];
                return new GeoCoordinate(lat, lon, alt);
            }

            return new GeoCoordinate(lat, lon);
        }
    }
}
