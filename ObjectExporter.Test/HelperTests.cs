using ObjectExporter.VsPackage.Helpers;

namespace ObjectExporter.Test
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
