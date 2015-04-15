using System;
using System.Linq;
using System.Xml;

namespace ObjectExporter.VsPackage.Helpers
{
    public static class VsixManifestHelper
    {
        public static string GetVersionNumber()
        {
            const string manifestPath = "/source.extension.vsixmanifest";

            var doc = new XmlDocument();
            doc.Load(manifestPath);
 
            if (doc.DocumentElement == null || doc.DocumentElement.Name != "PackageManifest") return String.Empty;
 
            var metaData = doc.DocumentElement.ChildNodes.Cast<XmlElement>().First(x => x.Name == "Metadata");
            var identity = metaData.ChildNodes.Cast<XmlElement>().First(x => x.Name == "Identity");
 
            string version = identity.GetAttribute("Version");

            return version;
        }
    }
}
