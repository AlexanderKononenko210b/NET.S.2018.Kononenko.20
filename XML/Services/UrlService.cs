using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Configuration;
using XML.Entity;
using XML.Enum;
using XML.Interfaces;
using System.Collections;

namespace XML.Services
{
    /// <summary>
    /// Repository for read from txt or xml file and write in xml file
    /// </summary>
    public class UrlService : IService<UrlAddress>, IEnumerable<UrlAddress>
    {
        private ILogger logger;

        private XmlSerializer serializer = new XmlSerializer(typeof(List<UrlAddress>));

        private List<UrlAddress> collection = new List<UrlAddress>();

        private string pathSourceFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            ConfigurationManager.AppSettings["fileSource"]);

        private string pathXmlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            ConfigurationManager.AppSettings["fileXml"]);

        public UrlService(ILogger logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Get IEnumerable instance typeof UrlAddress from text or xml file
        /// </summary>
        /// <param name="parser">object for parse string in instance typeof(T)</param>
        public void ReadTxt(IParser<UrlAddress> parser)
        {
            using (var fileStream = new FileStream(pathSourceFile, FileMode.Open, FileAccess.Read))
            {
                using (var stream = new StreamReader(fileStream, Encoding.Default))
                {
                    string helper = String.Empty;

                    while ((helper = stream.ReadLine()) != null)
                    {
                        var result = parser.IsVerify(helper);

                        if (result.Item1 == null)
                        {
                            logger.Write(result.Item2);

                            continue;
                        }

                        collection.Add(result.Item1);
                    }
                }
            }
        }

        /// <summary>
        /// Read from Xml file
        /// </summary>
        public void ReadXml()
        {
            using (var fileStream = new FileStream(pathXmlFile, FileMode.Open, FileAccess.Read))
            {
                collection = (List<UrlAddress>)serializer.Deserialize(fileStream);
            }
        }

        /// <summary>
        /// Write in to XmlFile
        /// </summary>
        public void WriteXml()
        {
            using (var fileStream = new FileStream(pathXmlFile, FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(fileStream, collection);
            }
        }

        /// <summary>
        /// Get enumerator on collection list
        /// </summary>
        /// <returns></returns>
        public IEnumerator<UrlAddress> GetEnumerator()
        {
            return this.collection.GetEnumerator();
        }

        /// <summary>
        /// Explicit reliaze interface IEnumerable
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
