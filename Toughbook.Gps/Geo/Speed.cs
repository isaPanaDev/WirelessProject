using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toughbook.Gps
{
    /// <summary>
    /// Speed struct represents an objects rate of travel.
    /// </summary>
    public struct Speed : IEquatable<Speed>, IComparable<Speed>
    {
        private const double MPHToKnot = 0.8689762;
        private const double KPHToKnot = 0.5399568;

        private const double KnotsToMPH = 1.150779;
        private const double KPHToMPH = 0.6213712;

        private const double KnotsToKPH = 1.852;
        private const double MPHToKPH = 1.609344;

        private double _SpeedValue;
        private SpeedUnit _SpeedUnit;
        /// <summary>
        /// Indicates invalid or unknown value.
        /// </summary>
        public static readonly Speed Invalid = new Speed(double.NaN, SpeedUnit.MilesPerHour);
        /// <summary>
        /// Contructs new Speed instance.
        /// </summary>
        /// <param name="speed">Speed value.</param>
        /// <param name="speedUnit">Units in which the speed value is measured.</param>
        public Speed(double speed, SpeedUnit speedUnit)
        {
            _SpeedValue = speed;
            _SpeedUnit = speedUnit;
        }
        /// <summary>
        /// Returns units in which the speed value is mearsured.
        /// </summary>
        public SpeedUnit SpeedUnit
        {
            get
            {
                return _SpeedUnit;
            }
        }
        /// <summary>
        /// Returns converted Speed object in specified speed units.
        /// </summary>
        /// <param name="speedUnit">Units of speed to convert to.</param>
        /// <returns>Speed object in specified speed units.</returns>
        public Speed ToUnits(SpeedUnit speedUnit)
        {
            double speedValue = GetSpeed(speedUnit);
            return new Speed(speedValue, speedUnit);
        }
        /// <summary>
        /// Determines whether the specified Speed is equal to the current Speed.
        /// </summary>
        /// <param name="value">The Latitude to compare with the current Speed.</param>
        /// <returns> true if the specified Latitude is equal to the current Speed 
        /// otherwise, false.</returns>
        public bool Equals(Speed value)
        {
            return _SpeedValue.Equals(value.GetSpeed(SpeedUnit));
        }
        /// <summary>
        /// Compares the current instance to the specified speed.
        /// </summary>
        public int CompareTo(Speed speed)
        {
            return _SpeedValue.CompareTo(speed.GetSpeed(SpeedUnit));
        }
        /// <summary>
        /// Determines whether the specified Speed is equal to the current Speed.
        /// </summary>
        /// <param name="obj">The Speed to compare with the current Speed.</param>
        /// <returns> true if the specified Speed is equal to the current Speed 
        /// otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Speed)
            {
                return Equals((Speed)obj);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Serves as a hash function for a Speed object.
        /// </summary>
        /// <returns>A hash code for the current Speed.</returns>
        public override int GetHashCode()
        {
            return this.GetSpeed(SpeedUnit.MilesPerHour).GetHashCode();
        }
        /// <summary>
        /// Returns whether current instance has a valid value.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return !double.IsNaN(_SpeedValue);
            }
        }
        /// <summary>
        /// Returns speed value in specified speed units
        /// </summary>
        /// <param name="speedUnit">Units in which speed value should be measured.</param>
        /// <returns>Speed value</returns>
        public double GetSpeed(SpeedUnit speedUnit)
        {
            if (speedUnit == _SpeedUnit)
            {
                return _SpeedValue;
            }
            else
            {
                if (SpeedUnit == SpeedUnit.Knots)
                {
                    switch (speedUnit)
                    {
                        case SpeedUnit.MilesPerHour:
                            return _SpeedValue * MPHToKnot;
                        case SpeedUnit.KilometerPerHour:
                            return _SpeedValue * KPHToKnot;
                        default:
                            return 0.0;
                    }
                }
                else if (SpeedUnit == SpeedUnit.MilesPerHour)
                {
                    switch (speedUnit)
                    {
                        case SpeedUnit.Knots:
                            return _SpeedValue * KnotsToMPH;
                        case SpeedUnit.KilometerPerHour:
                            return _SpeedValue * KPHToMPH;
                        default:
                            return 0.0;
                    }
                }
                else if (SpeedUnit == SpeedUnit.KilometerPerHour)
                {
                    switch (speedUnit)
                    {
                        case SpeedUnit.MilesPerHour:
                            return _SpeedValue * MPHToKPH;
                        case SpeedUnit.Knots:
                            return _SpeedValue * KnotsToKPH;
                        default:
                            return 0.0;
                    }
                }
                else
                {
                    return 0.0;
                }
            }
        }
        /// <summary>
        /// Returns a System.String that represents the current Speed.
        /// </summary>
        /// <returns>A System.String that represents the current Speed.</returns>
        public override string ToString()
        {
            if (!IsValid)
            {
                return "NaN";
            }
            string speedStr = _SpeedValue.ToString("##0.00");
            string units = "";
            switch (SpeedUnit)
            {
                case SpeedUnit.KilometerPerHour:
                    units = "km/h";
                    break;
                case SpeedUnit.MilesPerHour:
                    units = "mph";
                    break;
                case SpeedUnit.Knots:
                    units = "kts";
                    break;
                default:
                    units = "";
                    break;

            }
            speedStr += " " + units;
            return speedStr;
        }
    }
}
