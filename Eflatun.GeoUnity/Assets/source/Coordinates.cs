using System;

namespace starikcetin.Eflatun.GeoUnity
{
    public struct Coordinates : IEquatable<Coordinates>
    {
        private readonly AngleType _angleType;

        public float Latitude { get; }
        public float Longitude { get; }

        public Coordinates(float latitude, float longitude, AngleType angleType)
        {
            Latitude = latitude;
            Longitude = longitude;
            _angleType = angleType;
        }

        public Coordinates(Coordinates other)
        {
            Latitude = other.Latitude;
            Longitude = other.Longitude;
            _angleType = other._angleType;
        }

        //
        // Equality Members
        //

        public bool Equals(Coordinates other)
        {
            return Latitude.Equals(other.Latitude) && Longitude.Equals(other.Longitude);
        }

        public override bool Equals(object obj)
        {
            return obj is Coordinates other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Latitude.GetHashCode() * 397) ^ Longitude.GetHashCode();
            }
        }

        public static bool operator ==(Coordinates left, Coordinates right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Coordinates left, Coordinates right)
        {
            return !left.Equals(right);
        }

        //
        // Arithmetics
        //

        public static Coordinates operator +(Coordinates left, Coordinates right)
        {
            right = right.ConvertToAngleTypeOf(left);
            return new Coordinates(left.Latitude + right.Latitude, left.Longitude + right.Longitude, left._angleType);
        }

        public static Coordinates operator -(Coordinates left, Coordinates right)
        {
            right = right.ConvertToAngleTypeOf(left);
            return new Coordinates(left.Latitude - right.Latitude, left.Longitude - right.Longitude, left._angleType);
        }

        public static Coordinates operator *(Coordinates left, float right)
        {
            return new Coordinates(left.Latitude * right, left.Longitude * right, left._angleType);
        }

        public static Coordinates operator /(Coordinates left, float right)
        {
            return new Coordinates(left.Latitude / right, left.Longitude / right, left._angleType);
        }

        //
        // Rad / Deg Handling
        //

        public Coordinates ToRadians()
        {
            return this.ConvertTo(AngleType.Radians);
        }

        public Coordinates ToDegrees()
        {
            return this.ConvertTo(AngleType.Degrees);
        }

        public Coordinates ConvertTo(AngleType type)
        {
            if (_angleType == type)
            {
                return this;
            }

            switch (type)
            {
                case AngleType.Radians:
                    return this.ToRadians();

                case AngleType.Degrees:
                    return this.ToDegrees();

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public Coordinates ConvertToAngleTypeOf(Coordinates other)
        {
            return this.ConvertTo(other._angleType);
        }
    }
}
