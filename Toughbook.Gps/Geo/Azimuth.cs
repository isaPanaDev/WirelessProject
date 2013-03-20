using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toughbook.Gps
{
    /// <summary>
    /// Class represents a horizontal angle measured clockwise from a North base line or meridian
    /// </summary>
    public struct Azimuth : IEquatable<Azimuth>, IComparable<Azimuth>
    {
        private readonly double _Degrees;
        /// <summary>
        /// Indicates invalid or unknown value.
        /// </summary>
        public static readonly Azimuth Invalid = new Azimuth(double.NaN);
        /// <summary>
        /// Constructs new instance of Azimuth struct with specified decimal degree
        /// </summary>
        /// <param name="decimalDegrees">A double containing decimal degree</param>
        public Azimuth(double decimalDegrees)
        {
         
            _Degrees = decimalDegrees;
        }
        /// <summary>
        /// Returns azimuth as decimal degrees
        /// </summary>
        public double DecimalDegrees
        {
            get
            {
                return _Degrees;
            }
        }
        /// <summary>
        /// Returns direction of as expressed on a compass 
        /// </summary>
        public Direction Direction
        {
            get
            {
                if ((_Degrees >= (360 - 11.25) & _Degrees < 360) || (_Degrees >= 0 & _Degrees <= (0 + 11.25))) // North
                    return Direction.North;
                else if (_Degrees >= (22.5 - 11.25) & _Degrees < (22.5 + 11.25)) 
                    return Direction.NorthNortheast;
                else if (_Degrees >= (45 - 11.25) & _Degrees < (45 + 11.25)) 
                    return Direction.Northeast;
                else if (_Degrees >= (67.5 - 11.25) & _Degrees < (67.5 + 11.25)) 
                    return Direction.EastNortheast;
                else if (_Degrees >= (90 - 11.25) & _Degrees < (90 + 11.25)) 
                    return Direction.East;
                else if (_Degrees >= (112.5 - 11.25) & _Degrees < (112.5 + 11.25)) 
                    return Direction.EastSoutheast;
                else if (_Degrees >= (135 - 11.25) & _Degrees < (135 + 11.25)) 
                    return Direction.Southeast;
                else if (_Degrees >= (157.5 - 11.25) & _Degrees < (157.5 + 11.25)) 
                    return Direction.SouthSoutheast;
                else if (_Degrees >= (180 - 11.25) & _Degrees < (180 + 11.25)) 
                    return Direction.South;
                else if (_Degrees >= (202.5 - 11.25) & _Degrees < (202.5 + 11.25)) 
                    return Direction.SouthSouthwest;
                else if (_Degrees >= (225 - 11.25) & _Degrees < (225 + 11.25)) 
                    return Direction.Southwest;
                else if (_Degrees >= (247.5 - 11.25) & _Degrees < (247.5 + 11.25)) 
                    return Direction.WestSouthwest;
                else if (_Degrees >= (270 - 11.25) & _Degrees < (270 + 11.25)) 
                    return Direction.West;
                else if (_Degrees >= (292.5 - 11.25) & _Degrees < (292.5 + 11.25)) 
                    return Direction.WestNorthwest;
                else if (_Degrees >= (315 - 11.25) & _Degrees < (315 + 11.25)) 
                    return Direction.Northwest;
                else if (_Degrees >= (337.5 - 11.25) & _Degrees < (337.5 + 11.25)) 
                    return Direction.NorthNorthwest;
                return 0;
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
                return Math.Round((Math.Abs(_Degrees - Hours) * (double)(60.0 - Minutes)) * 60.0, 13 - 4);
            }
        }
        /// <summary>
        /// Returns whether current instance has a valid value.
        /// </summary>
        public bool IsValid
        {
            get { return !double.IsNaN(_Degrees); }
        }
        #region Override
        /// <summary>
        /// Determines whether the specified Azimuth is equal to the current Azimuth.
        /// </summary>
        /// <param name="obj">The Azimuth to compare with the current Azimuth.</param>
        /// <returns> true if the specified Azimuth is equal to the current Azimuth 
        /// otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Azimuth)
            {
                return Equals((Azimuth)obj);
            }
            return true;
        }
       
        /// <summary>
        /// Compares the value of this instance to a specified Azimuth value
        /// and returns an integer that indicates this instance sort order.
        /// </summary>
        /// <param name="value">An Azimuth object to compare.</param>
        /// <returns>A value of -1, 0, or 1</returns>
        public int CompareTo(Azimuth value)
        {
            return _Degrees.CompareTo(value.DecimalDegrees);
        }
        /// <summary>
        /// Determines whether the specified Azimuth is equal to the current Azimuth.
        /// </summary>
        /// <param name="azimuth">The Azimuth to compare with the current Azimuth.</param>
        /// <returns> true if the specified Azimuth is equal to the current Azimuth 
        /// otherwise, false.</returns>
        public bool Equals(Azimuth azimuth)
        {
            return _Degrees.Equals(azimuth._Degrees);
        }
        /// <summary>
        /// Serves as a hash function for a Azimuth object.
        /// </summary>
        /// <returns>A hash code for the current Azimuth.</returns>
        public override int GetHashCode()
        {
            return _Degrees.GetHashCode();
        }
        /// <summary>
        /// Returns a System.String that represents the current Azimuth.
        /// </summary>
        /// <returns>A System.String that represents the current Azimuth</returns>
        public override string ToString()
        {
            if (!IsValid)
                return "NaN";

            string hours = Hours.ToString("0.0000") + "°";
            string direction = "";
            switch (Direction)
            {
                case Direction.North:
                    direction = "N";
                    break;
                case Direction.NorthNortheast:
                    direction ="NNE";
                    break;
                case Direction.Northeast:
                    direction = "NE";
                    break;
                case Direction.EastNortheast:
                   direction = "ENE";
                    break;
                case Direction.East:
                    direction = "E";
                    break;
                case Direction.EastSoutheast:
                    direction = "ESE";
                    break;
                case Direction.Southeast:
                    direction = "SE";
                    break;
                case Direction.SouthSoutheast:
                    direction = "SSE";
                    break;
                case Direction.South:
                    direction = "S";
                    break;
                case Direction.SouthSouthwest:
                    direction = "SSW";
                    break;
                case Direction.Southwest:
                    direction = "SW";
                    break;
                case Direction.WestSouthwest:
                    direction = "WSW";
                    break;
                case Direction.West:
                    direction = "W";
                    break;
                case Direction.WestNorthwest:
                    direction = "WNW";
                    break;
                case Direction.Northwest:
                    direction = "NW";
                    break;
                case Direction.NorthNorthwest:
                    direction =  "NNW";
                    break;
            }
            return hours + direction;
        }
        #endregion
    }
    
}
