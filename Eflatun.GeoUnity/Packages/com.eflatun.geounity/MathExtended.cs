using static System.Math;
using doubleVector3 = System.DoubleNumerics.Vector3;
using unityVector3 = UnityEngine.Vector3;

namespace Eflatun.GeoUnity
{
    public static class MathExtended
    {
        public static double Ln(double val)
        {
            return Log(val, E);
        }

        public static double Sec(double val)
        {
            return 1.0 / Cos(val);
        }

        public static double ATanh(double x)
        {
            return (Log(1 + x) - Log(1 - x))/2;
        }

        public static double GdReverse(double val)
        {
            double d = val / 2.0;
            double d1 = 1.0 / d;
            double aTanh = ATanh(d1);
            double tan = Tan(aTanh);
            return tan / 2.0;
        }

        public static double Gd(double val)
        {
            double d = 2.0 * val;
            double value = 1.0 / d;
            double tanh = Tanh(value);
            double atan = Atan(tanh);
            return 2.0 * atan;
        }

        public static unityVector3 ConvertToUnityVector(doubleVector3 doubleVector)
        {
            return new unityVector3((float) doubleVector.X, (float) doubleVector.Y, (float) doubleVector.Z);
        }

        public static doubleVector3 ConvertToDoubleVector(unityVector3 unityVector)
        {
            return new doubleVector3(unityVector.x, unityVector.y, unityVector.z);
        }

        /// <summary>
        /// Normalize to [-180, +180] range.
        /// </summary>
        public static double NormalizeDegrees(double degrees)
        {
            while (degrees < -180)
            {
                degrees += 360;
            }

            return degrees % 360;
        }

        /// <summary>
        /// Normalize to [-PI, +PI] range.
        /// </summary>
        public static double NormalizeRadians(double radians)
        {
            while (radians < -PI)
            {
                radians += PI;
            }

            return radians % PI;
        }

        public static bool Approx(double a, double b, double maxError, out double error)
        {
            error = Abs(a - b);
            return error < maxError;
        }

        public static unityVector3 DoubleVectorToUnityVector(doubleVector3 dv3)
        {
            return new unityVector3((float) dv3.X, (float) dv3.Y, (float) dv3.Z);
        }

        public static doubleVector3 UnityVectorToDoubleVector(unityVector3 uv3)
        {
            return new doubleVector3(uv3.x, uv3.y, uv3.z);
        }
    }
}
