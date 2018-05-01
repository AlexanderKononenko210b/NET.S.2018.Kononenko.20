using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LabExam.Exceptions
{
    /// <summary>
    /// Exception that describe situation when printer with the same name and model exist in the system
    /// </summary>
    [Serializable]
    public class ExistPrinterException : Exception
    {
        public ExistPrinterException()
        {
        }

        public ExistPrinterException(string message) : base(message)
        {
        }

        public ExistPrinterException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ExistPrinterException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
