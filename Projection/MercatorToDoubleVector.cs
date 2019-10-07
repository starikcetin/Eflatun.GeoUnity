using System;
using System.Device.Location;
using System.DoubleNumerics;
using static System.Math;
using static starikcetin.Eflatun.GeoUnity.MathExtended;

namespace starikcetin.Eflatun.GeoUnity.Projection
{
    public class MercatorToDoubleVector
    {
        private readonly double _lon0;
        private readonly Vector3UpwardsAxis _altitudeAxis;

        public MercatorToDoubleVector(double lon0, Vector3UpwardsAxis altitudeAxis)
        {
            _lon0 = lon0;
            _altitudeAxis = altitudeAxis;
        }

        public Vector3 Project(GeoCoordinate geoCoordinate)
        {
            double lat = geoCoordinate.Latitude;
            double lon = geoCoordinate.Longitude;
            double alt = geoCoordinate.Altitude;

            double x2d = lon - _lon0;
            double y2d = Ln(Tan(lat) + Sec(lat));

            switch (_altitudeAxis)
            {
                case Vector3UpwardsAxis.Y:
                    return new Vector3(x2d, alt, y2d);
                case Vector3UpwardsAxis.Z:
                    return new Vector3(x2d, y2d, alt);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public GeoCoordinate InverseProject(Vector3 point)
        {
            double x2d = point.X;
            double y2d;
            double upwards;

            switch (_altitudeAxis)
            {
                case Vector3UpwardsAxis.Y:
                    y2d = point.Z;
                    upwards = point.Y;
                    break;
                case Vector3UpwardsAxis.Z:
                    y2d = point.Y;
                    upwards = point.Z;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            double lat = Gd(y2d);
            double lon = x2d + _lon0;

            return new GeoCoordinate(lat, lon, upwards);
        }
    }
}
