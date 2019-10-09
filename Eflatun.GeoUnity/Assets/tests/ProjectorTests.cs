using NUnit.Framework;
using starikcetin.Eflatun.GeoUnity.Calculation;
using starikcetin.Eflatun.GeoUnity.Projection;
using UnityEngine;

namespace starikcetin.Eflatun.GeoUnity.tests
{
    public class ProjectorTests
    {
        [Test]
        public void Wgs84ToMercatorDouble()
        {
            var projector = new Wgs84ToMercatorDouble();
            var actual = Coordinates.Zero;
            var forward = projector.Project(actual);
            var back = projector.InverseProject(forward);
            var backC = new Coordinates(back);

            Assert.AreEqual(actual, backC);
        }

        [Test]
        public void Wgs84ToMercatorUnity()
        {
            var projector = new Wgs84ToMercatorUnity();
            var actual = Coordinates.Zero;
            var forward = projector.Project(actual);
            var back = projector.InverseProject(forward);
            var backC = new Coordinates(back);

            Assert.AreEqual(actual, backC);
        }

        [Test]
        public void Wgs84ToMercatorDouble_DistanceTest()
        {
            var wgsToMercatorDouble = new Wgs84ToMercatorDouble();

            var wgsA = new Coordinates(10, 20);
            var wgsB = new Coordinates(20, 30);
            var wgsDist = MathUtils.DistanceBetween(wgsA, wgsB);

            var mercatorDoubleA = wgsToMercatorDouble.Project(wgsA);
            var mercatorDoubleB = wgsToMercatorDouble.Project(wgsB);
            var mercatorDoubleDist = System.DoubleNumerics.Vector3.Distance(mercatorDoubleA, mercatorDoubleB);

            Assert.AreEqual(wgsDist, mercatorDoubleDist);
        }

        [Test]
        public void Wgs84ToMercatorUnity_DistanceTest()
        {
            var wgsToMercatorUnity = new Wgs84ToMercatorUnity();

            var wgsA = new Coordinates(10, 20);
            var wgsB = new Coordinates(20, 30);
            var wgsDist = MathUtils.DistanceBetween(wgsA, wgsB);

            var unityDoubleA = wgsToMercatorUnity.Project(wgsA);
            var unityDoubleB = wgsToMercatorUnity.Project(wgsB);
            var unityDoubleDist = UnityEngine.Vector3.Distance(unityDoubleA, unityDoubleB);

            Assert.AreEqual(wgsDist, unityDoubleDist);
        }
    }
}
