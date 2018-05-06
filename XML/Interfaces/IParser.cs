using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XML.Interfaces
{
    /// <summary>
    /// Interface for parser
    /// </summary>
    public interface IParser<in TSource, out TResult>
    {
        IEnumerable<TResult> Map(IEnumerable<TSource> sources, ILogger logger);
    }
}
