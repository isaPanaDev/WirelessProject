using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading;
using System.Net.NetworkInformation;
using System.Xml;

namespace WirelessProject
{ 
    #region HostPinger Events Delegates Enums Interface
    public enum HostStatus
    {
        Dead, // Host is dead (it is not responsing to pings)
        Alive, // Host is alive (it is responsing to pings)
        DnsError, // IP address cannot be obtained using DNS server
        Unknown, // Host did not respond to any ping since pinging is started but not enough pings to declare host as dead
        Stopped // Ping has been stopped
    };
   
    public delegate void OnPingDelegate(PingObject host);
    public delegate void OnHostStatusChangeDelegate(PingObject host,
        HostStatus oldStatus, HostStatus newStatus);
    public delegate void OnHostPingerCommandDelegate(PingObject host);
    public delegate void OnHostNameChangedDelegate(PingObject host);
    
    public interface IPingLogger
    {
        void LogStart(PingObject host);
        void LogStop(PingObject host);
        void LogStatusChange(PingObject host, HostStatus oldStatus, HostStatus newStatus);
    }
    #endregion

    #region PingResultsBuffer
    // Keeps recent history (results of most recent pings).
    public class PingResultsBuffer
    {
        private List<Entry> _buffer = new List<Entry>(); // Entries of recent history buffer
        private object _syncObject = new object();

        // Object of this class represent several combined results that has same values in the recent history buffer.
        private class Entry
        {
            public Entry(bool received, int count)
            {
                _received = received;
                _count = count;
            }

            public bool _received; // Indicates whether the combined results was successfull pings or unresponded pings

            public int _count; // Number of combined results
        }

        #region BufferSize
        public static readonly int DEFAULT_BUFFER_SIZE = 100; // Default number of results stored in the buffer
        private int _bufferSize = DEFAULT_BUFFER_SIZE; 

        // Number of results stored in the buffer. When the new buffer size is smaller then previous 
        // results that cannot fit the new size are removed from the buffer.
        public int BufferSize
        {
            get
            {
                lock (_syncObject)
                    return _bufferSize;
            }
            set
            {
                lock (_syncObject)
                {
                    if (value < 0)
                        value = 0;

                    if (value > _bufferSize)
                        _bufferSize = value;
                    else
                    {
                        // new buffer size is smaller

                        // remove those results that cannot fit new size
                        for (int diff = _currentSize - value; diff > 0; )
                        {
                            Entry e = _buffer[0];

                            if (e._count <= diff)
                            {
                                // entire combined entry cannot fit	new size

                                // remove the entry complitely
                                _buffer.RemoveAt(0);
                                DecCount(e._received, e._count);

                                diff -= e._count;
                            }
                            else
                            {
                                // only part of the combined entry cannot fit new size

                                // remove only some	results from the entry
                                e._count -= diff;
                                DecCount(e._received, diff);

                                diff = 0;
                            }
                        }

                        if (_currentSize > value)
                            _currentSize = value;

                        _bufferSize = value;
                    }
                }
            }
        }

        private int _currentSize = 0; // Number of results currently stored in the buffer
        public int CurrentSize
        {
            get
            {
                lock (_syncObject)
                    return _currentSize;
            }
        }

        // Initializes recent history buffer with specified size of most recent pings that are stored in the buffer
        public PingResultsBuffer(int size)
        {
            _bufferSize = size;
        }

        public PingResultsBuffer() { }
        #endregion

        #region Statistics

        #region LostCount

        /// <summary>
        /// Number of unresponded pings.
        /// </summary>
        private int _lostCount;

        /// <summary>
        /// Number of unresponded pings.
        /// </summary>
        public int LostCount
        {
            get
            {
                lock (_syncObject)
                    return _lostCount;
            }
        }

        #endregion

        #region LostCountPercent

        /// <summary>
        /// Percent of unresponded pings.
        /// </summary>
        public float LostCountPercent
        {
            get
            {
                lock (_syncObject)
                    return (float)_lostCount / _currentSize * 100;
            }
        }

        #endregion

        #region ReceivedCount

        /// <summary>
        /// Number of successful pings.
        /// </summary>
        private int _receivedCount;

        /// <summary>
        /// Number of successful pings.
        /// </summary>
        public int ReceivedCount
        {
            get
            {
                lock (_syncObject)
                    return _receivedCount;
            }
        }

