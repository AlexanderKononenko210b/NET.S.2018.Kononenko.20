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

namespace XML.Services
{
    /// <summary>
    /// Repository for read from txt or xml file and write in xml file
    /// </summary>
    public class UrlService : IUrlService<UrlAddress>
    {
        private const string SCHEME = "https://";

        private XmlSerializer serializer = new XmlSerializer(typeof(List<UrlAddress>));

        private List<UrlAddress> urlAddresses = new List<UrlAddress>();

        private string pathSourceFile;

        private string pathXmlFile;

        private ILogger logger;

        public UrlService(ILogger logger)
        {
            this.logger = logger;

            this.pathSourceFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["fileSource"]);

            this.pathXmlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["fileXml"]);
        }

        public List<UrlAddress> UrlAddresses => urlAddresses;

        /// <summary>
        /// Get IEnumerable instance typeof UrlAddress from text or xml file
        /// </summary>
        /// <param name="type">type read: from txt or xml file</param>
        /// <returns>IEnumerable instance typeof UrlAddress</returns>
        public void Read(TypeRead type)
        {
            switch (type)
            {
                case TypeRead.Text:
                {
                    using (var fileStream = new FileStream(pathSourceFile, FileMode.Open, FileAccess.Read))
                    {
                        using (var stream = new StreamReader(fileStream, Encoding.Default))
                        {
                            string helper = String.Empty;

                            while ((helper = stream.ReadLine()) != null)
                            {
                                var resultVerify = Verify(helper);

                                if (!resultVerify.Item1)
                                {
                                    logger.Write(resultVerify.Item4);

                                    continue;
                                }

                                UrlAddress urlAddress = null;

                                if (resultVerify.Item2.Length == 1 && resultVerify.Item3 == null)
                                {
                                    urlAddress = new UrlAddress(resultVerify.Item2[0]);
                                }
                                else if (resultVerify.Item2.Length > 1 && resultVerify.Item3 == null)
                                {
                                    var helperArray = new string[resultVerify.Item2.Length - 1];

                                    Array.Copy(resultVerify.Item2, 1, helperArray, 0, resultVerify.Item2.Length - 1);

                                    urlAddress = new UrlAddress(resultVerify.Item2[0], helperArray);
                                }
                                else
                                {
                                    var helperArray = new string[resultVerify.Item2.Length - 1];

                                    Array.Copy(resultVerify.Item2, 1, helperArray, 0, resultVerify.Item2.Length - 1);

                                    urlAddress = new UrlAddress(resultVerify.Item2[0], helperArray, resultVerify.Item3);
                                }

                                urlAddresses.Add(urlAddress);
                            }
                        }
                    }
                    break;
                }
                case TypeRead.Xml:
                {
                    using (var fileStream = new FileStream(pathXmlFile, FileMode.Open, FileAccess.Write))
                    {
                        urlAddresses = (List<UrlAddress>)serializer.Deserialize(fileStream);
                    }
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException($"Unknown {type}");
            }
        }

        /// <summary>
        /// Write IEnumerable instance typeof UrlAddress in xml file
        /// </summary>
        public void Write()
        {
            using (var fileStream = new FileStream(pathXmlFile, FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(fileStream, urlAddresses);
            }
        }

        /// <summary>
        /// Method for check validate input string
        /// </summary>
        /// <param name="input">input string</param>
        /// <returns>true if string is valid, 
        /// array host and url params,
        /// array params or null if they absent,
        /// message result verify</returns>
        private (bool, string[], string[], string) Verify(string input)
        {
            if(String.IsNullOrWhiteSpace(input))
                return (false, null, null, $"{input} (null or whiteSpace, time: {DateTime.Now})");

            var indexScheme = input.IndexOf(SCHEME, StringComparison.CurrentCulture);

            if (indexScheme != 0)
                return (false, null, null, $"{input} (part scheme is not valid, time: {DateTime.Now})");

            if (input.Length <= SCHEME.Length)
                return (false, null, null, $"{input} (part <host>/<URL-path> is absent, time: {DateTime.Now})");

            var index = input.IndexOf('?');

            if (index != -1)
            {
                var parametrs = input.Substring(index + 1).Split(new [] { '=', ',' }, StringSplitOptions.RemoveEmptyEntries);

                if (!parametrs.Any())
                    return (false, null, null, $"{input} (part <parameters> exist but not valid, time: {DateTime.Now})");

                if (parametrs.Length % 2 != 0)
                    return (false, null, null, $"{input} (part <parameters> exist but not valid, time: {DateTime.Now})");

                var hostAndUrlPath = input.Substring(SCHEME.Length, index - SCHEME.Length)
                        .Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                if (!hostAndUrlPath.Any())
                    return (false, null, null, $"{input} (part <host>/<URL-path> is apsent, time: {DateTime.Now})");

                if (hostAndUrlPath[0].IndexOf('.') == -1)
                    return (false, null, null, $"{input} (part <host>is not valid, time: {DateTime.Now})");

                return (true, hostAndUrlPath, parametrs, $"Input string is valid, time: {DateTime.Now}");
            }
            else
            {
                var hostAndUrlPath = input.Substring(SCHEME.Length)
                        .Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                if (!hostAndUrlPath.Any())
                    return (false, null, null, $"{input} (part <host>/<URL-path> is apsent, time: {DateTime.Now})");

                if (hostAndUrlPath[0].IndexOf('.') == -1)
                    return (false, null, null, $"{input} (part <host>/<URL-path> is apsent, time: {DateTime.Now})");

                return (true, hostAndUrlPath, null, $"Input string is valid without params, time: {DateTime.Now}");
            }
        }
    }
}
