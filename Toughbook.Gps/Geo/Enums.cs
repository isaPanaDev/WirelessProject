using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toughbook.Gps
{
    /// <summary>
    /// Indicates the portion of the Earth.
    /// </summary>
    public enum Hemisphere
    {
        /// <summary>
        /// North of the equator.
        /// </summary>
        North,
        /// <summary>
        /// South of the equator.
        /// </summary>
        South,
        /// <summary>
        /// East of the Prime Meridian.
        /// </summary>
        East,
        /// <summary>
        /// West of the Prime Meridian.
        /// </summary>
        West
    }
    /// <summary>
    /// Indicates level of accuracy as given by Dilution of Precision.
    /// </summary>
    public enum PrecisionRating
    {
        /// <summary>
        /// Rating is invalid or unknown.
        /// </summary>
        Unknown,
        /// <summary>
        /// Highest possible precision.
        /// </summary>
        Ideal,
        /// <summary>
        /// At this confidence level, positional measurements are considered accurate enough 
        /// to meet all but the most sensitive applications.
        /// </summary>
        Excellent,
        /// <summary>
        /// Represents a level that marks the minimum appropriate for 
        /// making business decisions. Positional measurements could be used to make reliable in-route navigation suggestions to the user.
        /// </summary>
        Good,
        /// <summary>
        /// Positional measurements could be used for calculations, but the fix quality could still be improved. 
        /// A more open view of the sky is recommended.
        /// </summary>
        Moderate,
        /// <summary>
        /// Represents a low confidence level. Positional measurements should be discarded or used only to indicate a very rough estimate of the current location. 
        /// </summary>
        Fair,
        /// <summary>
        /// 	At this level, measurements are inaccurate by as much as 300 meters with a 6 meter accurate device (50 DOP × 6 meters) and should be discarded.
        /// </summary>
        Poor
    }
    /// <summary>
    /// Indicates GPS fix quality
    /// </summary>
    public enum FixQuality
    {
        /// <summary>
        /// Fix quality is unknown or invalid.
        /// </summary>
        Unknown,
        /// <summary>
        /// Fix has not been obtained yet.
        /// </summary>
        NoFix,
        /// <summary>
        /// Fix uses information from GPS satellites only.
        /// </summary>
        GpsFix,
        /// <summary>
        /// Fix uses information from GPS satellites and also a differential GPS (DGPS) station.
        /// </summary>
        DGpsFix
    }
    /// <summary>
    /// Indicates fix status
    /// </summary>
    public enum FixStatus
    {
        /// <summary>
        /// Fix status is invalid or unknown.
        /// </summary>
        Unknown,
        /// <summary>
        /// GPS currently does not have a fix
        /// </summary>
        NoFix,
        /// <summary>
        /// GPS currently has a fix.
        /// </summary>
        Fix
    }
    /// <summary>
    /// Indicates fix mode of GPS module.
    /// </summary>
    public enum FixMode
    {
        /// <summary>
        /// Fix mode is unknown.
        /// </summary>
        Unknown,
        /// <summary>
        /// Fix has not been obtained yet.
        /// </summary>
        ModeNoFix,
        /// <summary>
        /// GPS will only provide Latitude and Longitude.
        /// </summary>
        Mode2D,
        /// <summary>
        /// GPS will provide Latitude, Longitude and Altitude.
        /// </summary>
        Mode3D
    }
    /// <summary>
    /// Indicates whether GPS fix was selected manually or automatically.
    /// </summary>
    public enum FixSelection
    {
        /// <summary>
        /// Switch mode is invalid or unknown.
        /// </summary>
        Unknown,
        /// <summary>
        /// Automatic - allowed to automatically switch 2D/3D.
        /// </summary>
        Auto,
        /// <summary>
        ///  Manual - forced to operate in 2D or 3D mode.
        /// </summary>
        Manual
    }
    /// <summary>
    /// Represents units of speed
    /// </summary>
    public enum SpeedUnit
    {
        /// <summary>
        /// Unit of speed equal to one nautical mile per hour.
        /// </summary>
        Knots,
        /// <summary>
        /// Unit of speed, measured in imperial units expressing the number of international miles covered in one hour.
        /// </summary>
        MilesPerHour,
        /// <summary>
        /// Unit of speed, expressing the number of kilometres traveled in one hour.
        /// </summary>
        KilometerPerHour
    }
    /// <summary>
    /// Represents units of length.
    /// </summary>
    public enum DistanceUnit
    {
        /// <summary>Metric System. Kilometers (one thousand meters).</summary>
        Kilometers,
        /// <summary>Metric System. 1/1000th of a kilometer.</summary>
        Meters,
        /// <summary>Imperial System. A statute mile, most often referred to just as "mile."</summary>
        Miles,
        /// <summary>Imperial System. Feet.</summary>
        Feet
    }
    /// <summary>
    /// Indicates direction of motion
    /// </summary>
    public enum Direction 
    {
        /// <summary>An azimuth of approximately 0°</summary>
        North,
        /// <summary>Between north and northeast</summary>
        NorthNortheast,
        /// <summary>Between north and east</summary>
        Northeast,
        /// <summary>Between east and northeast</summary>
        EastNortheast,
        /// <summary>An azimuth of approximately 90°</summary>
        East,
        /// <summary>Between east and southeast</summary>
        EastSoutheast,
        /// <summary>Between south and east</summary>
        Southeast,
        /// <summary>Between south and southeast</summary>
        SouthSoutheast,
        /// <summary>An azimuth of approximately 180°</summary>
        South,
        /// <summary>Between south and southwest</summary>
        SouthSouthwest,
        /// <summary>Between south and west</summary>
        Southwest,
        /// <summary>Between west and southwest</summary>
        WestSouthwest,
        /// <summary>An azimuth of approximately 270°</summary>
        West,
        /// <summary>Between west and northwest</summary>
        WestNorthwest,
        /// <summary>Between north and west</summary>
        Northwest,
        /// <summary>Between north and northwest</summary>
        NorthNorthwest
    }
}
