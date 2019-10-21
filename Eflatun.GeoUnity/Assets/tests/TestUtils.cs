using System;
using Eflatun.GeoUnity;
using NUnit.Framework;

namespace starikcetin.Eflatun.GeoUnity.tests
{
    public static class TestUtils
    {
        private static readonly bool AllowErrors = true;

        private const double DistanceMaxErrorInMetersPerMeter = 0.000864;
        private const double AngleMaxErrorInDegreesPerMeter = 0.000015;

        public const double DistanceInMeters = 1000;
        public const double BearingInAngles = 100;

        public static void AssertApprox(double a, double b, ErrorType errorType)
        {
            double maxError;

            if (!AllowErrors)
            {
                maxError = double.Epsilon;
            }
            else
            {
                switch (errorType)
                {
                    case ErrorType.Distance:
                        maxError = CalculateMaxDistanceErrorInMeters();
                        break;
                    case ErrorType.Bearing:
                        maxError = CalculateMaxBearingErrorInDegrees();
                        break;
                    case ErrorType.DoubleComparison:
                        maxError = double.Epsilon;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(errorType), errorType, null);
                }
            }

            Assert.That(MathExtended.Approx(a, b, maxError, out var error),
                "expected: {0} result: {1} \n maxError: {2} error: {3} \n delta: {4}",
                a, b, maxError, error, error - maxError);
        }

        private static double CalculateMaxDistanceErrorInMeters()
        {
            return DistanceInMeters * DistanceMaxErrorInMetersPerMeter;
        }

        private static double CalculateMaxBearingErrorInDegrees()
        {
            return DistanceInMeters * AngleMaxErrorInDegreesPerMeter;
        }

        public enum ErrorType
        {
            Distance,
            Bearing,
            DoubleComparison
        }
    }
}
