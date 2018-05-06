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
        public void Write(string message)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                ConfigurationManager.AppSettings["log"]);

            if (!File.Exists(path))
                File.Create(path);

            using (var fileStream = new FileStream(path, FileMode.Append, FileAccess.Write))
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