        #endregion

        #region ReceivedCountPercent

        /// <summary>
        /// Percent of successful pings.
        /// </summary>
        public float ReceivedCountPercent
        {
            get
            {
                lock (_syncObject)
                    return (float)_receivedCount / _currentSize * 100;
            }
        }

        #endregion

        #endregion

        #region Counting
        // Increments number of results based on number of successful or unsuccessful ping results
        private void IncCount(bool received)
        {
            if (received)
                _receivedCount++;
            else
                _lostCount++;
        }

        // Decrements number of results based on number of successful or unsuccessful ping results
        private void DecCount(bool received, int count)
        {
            if (received)
                _receivedCount -= count;
            else
                _lostCount -= count;
        }

        // Inserts ping results into recent history buffer. Received: whether the ping was successful or not
        public void AddPingResult(bool received)
        {
            if (_bufferSize < 1)
                return;

            lock (_syncObject)
            {
                // buffer is not full yet?
                if (_currentSize < _bufferSize)
                {
                    if (_currentSize == 0)
                    {
                        // empty buffer create first entry

                        _buffer.Add(new Entry(received, 1));
                        IncCount(received);
                        _currentSize++;
                        return;
                    }

                    _currentSize++;
                }
                else
                {
                    // buffer is full

                    // remove the oldes result
                    Entry first = _buffer[0];
                    first._count--;
                    DecCount(first._received, 1);

                    // if the last result was occpied entire combined entry
                    /// remove that entry
                    if (first._count == 0)
                        _buffer.RemoveAt(0);
                }

                // insert new result
                Entry last = _buffer[_buffer.Count - 1];
                if (last._received == received)
                    // the newest result can be combined with newest combined entry
                    last._count++;
                else
                    // the newest result cannot be combined with newest combined entry 
                    // create new entry and add it tho the buffer
                    _buffer.Add(new Entry(received, 1));

                // increment number of results os specific type
                IncCount(received);
            }
        }

        #endregion

        #region Clear
        // Clears results of most recent pings.
        public void Clear()
        {
            lock (_syncObject)
            {
                _currentSize = 0;
                _lostCount = 0;
                _receivedCount = 0;
                _buffer.Clear();
            }
        }
        #endregion
    }
    #endregion

    // Stores information about host and ping options and performs pinging
    public class PingObject
    {   
        #region Member Variables
        byte[] _buffer = new byte[DEFAULT_BUFFER_SIZE];
        System.Timers.Timer _timer = new System.Timers.Timer(); // Timer to initiate the ping
        Ping _pinger = new Ping(); // Ping object to ping the host
        PingOptions _pingerOptions = new PingOptions(DEFAULT_TTL, true); // Storing ping options
        private PingResultsBuffer _recentHistory = new PingResultsBuffer(); // Storing only recent activities

        object _syncObject = new object();
        
        public const int NUMBER_OF_STATUSES = 5; // Number of defined host statuses
        public readonly string XML_ELEMENT_NAME_HOST = "host"; // XML Name. Contains data of the host
        public readonly string XML_ELEMENT_NAME_ID = "id"; // XML Name. Stores ID of the host

        public string hostNoname = "No Name";

        private bool pingStatus;
            IPAddress ip;
            int timeout;
            byte[] buffer;
            PingOptions options;
        #endregion

        #region Constructors
        // Initializes pinger that pings localhost (127.0.0.1) when no IP or Name has been specified by user
        public PingObject()
        {
            AssignID();

            _hostIP = new IPAddress(new byte[] { 127, 0, 0, 1 });
            _hostName = "localhost";
        }

        /// <summary>
        /// Initialize host pinger with host name. This constructor sends DNS query to obtain IP address.
        /// </summary>
        /// <param name="hostName">name of the host.</param>
        public PingObject(string hostName)
        {
            AssignID();

            _hostName = hostName;

            try
            {
                _hostIP = GetHostIpByName(_hostName);
            }
            catch
            {
                Status = HostStatus.DnsError;
            }

            InitTimer();
        }

        /// <summary>
        /// Initializes host pinger with IP address of the host.
        /// </summary>
        /// <param name="address">IP address of the host.</param>
        public PingObject(IPAddress address)
        {
            AssignID();

            _hostName = hostNoname; 
            _hostIP = address;

            InitTimer();
        }

