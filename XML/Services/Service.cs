using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Configuration;
using XML.Entity;
using XML.Interfaces;
using System.Collections;
using System.Xml;
using System.Xml.Linq;

namespace XML.Services
{
    /// <summary>
    /// Repository for read from txt and write in xml file
    /// </summary>
    public class Service<TSource, TResult>
    {
        private IDataProvider<TSource> provider;

        private IParser<TSource, TResult> parser;

        private IXmlProvider<TResult> storage;

        public Service(IDataProvider<TSource> provider, 
            IParser<TSource, TResult> parser, IXmlProvider<TResult> storage)
        {
            this.provider = provider;

            this.parser = parser;

            this.storage = storage;
        }

        public void SaveInStorage()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["source"]);

            if (!File.Exists(path))
                throw new FileNotFoundException($"File path {path} is not exist");

            var strings = provider.GetData(path);

            var addresses = parser.Map(strings);

            storage.Write(addresses);
        }
    }
}
