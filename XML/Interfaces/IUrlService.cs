using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XML.Entity;
using XML.Enum;

namespace XML.Interfaces
{
    public interface IUrlService<T> where T : UrlAddress
    {
        List<UrlAddress> UrlAddresses { get; }

        void Read(TypeRead type);

        void Write();
    }
}
