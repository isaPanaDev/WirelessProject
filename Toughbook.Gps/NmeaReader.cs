using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Toughbook.Gps
{
    internal class NmeaReader
    {
        private StreamReader _StreamReader;

        public NmeaReader(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream argument is null");

            _StreamReader = new StreamReader(stream, Encoding.ASCII, false, 128);
        }

        public bool isNmeaStream()
        {
            int MAX_TRIES = 20;
            for (int index = 0; index < MAX_TRIES; ++index)
            {
                try
                {
                    string sentence = _StreamReader.ReadLine();
                    if (sentence.StartsWith("$") && (sentence.IndexOf("*") == sentence.Length - 3))
                    {
                        return true;
                    }
                }
                catch(IOException)
                {
                    return false;
                }
            }
            return false;
        }
        /*public Longitude ParseMagneticVariation(string variation, string longitudeHemisphereStr)
        {
            int 
        }*/
        public Coordinate ParseCoordinate(string latitude, string latitudeHemisphereStr, string longitude, string longitudeHemisphereStr)
        {
            int latitudeHours = int.Parse(latitude.Substring(0, 2));
            double latitudeDecimalMinutes = double.Parse(latitude.Substring(2));
            Hemisphere latitudeHemisphere =
                latitudeHemisphereStr.Equals("N", StringComparison.Ordinal) ? Hemisphere.North : Hemisphere.South;


            int longitudeHours = int.Parse(longitude.Substring(0, 3));
            double longitudeDecimalMinutes = double.Parse(longitude.Substring(3));
            Hemisphere longitudeHemisphere =
                longitudeHemisphereStr.Equals("E", StringComparison.Ordinal) ? Hemisphere.East : Hemisphere.West;


           Coordinate coordinate = new Coordinate(
                         new Latitude(latitudeHours, latitudeDecimalMinutes, latitudeHemisphere),
                         new Longitude(longitudeHours, longitudeDecimalMinutes, longitudeHemisphere));
           return coordinate;
        }
        public Speed ParseSpeed(string speedInKnots)
        {
            return new Speed(double.Parse(speedInKnots), SpeedUnit.Knots);
        }
        public Azimuth ParseAzimuth(string azimuth)
        {
            return new Azimuth(double.Parse(azimuth));
        }
        public FixStatus ParseFixStatus(string fixStatus)
        {
           return fixStatus.Equals("A", StringComparison.OrdinalIgnoreCase) ? FixStatus.Fix : FixStatus.NoFix;
        }
        public string ReadSentence()
        {
            try
            {
                return _StreamReader.ReadLine();
            }
            catch
            {
                return "";
            }
        }
    }
}