        /// <summary>
        /// This method sends DNS query to obtain IP address of the host with specified name.
        /// </summary>
        /// <param name="name">host name.</param>
        /// <returns>Method returns IP address of the host.</returns>
        private IPAddress GetHostIpByName(string name)
        {
            IPHostEntry dnse;
            try
            {
                dnse = Dns.GetHostEntry(_hostName);
            }
            catch (Exception ex)
            {
                throw new Exception("Error connecting DNS for " + _hostName + " host", ex);
            }

            if (dnse != null)
                return dnse.AddressList[0];
            else
                throw new Exception("Cannot resolve host \"" + _hostName + "\" IP by its name.");
        }
        
        void InitTimer()
        {
            _pinger.PingCompleted += new PingCompletedEventHandler(_pinger_PingCompleted);//@MOD
            _timer.AutoReset = false;
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(_timer_Elapsed);
        }

        void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Pinger();
        }
        #endregion

        #region ID
        private int _id; // id of the host automatically assigned
        public int ID
        {
            get { return _id; }
        }

        private static object _idLock = new object(); // Protects ID assigning process from concurent access
        private static int _nextID; // ID that will be assigned to new host

        // Assigns ID to the host automatically.
        private void AssignID()
        {
            lock (_idLock)
                _id = _nextID++;
        }

        // Updates ID tracker with ID of loaded host.
        private static void UpdateIDTrack(int id)
        {
            lock (_idLock)
            {
                if (_nextID <= id)
                    _nextID = id + 1;
            }
        }
        #endregion

        #region Host Infromation
        // Host Print Info for logging purposes
        public string HostInfo
        {
            get
            {
                lock (_syncObject)
                {
                    if (_hostName == hostNoname)
                        return _hostIP.ToString();
                    else
                        return _hostName;
                }
            }            
        }

        // Host IP Address
        public readonly string XML_ELEMENT_NAME_HOST_IP = "ip"; 
        private IPAddress _hostIP;
        public IPAddress HostIP
        {
            get
            {
                lock (_syncObject)
                    return _hostIP != null ? _hostIP : new IPAddress(0);
            }
            set
            {
                lock (_syncObject)
                {
                    _hostIP = value;

                    // successfully obtained IP address from previously unavailable DNS
                    if (_status == HostStatus.DnsError)
                        Status = HostStatus.Unknown;
                }
            }
        }

        //  Host Name 
        public readonly string XML_ELEMENT_NAME_HOST_NAME = "name"; 
        private string _hostName = string.Empty;
        public string HostName
        {
            get
            {
                lock (_syncObject)
                    return _hostName;
            }
            set
            {
                lock (_syncObject)
                    _hostName = value;
            }
        }
        
        // Host Status
        private HostStatus _status = HostStatus.Unknown;
        public HostStatus Status
        {
            get
            {
                lock (_syncObject)
                {
                    if (_isRunning) // return current status
                        return _status;
                    else
                        if (stopped) // if test has been stopped
                            return HostStatus.Stopped;
                        else    // unknown cause
                            return HostStatus.Unknown;
                }
            }
            set
            {
                // no change to the status?
                if (_status == value && _status != HostStatus.Unknown)
                    // no need to perform calculation about status durations
                    return;

                DateTime now = DateTime.Now;

                // duration of the old status
                TimeSpan duration = now - _statusReachedAt;

                // duration of the old status is valid only if the pinging was active during that time
                if (_isRunning)
                    _statusDurations[(int)_status] += duration;

                // save time when the new status is reached
                _statusReachedAt = now;

                HostStatus old = _status;
                _status = value;

                // notify listeners that status of the host is changed
                ThreadPool.QueueUserWorkItem(new WaitCallback(RaiseOnStatusChange),
                    new OnHostStatusChangeParams(old, _status));
            }
        }

        public string StatusName
        {
            get
            {
                HostStatus status = Status;
                switch (status)
                {
                    case HostStatus.Dead:
                        return "Dead";
                    case HostStatus.Alive:
                        return "Alive";
                    case HostStatus.DnsError:
                        return "Dns Error";
                    case HostStatus.Stopped:
                        return "Stopped";
                }

                return "Unknown";
            }
        }
        #endregion

