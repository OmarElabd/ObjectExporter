using System;
using System.Collections.Generic;
using ObjectExporter.Core.ExtensionMethods;

namespace ObjectExporter.Core.Models.RuleSets
{
    /// <summary>
    /// Validate that the property we are attempting to export is actually contained in the object's class.
    /// This allows for dynamically added properties to be ignored
    /// </summary>
    public class PropertyInClassRuleSet : IRuleSet
    {
        private readonly TypeRetriever _retriever;

        public PropertyInClassRuleSet(TypeRetriever retriever)
        {
            _retriever = retriever;
        }

        public bool IsValid(string expressionType, string dataMemberPropertyName)
        {
            Type type = _retriever.GetTypeFromString(expressionType);

            List<string> properties = type.GetAllPropertyNames();

            return properties.Contains(dataMemberPropertyName);
        }
    }
}
