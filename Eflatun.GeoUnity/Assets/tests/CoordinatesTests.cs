using NUnit.Framework;
using starikcetin.Eflatun.GeoUnity.Calculation;

namespace starikcetin.Eflatun.GeoUnity.tests
{
    public class CoordinatesTests
    {
        [Test]
        public void ToRadians()
        {
            const double degLat = 5;
            const double degLng = 6;
            const double actualRadLat = degLat * Const.Deg2Rad;
            const double actualRadLng = degLng * Const.Deg2Rad;

            var degCoords = new Coordinates(degLat, degLng, 0, AngleType.Degrees);
            var radCoords = degCoords.ToRadians();

            TestUtils.AssertApprox(actualRadLat, radCoords.Latitude, TestUtils.ErrorType.DoubleComparison);
            TestUtils.AssertApprox(actualRadLng, radCoords.Longitude, TestUtils.ErrorType.DoubleComparison);
        }

        [Test]
        public void ToDegrees()
        {
            const double radLat = 0.3;
            const double radLng = -0.5;
            const double actualDegLat = radLat * Const.Rad2Deg;
            const double actualDegLng = radLng * Const.Rad2Deg;

            var radCoords = new Coordinates(radLat, radLng, 0, AngleType.Radians);
            var degCoords = radCoords.ToDegrees();

            TestUtils.AssertApprox(actualDegLat, degCoords.Latitude, TestUtils.ErrorType.DoubleComparison);
            TestUtils.AssertApprox(actualDegLng, degCoords.Longitude, TestUtils.ErrorType.DoubleComparison);
        }

        [Test]
        public void ConvertTo_Rad()
        {
            var degCoords = new Coordinates(9, 10, 0, AngleType.Degrees);


            var toRad = degCoords.ToRadians();
            var convert = degCoords.ConvertTo(AngleType.Radians);

            Assert.AreEqual(toRad, convert);
        }

        [Test]
        public void ConvertTo_Deg()
        {
            var radCoords = new Coordinates(0.1, -0.1, 0, AngleType.Radians);

            var toDeg = radCoords.ToDegrees();
            var convert = radCoords.ConvertTo(AngleType.Degrees);

            Assert.AreEqual(toDeg, convert);
        }

        [Test]
        public void ConvertToAngleTypeOf()
        {
            var rad = new Coordinates(1.5, -1.5, 0, AngleType.Radians);
            var deg = new Coordinates(15, 16, 0, AngleType.Degrees);

            var radToDeg = rad.ConvertToAngleTypeOf(deg);
            var degToRad = deg.ConvertToAngleTypeOf(rad);

            Assert.AreEqual(AngleType.Degrees, radToDeg.AngleType);
            Assert.AreEqual(AngleType.Radians, degToRad.AngleType);
        }

        [Test]
        public void PlusOperator_deg_rad()
        {
            const double radLat = 0.1;
            const double radLng = -0.1;
            var right_rad = new Coordinates(radLat, radLng, 0, AngleType.Radians);

            const double degLat = 19;
            const double degLng = 20;
            var left_deg = new Coordinates(degLat, degLng, 0, AngleType.Degrees);

            var manual = left_deg + right_rad.ToDegrees();
            var auto = left_deg + right_rad;

            Assert.AreEqual(manual, auto);
            Assert.AreEqual(left_deg.AngleType, manual.AngleType);
            Assert.AreEqual(left_deg.AngleType, auto.AngleType);
        }

        [Test]
        public void PlusOperator_rad_deg()
        {
            const double radLat = 0.2;
            const double radLng = -0.2;
            var left_rad = new Coordinates(radLat, radLng, 0, AngleType.Radians);

            const double degLat = 23;
            const double degLng = 24;
            var right_deg = new Coordinates(degLat, degLng, 0, AngleType.Degrees);

            var manual = left_rad + right_deg.ToRadians();
            var auto = left_rad + right_deg;

            Assert.AreEqual(manual, auto);
            Assert.AreEqual(left_rad.AngleType, manual.AngleType);
            Assert.AreEqual(left_rad.AngleType, auto.AngleType);
        }

        [Test]
        public void MinusOperator_deg_rad()
        {
            const double radLat = 0.4;
            const double radLng = 1.2;
            var right_rad = new Coordinates(radLat, radLng, 0, AngleType.Radians);

            const double degLat = 27;
            const double degLng = 28;
            var left_deg = new Coordinates(degLat, degLng, 0, AngleType.Degrees);

            var manual = left_deg - right_rad.ToDegrees();
            var auto = left_deg - right_rad;

            Assert.AreEqual(manual, auto);
            Assert.AreEqual(left_deg.AngleType, manual.AngleType);
            Assert.AreEqual(left_deg.AngleType, auto.AngleType);
        }

        [Test]
        public void MinusOperator_rad_deg()
        {
            const double radLat = -0.4;
            const double radLng = 1.4;
            var left_rad = new Coordinates(radLat, radLng, 0, AngleType.Radians);

            const double degLat = 40;
            const double degLng = 41;
            var right_deg = new Coordinates(degLat, degLng, 0, AngleType.Degrees);

            var manual = left_rad - right_deg.ToRadians();
            var auto = left_rad - right_deg;

            Assert.AreEqual(manual, auto);
            Assert.AreEqual(left_rad.AngleType, manual.AngleType);
            Assert.AreEqual(left_rad.AngleType, auto.AngleType);
        }
    }
}
