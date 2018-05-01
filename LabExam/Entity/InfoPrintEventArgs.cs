using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabExam.Entity
{
    /// <summary>
    /// Class for information about event print
    /// </summary>
    public class InfoPrintEventArgs : EventArgs
    {
        public InfoPrintEventArgs(string name, string model, string message)
        {
            this.Name = name;

            this.Model = model;

            this.Message = message;
        }

        public string Name { get; set; }

        public string Model { get; set; }

        public string Message { get; set; }
    }
}
