using System;
using System.Xml;
using EnvDTE;
using ObjectExporter.Core.Models.RuleSets;
using ObjectExporter.Core.Templates.Converters;

// ReSharper disable once CheckNamespace
namespace ObjectExporter.Core.Templates
{
    public partial class JsonGenerator : IGenerator
    {
        public IConverter Converter { get; set; }

        private readonly RuleSetValidator _ruleSetValidator;
        public JsonGenerator(RuleSetValidator ruleSetValidator)
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

            if (expression.Value == "null")
            {
                return "null";
            }

            switch (expressionType)
            {
                case "System.Guid":
                case "System.Guid?":
                    formattedString = GeneratorHelper.StripCurleyBraces(expression.Value);
                    return String.Format("\"{0}\"", formattedString);
                case "System.TimeSpan":
                case "System.TimeSpan?":
                    formattedString = GeneratorHelper.StripCurleyBraces(expression.Value);
                    return String.Format("\"{0}\"", formattedString);
                case "System.DateTimeOffset":
                case "System.DateTimeOffset?":
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

                    DateTimeOffset currentOffset = DateTimeOffset.Parse(formattedString);
                    return String.Format("\"{0}\"", XmlConvert.ToString(currentOffset));
                case "System.DateTime":
                case "System.DateTime?":
                    formattedString = GeneratorHelper.StripCurleyBraces(expression.Value);
                    DateTime currentDateTime = DateTime.Parse(formattedString);
                    return String.Format("\"{0}\"", XmlConvert.ToString(currentDateTime));
                case "System.Char":
                case "System.Char?":
                case "char":
                case "char?":
                    string charValue = Converter.GetCharWithLiteral(expression.Value); //Retrieve Character Value as a Letter

                    if (charValue == "'\\0'")
                    {
                        charValue = "\"\\u0000\"";
                    }
                    else
                    {
                        charValue = charValue.Replace("'", "\"");
                    }

                    return charValue;
                case "System.Decimal":
                case "System.Decimal?":
                case "decimal":
                case "decimal?":
                    return Converter.GetDecimal(expression.Value);
                case "System.Single":
                case "System.Single?":
                case "float":
                case "float?":
                    return Converter.GetFloat(expression.Value);
                default:
                    return String.Empty;
            }
        }
    }
}
