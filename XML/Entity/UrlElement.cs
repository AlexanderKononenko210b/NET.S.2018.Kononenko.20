using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XML.Entity
{
    /// <summary>
    /// Class represent url element
    /// </summary>
    [Serializable]
    public class UrlElement
    {
        #region Constructors

        public UrlElement() {}

        public UrlElement(string key, string value)
        {
            this.Key = key;

            this.Value = value;
        }

        #endregion

        #region Public Api

        public string Key { get; set; }

        public string Value { get; set; }

        #endregion
    }
}
