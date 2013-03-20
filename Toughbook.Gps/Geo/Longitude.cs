using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Toughbook.Gps
{
    /// <summary>
    /// Longitude indicates location of a place on Earth East or West of the Prime Meridian.
    /// </summary>
    public struct Longitude : IEquatable<Longitude>, IComparable<Longitude>
    {
        private readonly double _Degrees;
        /// <summary>
        /// Indicates invalid or unknown value.
        /// </summary>
        public static readonly Longitude Invalid = new Longitude(double.NaN);
        /// <summary>
        /// Constructs new Longitude instance with specified longitude in decimal degree format.
        /// </summary>
        /// <param name="decimalDegrees">Longitude in decimal degrees</param>
        public Longitude(double decimalDegrees)
        {
            _Degrees = decimalDegrees;

        }
        /// <summary>
        /// Constructs new Longitude instance with specified hours, minutes, and seconds.
        /// </summary>
        /// <param name="hours">Hours of angular mearsurement.</param>
        /// <param name="minutes">Minutes of angular mearsurement.</param>
        /// <param name="hemisphere">East or West Hemisphere.</param>
        public Longitude(int hours, double minutes, Hemisphere hemisphere)
        {
            switch (hemisphere)
            {
                case Hemisphere.West:
                    _Degrees = -Math.Abs(hours) - minutes / 60.0;
                    break;
                case Hemisphere.East:
                    _Degrees =  Math.Abs(hours) + minutes / 60.0;
                    break;
                default:
                     throw new ArgumentException("Hemisphere " + hemisphere.ToString() + " is not valid");
            }
      
        }
        /// <summary>
        /// Indicates whether line of Longitude is East or West Hemisphere
        /// </summary>
        public Hemisphere Hemisphere
        {
            get
            {
                return _Degrees < 0 ? Hemisphere.West : Hemisphere.East;
            }
        }
        /// <summary>
        /// Returns hours of a angular mearsurement.
        /// </summary>
        public int Hours
        {
            get
            {               
                return (int)Math.Truncate(_Degrees);
            }
        }
        /// <summary>
        /// Returns minutes of a angular mearsurement.
        /// </summary>
        public double Minutes
        {
            get
            {
                return (Math.Abs((_Degrees - Hours) * 60.0));
            }
        }
        /// <summary>
        /// Returns seconds of a angular mearsurement.
        /// </summary>
        public double Seconds
        {
            get
            {
                int minutes = (int)Math.Truncate(Minutes);
                double remainder = Minutes - (double)minutes;
                return remainder * 60.0;
                /*double result = Math.Round((Math.Abs(_Degrees - (double)(Hours)) * (double)(60.0 - Minutes)) * 60.0, 13 -4);
                return result;*/
            }
        }
        /// <summary>
        /// Returns Longitude as decimal fractions
        /// </summary>
        public double DecimalDegrees
        {
            get
            {
                return _Degrees;
            }
        }
        /// <summary>
        /// Compares the current instance to the specified longitude.
        /// </summary>
        public int CompareTo(Longitude value)
        {
            return _Degrees.CompareTo(value.DecimalDegrees);
        }
        /// <summary>
        /// Returns whether current instance has a valid value.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return !double.IsNaN(_Degrees);
            }
        }
        /// <summary>
        /// Determines whether the specified Longitude is equal to the current Longitude.
        /// </summary>
        /// <param name="obj">The Longitude to compare with the current Longitude.</param>
        /// <returns> true if the specified Longitude is equal to the current Longitude
        /// otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Longitude)
            {
                return Equals((Longitude)obj);
            }
            else
                return false;
        }
        /// <summary>
        /// Determines whether the specified Longitude is equal to the current Longitude.
        /// </summary>
        /// <param name="value">The Longitude to compare with the current Longitude.</param>
        /// <returns> true if the specified Longitude is equal to the current Longitude
        /// otherwise, false.</returns>
        public bool Equals(Longitude value)
        {
            return _Degrees.Equals(value.DecimalDegrees);
        }
        /// <summary>
        /// Serves as a hash function for a Longitude object.
        /// </summary>
        /// <returns>A hash code for the current Longitude.</returns>
        public override int GetHashCode()
        {
            return _Degrees.GetHashCode();
        }
        /// <summary>
        /// Returns a System.String that represents the current Longitude.
        /// </summary>
        /// <returns>A System.String that represents the current Longitude.</returns>
        public override string ToString()
        {
            if (!IsValid)
            {
                return "NaN";
            }
            //format = "HHH°MM'SS.SSSS\"i";
            string hours = Math.Abs(Hours).ToString("000");
            string minutes = Minutes.ToString("00");
            string seconds = Seconds.ToString("0.00");
            string result = hours + "°" + minutes + "'" + seconds + "\"" + Hemisphere.ToString().Substring(0, 1);
            return result;
        }
        /*public string ToString(string format, IFormatProvider provider)
        {
            if (string.IsNullOrEmpty(format))
                return ToString();

            CultureInfo culture = (CultureInfo)provider;

            if (culture == null)
                culture = CultureInfo.CurrentCulture;


            if (!IsValid)
                return "NaN";

            format = format.ToUpper(culture);
            int StartIndex = format.IndexOf("H");
            int endS

            if (hourStartIndex != -1)
            {
                
            }
            return format;
        }*/

    }
}
