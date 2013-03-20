using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toughbook.Gps
{
    /// <summary>
    /// Represents GPS Satellite in orbit.
    /// </summary>
    public struct Satellite : IEquatable<Satellite>
    {
        private int _PseudoRandomCode;
        private bool _IsFixed;
        private Azimuth _Azimuth;
        private Elevation _Elevation;
        private int _SignalToNoiseRatio;
        /// <summary>
        /// Contructs new Satellite instance with specified values
        /// </summary>
        /// <param name="pseudoRandomCode">Unique satellite identifier.</param>
        /// <param name="azimuth">Azimuth of Satellite</param>
        /// <param name="elevation">Elevation of Satellite</param>
        /// <param name="signalToNoiseRatio">Signal to noise ratio or signal strength of satellite</param>
        public Satellite(int pseudoRandomCode, Azimuth azimuth, Elevation elevation, int signalToNoiseRatio)
        {
            _PseudoRandomCode = pseudoRandomCode;
            _Azimuth = azimuth;
            _Elevation = elevation;
            _IsFixed = false;
            _SignalToNoiseRatio = signalToNoiseRatio;
        }
        /// <summary>
        /// Constructs new Satellite instance with specified unqiue identifier.
        /// </summary>
        /// <param name="pseudoRandomCode">Unique satellite identifier</param>
        public Satellite(int pseudoRandomCode)
            : this(pseudoRandomCode, new Azimuth(0.0), new Elevation(0.0), 0)
        {
        }
        /// <summary>
        /// Serves as a hash function for a Satellite object.
        /// </summary>
        /// <returns>A hash code for the current Satellite.</returns>
        public override int GetHashCode()
        {
            return _PseudoRandomCode.GetHashCode();
        }
        /// <summary>
        /// Indicates whether satellite is used to obtain a fix.
        /// </summary>
        public bool IsFixed
        {
            get
            {
                return _IsFixed;
            }
            set
            {
                _IsFixed = value;
            }
        }
        /// <summary>
        /// Returns unique idenitifer of Satellite
        /// </summary>
        public int PseudoRandomCode
        {
            get
            {
                return _PseudoRandomCode;
            }
        }
        /// <summary>
        /// Returns Satellite Azimuth.
        /// </summary>
        public Azimuth Azimuth
        {
            get
            {
                return _Azimuth;
            }
        }
        /// <summary>
        /// Returns Satellite Elevation.
        /// </summary>        
        public Elevation Elevation
        {
            get
            {
                return _Elevation;
            }
        }
        /// <summary>
        /// Returns Satellite Signal to Noise Ratio (SNR).
        /// </summary>
        public int SignalToNoiseRatio
        {
            get
            {
                return _SignalToNoiseRatio;
            }
        }
        /// <summary>
        /// Determines whether the specified Satellite is equal to the current Satellite.
        /// </summary>
        /// <param name="obj">The Satellite to compare with the current Satellite.</param>
        /// <returns> true if the specified Satellite is equal to the current Satellite
        /// otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Satellite)
            {
                return Equals((Satellite)obj);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Determines whether the specified Satellite is equal to the current Satellite.
        /// </summary>
        /// <param name="value">The Satellite to compare with the current Satellite.</param>
        /// <returns> true if the specified Satellite is equal to the current Satellite
        /// otherwise, false.</returns>
        public bool Equals(Satellite value)
        {
            return _PseudoRandomCode.Equals(value.PseudoRandomCode);
        }
    }
}
