using System;
using EnvDTE;

namespace ObjectExporter.Core.Templates
{
    public static class GeneratorHelper
    {
        public static bool CanBeExpressedAsSingleType(string expressionType)
        {
            switch (expressionType)
            {
                case "System.Guid":
                case "System.TimeSpan":
                case "System.DateTimeOffset":
                case "System.Char":
                case "char":
                case "System.DateTime":
                case "System.Decimal":
                case "decimal":
                case "System.Single":
                case "float":
                    return true;
                default:
                    return false;
            }
        }

        public static string WriteCommaIfNotLast(bool isLast)
        {
            if (isLast) return "";
            else return ",";
        }

        public static bool IsSerializable(string expressionName)
        {
            switch (expressionName)
            {
                case "Raw View":
                case "Static members":
                case "Non-Public members":
                    return false;
                default:
                    return true;
            }
        }

        public static bool IsTypeOfCollection(string expressionType)
        {
            return (expressionType.Contains("<") || expressionType.Contains(">") || expressionType.Contains("[") || expressionType.Contains("]"));
        }

        public static string StripCurleyBraces(string input)
        {
            return input.Replace("{", "").Replace("}", "");
        }

        public static string GetBugFixedDateTimeOffset(Expression expression)
        {
            string dateTimePartStr = StripCurleyBraces(expression.DataMembers.Item(2).Value);
            string offsetPartStr = StripCurleyBraces(expression.DataMembers.Item(11).Value);

            TimeSpan offset = TimeSpan.Parse(offsetPartStr);
            DateTime dateTime = DateTime.Parse(dateTimePartStr);

            DateTimeOffset dateTimeOffset = new DateTimeOffset(dateTime, offset);

            return dateTimeOffset.ToString();
        }
    }
}
