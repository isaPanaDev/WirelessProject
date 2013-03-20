using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;

namespace Toughbook.Gps
{
    /// <summary>
    /// Abstract GPS connection device.
    /// </summary>
    public abstract class Device : IDisposable
    {
        /// <summary>
        /// When implemented will return stream that contains NMEA data.
        /// </summary>
        public abstract Stream NmeaStream
        {
            get;
        }
        /// <summary>
        /// Internally maintains state of stream.
        /// </summary>
        protected bool _IsStreamOpen = false;
        /// <summary>
        /// Internally maintains state of object disposal.
        /// </summary>
        protected bool _IsDisposed = false;
        /// <summary>
        /// When implemented returns whether GPS device is open.
        /// </summary>
        /// <returns>Returns true if open and false if not open.</returns>
        public abstract bool IsOpen();
        /// <summary>
        /// When implemented Dispose will release all unmanaged resources.
        /// </summary>
        public abstract void Dispose();
        /// <summary>
        /// When implemented function will open GPS connection to GPS device.
        /// </summary>
        public abstract void Open();
        /// <summary>
        /// When implemented function will close connection to GPS device.
        /// </summary>
        public abstract void Close();
        /// <summary>
        /// Name of Device or Port that will be connected.
        /// </summary>
        public abstract string Name
        {
            get;
        }
        /// <summary>
        /// Returns Device with default values for Toughbook model that library is currently
        /// running on.
        /// </summary>
        public static Device Default
        {
            get
            {
                string model = ModelChecker.Model.ToUpper();
                if (model == "CF-U1" || model == "CF-H1" || model == "CF-H2")
                {
                    return new SerialDevice("COM2", 4800);
                }
                else if (model == "CF-30" || model == "CF-31" || model == "CF-19")
                {
                    return new SerialDevice("COM3", 4800);
                }
                else
                {
                    throw new InvalidOperationException("No known defaults for model " + ModelChecker.ModelNo + ".");
                }
                
            }
        }

    }
    internal class Emulator : Device
    {
        private string _FileName;
        private StreamReader _StreamReader;

        public Emulator(string fileName)
        {
            _FileName = fileName;
            _StreamReader = new StreamReader(fileName);
        }
        public override bool IsOpen()
        {
            if (_IsDisposed)
                throw new ObjectDisposedException("Device has already been disposed");
            return true;
        }
        public override Stream NmeaStream
        {
            get 
            {
                if (_IsDisposed)
                    throw new ObjectDisposedException("Device has already been disposed");
                return _StreamReader.BaseStream; 
            }
        }
        public override string Name
        {
            get 
            {
                if (_IsDisposed)
                    throw new ObjectDisposedException("Device has already been disposed"); 
                return _FileName; 
            }
        }
        public override void Open()
        {
            
        }
        public override void Close()
        {
            if (_IsStreamOpen)
            {
                _StreamReader.Close();
                _IsStreamOpen = false;
            }
        }
        public override void Dispose()
        {
            if (!_IsDisposed)
            {
                _StreamReader.Dispose();
            }
        }
    }
    /// <summary>
    /// Device that will connect only to serial ports.
    /// </summary>
    public class SerialDevice : Device
    {
        private SerialPort _SerialPort;
        /// <summary>
        /// Creates a new instance of SerialDevice.
        /// </summary>
        /// <param name="comPort">Com port name that this device will connect to.</param>
        /// <param name="baudRate">Baud rate of Com Port (i.e. 4800).</param>
        public SerialDevice(string comPort, int baudRate)
        {
            _SerialPort = new SerialPort(comPort, baudRate);
        }
        /// <summary>
        /// Indicates whether SerialDevice is connected to the underlying serial port.
        /// </summary>
        /// <returns>Returns true if serial port is open and false if closed.</returns>
        public override bool IsOpen()
        {
            if (_IsDisposed)
                throw new ObjectDisposedException("Device has already been disposed");
            return _SerialPort.IsOpen;
        }
        /// <summary>
        /// Returns name of com port instance will connect to.
        /// </summary>
        public override string Name
        {
            get 
            {
                if (_IsDisposed)
                    throw new ObjectDisposedException("Device has already been disposed");

                return _SerialPort.PortName; 
            }
        }
        /// <summary>
        /// Connects SerialDevice to com port.
        /// </summary>
        public override void Open()
        {
            if (_IsDisposed)
                throw new ObjectDisposedException("Device has already been disposed");

            if (!_SerialPort.IsOpen)
            {
                _SerialPort.Open();
                //_SerialPort.
                _IsStreamOpen = true;
            }
        }
        /// <summary>
        /// Closes connection to com port.
        /// </summary>
        public override void Close()
        {
            if (_IsDisposed)
                throw new ObjectDisposedException("Device has already been disposed");

            if (_SerialPort.IsOpen)
            {
                try { _SerialPort.BaseStream.Close(); }
                catch { }
                try { _SerialPort.BaseStream.Dispose(); }
                catch { }
                try { _SerialPort.Close(); }
                catch { }
            }
        }
        /// <summary>
        /// Returns base stream from serial port.
        /// </summary>
        public override Stream NmeaStream
        {
            get 
            {
                if (_IsDisposed)
                    throw new ObjectDisposedException("Device has already been disposed");

                return _SerialPort.BaseStream; 
            }
        }
        /// <summary>
        /// Disposes of unmanaged resource that have been allocated for this class.
        /// </summary>
        public override void Dispose()
        {
            if (!_IsDisposed)
            {
                try { _SerialPort.Dispose(); }
                catch { };
                _IsDisposed = true;
            }
        }
    }
}
