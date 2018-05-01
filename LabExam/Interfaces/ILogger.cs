using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabExam.Interfaces
{
    /// <summary>
    /// Interface for any logger
    /// </summary>
    public interface ILogger
    {
        void Write(string message);
    }
}
