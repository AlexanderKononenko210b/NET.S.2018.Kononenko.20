using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XML.Entity;
using XML.Enum;

namespace XML.Interfaces
{
    public interface IService<T> where T : class
    {
        void ReadTxt(IParser<T> parser);

        void ReadXml();

        void WriteXml();
    }
}
