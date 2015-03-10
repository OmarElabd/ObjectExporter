using System;
using ObjectExporter.Core.Templates.Converters;
using EnvDTE;

namespace ObjectExporter.Core.Templates
{
    public partial class CSharpGenerator : IGenerator
    {
        public IConverter Converter { get; set; }

        private bool CanBeExpressedAsSingleType(string expressionType)
        {
            switch (expressionType)
            {
                case "System.Guid":
                case "System.TimeSpan":
                case "System.DateTimeOffset":
                case "System.DateTime":
                case "System.Decimal":
                case "decimal":
                case "System.Char":
                case "char":
                case "System.Single":
                case "float":
                    return true;
                default:
                    return false;
            }
        }


        public void Clear()
        {
            this.isFirstElement = true;
            this.GenerationEnvironment.Clear();
        }

        public string GetSingleTypeValue(Expression expression)
        {
            string formattedString;

            switch (expression.Type)
            {
                case "System.Guid":
                    formattedString = GeneratorHelper.StripCurleyBraces(expression.Value);
                    return String.Format("Guid.Parse(\"{0}\")", formattedString);
                case "System.TimeSpan":
                    formattedString = GeneratorHelper.StripCurleyBraces(expression.Value);
                    return String.Format("TimeSpan.Parse(\"{0}\")", formattedString);
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

                    return String.Format("DateTimeOffset.Parse(\"{0}\")", formattedString);
                case "System.DateTime":
                    formattedString = GeneratorHelper.StripCurleyBraces(expression.Value);
                    return String.Format("DateTime.Parse(\"{0}\")", formattedString);
                case "System.Decimal":
                case "decimal":
                    return Converter.GetDecimalWithLiteral(expression.Value);
                case "System.Char":
                case "char":
                    string charValue = Converter.GetCharWithLiteral(expression.Value); //Retrieve Character Value as a Letter
                    return charValue;
                case "System.Single":
                case "float":
                    return Converter.GetFloatWithLiteral(expression.Value);
                default:
                    return "";
            }
        }
    }
}
