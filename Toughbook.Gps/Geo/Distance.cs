using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toughbook.Gps
{
    /// <summary>
    /// Distance indicates measurement of length between two points.
    /// </summary>
    public struct Distance : IEquatable<Distance>, IComparable<Distance>
    {
        private readonly double _DistanceValue;
        private DistanceUnit _DistanceUnits;

        private const double KilometersPerMeter = 0.001;
        private const double KilometersPerStatuteMile = 1.609344;
        private const double KilometersPerFoot = 0.0003048;

        private const double MetersPerStatuteMile = 1609.344;
        private const double MetersPerKilometer = 1000;
        private const double MetersPerFoot = 0.3048;

        private const double StatuteMilesPerMeter = 0.000621371192;
        private const double StatuteMilesPerKilometer = 0.621371192;
        private const double StatuteMilesPerFoot = 0.000189393939;

        private const double FeetPerMeter = 3.2808399;
        private const double FeetPerStatuteMile = 5280;
        private const double FeetPerKilometer = 3280.8399;

        /// <summary>
        /// Indicates invalid or unknown value.
        /// </summary>
        public static readonly Distance Invalid = new Distance(double.NaN, DistanceUnit.Meters);
        /// <summary>
        /// Contructs new Distance instance.
        /// </summary>
        /// <param name="distance">Distance value.</param>
        /// <param name="unit">Units in which the distance value is measured.</param>
        public Distance(double distance, DistanceUnit unit)
        {
            _DistanceUnits = unit;
            _DistanceValue = distance;
        }
        /// <summary>
        /// Returns distance value.
        /// </summary>
        public double Value
        {
            get
            {
                return _DistanceValue;
            }
        }
        /// <summary>
        /// Returns whether current instance has a valid value.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return !double.IsNaN(_DistanceValue);
            }
        }
        /// <summary>
        /// Determines whether the specified Distance is equal to the current Distance.
        /// </summary>
        /// <param name="obj">The Distance to compare with the current Distance.</param>
        /// <returns> true if the specified Distance is equal to the current Distance
        /// otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Distance)
            {
                return Equals((Distance)obj);
            }
            else
                return false;
        }
        /// <summary>
        /// Returns units in which the distance value is mearsured in.
        /// </summary>
        public DistanceUnit Units
        {
            get
            {
                return _DistanceUnits;
            }
        }
        private Distance ToKilometers()
        {
            switch (_DistanceUnits)
            {
                case DistanceUnit.Meters:
                    return new Distance(_DistanceValue * KilometersPerMeter, DistanceUnit.Kilometers);

                case DistanceUnit.Miles:
                    return new Distance(_DistanceValue * KilometersPerStatuteMile, DistanceUnit.Kilometers);
                case DistanceUnit.Feet:
                    return new Distance(_DistanceValue * KilometersPerFoot, DistanceUnit.Kilometers);
                default:
                    return new Distance(0, DistanceUnit.Kilometers);
            }
        }
        private Distance ToMeters()
        {
            switch (_DistanceUnits)
            {
                case DistanceUnit.Kilometers:
                    return new Distance(_DistanceValue * MetersPerKilometer, DistanceUnit.Meters);
                case DistanceUnit.Miles:
                    return new Distance(_DistanceValue * MetersPerStatuteMile, DistanceUnit.Meters);
                case DistanceUnit.Feet:
                    return new Distance(_DistanceValue * MetersPerFoot, DistanceUnit.Meters);
                default:
                    return new Distance(0, DistanceUnit.Meters);
            }
        }
        private Distance ToMiles()
        {
            switch (_DistanceUnits)
            {
                case DistanceUnit.Meters:
                    return new Distance(_DistanceValue * StatuteMilesPerMeter, DistanceUnit.Miles);
                case DistanceUnit.Kilometers:
                    return new Distance(_DistanceValue * StatuteMilesPerKilometer, DistanceUnit.Miles);
                case DistanceUnit.Feet:
                    return new Distance(_DistanceValue * StatuteMilesPerFoot, DistanceUnit.Miles);
                default:
                    return new Distance(0, DistanceUnit.Miles);
            }
        }
        private Distance ToFeet()
        {
            switch (_DistanceUnits)
            {
                case DistanceUnit.Meters:
                    return new Distance(_DistanceValue * FeetPerMeter, DistanceUnit.Feet);
                case DistanceUnit.Kilometers:
                    return new Distance(_DistanceValue * FeetPerKilometer, DistanceUnit.Feet);
                case DistanceUnit.Miles:
                    return new Distance(_DistanceValue * FeetPerStatuteMile, DistanceUnit.Feet);
                default:
                    return new Distance(0, DistanceUnit.Miles);
            }
        }
        /// <summary>
        /// Returns converted Distance object in specified distance/length units.
        /// </summary>
        /// <param name="unit">Units of length to convert to.</param>
        /// <returns>Distance object in specified distance units.</returns>
        public Distance ToUnits(DistanceUnit unit)
        {
            if (_DistanceUnits == unit)
            {
                return new Distance(_DistanceValue, _DistanceUnits);
            }
            else if (DistanceUnit.Kilometers == unit)
            {
                return ToKilometers();
            }
            else if (DistanceUnit.Meters == unit)
            {
                return ToMeters();
            }
            else if (DistanceUnit.Miles == unit)
            {
                return ToMiles();
            }
            else if (DistanceUnit.Feet == unit)
            {
                return ToFeet();
            }
            else
                throw new ArgumentException(String.Format("Invalid argument {0}", unit));
        }
        /// <summary>
        /// Determines whether the specified Distance is equal to the current Distance.
        /// </summary>
        /// <param name="value">The Distance to compare with the current Distance.</param>
        /// <returns> true if the specified Distance is equal to the current Distance
        /// otherwise, false.</returns>
        public bool Equals(Distance value)
        {
            return _DistanceValue.Equals(value.ToUnits(Units).Value);
        }
        /// <summary>
        /// Serves as a hash function for a Distance object.
        /// </summary>
        /// <returns>A hash code for the current Distance.</returns>
        public override int GetHashCode()
        {
            return _DistanceValue.GetHashCode();
        }
        /// <summary>
        /// Compares the current instance to the specified distance.
        /// </summary>
        public int CompareTo(Distance value)
        {
            return _DistanceValue.CompareTo(value.ToUnits(Units).Value);
        }
        /// <summary>
        /// Returns a System.String that represents the current Distance.
        /// </summary>
        /// <returns>A System.String that represents the current Distance.</returns>
        public override string ToString()
        {
            if (!IsValid)
            {
                return "NaN";
            }
            switch (_DistanceUnits)
            {
                case DistanceUnit.Kilometers:
                    return _DistanceValue.ToString("0.00") + "km";
                case DistanceUnit.Meters:
                    return  _DistanceValue.ToString("0.00") + "m";
                case DistanceUnit.Miles:
                    return  _DistanceValue.ToString("0.00") + "mi";
                case DistanceUnit.Feet:
                    return _DistanceValue.ToString("0.00") + "ft";
                default:
                    return _DistanceValue.ToString("0.00");
            }
        }
    }
}
