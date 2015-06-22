using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using ObjectExporter.Core.ExtensionMethods;

namespace ObjectExporter.Core.Models.RuleSets
{
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
