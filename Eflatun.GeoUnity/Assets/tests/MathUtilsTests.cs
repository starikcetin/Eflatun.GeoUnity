using Eflatun.GeoUnity;
using Eflatun.GeoUnity.Calculation;
using NUnit.Framework;

namespace starikcetin.Eflatun.GeoUnity.tests
{
    public class MathsUtilsTests
    {
        [Test]
        public void CalculateDestination_Distance()
        {
            var origin = new Coordinates(0.0, 0.0, 0.0, AngleType.Degrees);

            var target = MathUtils.CalculateDestination(origin, TestUtils.DistanceInMeters, TestUtils.BearingInAngles);
            var calculatedDistance = MathUtils.DistanceBetween(origin, target);

            TestUtils.AssertApprox(TestUtils.DistanceInMeters, calculatedDistance, TestUtils.ErrorType.Distance);
        }

        [Test]
        public void CalculateDestination_Bearing()
        {
            var origin = new Coordinates(0.0, 0.0, 0.0, AngleType.Degrees);

            var target = MathUtils.CalculateDestination(origin, TestUtils.DistanceInMeters, TestUtils.BearingInAngles);
            var calculatedBearing = MathUtils.BearingBetween(origin, target);

            var normalizedActualBearing = MathExtended.NormalizeDegrees(TestUtils.BearingInAngles);
            TestUtils.AssertApprox(normalizedActualBearing, calculatedBearing, TestUtils.ErrorType.Bearing);
        }
    }
}
