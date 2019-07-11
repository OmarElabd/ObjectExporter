using System;
using System.Collections.Generic;
using ObjectExporter.Core.Templates.Converters;
using EnvDTE;
using ObjectExporter.Core.Models.RuleSets;

// ReSharper disable once CheckNamespace
namespace ObjectExporter.Core.Templates
{
    public partial class CSharpGenerator : IGenerator
    {
        public IConverter Converter { get; set; }
        private readonly RuleSetValidator _ruleSetValidator;

        public CSharpGenerator(RuleSetValidator ruleSetValidator)
        {
            _ruleSetValidator = ruleSetValidator;
        }

        private bool CanBeExpressedAsSingleType(string expressionType)
        {
            List<string> whiteList = new List<string>()
            {
                "System.Guid",
                "System.Guid?",
                "System.TimeSpan",
                "System.TimeSpan?",
                "System.DateTime",
                "System.DateTime?",
                "System.DateTimeOffset",
                "System.DateTimeOffset?",
                "System.Decimal",
                "System.Decimal?",
                "decimal",
                "decimal?",
                "System.Char",
                "System.Char?",
                "char",
                "char?",
                "System.Single",
                "System.Single?",
                "float",
                "float?"
            };

            return whiteList.Contains(expressionType);
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
                    return String.Format("new Guid(\"{0}\")", formattedString);
                case "System.TimeSpan":
                case "System.TimeSpan?":
                    formattedString = GeneratorHelper.StripCurleyBraces(expression.Value);
                    TimeSpan timeSpan = TimeSpan.Parse(formattedString);

                    return String.Format("new TimeSpan({0}, {1}, {2}, {3}, {4})", 
                        timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
                case "System.DateTimeOffset":
                case "System.DateTimeOffset?":
                    // TODO check System.DateTimeOffset?
                    if (expression.Value == "{System.DateTimeOffset}")
                    {
                        //NOTE: for some reason the expression.Value is not being set correctly 
                        //More details can be found here https://connect.microsoft.com/VisualStudio/feedback/details/1159889
                        formattedString = GeneratorHelper.GetBugFixedDateTimeOffset(expression);
                    }
                    else
                    {
                        formattedString = GeneratorHelper.StripCurleyBraces(expression.Value);
                    }

                    DateTimeOffset dateTimeOffset = DateTimeOffset.Parse(formattedString);

                    return String.Format("new DateTimeOffset({0}, {1}, {2}, {3}, {4}, {5}, {6}, new TimeSpan({7}, {8}, {9}, {10}, {11}))", 
                        dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day, dateTimeOffset.Hour, 
                        dateTimeOffset.Minute, dateTimeOffset.Second, dateTimeOffset.Millisecond, 
                        dateTimeOffset.Offset.Days, dateTimeOffset.Offset.Hours, dateTimeOffset.Offset.Minutes, 
                        dateTimeOffset.Offset.Seconds, dateTimeOffset.Millisecond);
                case "System.DateTime":
                case "System.DateTime?":
                    formattedString = GeneratorHelper.StripCurleyBraces(expression.Value);
                    DateTime dateTime = DateTime.Parse(formattedString);

                    // TODO parse children for milisecond and datetime.kind
                    return String.Format("new DateTime({0}, {1}, {2}, {3}, {4}, {5})", 
                        dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 
                        dateTime.Minute, dateTime.Second);
                case "System.Decimal":
                case "System.Decimal?":
                case "decimal":
                case "decimal?":
                    return Converter.GetDecimalWithLiteral(expression.Value);
                case "System.Char":
                case "System.Char?":
                case "char":
                case "char?":
                    string charValue = Converter.GetCharWithLiteral(expression.Value); //Retrieve Character Value as a Letter
                    return charValue;
                case "System.Single":
                case "System.Single?":
                case "float":
                case "float?":
                    return Converter.GetFloatWithLiteral(expression.Value);
                default:
                    return String.Empty;
            }
        }
    }
}
