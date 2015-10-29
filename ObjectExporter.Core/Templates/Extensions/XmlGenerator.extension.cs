using System;
using System.Xml;
using EnvDTE;
using ObjectExporter.Core.Models;
using ObjectExporter.Core.Models.RuleSets;
using ObjectExporter.Core.Templates.Converters;

namespace ObjectExporter.Core.Templates
{
    public partial class XmlGenerator : IGenerator
    {
        public IConverter Converter { get; set; }

        private readonly RuleSetValidator _ruleSetValidator;
        public XmlGenerator(RuleSetValidator ruleSetValidator)
        {
            _ruleSetValidator = ruleSetValidator;
        }

        public void Clear()
        {
            this.isFirstElement = true;
            this.GenerationEnvironment.Clear();
        }

        public string GetSingleTypeValue(Expression expression)
        {
            string formattedString;

            string expressionType = GeneratorHelper.GetBaseClassFromType(expression.Type);

            switch (expressionType)
            {
                case "System.Guid":
                    formattedString = GeneratorHelper.StripCurleyBraces(expression.Value);
                    return XmlConvert.ToString(Guid.Parse(formattedString));
                case "System.TimeSpan":
                    formattedString = GeneratorHelper.StripCurleyBraces(expression.Value);
                    return XmlConvert.ToString(TimeSpan.Parse(formattedString));
                case "System.DateTimeOffset":
                    if (expression.Value == "{System.DateTimeOffset}")
                    {
                        //Fix: for some reason the expression.Value is not being set correctly 
                        //More details can be found here https://connect.microsoft.com/VisualStudio/feedback/details/1159889
                        formattedString = GeneratorHelper.GetBugFixedDateTimeOffset(expression);
                    }
                    else
                    {
                        formattedString = GeneratorHelper.StripCurleyBraces(expression.Value);
                    }

                    return XmlConvert.ToString(DateTimeOffset.Parse(formattedString));
                case "System.DateTime":
                    formattedString = GeneratorHelper.StripCurleyBraces(expression.Value);
                    return XmlConvert.ToString(DateTime.Parse(formattedString));
                case "System.Char":
                case "char":
                    string charValue = Converter.GetCharAsciiCode(expression.Value); //Retrieve the CharValue as a number
                    return charValue;
                case "System.Decimal":
                case "decimal":
                    return Converter.GetDecimal(expression.Value);
                case "System.Single":
                case "float":
                    return Converter.GetFloat(expression.Value);
                default:
                    return "";
            }
        }
    }
}
