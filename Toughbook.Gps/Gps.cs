using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Toughbook.Gps
{
    /// <summary>
    /// Gps object parses NMEA data and converts them to meaningful objects and events.
    /// </summary>
    public class Gps : IDisposable
    {
        #region Private variables
        private Device _Device = null;
        private NmeaReader _NmeaReader = null;
        private Thread _nmeaParsingThread = null;
        private bool _isStarted = false;
        private object _syncRoot = new Object();
        private static TimeSpan _CommandTimeout = TimeSpan.FromSeconds(5);
        private bool _FixNeeded = true;
        private FixStatus _FixStatus;
        private FixMode _FixMode;
        private FixQuality _FixQuality;
        private FixSelection _SelectionType;
        private DateTime _UtcDataTime;
        private Coordinate _Coordinate;
        private Speed _Speed;
        private Azimuth _Heading;
        private Distance _Altitude;
        private Longitude _MagneticVariation;
        private DilutionOfPrecision _HDOP;
        private DilutionOfPrecision _VDOP;
        private DilutionOfPrecision _PDOP;
        private int _SatellitesInView;
        private List<Satellite> _Satellites = new List<Satellite>(25);
        #endregion

        #region Constructor
        /// <summary>
        /// Constructs a new GPS instance
        /// </summary>
        public Gps()
        {
            bool IsPanasonicMachine = ModelChecker.IsPanasonic;
            if (!IsPanasonicMachine)
            {
                throw new GpsException("Unsupported PC Model");
            }
            Initialize();
        }
        #endregion

        #region Protected variables
        /// <summary>
        /// Indicates whether GPS instance has been disposed or not.
        /// </summary>
        protected bool _IsDisposed = false;
        #endregion

        #region Events
        /// <summary>
        /// Event notifies that GPS module has been open and parsing will begin.
        /// </summary>
        public event EventHandler DeviceOpened;
        /// <summary>
        /// Event notifies that GPS connection has been closed and parsing thread has ended.
        /// </summary>
        public event EventHandler DeviceClosed;
        /// <summary>
        /// Event notifies that GPS module has acquired a fix.
        /// </summary>
        public event EventHandler FixObtained;
        /// <summary>
        /// Event notifies that GPS module has lost fix.
        /// </summary>
        public event EventHandler FixLost;
        /// <summary>
        /// Event notifies that GPS module fix quality has changed.
        /// </summary>
        public event EventHandler<FixQualityEventArgs> FixQualityChanged;
        /// <summary>
        /// Event notifies that GPS module fix mode has changed.
        /// </summary>
        public event EventHandler<FixModeEventArgs> FixModeChanged;
        /// <summary>
        /// Event notifies that GPS module changed operating modes.
        /// </summary>
        public event EventHandler<FixSelectionEventArgs> FixSelectionChanged;
        /// <summary>
        /// Event notifies that latitude or longitude has changed.
        /// </summary>
        public event EventHandler<CoordinateEventArgs> CoordinateChanged;
        /// <summary>
        /// Event notifies that latitude or longitude has been received.
        /// </summary>
        public event EventHandler<CoordinateEventArgs> CoordianteReceived;
        /// <summary>
        /// Event notifies that magnetic variation has changed.
        /// </summary>
        public event EventHandler<MagneticVariationEventArgs> MagneticVariationChanged;
        /// <summary>
        /// Event notifies that altitude has changed.
        /// </summary>
        public event EventHandler<AltitudeEventArgs> AltitudeChanged;
        /// <summary>
        /// Event notifies that altitude has received.
        /// </summary>
        public event EventHandler<AltitudeEventArgs> AltitudeReceived;
        /// <summary>
        /// Event notifies that date-time information has changed.
        /// </summary>
        public event EventHandler<DateTimeEventArgs> DataTimeChanged;
        /// <summary>
        /// Event notifies that speed has changed.
        /// </summary>
        public event EventHandler<SpeedEventArgs> SpeedChanged;
        /// <summary>
        /// Event notifies that speed has been received.
        /// </summary>
        public event EventHandler<SpeedEventArgs> SpeedReceived;
        /// <summary>
        /// Event notifies that direction of travel has changed.
        /// </summary>
        public event EventHandler<AzimuthEventArgs> CourseChanged;
        /// <summary>
        /// Event notifies that direction of travel has been received.
        /// </summary>
        public event EventHandler<AzimuthEventArgs> CourseReceived;
        /// <summary>
        /// Events notifies that nmea sentences has been received from GPS module.
        /// </summary>
        public event EventHandler<NmeaSentenceEventArgs> NmeaSentenceReceived;
        /// <summary>
        /// Event notifies that Satellites properties has changed.
        /// </summary>
        public event EventHandler<SatellitesEventArgs> SatellitesChanged;
        /// <summary>
        /// Event notifies that Mean/Postion Dilution of Precision has changed.
        /// </summary>
        public event EventHandler<DilutionOfPrecisionEventArgs> PositionDilutionOfPrecisionChanged;
        /// <summary>
        /// Event notifies that Vertical Dilution of Precision has changed.
        /// </summary>
        public event EventHandler<DilutionOfPrecisionEventArgs> VerticalDilutionOfPrecisionChanged;
        /// <summary>
        /// Event notifies that Horizontal Dilution of Precision has changed.
        /// </summary>
        public event EventHandler<DilutionOfPrecisionEventArgs> HorizontalDilutionOfPrecisionChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Returns if GPS module currently has a fix or not.
        /// </summary>
        public FixStatus FixStatus
        {
            get
            {
                return _FixStatus;
            }
        }
        /// <summary>
        /// Returns fix mode.
        /// </summary>
        public FixMode FixMode
        {
            get
            {
                return _FixMode;
            }
        }
        /// <summary>
        /// Returns quality of fix.
        /// </summary>
        public FixQuality FixQuality
        {
            get
            {
                return _FixQuality;
            }
        }
        /// <summary>
        /// Returns fix selection.
        /// </summary>
        public FixSelection FixSelection
        {
            get
            {
                return _SelectionType;
            }
        }
        /// <summary>
        /// Returns current position reported by GPS module.
        /// </summary>
        public Coordinate Coordinate
        {
            get
            {
                return _Coordinate;
            }
        }
        /// <summary>
        /// Returns current altitude in distance reported by GPS module.
        /// </summary>
        public Distance Altitude
        {
            get
            {
                return _Altitude;
            }
        }
        /// <summary>
        /// Returns the current UTC time.
        /// </summary>
        public DateTime DateTime
        {
            get
            {
                return _UtcDataTime;
            }
        }
        /// <summary>
        /// Return the current speed reported by GPS module.
        /// </summary>
        public Speed Speed
        {
            get
            {
                return _Speed;
            }
        }
        /// <summary>
        /// Returns the bearing or course over ground reported by GPS module..
        /// </summary>
        public Azimuth Course
        {
            get
            {
                return _Heading;
            }
        }
        /// <summary>
        /// Returns horizontal dilution of precision.
        /// </summary>
        public DilutionOfPrecision HorizontalDilutionOfPrecision
        {
            get
            {
                return _HDOP;
            }
        }
        /// <summary>
        /// Returns vertical dilution of precision.
        /// </summary>
        public DilutionOfPrecision VerticalDilutionOfPrecision
        {
            get
            {
                return _VDOP;
            }
        }
        /// <summary>
        /// Returns position dilution of precision.
        /// </summary>
        public DilutionOfPrecision PositionDilutionOfPrecision
        {
            get
            {
                return _PDOP;
            }
        }
        /// <summary>
        /// Returns longitude that represents magnetic variation.
        /// </summary>
        public Longitude MagneticVariation
        {
            get { return _MagneticVariation; }
        }
        #endregion

        #region Public Functions
        /// <summary>
        /// Connects to GPS module with default information of Toughbook model and begins interpretation of NMEA sentence data
        /// </summary>
        public void Open()
        {
            Open(Device.Default);
        }
        /// <summary>
        /// Returns whether GPS device is open
        /// </summary>
        /// <returns>If return true GPS device is open. If false GPS device is closed.</returns>
        public bool IsOpen()
        {
            return (_isStarted);
        }
        /// <summary>
        /// Connects to GPS module and begins interpretation of NMEA sentence data.
        /// </summary>
        /// <param name="device">Device or port that will provide NMEA data</param>
        public void Open(Device device)
        {
            if (_IsDisposed)
                throw new ObjectDisposedException("This Gps instance has already been disposed");
            
            if (device == null)
                throw new ArgumentNullException("Device argument is null");

            /*lock(_syncRoot)*/
            if (Monitor.TryEnter(_syncRoot))
            {
                if (_isStarted)
                    return;

                try
                {
                    if (_Device != null)
                    {
                        _Device.Dispose();
                        _Device = null;
                    }
                    _Device = device;
                    _Device.Open();

                    _NmeaReader = new NmeaReader(_Device.NmeaStream);
                    if (!_NmeaReader.isNmeaStream())
                        throw new System.IO.IOException("Device " + _Device.Name + " is not a valid GPS device");

                    if (DeviceOpened != null)
                        DeviceOpened(this, EventArgs.Empty);

                    _nmeaParsingThread = new Thread(new ThreadStart(ParsingThreadProc));
                    _nmeaParsingThread.IsBackground = true;
                    _nmeaParsingThread.Start();
                    _isStarted = true;

                }
                catch (System.Exception ex)
                {
                    try { _Device.Close(); }
                    catch { }
                    throw ex;
                }
                finally
                {
                    Monitor.Exit(_syncRoot);
                }
            }
            //else
                //throw new System.IO.IOException("GPS is already in the processing of opening.");
            
        }
        /// <summary>
        /// Returns device that is provide stream data to be interpreted.
        /// </summary>
        public Device Device
        {
            get
            {
                return _Device;
            }
        }
        /// <summary>
        /// Disconnect from GPS module and ends parsing thread.
        /// </summary>
        public void Close()
        {
            if (_IsDisposed)
                throw new ObjectDisposedException("This Gps instance has already been disposed");
            
            if (!_isStarted)
                return;
            if (Monitor.TryEnter(_syncRoot))
            {
                try
                {
                    _Device.Close();
                    _Device = null;
                    if (_nmeaParsingThread.IsAlive /*&& !_nmeaParsingThread.Join(_CommandTimeout)*/)
                    {
                        _nmeaParsingThread.Abort();
                    }

                }
                catch
                {

                }
                finally
                {
                    if (DeviceClosed != null)
                        DeviceClosed(this, EventArgs.Empty);

                    Initialize();
                    _isStarted = false;
                    Monitor.Exit(_syncRoot);
                }
            }
        }
        /// <summary>
        /// Disposes of unmanaged resources allocated by GPS instance.
        /// </summary>
        public void Dispose()
        {
            if (!_IsDisposed)
            {
                try
                {


                    if (_Device != null)
                    {
                        _Device.Close();
                        _Device.Dispose();
                    }
                    if (_nmeaParsingThread != null && _nmeaParsingThread.IsAlive)
                    {
                        _nmeaParsingThread.Abort();
                        //_nmeaParsingThread.Join();
                    }


                    _IsDisposed = true;

                }
                catch
                {
                }
                finally
                {
                    CoordianteReceived = null;
                    CoordinateChanged = null;
                    SpeedChanged = null;
                    SpeedReceived = null;
                    AltitudeChanged = null;
                    AltitudeReceived = null;
                    CourseChanged = null;
                    CourseReceived = null;
                    MagneticVariationChanged = null;
                    SatellitesChanged = null;
                    FixQualityChanged = null;
                    FixSelectionChanged = null;
                    FixLost = null;
                    FixObtained = null;
                    NmeaSentenceReceived = null;
                    DataTimeChanged = null;
                    HorizontalDilutionOfPrecisionChanged = null;
                    VerticalDilutionOfPrecisionChanged = null;
                    PositionDilutionOfPrecisionChanged = null;

                }
            }
        }
        /// <summary>
        /// Indicates whether GPS module currently has a fix
        /// </summary>
        public bool IsFixed
        {
            get { return _FixStatus == FixStatus.Fix; }
        }
        /// <summary>
        /// Whether fix is needed before GPS instance provides all data gathered.
        /// </summary>
        public bool FixNeeded
        {
            get
            {
                return _FixNeeded;
            }
            set
            {
                _FixNeeded = value;
            }
        }
        /// <summary>
        /// Indicates number of satellites in view.
        /// </summary>
        /// <returns>Returns number of satellites in view.</returns>
        public int SatellitesInView
        {
            get
            {
                return _SatellitesInView;
            }
        }
        #endregion

        #region Private Setters
        private void SetFixStatus(FixStatus fixStatus)
        {
            if (_FixStatus != fixStatus)
            {
                _FixStatus = fixStatus;
                if (_FixStatus == FixStatus.Fix)
                {
                    if (FixObtained != null)
                        FixObtained(this, EventArgs.Empty);
                }
                else
                {
                    if (FixLost != null)
                        FixLost(this, EventArgs.Empty);
                }
                _FixStatus = fixStatus;
            }
            
        }
        private void SetFixMode(FixMode fixMode)
        {
            if (_FixMode != fixMode)
            {
                _FixMode = fixMode;
                if (FixModeChanged != null)
                {
                    FixModeChanged(this, new FixModeEventArgs(_FixMode));
                }
            }
        }
        private void SetFixSelection(FixSelection selectionType)
        {
            if (_SelectionType != selectionType)
            {
                _SelectionType = selectionType;
                if (FixSelectionChanged != null)
                {
                    FixSelectionChanged(this, new FixSelectionEventArgs(_SelectionType));
                }
            }

        }
        private void SetCooridnate(Coordinate coordinate)
        {
            if (CoordianteReceived != null)
                CoordianteReceived(this, new CoordinateEventArgs(coordinate));

            if (!coordinate.Equals(_Coordinate))
            {
                _Coordinate = coordinate;
                if (CoordinateChanged != null)
                    CoordinateChanged(this, new CoordinateEventArgs(_Coordinate));
            }
        }
        private void SetMagneticVariation(Longitude magneticVariation)
        {
            if (!_MagneticVariation.Equals(magneticVariation))
            {
                _MagneticVariation = magneticVariation;
                if (MagneticVariationChanged != null)
                {
                    MagneticVariationChanged(this, new MagneticVariationEventArgs(_MagneticVariation));
                }
            }
        }
        private void SetSpeed(Speed speed)
        {
            
            if (SpeedReceived != null)
                SpeedReceived(this, new SpeedEventArgs(speed));

            if (!speed.Equals(_Speed))
            {
                _Speed = speed;
                if (SpeedChanged != null)
                    SpeedChanged(this, new SpeedEventArgs(_Speed));

            }
        } 
        private void SetAzimuth(Azimuth heading)
        {
            if (CourseReceived != null)
                CourseReceived(this, new AzimuthEventArgs(heading));

            if (!heading.Equals(_Heading))
            {
                _Heading = heading;

                if (CourseChanged != null)
                    CourseChanged(this, new AzimuthEventArgs(_Heading));

            }
        }
        private void SetDataTime(DateTime utcDateTime)
        {
            if (!utcDateTime.Equals(_UtcDataTime))
            {
                if (DataTimeChanged != null)
                {
                    _UtcDataTime = utcDateTime;
                    DataTimeChanged(this, new DateTimeEventArgs(utcDateTime));
                }
            }
        }
        private void SetFixQuality(FixQuality fixQuality)
        {
            if (fixQuality != _FixQuality)
            {
                _FixQuality = fixQuality;
                if (FixQualityChanged != null)
                {
                    FixQualityChanged(this, new FixQualityEventArgs(_FixQuality));
                }
            }
        }
        private void SetFixedSatellites(List<Satellite> fixedSatellites)
        {
            int satellitesInViewCount = _Satellites.Count;
            int fixedSatellitesCount = fixedSatellites.Count;
            bool hasChanged = false;
            for (int index = 0; index < satellitesInViewCount; ++index)
            {
                Satellite existing = _Satellites[index];
                bool isFixed = false;
                for (int fixedSatIndex = 0; fixedSatIndex  < fixedSatellitesCount; ++fixedSatIndex )
                {
                    Satellite fixedSatellite = fixedSatellites[fixedSatIndex];
                    if (existing.Equals(fixedSatellite))
                    {
                        isFixed = true;
                        break;
                    }
                }
                if (existing.IsFixed != isFixed)
                {
                    existing.IsFixed = isFixed;
                    _Satellites[index] = existing;
                    hasChanged = true;
                }
            }
            if (hasChanged && SatellitesChanged != null)
            {
                List<Satellite> list = new List<Satellite>(_Satellites);
                SatellitesChanged(this, new SatellitesEventArgs(list));
            }
        }
        private void SetAltitude(Distance value)
        {
            if (!_Altitude.Equals(value))
            {
                _Altitude = value;
                if (AltitudeChanged != null)
                    AltitudeChanged(this, new AltitudeEventArgs(_Altitude));

            }
            if (AltitudeReceived != null)
                AltitudeReceived(this, new AltitudeEventArgs(value));
        }
        private void SetVDilutionOfPrecision(DilutionOfPrecision value)
        {
            if (!_VDOP.Equals(value))
            {
                _VDOP = value;
                if (VerticalDilutionOfPrecisionChanged != null)
                {
                    VerticalDilutionOfPrecisionChanged(this, new DilutionOfPrecisionEventArgs(_VDOP));
                }

            }
        }
        private void SetHDilutionOfPrecision(DilutionOfPrecision value)
        {
            if (!_HDOP.Equals(value))
            {
                _HDOP = value;
                if (HorizontalDilutionOfPrecisionChanged != null)
                {
                    HorizontalDilutionOfPrecisionChanged(this, new DilutionOfPrecisionEventArgs(_HDOP));
                }

            }
        }
        private void SetPDilutionOfPrecision(DilutionOfPrecision value)
        {
            if (!_PDOP.Equals(value))
            {
                _PDOP = value;
                if (PositionDilutionOfPrecisionChanged != null)
                {
                    PositionDilutionOfPrecisionChanged(this, new DilutionOfPrecisionEventArgs(_PDOP));
                }

            }

        }
        #endregion

        #region Private methods
        private void ParsingThreadProc()
        {
            try
            {

                while (true)
                {
                    string sentence = _NmeaReader.ReadSentence();
                    if (string.IsNullOrEmpty(sentence))
                        continue;
                    lock (_syncRoot)
                    {
                        Parse(sentence);
                        if (NmeaSentenceReceived != null)
                            NmeaSentenceReceived(this, new NmeaSentenceEventArgs(sentence));
                    }
                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (Exception)
            {
            }
        }
        private void AddSatetillites(List<Satellite> value)
        {
            int count = value.Count;
            bool hasChanged = false;
            for (int index = 0; index < count; ++index)
            {
                Satellite satellite = value[index];
                int indexOfSatellite = _Satellites.IndexOf(satellite);
                if (indexOfSatellite == -1)
                {
                    _Satellites.Add(satellite);
                    hasChanged = true;
                }
                else if (satellite.SignalToNoiseRatio != _Satellites[indexOfSatellite].SignalToNoiseRatio ||
                    !satellite.Azimuth.Equals(_Satellites[indexOfSatellite].Azimuth) || !satellite.Elevation.Equals(_Satellites[indexOfSatellite].Elevation))
                {
                    satellite.IsFixed = _Satellites[indexOfSatellite].IsFixed;
                    _Satellites[indexOfSatellite] = satellite;
                    hasChanged = true;
                }
            }
            if (hasChanged && SatellitesChanged != null)
            {
                List<Satellite> list = new List<Satellite>(_Satellites);
                SatellitesChanged(this, new SatellitesEventArgs(list));
            }
        }
        private void Initialize()
        {
            _FixStatus = FixStatus.Unknown;
            _FixMode = FixMode.Unknown;
            _FixQuality = FixQuality.Unknown;
            _SelectionType = FixSelection.Unknown;
            _Coordinate = Coordinate.Invalid;
            _Speed = Speed.Invalid;
            _Heading = Azimuth.Invalid;
            _Altitude = Distance.Invalid;
            _MagneticVariation = Longitude.Invalid;
            _SatellitesInView = 0;
            _PDOP = _HDOP = _VDOP = DilutionOfPrecision.InValid;
            if (_Satellites != null)
                _Satellites.Clear();
        }
        private string[] GetWords(string sentence)
        {
            int asteriskIndex = sentence.IndexOf("*", StringComparison.Ordinal);
            int endIndex;
            if (asteriskIndex == -1)
            {
                endIndex = sentence.Length - 1;
            }
            else
            {
                endIndex = asteriskIndex - 1;
            }
            return sentence.Substring(0, endIndex + 1).Split(',');
        }
        #endregion

        #region Sentence Parsers
        private void ParseGPRMC(string[] Words)
        {
            try
            {
                #region Parse and Set FixStatus
                FixStatus fixStatus = FixStatus.Unknown;
                if (Words.Length > 3 && Words[2].Length != 0)
                {
                    fixStatus = _NmeaReader.ParseFixStatus(Words[2]);
                }
                SetFixStatus(fixStatus);
                #endregion

                #region Parse and Set DateTime
                DateTime utcDateTime;
                if ((!FixNeeded || FixNeeded && IsFixed) && Words.Length >= 10 && Words[9].Length != 0)
                {
                    string utcTimeWord = Words[1];
                    int utcHours = int.Parse(utcTimeWord.Substring(0, 2));
                    int utcMinutes = int.Parse(utcTimeWord.Substring(2, 2));
                    int utcSeconds = int.Parse(utcTimeWord.Substring(4, 2));
                    int utcMilliseconds = 0;
                    if (utcTimeWord.Length > 6)
                        utcMilliseconds = Convert.ToInt32(float.Parse(utcTimeWord.Substring(6)) * 1000);


                    string utcDateWord = Words[9];
                    int utcDay = int.Parse(utcDateWord.Substring(0, 2));
                    int utcMonth = int.Parse(utcDateWord.Substring(2, 2));
                    int utcYear = int.Parse(utcDateWord.Substring(4, 2)) + 2000;


                    utcDateTime = new DateTime(utcYear, utcMonth, utcDay, utcHours, utcMinutes, utcSeconds, utcMilliseconds, DateTimeKind.Utc);
                }
                else
                {
                    utcDateTime = DateTime.MinValue;
                }
                SetDataTime(utcDateTime);
                #endregion

                #region Parse and Set Coordinate
                Coordinate coordinate = Coordinate.Invalid;

                if ((!FixNeeded || FixNeeded && IsFixed) && Words.Length >= 7 && Words[3].Length != 0 && Words[4].Length != 0 && Words[5].Length != 0 && Words[6].Length != 0)
                {
                    coordinate = _NmeaReader.ParseCoordinate(Words[3], Words[4], Words[5], Words[6]);
                }
                SetCooridnate(coordinate);
                #endregion

                #region Parse and Set Speed
                Speed speed = Speed.Invalid;
                if ((!FixNeeded || FixNeeded && IsFixed) && Words.Length >= 8 && Words[7].Length != 0)
                {
                    speed = _NmeaReader.ParseSpeed(Words[7]);
                }
                SetSpeed(speed);
                #endregion

                #region Parse and Set Heading
                Azimuth heading = Azimuth.Invalid;
                if ((!FixNeeded || FixNeeded && IsFixed) && Words.Length >= 9 && Words[8].Length != 0)
                {
                    heading = _NmeaReader.ParseAzimuth(Words[8]);
                }
                SetAzimuth(heading);
                #endregion
                #region Parse and Set Magnetic variation
                Longitude magneticVariation = Longitude.Invalid ;
                if ((!FixNeeded || FixNeeded && IsFixed) && Words.Length >= 11 && Words[10].Length != 0)
                {
                    double variation = double.Parse(Words[10]);
                    magneticVariation = new Longitude(variation);
                }
                SetMagneticVariation(magneticVariation);
                #endregion
            }
            catch(System.Exception)
            {
                //If exception is allowed to propergate it can tear down the theard.
            }
        }
        private void ParseGPGSV(string[] Words)
        {
            try
            {
                #region Parse Satellites In View
                string[] SubWords = new string[Words.Length - 1];
                Array.Copy(Words, 1, SubWords, 0, SubWords.Length);
                List<Satellite> satetilliteList = new List<Satellite>(6);
                if (Words.Length >= 4 && Words[3].Length != 0)
                {
                    _SatellitesInView = int.Parse(Words[3]);
                }
                for (int index = 0; index < 6; ++index)
                {
                    int satilliteIndex = index * 4 + 3;

                    if (satilliteIndex > SubWords.Length - 1)
                        break;

                    int pseudoRandomNumber = int.Parse(SubWords[satilliteIndex]);
                    Elevation elevation;
                    Azimuth azimuth;
                    int signalToNoiseRatio;
                    if (SubWords.Length > satilliteIndex + 1 && SubWords[satilliteIndex + 1].Length != 0)
                    {
                        elevation = new Elevation(double.Parse(SubWords[satilliteIndex + 1]));
                    }
                    else
                    {
                        elevation = new Elevation(0.0);
                    }

                    if (SubWords.Length > satilliteIndex + 2 && SubWords[satilliteIndex + 2].Length != 0)
                    {
                        azimuth = new Azimuth(double.Parse(SubWords[satilliteIndex + 2]));
                    }
                    else
                    {
                        azimuth = new Azimuth(0.0);
                    }
 
                    if (SubWords.Length > satilliteIndex + 3 && SubWords[satilliteIndex + 3].Length != 0)
                    {
                        signalToNoiseRatio = int.Parse(SubWords[satilliteIndex + 3]);
                    }
                    else
                    {
                        signalToNoiseRatio = 0;
                    }
                    satetilliteList.Add(new Satellite(pseudoRandomNumber, azimuth, elevation , signalToNoiseRatio));

                }
                AddSatetillites(satetilliteList);
                #endregion
            }
            catch (System.Exception)
            {
            }
        }
        private void ParseGVTG(string[] Words)
        {
            try
            {
                #region Parse and Set Heading
                Azimuth heading = Azimuth.Invalid;
                if ((!FixNeeded || FixNeeded && IsFixed) && Words.Length >= 2 && Words[1].Length != 0)
                {
                    heading = _NmeaReader.ParseAzimuth(Words[1]);
                }
                SetAzimuth(heading);
                #endregion

                #region Parse and Set Speed
                Speed speed = Speed.Invalid;
                if ((!FixNeeded || FixNeeded && IsFixed) && Words.Length >= 7 && Words[6].Length != 0)
                {
                    speed = _NmeaReader.ParseSpeed(Words[7]);
                }
                SetSpeed(speed);
                #endregion
            }
            catch(System.Exception)
            {
            }
        }
        private void ParseGPGSA(string[] Words)
        {

            try
            {
                #region Switch Mode
                FixSelection switchMode = FixSelection.Unknown;
                if (Words.Length >= 2 && Words[1].Length != 0)
                {
                    switch (Words[1])
                    {
                        case "A":
                            switchMode = FixSelection.Auto;
                            break;
                        case "M":
                            switchMode = FixSelection.Manual;
                            break;
                    }
                    SetFixSelection(switchMode);
                }
                #endregion
                #region Fix Mode
                FixMode fixMode = FixMode.Unknown;
                if (Words.Length >= 3 && Words[2].Length != 0)
                {
                    switch (Words[2])
                    {
                        case "1":
                            fixMode = FixMode.ModeNoFix;
                            break;
                        case "2":
                            fixMode = FixMode.Mode2D;
                            break;
                        case "3":
                            fixMode = FixMode.Mode3D;
                            break;
                    }
                    SetFixMode(fixMode);

                }
                #endregion
                #region Fixed Satellites
                if (Words.Length >= 4)
                {
                    List<Satellite> fixedSatellites = new List<Satellite>(12);
                    int count = Words.Length < 15 ? Words.Length : 15;
                    for (int index = 3; index < count; index++)
                    {
                        if (Words[index].Length != 0)
                        {
                            int pseudoRandomNumber = int.Parse(Words[index]);
                            Satellite fixedSatellite = new Satellite(pseudoRandomNumber);
                            fixedSatellites.Add(fixedSatellite);
                        }
                    }
                    if (fixedSatellites.Count > 0)
                        SetFixedSatellites(fixedSatellites);
                }

                #endregion

                #region Dilution of Precision

                DilutionOfPrecision PDOP, HDOP, VDOP;
                PDOP = HDOP = VDOP = DilutionOfPrecision.InValid;

                if (Words.Length >= 16 && Words[15].Length != 0)
                    PDOP = new DilutionOfPrecision(float.Parse(Words[15]));

                SetPDilutionOfPrecision(PDOP);

                if (Words.Length >= 17 && Words[16].Length != 0)
                    HDOP = new DilutionOfPrecision(float.Parse(Words[16]));

                SetHDilutionOfPrecision(HDOP);

                if (Words.Length >= 18 && Words[17].Length != 0)
                    VDOP = new DilutionOfPrecision(float.Parse(Words[17]));
                SetVDilutionOfPrecision(VDOP);
                #endregion
            }
            catch (System.Exception)
            {
            }
            
        }
        private void ParseGPGGA(string[] Words)
        {
            try
            {    
                #region Fix Quality

                FixQuality fixQuality = FixQuality.Unknown;
                if (Words.Length >= 7 && Words[6].Length != 0)
                {

                    int fixQualityValue = int.Parse(Words[6]);
                    switch (fixQualityValue)
                    {
                        case 0:
                            fixQuality = FixQuality.NoFix;
                            break;
                        case 1:
                            fixQuality = FixQuality.GpsFix;
                            break;
                        case 2:
                            fixQuality = FixQuality.DGpsFix;
                            break;
                    }
                }
                SetFixQuality(fixQuality);
                #endregion

                #region Altitude
                Distance altitude = Distance.Invalid;          
                if ((!FixNeeded || FixNeeded && IsFixed)&& Words.Length >= 10 && Words[9].Length != 0)
                {

                    altitude = new Distance(float.Parse(Words[9]), DistanceUnit.Meters);

                }
                SetAltitude(altitude);
                #endregion
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region Protected methods
        /// <summary>
        /// Parses raw NMEA data and fires events with data received.
        /// </summary>
        /// <param name="sentence">String containing raw NMEA data.</param>
        protected void Parse(string sentence)
        {
            string[] Words = GetWords(sentence);
            switch (Words[0])
            {
                case "$GPRMC":
                    ParseGPRMC(Words);
                    break;
                case "$GPGGA":
                   ParseGPGGA(Words);
                    break;
                case "$GPGSV":
                    ParseGPGSV(Words);
                    break;
                case "$GPGSA":
                    ParseGPGSA(Words);
                    break;
                case "$GPVTG":
                    ParseGVTG(Words);
                    break;
                //default:
                    // Indicate that the sentence was not recognized
                    //return false;
            }
        }
        #endregion

    }
}