        #region Packet Counting
        private int _continousPacketLost = 0;

        public static readonly int DEFAULT_PACKET_SENT = 10; // default
        private int _sentPackets;
        public int SentPackets
        {
            get
            {
                lock (_syncObject)
                    return _sentPackets;
            }
            set
            {
                lock (_syncObject)
                    _sentPackets = value;
            }
        }

        private int _receivedPackets;
        public int ReceivedPackets
        {
            get
            {
                lock (_syncObject)
                    return _receivedPackets;
            }
        }

        public float ReceivedPacketsPercent
        {
            get
            {
                lock (_syncObject)
                    return (float)_receivedPackets / _sentPackets * 100;
            }
        }

        private int _lostPackets;
        public int LostPackets
        {
            get
            {
                lock (_syncObject)
                    return _lostPackets;
            }
        }

        private int _consecutivePacketsLost;
        public int ConsecutivePacketsLost
        {
            get
            {
                lock (_syncObject)
                    return _consecutivePacketsLost;
            }
        }

        private int _maxConsecutivePacketsLost = 0;
        public int MaxConsecutivePacketsLost
        {
            get
            {
                lock (_syncObject)
                    return _maxConsecutivePacketsLost;
            }
        }

        public int RecentlyLostPackets
        {
            get
            {
                lock (_syncObject)
                    return _recentHistory.LostCount;
            }
        }
        
        private long _totalResponseTime = 0;
        public float AverageResponseTime
        {
            get
            {
                lock (_syncObject)
                    return _receivedPackets != 0 ? (float)_totalResponseTime / _receivedPackets : 0;
            }
        }
        #endregion

        #region CurrentStatusDuration

        /// <summary>
        /// Time when the current status is reached.
        /// </summary>
        private DateTime _statusReachedAt = DateTime.Now;

        /// <summary>
        /// Duration of the current status. 
        /// </summary>
        public TimeSpan CurrentStatusDuration
        {
            get
            {
                lock (_syncObject)
                    return DateTime.Now.Subtract(_statusReachedAt);
            }
        }

        #endregion

        #region StatusDurations

        /// <summary>
        /// Array of the durations of each status.
        /// </summary>
        private TimeSpan[] _statusDurations = new TimeSpan[NUMBER_OF_STATUSES];

        /// <summary>
        /// Method returns duration of specific status.
        /// </summary>
        /// <param name="status">status whose duration is queried.</param>
        /// <returns>Returns duration of specific status.</returns>
        public TimeSpan GetStatusDuration(HostStatus status)
        {
            lock (_syncObject)
            {
                TimeSpan duration = _statusDurations[(int)status];
                if (_status == status && _isRunning)
                    duration += DateTime.Now - _statusReachedAt;

                return duration;
            }
        }

        #endregion

        #region Test Durations
        private DateTime _startTime = DateTime.Now;
        public TimeSpan CurrentTestDuration
        {  // Time that has passed since the current pinging is started
            get
            {
                lock (_syncObject)
                {
                    return _isRunning ? DateTime.Now.Subtract(_startTime) : new TimeSpan(0);
                }
            }
        }
        #endregion

        #region Counting

        /// <summary>
        /// Increments number of lost packets and change status of the host if enough successive packets are lost.
        /// </summary>
        private void IncLost()
        {
            _lostPackets = _consecutivePacketsLost;
            _consecutivePacketsLost++;

            if (_consecutivePacketsLost > _maxConsecutivePacketsLost)
                _maxConsecutivePacketsLost = _consecutivePacketsLost;

            _recentHistory.AddPingResult(false);

            // enough packets has been lost so we can assume that the host is dead 
            //Console.WriteLine(_consecutivePacketsLost + " > " + _pingsBeforeDead);
            if (++_continousPacketLost > _pingsBeforeDead && _status != HostStatus.Dead)
            {
                Status = HostStatus.Dead;
            }
        }

        /// <summary>
        /// Increments number of responded pings and sets status of the host to <see cref="Alive"/>.
        /// </summary>
        /// <param name="time">response time.</param>
        private void IncReceived(long time)
        {
            _receivedPackets = _sentPackets;
            _consecutivePacketsLost = 0;

            _recentHistory.AddPingResult(true);

            _totalResponseTime += time;

            // restarts counter
            _continousPacketLost = 0;

            if (_status != HostStatus.Alive)
                Status = HostStatus.Alive;
        }

