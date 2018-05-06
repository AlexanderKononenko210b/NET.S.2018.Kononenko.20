using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XML.Entity;
using XML.Interfaces;

namespace XML.Services
{
    /// <summary>
    /// Provider for data in text file
    /// </summary>
    public class DataProvider : IDataProvider<string>
    {
        public IEnumerable<string> GetData()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                ConfigurationManager.AppSettings["source"]);

            if (!File.Exists(path))
                throw new FileNotFoundException($"File path {path} is not exist");

            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (var stream = new StreamReader(fileStream, Encoding.Default))
                {
                    var helper = String.Empty;

                    while ((helper = stream.ReadLine()) != null)
                    {
                        yield return helper;
                    }
                }
            }
        }
    }
}
