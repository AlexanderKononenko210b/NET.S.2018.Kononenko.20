using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XML.Entity;
using XML.Interfaces;
using XML.Services;

namespace XML.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new Service<string,UrlAddress>(new DataProvider(),
                new Parser(), new XmlProvider());

            service.SaveInStorage();
        }
    }
}
