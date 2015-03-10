using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccretionDynamics.ObjectExporter.VsPackage.Helpers;

namespace ObjectExplorer.Test
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class MyTestClass
    {
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void TestGetVersionNumber()
        {
            string version = VsixManifestHelper.GetVersionNumber();
        }
    }
}
