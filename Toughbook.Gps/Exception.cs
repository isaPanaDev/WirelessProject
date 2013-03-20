using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toughbook.Gps
{
    /// <summary>
    /// Represents error that occur in Toughbook GPS class execution
    /// </summary>
    class GpsException : System.Exception
    {
        /// <summary>
        /// Initializes a new instance of the GpsException class
        /// </summary>
        public GpsException() : base() { }
        /// <summary>
        /// Initializes a new instance of the GpsException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public GpsException(string message) : base(message) { }
        /// <summary>
        ///     Initializes a new instance of the System.Exception class with a specified
        ///     error message and a reference to the inner exception that is the cause of
        ///     this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference
        ///if no inner exception is specified.</param>
        public GpsException(string message, System.Exception inner) : base(message, inner) { }

    }
}
