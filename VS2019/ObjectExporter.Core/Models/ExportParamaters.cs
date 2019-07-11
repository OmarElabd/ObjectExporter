using ObjectExporter.Core.Globals;

namespace ObjectExporter.Core.Models
{
    public class ExportParamaters
    {
        public ExportType ExportType { get; set; }
        public int MaxDepth { get; set; }
        public bool ExludePrivateProperties { get; set; }
        public bool ExcludePropertiesNotInClass { get; set; }

        public ExportParamaters(bool exludePrivateProperties, bool excludePropertiesNotInClass, int maxDepth, ExportType exportType)
        {
            ExludePrivateProperties = exludePrivateProperties;
            ExcludePropertiesNotInClass = excludePropertiesNotInClass;
            MaxDepth = maxDepth;
            ExportType = exportType;
        }
    }
}
