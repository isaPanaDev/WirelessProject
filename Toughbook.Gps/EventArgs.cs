using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toughbook.Gps
{
    /// <summary>
    /// Provides Cooridinate information to raised event
    /// </summary>
    public class CoordinateEventArgs : EventArgs
    {
        private Coordinate _Coordinate;
        /// <summary>
        /// Contructs new instance of CoordinateEventArgs with specified Coordinate
        /// </summary>
        /// <param name="coordinate">Coordinate argument</param>
        public CoordinateEventArgs(Coordinate coordinate)
        {
            _Coordinate = coordinate;
        }
        /// <summary>
        /// rovides Cooridinate information to raised event
        /// </summary>
        public Coordinate Coordinate
        {
            get
            {
                return _Coordinate;
            }
        }
    }
    /// <summary>
    /// Provides magnetic variation to raised event
    /// </summary>
    public class MagneticVariationEventArgs : EventArgs
    {
        private Longitude _MagneticVariation;
        /// <summary>
        /// Constructs new MagneticVariationEventArgs with specified longitude.
        /// </summary>
        /// <param name="magneticVariation">Magnetic Variation.</param>
        public MagneticVariationEventArgs(Longitude magneticVariation)
        {
            _MagneticVariation = magneticVariation;
        }
        /// <summary>
        /// Returns longitude that represents magnetic variation.
        /// </summary>
        public Longitude MagneticVariation
        { 
            get { return _MagneticVariation; } 
        }
    }
    /// <summary>
    /// Provides Date-Time information to raised event
    /// </summary>
    public class DateTimeEventArgs : EventArgs
    {
        private DateTime _DataTime;
        /// <summary>
        /// Contructs new instance of DateTimeEventArgs with specified DateTime
        /// </summary>
        /// <param name="dataTime">DateTime argument</param>
        public DateTimeEventArgs(DateTime dataTime)
        {
            _DataTime = dataTime;
        }
        /// <summary>
        /// Returns DataTime agrument
        /// </summary>
        public DateTime DataTime
        {
            get
            {
                return _DataTime;
            }
        }
    }
    /// <summary>
    /// Provides speed information to raised event
    /// </summary>
    public class SpeedEventArgs : EventArgs
    {
        private Speed _Speed;
        /// <summary>
        /// Contructs new instance of SpeedEventArgs with specified Speed
        /// </summary>
        /// <param name="speed">Speed argument</param>
        public SpeedEventArgs(Speed speed)
        {
            _Speed = speed;
        }
        /// <summary>
        /// Returns Speed argument
        /// </summary>
        public Speed Speed
        {
            get
            {
                return _Speed;
            }
        }
    }
    /// <summary>
    /// Provides azimuth information to raised event
    /// </summary>
    public class AzimuthEventArgs : EventArgs
    {
        private Azimuth _Course;
        /// <summary>
        /// Contructs new instance of AzimuthEventArgs with specified Azimuth.
        /// </summary>
        /// <param name="azimuth">Azimuth argument.</param>
        public AzimuthEventArgs(Azimuth azimuth)
        {
            _Course = azimuth;
        }
        /// <summary>
        /// Returns Azimuth argument.
        /// </summary>
        public Azimuth Azimuth
        {
            get
            {
                return _Course;
            }
        }
    }
    /// <summary>
    /// Provides raw NMEA sentenceinformation to raised event
    /// </summary>
    public class NmeaSentenceEventArgs : EventArgs
    {
        private string _NmeaSentence;
        /// <summary>
        /// Contructs new instance of NmeaSentenceEventArgs with specified NMEA string
        /// </summary>
        /// <param name="nmeaSentence">String of raw NMEA data</param>
        public NmeaSentenceEventArgs(string nmeaSentence)
        {
            _NmeaSentence = nmeaSentence;
        }
        /// <summary>
        /// Returns string with raw NMEA data
        /// </summary>
        public string NmeaSentence
        {
            get {return _NmeaSentence;}
        }
    }
    /// <summary>
    /// Provides satellite information to raised event
    /// </summary>
    public class SatellitesEventArgs : EventArgs
    {
        private List<Satellite> _Satellites;
        /// <summary>
        /// Construct new SatellitesEventArgs instance with specified list of Satellites
        /// </summary>
        /// <param name="satellitesList">list of Satellites argument</param>
        public SatellitesEventArgs(List<Satellite> satellitesList)
        {
            _Satellites = satellitesList;
        }
        /// <summary>
        /// Returns list of Satellites
        /// </summary>
        public List<Satellite> Satellites
        {
            get
            {
                return _Satellites;
            }
        }
    }
    /// <summary>
    /// Provides Fix quality to raised event.
    /// </summary>
    public class FixQualityEventArgs : EventArgs
    {
        private FixQuality _FixQuality;
        /// <summary>
        /// Contructs new instance of FixQualityEventArgs with specified FixQuality.
        /// </summary>
        /// <param name="fixQuality">FixQuality argument.</param>
        public FixQualityEventArgs(FixQuality fixQuality)
        {
            _FixQuality = fixQuality;
        }
        /// <summary>
        /// Returns fix quality.
        /// </summary>
        public FixQuality FixQuality
        {
            get
            {
                return _FixQuality;
            }
        }
    }
    /// <summary>
    /// Provides fix selection to raised event.
    /// </summary>
    public class FixSelectionEventArgs : EventArgs
    {
        private FixSelection _Selection;
        /// <summary>
        /// Contructs new instance of FixSelectionEventArgs  with specified FixSelection.
        /// </summary>
        /// <param name="selection">FixSelection argument.</param>
        public FixSelectionEventArgs(FixSelection selection)
        {
            _Selection = selection;
        }
        /// <summary>
        /// Returns FixSelection argument.
        /// </summary>
        public FixSelection SelectionTypeMode
        {
            get
            {
                return _Selection;
            }
        }
    }
    /// <summary>
    /// Provides Fix mode to raised event.
    /// </summary>
    public class FixModeEventArgs : EventArgs
    {
        private FixMode _FixMode;
        /// <summary>
        /// Contructs new instance of FixModeEventArgs with specified FixMode.
        /// </summary>
        /// <param name="fixMode">FixMode argurment.</param>
        public FixModeEventArgs(FixMode fixMode)
        {
            _FixMode = fixMode;
        }
        /// <summary>
        ///Returns fix mode.
        /// </summary>
        public FixMode FixMode
        {
            get
            {
                return _FixMode;
            }
        }
    }
    /// <summary>
    /// Provides dilution of precision to raised event.
    /// </summary>
    public class DilutionOfPrecisionEventArgs : EventArgs
    {
        private DilutionOfPrecision _GenericDilutionOfPrecision;
        /// <summary>
        /// Contructs new instance of DilutionOfPrecisionEventArgs with specified dilutionOfPrecision.
        /// </summary>
        /// <param name="dilutionOfPrecision">DilutionOfPrecision Argurment</param>
        public DilutionOfPrecisionEventArgs(DilutionOfPrecision dilutionOfPrecision)
        {
            _GenericDilutionOfPrecision = dilutionOfPrecision;
        }
        /// <summary>
        /// Returns DilutionOfPrecision Argurment.
        /// </summary>
        public DilutionOfPrecision DilutionOfPrecision
        {
            get
            {
                return _GenericDilutionOfPrecision;
            }
        }
    }

    /// <summary>
    /// Provides Altitude information to raised event
    /// </summary>
    public class AltitudeEventArgs : EventArgs
    {
        private Distance _Altitude;
        /// <summary>
        /// Contructs new instance of AltitudeEventArgs with specified Altitude.
        /// </summary>
        /// <param name="altitude">Altitude argument.</param>
        public AltitudeEventArgs(Distance altitude)
        {
            _Altitude = altitude;
        }
        /// <summary>
        ///  Returns altitude as distance.
        /// </summary>
        public Distance Altitude
        {
            get
            {
                return _Altitude;
            }
        }
    }
}
