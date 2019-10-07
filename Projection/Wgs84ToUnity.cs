using System.Device.Location;
using ProjNet.CoordinateSystems;
using doubleVector3 = System.DoubleNumerics.Vector3;
using unityVector3 = UnityEngine.Vector3;

namespace starikcetin.Eflatun.GeoUnity.Projection
{
    public class Wgs84ToUnity
    {
        private readonly Projector _wgs84ToMercator;
        private readonly MercatorToUnity _mercatorToUnity;

        public Wgs84ToUnity(double lon0, float metersPerUnityUnit = 1)
        {
            _wgs84ToMercator = new Projector(GeocentricCoordinateSystem.WGS84, ProjectedCoordinateSystem.WebMercator);
            _mercatorToUnity = new MercatorToUnity(lon0, metersPerUnityUnit);
        }

        public unityVector3 Project(GeoCoordinate wgs84)
        {
            GeoCoordinate mercator = _wgs84ToMercator.Project(wgs84);
            unityVector3 unityPoint = _mercatorToUnity.Project(mercator);
            return unityPoint;
        }

        public GeoCoordinate InverseProject(unityVector3 unityPoint)
        {
            GeoCoordinate mercator = _mercatorToUnity.InverseProject(unityPoint);
            GeoCoordinate wgs84 = _wgs84ToMercator.InverseProject(mercator);
            return wgs84;
        }
    }
}
