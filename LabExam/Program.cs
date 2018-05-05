using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LabExam.Entity;
using LabExam.Exceptions;
using LabExam.Interfaces;
using LabExam.Service;
using Ninject;

namespace LabExam
{
    class Program
    {
        private static readonly PrintManagerService manager;

        static Program()
        {
            manager = PrintManagerService.Instance;
        }

        [STAThread]
        static void Main(string[] args)
        {
            GetMenu();
        }

        /// <summary>
        /// Get menu and check choose user
        /// </summary>
        private static void GetMenu()
        {
            Console.WriteLine("Select your choice:");
            Console.WriteLine("1:Add new printer");
            var index = 2;
            foreach (BasePrinter item in manager.GetAll())
            {
                Console.WriteLine($"{index++}.Print in {item.Name} - {item.Model}");
            }
            Console.WriteLine("0 - end program");

            var key = Console.ReadKey();

            if (key.Key == ConsoleKey.D1)
            {
                Console.WriteLine();
                CreatePrinter();
            }
            else if (key.Key >= ConsoleKey.D2 && key.Key <= ConsoleKey.D2 + index)
            {
                try
                {
                    Print(manager[(int)key.Key - (int)ConsoleKey.D2]);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                    GetMenu();
                }
            }
            else if (key.Key == ConsoleKey.D0)
            {
                Console.WriteLine();
                Console.WriteLine($"Application is over");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"Undefined key try again");
                GetMenu();
            }
        }

        /// <summary>
        /// Print data in printer
        /// </summary>
        /// <param name="printer"></param>
        private static void Print(BasePrinter printer)
        {
            try
            {
                using (var dialog = new OpenFileDialog())
                {
                    dialog.ShowDialog();
                    using (var fileStream = File.OpenRead(dialog.FileName))
                    {
                        printer.Print(fileStream);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                GetMenu();
            }

            GetMenu();
        }

        /// <summary>
        /// Create new printer
        /// </summary>
        private static void CreatePrinter()
        {
            Console.WriteLine("Enter printer name");
            var name = Console.ReadLine();
            Console.WriteLine("Enter printer model");
            var model = Console.ReadLine();

            try
            {
                manager.Add(name, model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                GetMenu();
            }

            GetMenu();
        }
    }
}
