using ProjNet.CoordinateSystems;
using doubleVector3 = System.DoubleNumerics.Vector3;

namespace starikcetin.Eflatun.GeoUnity.Projection
{
    public class Wgs84ToMercatorDouble
    {
        private readonly Vector3AltitudeAxis _altitudeAxis;
        private readonly Projector _wgs84ToMercator;

        public Wgs84ToMercatorDouble(Vector3AltitudeAxis altitudeAxis = Vector3AltitudeAxis.Y)
        {
            _altitudeAxis = altitudeAxis;
            _wgs84ToMercator = new Projector(GeographicCoordinateSystem.WGS84, ProjectedCoordinateSystem.WebMercator);
        }

        public doubleVector3 Project(Coordinates wgs84)
        {
            var wgs84Arr = ProjectionUtils.CoordinateToArray(wgs84);
            var mercator = _wgs84ToMercator.Project(wgs84Arr);
            return ProjectionUtils.ArrayToVector(mercator, _altitudeAxis);
        }

        public Coordinates InverseProject(doubleVector3 mercator)
        {
            var mercatorArr = ProjectionUtils.VectorToArray(mercator, _altitudeAxis);
            var wgs84 = _wgs84ToMercator.InverseProject(mercatorArr);
            return ProjectionUtils.ArrayToCoordinate(wgs84);
        }
    }
}
