using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Moq;
using NUnit.Framework;
using XML.Entity;
using XML.Interfaces;
using XML.Services;

namespace XML.Moq.Test
{
    [TestFixture]
    public class MoqTest
    {
        private Mock<IDataProvider<string>> dataMock;

        private Mock<IParser<string, UrlAddress>> parserMock;

        private Mock<IXmlProvider<UrlAddress>> xmlMock;

        private List<string> dataList;

        private List<UrlAddress> urlAddresses;

        /// <summary>
        /// Initialize necesery classes members for test
        /// </summary>
        [SetUp]
        public void Initialize()
        {
            this.dataMock = new Mock<IDataProvider<string>>();

            this.parserMock = new Mock<IParser<string, UrlAddress>>();

            this.xmlMock = new Mock<IXmlProvider<UrlAddress>>();

            this.dataList = new List<string>
            {
                "https://github.com/AnzhelikaKravchuk?tab=repositories",
                "https://github.com/AnzhelikaKravchuk/2017-2018.MMF.BSU",
                "https://habrahabr.ru/company/it-grad/blog/341486/"
            };

            this.urlAddresses = new List<UrlAddress>
            {
                new UrlAddress
                {
                    HostName = "github.com",
                    Uri = new List<UrlSegment> { new UrlSegment {Segment = "AnzhelikaKravchuk" } },
                    Parametres = new List<UrlElement> { new UrlElement { Key = "tab", Value = "repositories"} }
                },
                new UrlAddress
                {
                    HostName = "github.com",
                    Uri = new List<UrlSegment>
                    {
                        new UrlSegment {Segment = "AnzhelikaKravchuk" },
                        new UrlSegment {Segment = "2017-2018.MMF.BSU" }
                    },
                    Parametres = null
                },
                new UrlAddress
                {
                    HostName = "habrahabr.ru",
                    Uri = new List<UrlSegment>
                    {
                        new UrlSegment {Segment = "company" },
                        new UrlSegment {Segment = "it-grad" },
                        new UrlSegment {Segment = "blog" },
                        new UrlSegment {Segment = "341486" }
                    },
                    Parametres = null
                }
            };
        }

        /// <summary>
        /// Test save info in storage with valid date
        /// </summary>
        [TestCase]
        public void Save_In_Storage_With_Valid_Data()
        {
            this.dataMock.Setup(item => item.GetData())
                .Returns(() => dataList);

            this.parserMock.Setup(item => item.Map(It.IsAny<IEnumerable<string>>(), new Logger()))
                .Returns(() => urlAddresses);

            this.xmlMock.Setup(item => item.Write(It.IsAny<IEnumerable<UrlAddress>>()))
                .Returns(() => urlAddresses.Count);

            var service = new Service<string, UrlAddress>(dataMock.Object, parserMock.Object, xmlMock.Object);

            Assert.AreEqual(3, service.SaveInStorage());
        }
    }
}
