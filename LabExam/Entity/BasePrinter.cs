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

        public event EventHandler<InfoPrintEventArgs> PrintEvent;

        /// <summary>
        /// Method for call event about start or end print
        /// </summary>
        protected virtual void OnPrint(InfoPrintEventArgs e)
        {
            PrintEvent?.Invoke(this, e);
        }

        /// <summary>
        /// Method for print
        /// </summary>
        /// <param name="stream"></param>
        public void Print(Stream stream)
        {
            if(stream == null)
                throw new ArgumentNullException($"Argument {nameof(stream)} is null");

            OnPrint(new InfoPrintEventArgs(this.Name, this.Model, "Print start"));

            CurrentPrint(stream);

            OnPrint(new InfoPrintEventArgs(this.Name, this.Model, "Print end"));
        }

        /// <summary>
        /// Abstract method for override in inheritance classes
        /// </summary>
        protected abstract void CurrentPrint(Stream stream);
    }
}
