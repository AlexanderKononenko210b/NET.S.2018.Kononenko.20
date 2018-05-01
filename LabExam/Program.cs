using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LabExam.Entity;
using LabExam.Interfaces;
using LabExam.Service;
using Ninject;

namespace LabExam
{
    class Program
    {
        private static readonly IPrinterManager manager;

        private static readonly int index;

        static Program()
        {
            var resolver = new StandardKernel();
            resolver.ConfigurateResolver();
            manager = resolver.Get<IPrinterManager>();
            index = manager.GetAll().Count;
        }

        [STAThread]
        static void Main(string[] args)
        {
            GetMenu();
        }

        private static void GetMenu()
        {
            Console.WriteLine("Select your choice:");
            Console.WriteLine("1:Add new printer");
            var index = 2;
            foreach (BasePrinter item in manager.GetAll())
            {
                Console.WriteLine($"{index++}.Print in {item.Name} - {item.Model}");
            }

            var key = Console.ReadKey();

            if (key.Key == ConsoleKey.D1)
            {
                CreatePrinter();
            }

            if (key.Key >= ConsoleKey.D2 || key.Key <= ConsoleKey.D2 + index)
            {
                Print(manager.GetAll()[(int)key.Key - (int)ConsoleKey.D2]);
            }

            while (true)
            {
                // waiting
            }
        }

        private static void Print(BasePrinter printer)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.ShowDialog();
                using (var fileStream = File.OpenRead(dialog.FileName))
                {
                    printer.Print(fileStream);
                }
            }

            GetMenu();
        }

        private static void CreatePrinter()
        {
            Console.WriteLine("Enter printer name");
            var name = Console.ReadLine();
            Console.WriteLine("Enter printer model");
            var model = Console.ReadLine();
            
            manager.Add(name, model);

            GetMenu();
        }
    }
}
