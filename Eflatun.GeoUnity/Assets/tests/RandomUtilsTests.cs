using NUnit.Framework;

namespace starikcetin.Eflatun.GeoUnity.tests
{
    public class RandomUtilsTests
    {
        [Test]
        public void OnCircle_Distance()
        {
            var center = new Coordinates();

            var target = RandomUtils.OnCircle(center, TestUtils.DistanceInMeters);
            var calculatedDistance = MathUtils.DistanceBetween(center, target);

            TestUtils.AssertApprox(TestUtils.DistanceInMeters, calculatedDistance, TestUtils.ErrorType.Distance);
        }
    }
}
