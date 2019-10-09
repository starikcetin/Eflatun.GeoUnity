using static starikcetin.Eflatun.GeoUnity.MathExtended;
using doubleVector3 = System.DoubleNumerics.Vector3;
using unityVector3 = UnityEngine.Vector3;

namespace starikcetin.Eflatun.GeoUnity.Projection
{
    public class Wgs84ToMercatorUnity
    {
        private readonly Wgs84ToMercatorDouble _wgs84ToMercatorDouble;

        public Wgs84ToMercatorUnity()
        {
            _wgs84ToMercatorDouble = new Wgs84ToMercatorDouble(Vector3AltitudeAxis.Y);
        }

        public unityVector3 Project(Coordinates wgs84)
        {
            var mercatorInDouble = _wgs84ToMercatorDouble.Project(wgs84);
            return DoubleVectorToUnityVector(mercatorInDouble);
        }

        public Coordinates InverseProject(unityVector3 mercator)
        {
            var mercatorInDouble = UnityVectorToDoubleVector(mercator);
            return _wgs84ToMercatorDouble.InverseProject(mercatorInDouble);
        }
    }
}