        #endregion

        #region Options
        public static readonly int DEFAULT_TTL = 64; // Default Time-To-Live

        #region BufferSize
        public readonly string XML_ELEMENT_NAME_BUFFER_SIZE = "buffersize"; // XML Name
        public static readonly int DEFAULT_BUFFER_SIZE = 32; // default
        private int _bufferSize = DEFAULT_BUFFER_SIZE;
        public int BufferSize
        {
            get
            {
                lock (_syncObject)
                    return _bufferSize;
            }
            set
            {
                lock (_syncObject)
                {
                    if (value > 0)
                    {
                        _bufferSize = value;
                        _buffer = new byte[value];
                    }
                }
            }
        }
        #endregion

        #region Timeout
        public readonly string XML_ELEMENT_NAME_TIMEOUT = "timeout"; // XML Name
        public static readonly int DEFAULT_TIMEOUT = 2000; // default
        private int _timeout = DEFAULT_TIMEOUT;
        public int Timeout
        {
            get
            {
                lock (_syncObject)
                    return _timeout;
            }
            set
            {
                lock (_syncObject)
                    _timeout = value;
            }
        }
        #endregion

        #region PingInterval
        // Interval between end of processing of the previous message and sending of new message
        public readonly string XML_ELEMENT_NAME_PING_INTERVAL = "interval"; // XML Name
        public static readonly int DEFAULT_PING_INTERVAL = 1000; // default: in milliseconds!
        private int _pingInterval = DEFAULT_PING_INTERVAL;
        public int PingInterval
        {
            get
            {
                lock (_syncObject)
                    return _pingInterval;
            }
            set
            {
                lock (_syncObject)
                    _pingInterval = value;
            }
        }
        #endregion

        #region DnsQueryInterval

        /// <summary>
        /// Name of XML element that stores interval duration between DNS quiries while pinger tries to obtain IP address.
        /// </summary>
        public readonly string XML_ELEMENT_NAME_DNS_QUERY_INTERVAL = "dnsinterval";

        /// <summary>
        /// Default duration interval between DNS quiries 
        /// while pinger tries to obtain IP address (in milliseconds).
        /// </summary>
        public static readonly int DEFAULT_DNS_QUERY_INTERVAL = 60000;

        /// <summary>
        /// Interval duration between DNS quiries while pinger tries to obtain IP address (in milliseconds).
        /// </summary>
        private int _dnsQueryInterval = DEFAULT_DNS_QUERY_INTERVAL;

        /// <summary>
        /// Interval duration between DNS quiries while pinger tries to obtain IP address (in milliseconds).
        /// </summary>
        public int DnsQueryInterval
        {
            get
            {
                lock (_syncObject)
                    return _dnsQueryInterval;
            }

            set
            {
                lock (_syncObject)
                    _dnsQueryInterval = value;
            }
        }

        #endregion

        #region PingsBeforeDead

        /// <summary>
        /// Name of XML element that stores number of packets that should be lost successivly to declare host as dead. 
        /// </summary>
        public readonly string XML_ELEMENT_NAME_PINGS_BEFORE_DEAD = "pingsbeforedead";

        /// <summary>
        /// Default number of packets that should be lost successivly to declare host as dead.
        /// </summary>
        public static readonly int DEFALUT_PINGS_BEFORE_DEAD = 10;

        /// <summary>
        /// Number of packets that should be lost successivly to declare host as dead.
        /// </summary>
        private int _pingsBeforeDead = DEFALUT_PINGS_BEFORE_DEAD;

        /// <summary>
        /// Number of packets that should be lost successivly to declare host as dead.
        /// </summary>
        public int PingsBeforeDead
        {
            get
            {
                lock (_syncObject)
                    return _pingsBeforeDead;
            }
            set
            {
                lock (_syncObject)
                    _pingsBeforeDead = value;
            }
        }

        #endregion

        #region RecentHisoryDepth

        /// <summary>
        /// Name of XML element that stores depth of recent history.
        /// </summary>
        public readonly string XML_ELEMENT_NAME_RECENT_HISTORY_DEPTH = "recenthistorydepth";

