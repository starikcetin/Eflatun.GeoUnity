using static System.Math;
using doubleVector3 = System.DoubleNumerics.Vector3;
using unityVector3 = UnityEngine.Vector3;

namespace starikcetin.Eflatun.GeoUnity
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

        public static double Gd(double val)
        {
            return 2 * Atan(Tanh(1 / (2 * val)));
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
    }
}
