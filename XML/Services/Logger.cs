using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XML.Interfaces;

namespace XML.Services
{
    /// <summary>
    /// Logger service for write information about don`t valid string
    /// </summary>
    public class Logger : ILogger
    {
        private string pathLogFile;

        public Logger()
        {
            this.pathLogFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["logFile"]);
        }

        public void Write(string message)
        {
            using (var fileStream = new FileStream(pathLogFile, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter strWriter = new StreamWriter(fileStream, Encoding.Default))
                {
                    strWriter.Write(message);

                    strWriter.WriteLine();
                }
            }
        }
    }
}
