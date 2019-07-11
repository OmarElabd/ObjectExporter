using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using ObjectExporter.Core.Templates;

namespace ObjectExporter.Core.Models.RuleSets
{
    public class RuleSetValidator
    {
        private readonly List<IRuleSet> _ruleSets;

        public RuleSetValidator(List<IRuleSet> ruleSets )
        {
            _ruleSets = ruleSets;
        }

        public bool ValidateAllSubRules(string expressionType, string dataMemberName)
        {
            //If any rule is not valid, return false. Otherwise return true
            foreach (IRuleSet ruleSet in _ruleSets)
            {
                try
                {
                    if (!ruleSet.IsValid(expressionType, dataMemberName))
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    //If any rule throws an exception we state it is valid, however other rule sets might invalidate it. 
                    //So eat the exception and if the rest are valid, it will return true anyway.
                }
            }

            return true;
        }
    }
}
