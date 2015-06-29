using System;
using System.Collections.Generic;
using ObjectExporter.Core.ExtensionMethods;

namespace ObjectExporter.Core.Models.RuleSets
{
    public class AccessiblePropertiesRuleSet : IRuleSet
    {
        private readonly TypeRetriever _retriever;
        private readonly Dictionary<Type, List<string>> AccessiblePropertiesInType = new Dictionary<Type, List<string>>();

        public AccessiblePropertiesRuleSet(TypeRetriever retriever)
        {
            _retriever = retriever;
        }

        public bool IsValid(string expressionType, string childPropertyName)
        {
            if (childPropertyName.Contains("<") || childPropertyName.Contains(">") ||
                childPropertyName.Contains("[") || childPropertyName.Contains("]"))
            {
                return true;
            }

            try
            {
                List<string> properties = GetAccessibleProperties(expressionType);
                return properties.Contains(childPropertyName);
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        private List<string> GetAccessibleProperties(string expressionType)
        {
            Type type = _retriever.GetTypeFromString(expressionType);

            if (AccessiblePropertiesInType.ContainsKey(type))
            {
                return AccessiblePropertiesInType[type];
            }
            else
            {
                List<string> properties = type.GetAccessibleFieldAndPropertyNames();
                AccessiblePropertiesInType.Add(type, properties);
                return properties;
            }
        }
    }
}