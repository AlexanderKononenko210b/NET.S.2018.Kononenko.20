using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using LabExam.Entity;

namespace LabExam.Interfaces
{
    /// <summary>
    /// Interface for printer manager
    /// </summary>
    public interface IPrinterManager
    {
        IList<BasePrinter> GetAll();

        void Add(string name, string model);

        bool Contains(BasePrinter printer);

        void Print(BasePrinter printer, FileStream stream);
    }
}
