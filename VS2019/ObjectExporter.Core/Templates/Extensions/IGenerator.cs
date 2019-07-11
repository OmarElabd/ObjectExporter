using System.Collections.Generic;
using EnvDTE;
using ObjectExporter.Core.Templates.Converters;

namespace ObjectExporter.Core.Templates
{
    public interface IGenerator
    {
        IDictionary<string, object> Session { get; set; }
        IConverter Converter { get; set; }
        void Clear();
        string GetSingleTypeValue(Expression expression);
        string TransformText();
        void Initialize();
    }
}