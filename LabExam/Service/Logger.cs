using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabExam.Interfaces;

namespace LabExam.Service
{
    public class Logger : ILogger
    {
        public void Write(string message)
        {
            string source = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log.txt");

            using (FileStream fileOut = new FileStream(source, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter strWriter = new StreamWriter(fileOut, Encoding.Default))
                {
                    strWriter.Write(message);

                    strWriter.WriteLine();
                }
            }
        }
    }
}
