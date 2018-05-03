using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XML.Entity
{
    /// <summary>
    /// Class represent url address
    /// </summary>
    [Serializable]
    public class UrlAddress
    {
        #region Constructors

        public UrlAddress() { }

        #endregion

        #region Public Api

        public string HostName { get; set; }

        public List<UrlSegment> Uri { get; set; }

        public List<UrlElement> Parametres { get; set; }

        /// <summary>
        /// Override method ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = $"http://{HostName}";

            if (Uri != null)
            {
                foreach (UrlSegment item in Uri)
                    result = $"{result}/{item.Segment}";
            }

            if (Parametres != null)
            {
                result = $"{result}?";

                foreach (UrlElement item in Parametres)
                    result = $"{result}{item.Key}={item.Value},";

                return result.Remove(result.Length - 1, 1);
            }

            return result;
        }

        #endregion
    }
}
