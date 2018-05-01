using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XML.Entity;

namespace XML.Interfaces
{
    /// <summary>
    /// Interface logger service
    /// </summary>
    public interface ILogger
    {
        void Write(string message);
    }
}
