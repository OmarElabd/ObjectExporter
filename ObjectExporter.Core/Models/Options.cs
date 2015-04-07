using ObjectExporter.Core.Globals;

namespace ObjectExporter.Core.Models
{
    public class Options
    {
        public ExportType ExportType { get; set; }
        public int MaxDepth { get; set; }
        public bool ExludePrivateProperties { get; set; }

        public Options(bool exludePrivateProperties, int maxDepth, ExportType exportType)
        {
            ExludePrivateProperties = exludePrivateProperties;
            MaxDepth = maxDepth;
            ExportType = exportType;
        }
    }
}
