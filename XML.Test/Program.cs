using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XML.Entity;
using XML.Enum;
using XML.Interfaces;
using XML.Services;

namespace XML.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new UrlService(new Logger());

            var parser = new UrlAddressParser();

            service.ReadTxt(parser);

            Console.WriteLine("Read at txt file");

            foreach (UrlAddress item in service)
            {
                Console.WriteLine(item);
            }

            service.WriteXml();

            Console.WriteLine("Read at xml file");

            foreach (UrlAddress item in service)
            {
                Console.WriteLine(item);
            }
        }
    }
}
