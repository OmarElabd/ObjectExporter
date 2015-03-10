using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AccretionDynamics.ObjectExporter.VsPackage.Helpers
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
