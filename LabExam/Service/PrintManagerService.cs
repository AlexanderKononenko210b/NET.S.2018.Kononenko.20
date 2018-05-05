using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using LabExam.Entity;
using LabExam.Exceptions;
using LabExam.Interfaces;

namespace LabExam.Service
{
    /// <summary>
    /// Manager for print in singlton scope
    /// </summary>
    public class PrintManagerService
    {
        private readonly IList<BasePrinter> printers = new List<BasePrinter>();

        private ILogger logger;

        private static volatile PrintManagerService instance;

        private static readonly object padlock = new object();

        PrintManagerService(ILogger logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Get property instance PrintManagerService
        /// </summary>
        public static PrintManagerService Instance
        {
            get
            {
                if(instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new PrintManagerService(new Logger());
                        }
                    }
                }
                return instance;
            }
        }

        public void PrintEventHandler(object sender, InfoPrintEventArgs info)
        {
            logger.Write($"Printer name: {info.Name} model: {info.Model} {info.Message} in {DateTime.Now}");
        }

        /// <summary>
        /// Index for acces items in printers
        /// </summary>
        /// <param name="index">index in printers</param>
        /// <returns>item from printers</returns>
        public BasePrinter this[int index]
        {
            get
            {
                if(index < 0 || index > this.printers.Count)
                    throw new ArgumentOutOfRangeException($"Argument {nameof(index)} is out of bounds");

                return printers[index];
            }
        }

        /// <summary>
        /// Add new printer in list printers
        /// </summary>
        /// <param name="name">printer`s name</param>
        /// <param name="model">printer`s model</param>
        public void Add(string name, string model)
        {
            if (name == null)
                throw new ArgumentNullException($"Argument {nameof(name)} is null");

            if (model == null)
                throw new ArgumentNullException($"Argument {nameof(model)} is null");

            var printer = new Printer(name, model);

            if (this.Contains(printer))
                throw new ExistPrinterException($"Printer with the same parametrs {nameof(name)} and {nameof(model)} already exist");

            printer.PrintEvent += PrintEventHandler;

            printers.Add(printer);
        }

        /// <summary>
        /// Public method call for print
        /// </summary>
        /// <param name="printer">printer for print</param>
        /// <param name="stream">data stream</param>
        public void Print(BasePrinter printer, Stream stream)
        {
            if (printer == null)
                throw new ArgumentNullException($"Argument {nameof(printer)} is null");

            if (stream == null)
                throw new ArgumentNullException($"Argument {nameof(printer)} is null");

            if(!this.Contains(printer))
                throw new ExistPrinterException($"Printer {nameof(printer)} is absent in list. Please add new printer and than print");

            printer.Print(stream);
        }

        /// <summary>
        /// Check contains printer in printer`s list
        /// </summary>
        /// <param name="printer">instance class printer</param>
        /// <returns>true if contains</returns>
        public bool Contains(BasePrinter printer)
        {
            var equalityComparer = EqualityComparer<BasePrinter>.Default;

            return this.printers.Contains(printer, equalityComparer);
        }

        /// <summary>
        /// Method for get item in list printers
        /// </summary>
        /// <returns>IEnumerable<BasePrinter></BasePrinter></returns>
        public IEnumerable<BasePrinter> GetAll()
        {
            return printers;
        }
    }
}