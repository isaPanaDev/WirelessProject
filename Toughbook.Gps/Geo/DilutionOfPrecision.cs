using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toughbook.Gps
{
    /// <summary>
    /// DilutionOfPrecision represents degree to which position is by dilution of position.
    /// Lower numbers indicate a more accurate position. A value of 1.0 indicates the least dilution (highest accurary);
    /// A value of 50.0 indicates the most dilution (lowest accuracy).
    /// </summary>
    public struct DilutionOfPrecision : IEquatable<DilutionOfPrecision>
    {
        private readonly float _Value;
        /// <summary>
        /// Indicates invalid or unknown value.
        /// </summary>
        public static readonly DilutionOfPrecision InValid = new DilutionOfPrecision(float.NaN);
        /// <summary>
        /// Construct new DilutionOfPrecision.
        /// </summary>
        /// <param name="value">Precision value.</param>
        public DilutionOfPrecision(float value)
        {
            _Value = value > 50 ? 50 : value;
        }
        /// <summary>
        /// Returns precision value.
        /// </summary>
        public float PrecisionValue
        {
            get
            {
                return _Value;
            }
        }
        /// <summary>
        /// Returns rating translated from precision value.
        /// </summary>
        public PrecisionRating PrecisionRating
        {
            get
            {
                if (_Value == float.NaN)
                    return PrecisionRating.Unknown;

                if (_Value > 0.0f && _Value <= 1.0f)
                {
                    return PrecisionRating.Ideal;
                }
                else if ( _Value <= 3.0f)
                {
                    return PrecisionRating.Excellent;
                }
                else if ( _Value <= 6.0f)
                {
                    return PrecisionRating.Good;
                }
                else if (_Value <= 8.0f)
                {
                    return PrecisionRating.Moderate;
                }
                else if ( _Value <= 20f)
                {
                    return PrecisionRating.Fair;
                }
                else
                {
                    return PrecisionRating.Poor;
                }
            }
        }
        /// <summary>
        /// Returns whether current instance has a valid value.
        /// </summary>
        public bool isValid
        {
            get
            {
                return !_Value.Equals(float.NaN);
            }
        }
        /// <summary>
        /// Determines whether the specified DilutionOfPrecision is equal to the current DilutionOfPrecision.
        /// </summary>
        /// <param name="value">The DilutionOfPrecision to compare with the current DilutionOfPrecision.</param>
        /// <returns> true if the specified DilutionOfPrecision is equal to the current DilutionOfPrecision
        /// otherwise, false.</returns>
        public bool Equals(DilutionOfPrecision value)
        {
            return _Value == value.PrecisionValue;
        }
        /// <summary>
        /// Determines whether the specified DilutionOfPrecision is equal to the current DilutionOfPrecision.
        /// </summary>
        /// <param name="obj">The DilutionOfPrecision to compare with the current DilutionOfPrecision.</param>
        /// <returns> true if the specified DilutionOfPrecision is equal to the current DilutionOfPrecision
        /// otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is DilutionOfPrecision)
            {
                return Equals((DilutionOfPrecision)obj);
            }
            else
                return false;
        }
        /// <summary>
        /// Serves as a hash function for a DilutionOfPrecision object.
        /// </summary>
        /// <returns>A hash code for the current DilutionOfPrecision.</returns>
        public override int GetHashCode()
        {
            return _Value.GetHashCode();
        }
        /// <summary>
        /// Returns a System.String that represents the current DilutionOfPrecision.
        /// </summary>
        /// <returns>A System.String that represents the current DilutionOfPrecision.</returns>
        public override string ToString()
        {
            if (!isValid)
            {
                return "NaN";
            }
            return _Value.ToString("0.0") + " " + PrecisionRating.ToString();
        }
    }
}
