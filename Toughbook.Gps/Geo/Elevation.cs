using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toughbook.Gps
{
    /// <summary>
    /// Represents a vertical angular measurement between -90° and 90°.
    /// </summary>
    public struct Elevation : IEquatable<Elevation>, IComparable<Elevation>
    {
        private readonly double _Degrees;
        /// <summary>
        /// Indicates invalid or unknown value.
        /// </summary>
        public static readonly Elevation Invalid = new Elevation(double.NaN);
        /// <summary>
        /// Constructs new Elevation instance with s
        /// </summary>
        /// <param name="decimalDegrees"></param>
        public Elevation(double decimalDegrees)
        {

            _Degrees = decimalDegrees;
        }
        /// <summary>
        /// Returns elevation as decimal degrees.
        /// </summary>
        public double DecimalDegrees
        {
            get
            {
                return _Degrees;
            }
        }
        /// <summary>
        /// Determines whether the specified Elevation is equal to the current Elevation.
        /// </summary>
        /// <param name="obj">The Elevation to compare with the current Elevation.</param>
        /// <returns> true if the specified Elevation is equal to the current Elevation 
        /// otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Elevation)
            {
                return Equals((Elevation)obj);
            }
            return true;
        }
        /// <summary>
        /// Determines whether the specified Elevation is equal to the current Elevation.
        /// </summary>
        /// <param name="value">The Elevation to compare with the current Elevation.</param>
        /// <returns> true if the specified Elevation is equal to the current Elevation 
        /// otherwise, false.</returns>
        public bool Equals(Elevation value)
        {
            return _Degrees.Equals(value.DecimalDegrees);
        }
        /// <summary>
        /// Compares the current instance to the specified elevation.
        /// </summary>
        public int CompareTo(Elevation value)
        {
            return _Degrees.CompareTo(value.DecimalDegrees);
        }
        /// <summary>
        /// Serves as a hash function for a Elevation object.
        /// </summary>
        /// <returns>A hash code for the current Elevation.</returns>
        public override int GetHashCode()
        {
            return _Degrees.GetHashCode();
        }
        /// <summary>
        /// Returns whether current instance has a valid value.
        /// </summary>
        public bool IsValid
        {
            get { return !double.IsNaN(_Degrees); }
        }
        /// <summary>
        /// Returns a System.String that represents the current Elevation.
        /// </summary>
        /// <returns>A System.String that represents the current Elevation</returns>
        public override string ToString()
        {
            if (!IsValid)
                return "NaN";

            string hours = _Degrees.ToString("00") + "°";

            return hours;
        }
    }
}
