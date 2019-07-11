using System;
using System.Collections.Generic;
using ObjectExporter.Core.ExtensionMethods;

namespace ObjectExporter.Core.Models.RuleSets
{
    /// <summary>
    /// Validates that the property contained inside the class is "accessible" (or public).
    /// This is used for ignoring private fields and properties.
    /// </summary>
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

                // if unable to retrieve type from the TypeRetriever
                if (properties == null)
                {
                    return true;
                }
                else
                {
                    return properties.Contains(childPropertyName);
                }
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        private List<string> GetAccessibleProperties(string expressionType)
        {
            Type type = _retriever.GetTypeFromString(expressionType);

            if (type == null)
            {
                return null;
            }

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