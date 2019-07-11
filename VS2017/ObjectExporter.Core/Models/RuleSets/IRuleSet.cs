//NOTE: strategy pattern

namespace ObjectExporter.Core.Models.RuleSets
{
    public interface IRuleSet
    {
        bool IsValid(string expressionType, string dataMemberPropertyName);
    }
}