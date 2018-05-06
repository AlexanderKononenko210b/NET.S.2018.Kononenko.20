using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML.Interfaces
{
    /// <summary>
    /// Interface for XmlProvider
    /// </summary>
    public interface IXmlProvider<TResult>
    {
        int Write(IEnumerable<TResult> results);
    }
}
