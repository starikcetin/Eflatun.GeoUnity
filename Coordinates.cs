using System;
using Eflatun.GeoUnity.External;
using static Eflatun.GeoUnity.Calculation.Const;
using static Eflatun.GeoUnity.MathExtended;

namespace Eflatun.GeoUnity
{
    public class Coordinates : IEquatable<Coordinates>
    {
        private GeoCoordinate Geo { get; }

        public AngleType AngleType { get; }

        public double Latitude => AngleType == AngleType.Radians ? Geo.Latitude * Deg2Rad : Geo.Latitude;
        public double Longitude => AngleType == AngleType.Radians ? Geo.Longitude * Deg2Rad : Geo.Longitude;
        public double Altitude => Geo.Altitude;

        /// <param name="latitude">Latitude.</param>
        /// <param name="longitude">Longitude.</param>
        /// <param name="altitude">Altitude.</param>
        /// <param name="angleType">Angle type of latitude and longitude.</param>
        public Coordinates(double latitude, double longitude, double altitude = 0,
            AngleType angleType = AngleType.Degrees)
        {
            if (angleType == AngleType.Radians)
            {
                latitude *= Rad2Deg;
                longitude *= Rad2Deg;
            }

            latitude = NormalizeDegrees(latitude);
            longitude = NormalizeDegrees(longitude);

            if (latitude > 90.0 || latitude < -90.0)
            {
                throw new ArgumentOutOfRangeException(nameof(latitude), "Argument must be in range of -90 to 90");
            }

            if (longitude > 180.0 || longitude < -180.0)
            {
                throw new ArgumentOutOfRangeException(nameof(longitude), "Argument must be in range of -180 to 180");
            }

            AngleType = angleType;
            Geo = new GeoCoordinate(latitude, longitude, altitude);
        }

        /// <param name="geoCoords">Geographical coordinates.</param>
        public Coordinates(GeoCoordinate geoCoords)
        {
            Geo = geoCoords;
            AngleType = AngleType.Degrees;
        }

        public Coordinates(Coordinates other)
        {
            Geo = other.Geo;
            AngleType = other.AngleType;
        }

        private Coordinates(Coordinates other, AngleType type)
        {
            Geo = other.Geo;
            AngleType = type;
        }

        /// <summary>
        /// Returns in degrees.
        /// </summary>
        public GeoCoordinate ToGeoCoordinate()
        {
            return new GeoCoordinate(Geo.Latitude, Geo.Longitude, Geo.Altitude, Geo.HorizontalAccuracy,
                Geo.VerticalAccuracy, Geo.Speed, Geo.Course);
        }

        //
        // Equality Members
        //

        public bool Equals(Coordinates other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Geo, other.Geo);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Coordinates other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Geo != null ? Geo.GetHashCode() : 0;
        }

        public static bool operator ==(Coordinates left, Coordinates right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Coordinates left, Coordinates right)
        {
            return !Equals(left, right);
        }

        //
        // Arithmetics
        //

        /// <summary>
        /// Altitudes are also added together.
        /// Returns in angle type of <paramref name="left"/>.
        /// </summary>
        public static Coordinates operator +(Coordinates left, Coordinates right)
        {
            right = right.ConvertToAngleTypeOf(left);
            return new Coordinates(left.Latitude + right.Latitude, left.Longitude + right.Longitude,
                left.Altitude + right.Altitude, left.AngleType);
        }

        /// <summary>
        /// Altitudes are also added together.
        /// Returns in angle type of <paramref name="left"/>.
        /// </summary>
        public static Coordinates operator -(Coordinates left, Coordinates right)
        {
            right = right.ConvertToAngleTypeOf(left);
            return new Coordinates(left.Latitude - right.Latitude, left.Longitude - right.Longitude,
                left.Altitude + right.Altitude, left.AngleType);
        }

        /// <summary>
        /// Angle type and altitude remains the same.
        /// </summary>
        public static Coordinates operator *(Coordinates left, double right)
        {
            return new Coordinates(left.Latitude * right, left.Longitude * right,
                left.Altitude, left.AngleType);
        }

        /// <summary>
        /// Angle type and altitude remains the same.
        /// </summary>
        public static Coordinates operator /(Coordinates left, double right)
        {
            return new Coordinates(left.Latitude / right, left.Longitude / right,
                left.Altitude, left.AngleType);
        }

        //
        // Rad / Deg Handling
        //

        public Coordinates ToRadians()
        {
            return new Coordinates(this, AngleType.Radians);
        }

        public Coordinates ToDegrees()
        {
            return new Coordinates(this, AngleType.Degrees);
        }

        public Coordinates ConvertTo(AngleType type)
        {
            return new Coordinates(this, type);
        }

        public Coordinates ConvertToAngleTypeOf(Coordinates other)
        {
            return new Coordinates(this, other.AngleType);
        }

        //
        // Pre-Defined
        //

        /// <summary>
        /// (+90, 0)
        /// </summary>
        public static readonly Coordinates NorthPole = new Coordinates(90, 0);

        /// <summary>
        /// (-90, 0)
        /// </summary>
        public static readonly Coordinates SouthPole = new Coordinates(-90, 0);

        /// <summary>
        /// (0, 0)
        /// </summary>
        public static readonly Coordinates Zero = new Coordinates(0, 0);

        //
        // ToString
        //

        public override string ToString()
        {
            return $"({AngleType}, {nameof(Latitude)}: {Latitude}, {nameof(Longitude)}: {Longitude}, {nameof(Altitude)}: {Altitude})";
        }
    }
}
