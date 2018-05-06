using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML.Interfaces
{
    /// <summary>
    /// Interface for data provider
    /// </summary>
    public interface IDataProvider<out TSourse>
    {
        IEnumerable<TSourse> GetData();
    }
}
