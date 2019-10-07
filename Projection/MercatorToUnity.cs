using System.Device.Location;
using static starikcetin.Eflatun.GeoUnity.MathExtended;
using doubleVector3 = System.DoubleNumerics.Vector3;
using unityVector3 = UnityEngine.Vector3;

namespace starikcetin.Eflatun.GeoUnity.Projection
{
    public class MercatorToUnity
    {
        private readonly MercatorToDoubleVector _mercatorToDoubleVector;
        private readonly float _metersPerUnityUnit;

        public MercatorToUnity(double lon0, float metersPerUnityUnit = 1)
        {
            _metersPerUnityUnit = metersPerUnityUnit;
            _mercatorToDoubleVector = new MercatorToDoubleVector(lon0, Vector3UpwardsAxis.Y);
        }

        public unityVector3 Project(GeoCoordinate mercator)
        {
            doubleVector3 doublePoint = _mercatorToDoubleVector.Project(mercator);
            doubleVector3 scaledDoublePoint = doublePoint / _metersPerUnityUnit;
            unityVector3 scaledUnityPoint = ConvertToUnityVector(scaledDoublePoint);
            return scaledUnityPoint;
        }

        public GeoCoordinate InverseProject(unityVector3 point)
        {
            doubleVector3 scaledDoublePoint = ConvertToDoubleVector(point);
            doubleVector3 doublePoint = scaledDoublePoint * _metersPerUnityUnit;
            GeoCoordinate mercator = _mercatorToDoubleVector.InverseProject(doublePoint);
            return mercator;
        }
    }
}
