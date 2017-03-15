using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PKLib.VendingSystem.Exceptions
{
    /// <summary>
    /// Raised when user provides incorrect pin for card
    /// </summary>
    [Serializable]
    public class InvalidPinException : Exception
    {
        public InvalidPinException()
        {
        }

        public InvalidPinException(string message) : base(message)
        {
        }

        public InvalidPinException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidPinException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
