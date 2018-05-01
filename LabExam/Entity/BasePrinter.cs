using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabExam.Entity
{
    /// <summary>
    /// Abstract class with property and method Print for all inheritors
    /// </summary>
    public abstract class BasePrinter
    {
        private string name;

        private string model;

        /// <summary>
        /// Get name printer
        /// </summary>
        public string Name
        {
            get => name;

            protected set => name = value;
        }

        /// <summary>
        /// Get model printer
        /// </summary>
        public string Model
        {
            get => model;

            protected set => model = value;
        }

        public event EventHandler<InfoPrintEventArgs> PrintEvent = delegate { };

        /// <summary>
        /// Method for call event about start or end print
        /// </summary>
        public void OnPrint(string message)
        {
            PrintEvent(this, new InfoPrintEventArgs(this.Name, this.Model, message));
        }

        /// <summary>
        /// Method for all inheritor
        /// </summary>
        /// <param name="fileStream"></param>
        public void Print(FileStream fileStream)
        {
            OnPrint("Print start");

            for (int i = 0; i < fileStream.Length; i++)
            {
                // simulate printing
                Console.WriteLine(fileStream.ReadByte());
            }

            OnPrint("Print end");
        }
    }
}