        /// <summary>
        /// Depth of recent history (number of results of previous pings that are stored in history buffer).
        /// </summary>
        public int RecentHisoryDepth
        {
            get
            {
                lock (_syncObject)
                    return _recentHistory.BufferSize;
            }

            set
            {
                lock (_syncObject)
                    _recentHistory.BufferSize = value;
            }
        }

        #endregion

        #endregion    

        #region Logger

        /// <summary>
        /// Logger used to log host changes and commands issued to this host pinger.
        /// </summary>
        private IPingLogger _logger = null;

        /// <summary>
        /// Logger used to log host changes and commands issued to this host pinger.
        /// </summary>
        public IPingLogger Logger
        {
            get
            {
                lock (_syncObject)
                    return _logger;
            }
            set
            {
                lock (_syncObject)
                    _logger = value;
            }
        }

        #endregion

        #region Events
        public event OnPingDelegate OnPing;
        private void RaiseOnPing()
        {
            if (OnPing != null)
                OnPing(this);
        }

        private class OnHostStatusChangeParams
        {
            public HostStatus _oldState; // Previous status of the host
            public HostStatus _newState; // New status of the host
            public OnHostStatusChangeParams(HostStatus oldStatus, HostStatus newStatus)
            {
                _oldState = oldStatus;
                _newState = newStatus;
            }
        }

        public event OnHostStatusChangeDelegate OnStatusChange;
        private void RaiseOnStatusChange(object param)
        {
            OnHostStatusChangeParams p = (OnHostStatusChangeParams)param;

            // log status change
            if (_logger != null)
                _logger.LogStatusChange(this, p._oldState, p._newState);

            if (OnStatusChange != null)
                OnStatusChange(this, p._oldState, p._newState);
        }

        public event OnHostPingerCommandDelegate OnStartPinging;
        private void RaiseOnStartPinging()
        {
            // log start command
            if (_logger != null)
                _logger.LogStart(this);

            if (OnStartPinging != null)
                OnStartPinging(this);
        }

        public event OnHostPingerCommandDelegate OnStopPinging;
        private void RaiseOnStopPinging()
        {
            // log stop command
            if (_logger != null)
                _logger.LogStop(this);

            if (OnStopPinging != null)
                OnStopPinging(this);
        }

        public event OnHostNameChangedDelegate OnHostNameChanged;
        private void RaiseOnHostNameChanged()
        {
            if (OnHostNameChanged != null)
                OnHostNameChanged(this);
        }

        #endregion
        
        #region Pinging Methods

        #region IsRunning

        // Inidicate whether the pinging is active.
        private bool _isRunning;

        // Inidicate whether the pinging has been stopped
        private bool stopped;

        /// <summary>
        /// Inidicate whether the pinging is active.
        /// </summary>
        public bool IsRunning
        {
            get
            {
                lock (_syncObject)
                    return _isRunning;
            }

            set
            {
                if (value)
                    Start();
                else
                    Stop();
            }
        }

        #endregion

        // Starts pinging the host. If the pinging is already active this method performs no actions.
        public void Start()
        {
            bool started = false;
            Monitor.Enter(_syncObject);
            if (!_isRunning)
            {
                // save the time when the pinging is started
                _startTime = DateTime.Now;

                if (_status != HostStatus.DnsError)
                    Status = HostStatus.Unknown;

                started = _isRunning = true;

                // schedule ping if it is not already
                if (!_pingScheduled)
                {
                    _pingScheduled = true;
                    _timer.Interval = _pingInterval;
                    _timer.Start();
                }
            }
            Monitor.Exit(_syncObject);

           
            if (started)
                RaiseOnStartPinging();
        }

        // Stops pinging the host. If the pinging is not active this method performs no actions.
        public void Stop()
        {
            stopped = false;

            lock (_syncObject)
            {
                if (_isRunning)
                {
                    _continousPacketLost = 0;

                    if (_status != HostStatus.DnsError)
                        Status = HostStatus.Unknown;

                    _isRunning = false;
                    stopped = true;
                }
            }

            if (stopped)
            {
                Status = HostStatus.Stopped;
                RaiseOnStopPinging();
                stopped = false;
            }
        }

