using System;
using GeoCoordinatePortable;
using UnityEngine;

namespace starikcetin.Eflatun.GeoUnity
{
    public struct Coordinates : IEquatable<Coordinates>
    {
        public double Latitude { get; }
        public double Longitude { get; }
        public AngleType AngleType { get; }

        public Coordinates(double latitude, double longitude, AngleType angleType)
        {
            Latitude = latitude;
            Longitude = longitude;
            AngleType = angleType;
        }

        public Coordinates(Coordinates other)
        {
            Latitude = other.Latitude;
            Longitude = other.Longitude;
            AngleType = other.AngleType;
        }

        public Coordinates(GeoCoordinate gc)
        {
            Latitude = gc.Latitude;
            Longitude = gc.Longitude;
            AngleType = AngleType.Degrees;
        }

        public static explicit operator GeoCoordinate(Coordinates coords)
        {
            coords = coords.ToDegrees();
            return new GeoCoordinate(coords.Latitude, coords.Longitude);
        }

        public static explicit operator Coordinates(GeoCoordinate coords)
        {
            return new Coordinates(coords.Latitude, coords.Longitude, AngleType.Degrees);
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
            return new Coordinates(left.Latitude + right.Latitude, left.Longitude + right.Longitude, left.AngleType);
        }

        public static Coordinates operator -(Coordinates left, Coordinates right)
        {
            right = right.ConvertToAngleTypeOf(left);
            return new Coordinates(left.Latitude - right.Latitude, left.Longitude - right.Longitude, left.AngleType);
        }

        public static Coordinates operator *(Coordinates left, double right)
        {
            return new Coordinates(left.Latitude * right, left.Longitude * right, left.AngleType);
        }

        public static Coordinates operator /(Coordinates left, double right)
        {
            return new Coordinates(left.Latitude / right, left.Longitude / right, left.AngleType);
        }

        //
        // Rad / Deg Handling
        //

        public Coordinates ToRadians()
        {
            if (AngleType == AngleType.Radians)
            {
                return this;
            }

            var rad =  this * Const.Deg2Rad;
            return new Coordinates(rad.Latitude, rad.Longitude, AngleType.Radians);
        }

        public Coordinates ToDegrees()
        {
            if (AngleType == AngleType.Degrees)
            {
                return this;
            }

            var rad =  this * Const.Rad2Deg;
            return new Coordinates(rad.Latitude, rad.Longitude, AngleType.Degrees);
        }

        public Coordinates ConvertTo(AngleType type)
        {
            if (AngleType == type)
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
            return this.ConvertTo(other.AngleType);
        }
    }
}
