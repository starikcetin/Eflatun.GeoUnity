using System.Device.Location;
using ProjNet.CoordinateSystems;

namespace starikcetin.Eflatun.GeoUnity.Projection
{
    public class Wgs84ToMercator
    {
        private readonly Projector _wgs84ToMercator;

        public Wgs84ToMercator()
        {
            _wgs84ToMercator = new Projector(GeocentricCoordinateSystem.WGS84, ProjectedCoordinateSystem.WebMercator);
        }

        public GeoCoordinate Project(GeoCoordinate wgs84)
        {
            GeoCoordinate mercator = _wgs84ToMercator.Project(wgs84);
            return mercator;
        }

        public GeoCoordinate InverseProject(GeoCoordinate mercator)
        {
            GeoCoordinate wgs84 = _wgs84ToMercator.InverseProject(mercator);
            return wgs84;
        }
    }
}
