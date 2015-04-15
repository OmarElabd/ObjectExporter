using System;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ObjectExporter.Test
{
    [TestClass]
    public class XmlTests
    {
        [TestMethod]
        public void XmlConverter()
        {
            
            string dateNow = DateTime.Now.ToString();
            DateTime time = DateTime.Parse(dateNow);
            string dateTime = XmlConvert.ToString(DateTime.Now);
        }
    }
}
