using NUnit.Framework;

namespace starikcetin.Eflatun.GeoUnity.tests
{
    public class CoordinatesTests
    {
        [Test]
        public void ToRadians()
        {
            const double degLat = 500;
            const double degLng = 600;
            const double actualRadLat = degLat * Const.Deg2Rad;
            const double actualRadLng = degLng * Const.Deg2Rad;

            var degCoords = new Coordinates(degLat, degLng, AngleType.Degrees);
            var radCoords = degCoords.ToRadians();

            TestUtils.AssertApprox(actualRadLat, radCoords.Latitude, TestUtils.ErrorType.DoubleComparison);
            TestUtils.AssertApprox(actualRadLng, radCoords.Longitude, TestUtils.ErrorType.DoubleComparison);
        }

        [Test]
        public void ToDegrees()
        {
            const double radLat = 5;
            const double radLng = 6;
            const double actualDegLat = radLat * Const.Rad2Deg;
            const double actualDegLng = radLng * Const.Rad2Deg;

            var radCoords = new Coordinates(radLat, radLng, AngleType.Radians);
            var degCoords = radCoords.ToDegrees();

            TestUtils.AssertApprox(actualDegLat, degCoords.Latitude, TestUtils.ErrorType.DoubleComparison);
            TestUtils.AssertApprox(actualDegLng, degCoords.Longitude, TestUtils.ErrorType.DoubleComparison);
        }

        [Test]
        public void ConvertTo_Rad()
        {
            var degCoords = new Coordinates(500, 600, AngleType.Degrees);


            var toRad = degCoords.ToRadians();
            var convert = degCoords.ConvertTo(AngleType.Radians);

            Assert.AreEqual(toRad, convert);
        }

        [Test]
        public void ConvertTo_Deg()
        {
            var radCoords = new Coordinates(5, 6, AngleType.Radians);

            var toDeg = radCoords.ToDegrees();
            var convert = radCoords.ConvertTo(AngleType.Degrees);

            Assert.AreEqual(toDeg, convert);
        }

        [Test]
        public void ConvertToAngleTypeOf()
        {
            var rad = new Coordinates(5, 6, AngleType.Radians);
            var deg = new Coordinates(500, 600, AngleType.Degrees);

            var radToDeg = rad.ConvertToAngleTypeOf(deg);
            var degToRad = deg.ConvertToAngleTypeOf(rad);

            Assert.AreEqual(AngleType.Degrees, radToDeg.AngleType);
            Assert.AreEqual(AngleType.Radians, degToRad.AngleType);
        }

        [Test]
        public void PlusOperator_deg_rad()
        {
            const double radLat = 5;
            const double radLng = 6;
            var right_rad = new Coordinates(radLat, radLng, AngleType.Radians);

            const double degLat = 500;
            const double degLng = 600;
            var left_deg = new Coordinates(degLat, degLng, AngleType.Degrees);

            var manual = left_deg + right_rad.ToDegrees();
            var auto = left_deg + right_rad;

            Assert.AreEqual(manual, auto);
            Assert.AreEqual(left_deg.AngleType, manual.AngleType);
            Assert.AreEqual(left_deg.AngleType, auto.AngleType);
        }

        [Test]
        public void PlusOperator_rad_deg()
        {
            const double radLat = 5;
            const double radLng = 6;
            var left_rad = new Coordinates(radLat, radLng, AngleType.Radians);

            const double degLat = 500;
            const double degLng = 600;
            var right_deg = new Coordinates(degLat, degLng, AngleType.Degrees);

            var manual = left_rad + right_deg.ToRadians();
            var auto = left_rad + right_deg;

            Assert.AreEqual(manual, auto);
            Assert.AreEqual(left_rad.AngleType, manual.AngleType);
            Assert.AreEqual(left_rad.AngleType, auto.AngleType);
        }

        [Test]
        public void MinusOperator_deg_rad()
        {
            const double radLat = 5;
            const double radLng = 6;
            var right_rad = new Coordinates(radLat, radLng, AngleType.Radians);

            const double degLat = 500;
            const double degLng = 600;
            var left_deg = new Coordinates(degLat, degLng, AngleType.Degrees);

            var manual = left_deg - right_rad.ToDegrees();
            var auto = left_deg - right_rad;

            Assert.AreEqual(manual, auto);
            Assert.AreEqual(left_deg.AngleType, manual.AngleType);
            Assert.AreEqual(left_deg.AngleType, auto.AngleType);
        }

        [Test]
        public void MinusOperator_rad_deg()
        {
            const double radLat = 5;
            const double radLng = 6;
            var left_rad = new Coordinates(radLat, radLng, AngleType.Radians);

            const double degLat = 500;
            const double degLng = 600;
            var right_deg = new Coordinates(degLat, degLng, AngleType.Degrees);

            var manual = left_rad - right_deg.ToRadians();
            var auto = left_rad - right_deg;

            Assert.AreEqual(manual, auto);
            Assert.AreEqual(left_rad.AngleType, manual.AngleType);
            Assert.AreEqual(left_rad.AngleType, auto.AngleType);
        }
    }
}
