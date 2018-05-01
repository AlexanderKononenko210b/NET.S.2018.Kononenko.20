using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            IUrlService<UrlAddress> service = new UrlService(new Logger());

            service.Read(TypeRead.Text);

            Console.WriteLine("Read at txt file");

            foreach (UrlAddress item in service.UrlAddresses)
            {
                Console.WriteLine(item);
            }

            service.Write();

            Console.WriteLine("Read at xml file");

            foreach (UrlAddress item in service.UrlAddresses)
            {
                Console.WriteLine(item);
            }
        }
    }
}
