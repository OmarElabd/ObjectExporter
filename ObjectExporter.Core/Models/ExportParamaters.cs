using ObjectExporter.Core.Globals;

namespace ObjectExporter.Core.Models
{
    public class ExportParamaters
    {
        public ExportType ExportType { get; set; }
        public int MaxDepth { get; set; }
        public bool ExludePrivateProperties { get; set; }

        public ExportParamaters(bool exludePrivateProperties, int maxDepth, ExportType exportType)
        {
            ExludePrivateProperties = exludePrivateProperties;
            MaxDepth = maxDepth;
            ExportType = exportType;
        }
    }
}
