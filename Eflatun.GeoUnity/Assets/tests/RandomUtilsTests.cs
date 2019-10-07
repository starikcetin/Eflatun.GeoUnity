using NUnit.Framework;
using starikcetin.Eflatun.GeoUnity.Calculation;

namespace starikcetin.Eflatun.GeoUnity.tests
{
    public class RandomUtilsTests
    {
        [Test]
        public void OnCircle_Distance()
        {
            var center = new Coordinates(0, 0);

            var target = RandomUtils.OnCircle(center, TestUtils.DistanceInMeters);
            var calculatedDistance = MathUtils.DistanceBetween(center, target);

            TestUtils.AssertApprox(TestUtils.DistanceInMeters, calculatedDistance, TestUtils.ErrorType.Distance);
        }
    }
}
