using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XML.Entity
{
    /// <summary>
    /// Class represent url segment
    /// </summary>
    [Serializable]
    public class UrlSegment
    {
        #region Constructors

        public UrlSegment() {}

        public UrlSegment(string segment)
        {
            this.Segment = segment;
        }

        #endregion

        #region Public Api

        public string Segment { get; set; }

        #endregion
    }
}
