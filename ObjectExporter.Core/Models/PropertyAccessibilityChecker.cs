using System;
using System.Collections.Generic;
using EnvDTE80;

namespace ObjectExporter.Core.Models
{
    public class PropertyAccessibilityChecker
    {
        private readonly AccessibilityRetriever _retriever;
        private readonly Dictionary<Type, List<string>> AccessiblePropertiesInType = new Dictionary<Type, List<string>>();

        public PropertyAccessibilityChecker(AccessibilityRetriever retriever)
        {
            _retriever = retriever;
        }

        public bool IsAccessiblePropertyOrField(string childPropertyName, string expressionType)
        {
            if (childPropertyName.Contains("<") || childPropertyName.Contains(">") ||
                childPropertyName.Contains("[") || childPropertyName.Contains("]"))
            {
                return true;
            }

            List<string> properties = GetAccessibleProperties(expressionType);

            return properties.Contains(childPropertyName);
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
                List<string> properties = _retriever.GetAccessibleFieldsAndProperties(type);
                AccessiblePropertiesInType.Add(type, properties);
                return properties;
            }
        }
    }
}