        // Indicates that next ping is scheduled.
        private bool _pingScheduled = false;
        // Send ping echo message, waits for the response and schedule next ping.
        private void Pinger()
        {
            pingStatus = false;

            lock (_syncObject)
                // did pinger obtain IP address
                pingStatus = _status != HostStatus.DnsError;

           
            // pinger has not yet obtained IP address
            if (!pingStatus)
            {
                try
                {
                    // send DNS query
                    IPAddress addr = GetHostIpByName(HostName);

                    lock (_syncObject)
                    {
                        // IP address obtained sucessfully change host status
                        if (_status == HostStatus.DnsError && _isRunning)
                        {
                            _hostIP = addr;
                            Status = HostStatus.Unknown;
                        }
                    }
                }
                catch
                {
                    // DNS query failed and IP address was not obtained
                    lock (_syncObject)
                    {
                        if (_isRunning)
                        {
                            // schedule next DNS query
                            _pingScheduled = true;
                            _timer.Interval = _dnsQueryInterval;
                            _timer.Start();
                        }
                        else
                            _pingScheduled = false;
                    }

                    RaiseOnPing();
                    return;
                }
            }

            Console.WriteLine(this.HostName + " ---- " + _pingScheduled.ToString());
         
            lock (_syncObject)
            {
                // copy ping options
                ip = _hostIP;
                timeout = _timeout;
                buffer = _buffer;
                options = _pingerOptions;
            }
            
            // send ping message
           // reply = _pinger.Send(ip, timeout, buffer, options);  
            // Changed to Async Call..!!
           
           //_pinger.PingCompleted += new PingCompletedEventHandler(_pinger_PingCompleted);//@MOD
            try
            {
                if (this.isIPValid(ip))
                    _pinger.SendAsync(ip, timeout, buffer, options);
                else
                    throw new ArgumentException();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception Occured in SendAsync: " + e.Message);
                Status = HostStatus.Dead;
            }
            if (_status == HostStatus.Unknown) { IncLost(); }
          

            if (pingStatus)
                RaiseOnPing();
        }

        bool isIPValid(IPAddress ip)
        {
            string ipString = ip.ToString().Split('.')[0];
            //Console.WriteLine("First peice of the IP Address: " + ipString);
            int value = Int32.Parse(ipString);
            if (value > 0 && value < 256)
                return true;
            else
                return false;
        }

        void _pinger_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            PingReply reply = e.Reply;
            lock (_syncObject)
            {
                pingStatus = false;

                if (_isRunning)
                {
                    if (ip == _hostIP)
                    {
                        switch (reply.Status)
                        {
                            #region Checking Reply Status

                            case IPStatus.BadDestination:
                            case IPStatus.BadHeader:
                            case IPStatus.BadOption:
                            case IPStatus.BadRoute:
                            case IPStatus.UnrecognizedNextHeader:
                            case IPStatus.PacketTooBig:
                            case IPStatus.ParameterProblem:
                                // wrong message format
                                IncLost();
                                break;

                            case IPStatus.DestinationScopeMismatch:
                            case IPStatus.Unknown:
                            case IPStatus.HardwareError:
                            case IPStatus.IcmpError:
                            case IPStatus.NoResources:
                            case IPStatus.SourceQuench:
                                // error
                                IncLost();
                                break;

                            case IPStatus.DestinationHostUnreachable:
                            case IPStatus.DestinationNetworkUnreachable:
                            case IPStatus.DestinationPortUnreachable:
                            case IPStatus.DestinationProhibited:
                            case IPStatus.DestinationUnreachable:
                                // unreachability of the remote host
                                IncLost();
                                break;

                            case IPStatus.TimeExceeded:
                            case IPStatus.TimedOut:
                            case IPStatus.TtlExpired:
                            case IPStatus.TtlReassemblyTimeExceeded:
                                // time outs
                                IncLost();
                                break;

                            case IPStatus.Success:
                                // success
                                IncReceived(reply.RoundtripTime);
                                break;

                            default:
                                // something went wrong
                                IncLost();
                                break;

                            #endregion
                        }

                        pingStatus = true;
                    }

                    // schedule next ping
                    _pingScheduled = true;
                    _timer.Interval = _pingInterval;
                    _timer.Start();
                }
                else
                    _pingScheduled = false;
            }
        }

        #endregion
    }
}
