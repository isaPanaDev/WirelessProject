using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toughbook.Gps
{
    /// <summary>
    /// Latitude indicates location of a place on Earth North or South of the equator
    /// </summary>
    public struct Latitude : IEquatable<Latitude>, IComparable<Latitude>
    {
        private readonly double _Degrees;
        /// <summary>
        /// Indicates invalid or unknown value.
        /// </summary>
        public static readonly Latitude Invalid = new Latitude(double.NaN);
        /// <summary>
        /// Constructs new Latitude instance with specified latitude in decimal degree format.
        /// </summary>
        /// <param name="decimalDegrees">Latitude in decimal degrees</param>
        public Latitude(double decimalDegrees)
        {
            _Degrees = decimalDegrees;
        }
        /// <summary>
        /// Constructs new Latitude instance with specified hours, minutes, and seconds.
        /// </summary>
        /// <param name="hours">Hours of angular mearsurement.</param>
        /// <param name="minutes">Minutes of angular mearsurement.</param>
        /// <param name="hemisphere">North or South Hemisphere.</param>
        public Latitude(int hours, double minutes, Hemisphere hemisphere)
        {
            switch (hemisphere)
            {
                case Hemisphere.South:
                    _Degrees = -Math.Abs(hours) - minutes / 60.0;
                    break;
                case Hemisphere.North:
                    _Degrees = Math.Abs(hours) + minutes / 60.0;
                    break;
                default:
                    throw new ArgumentException("Hemisphere " + hemisphere.ToString() + " is not valid");
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

                return (Math.Abs((_Degrees - Hours) * 60.0));//(, 13 - 1)));
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
                //double result = Math.Round((Math.Abs(_Degrees - (Hours)) * (60.0 - Minutes)) * 60.0, 13-4);
                //return result;
            }
        }
        /// <summary>
        /// Indicates whether line of Longitude is North or South Hemisphere.
        /// </summary>
        public Hemisphere Hemisphere
        {
            get
            {
                return _Degrees < 0 ? Hemisphere.South : Hemisphere.North;
            }
        }
        /// <summary>
        /// Determines whether the specified Latitude is equal to the current Latitude.
        /// </summary>
        /// <param name="obj">The Latitude to compare with the current Latitude.</param>
        /// <returns> true if the specified Latitude is equal to the current Latitude 
        /// otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Latitude)
            {
                return Equals((Latitude)obj);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Determines whether the specified Latitude is equal to the current Latitude.
        /// </summary>
        /// <param name="value">The Latitude to compare with the current Latitude.</param>
        /// <returns> true if the specified Latitude is equal to the current Latitude 
        /// otherwise, false.</returns>
        public bool Equals(Latitude value)
        {
            return _Degrees.Equals(value.DecimalDegrees);
        }
        /// <summary>
        /// Compares the current instance to the specified latitude.
        /// </summary>
        public int CompareTo(Latitude value)
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
        /// Returns Latitude as decimal fractions
        /// </summary>
        public double DecimalDegrees
        {
            get
            {
                return _Degrees;
            }
        }
        /// <summary>
        /// Serves as a hash function for a Latitude object.
        /// </summary>
        /// <returns>A hash code for the current Latitude.</returns>
        public override int GetHashCode()
        {
            return _Degrees.GetHashCode();
        }
        /// <summary>
        /// Returns a System.String that represents the current Latitude.
        /// </summary>
        /// <returns>A System.String that represents the current Latitude.</returns>
        public override string ToString()
        {
            if (!IsValid)
            {
                return "NaN";
            }
            //string format = "HH°MM'SS.SSSS\"i";

            string hours = Hours.ToString("00");
            string minutes = Minutes.ToString("00");
            string seconds = Seconds.ToString("0.00");
            string result = hours + "°" + minutes + "'" + seconds + "\"" + Hemisphere.ToString().Substring(0, 1);
            return result;
           
        }
        
    }
}
