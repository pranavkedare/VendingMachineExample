using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PKLib.VendingSystem.Exceptions
{
    /// <summary>
    /// Raises exception when Vending machine is out of stock.
    /// </summary>
    [Serializable]
    public class EmptyMachineException : Exception
    {
        public EmptyMachineException()
        {
        }

        public EmptyMachineException(string message) : base(message)
        {
        }

        public EmptyMachineException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmptyMachineException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
