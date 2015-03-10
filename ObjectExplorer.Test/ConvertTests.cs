using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;

namespace ObjectExplorer.Test
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
