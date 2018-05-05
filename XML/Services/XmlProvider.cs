using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using XML.Entity;
using XML.Interfaces;

namespace XML.Services
{
    /// <summary>
    /// Service for write and read xml file
    /// </summary>
    public class XmlProvider : IXmlProvider<UrlAddress>
    {
        public void Write(IEnumerable<UrlAddress> results)
        {
            XDocument document = new XDocument(
                new XDeclaration("1.0", "utf-16", "yes"),
                new XElement("addresses",
                    from address in results
                    select
                    new XElement("address",
                        new XElement("host", address.HostName),
                        address.Uri == null ? null : 
                        from segment in address.Uri
                        select
                        new XElement("uri", segment.Segment),
                        address.Parametres == null ? null :
                        from parameter in address.Parametres
                        select
                        new XElement("parameter",
                            new XAttribute("key", parameter.Key),
                            new XAttribute("value", parameter.Value)))));

           document.Save(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["xml"]));
        }
    }
}
