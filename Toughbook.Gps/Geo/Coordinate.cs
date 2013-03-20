using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Toughbook.Gps
{
    /// <summary>
    /// Coordinate uniquely idenifies a position on Earth.
    /// </summary>
    public struct Coordinate : IEquatable<Coordinate>
    {
        private readonly Longitude _Longitude;
        private readonly Latitude _Latitude;
        /// <summary>
        /// Indicates invalid or unknown value.
        /// </summary>
        public static readonly Coordinate Invalid = new Coordinate(Latitude.Invalid, Longitude.Invalid);
        /// <summary>
        /// Contructs new Coordinate instance.
        /// </summary>
        /// <param name="latitude">Latitude instance.</param>
        /// <param name="longitude">Longitude instance.</param>
        public Coordinate(Latitude latitude, Longitude longitude)
        {
            _Longitude = longitude;
            _Latitude = latitude;
        }
        /// <summary>
        /// Returns Coordinate's latitude.
        /// </summary>
        public Latitude Latitude
        {
            get { return _Latitude; }
        }
        /// <summary>
        /// Returns Coordinate's longitude.
        /// </summary>
        public Longitude Longitude
        {
            get { return _Longitude; }
        }
        /// <summary>
        /// Determines whether the specified Speed is equal to the current Coordinate .
        /// </summary>
        /// <param name="obj">The Speed to compare with the current Coordinate.</param>
        /// <returns> true if the specified Speed is equal to the current Coordinate
        /// otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Coordinate)
            {
                return Equals((Coordinate)obj);
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// Returns whether current instance has a valid value.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return Latitude.IsValid && Longitude.IsValid;
            }
        }
        /// <summary>
        /// Determines whether the specified Speed is equal to the current Coordinate.
        /// </summary>
        /// <param name="value">The Speed to compare with the current Coordinate.</param>
        /// <returns> true if the specified Speed is equal to the current Coordinate
        /// otherwise, false.</returns>
        public bool Equals(Coordinate value)
        {
            return (this.Longitude.Equals(value.Longitude) && this.Latitude.Equals(value.Latitude));
        }
        /// <summary>
        /// Serves as a hash function for a Coordinate object.
        /// </summary>
        /// <returns>A hash code for the current Coordinate.</returns>
        public override int GetHashCode()
        {
            return _Latitude.GetHashCode() ^ _Longitude.GetHashCode();
        }
        /// <summary>
        /// Returns a System.String that represents the current Coordinate.
        /// </summary>
        /// <returns>A System.String that represents the current Coordinate</returns>
        public override string ToString()
        {
            CultureInfo culture = CultureInfo.CurrentCulture;

            return Latitude.ToString() + culture.TextInfo.ListSeparator + Longitude.ToString();
        }
    }
}
