using System;
using System.Collections.Generic;
using System.Linq;
using EnvDTE;
using EnvDTE80;
using ObjectExporter.Core.ExtensionMethods;
using ObjectExporter.Core.Globals;
using ObjectExporter.Core.Models;
using ObjectExporter.Core.Models.RuleSets;

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

        public static bool IsBase(Expression expression)
        {
            return (expression.Name == "base" && expression.Type.Contains("{"));
        }

        private static List<string> SimpleTypes = new List<string>()
        {
            "bool",
            "byte",
            "sbyte",
            "char",
            "decimal",
            "double",
            "float",
            "int",
            "uint",
            "long",
            "ulong",
            "object",
            "short",
            "ushort",
            "string"
        };

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
            return (expressionType.Contains("<") || expressionType.Contains(">") || expressionType.Contains("[") ||
                expressionType.Contains("]") || expressionType.Contains("Count ="));
        }

        public static bool IsCollectionMember(string expressionName)
        {
            return (expressionName.Contains("[") || expressionName.Contains("]"));
        }

        public static string StripCurleyBraces(string input)
        {
            return input.Replace("{", "").Replace("}", "");
        }

        public static string StripObjectReference(string input)
        {
            if (input.Contains("{") && input.Contains("}"))
            {
                return input.Between('{', '}').Trim();
            }
            else
            {
                return input;
            }
        }

        public static string StripChildReference(string input)
        {
            if (input.Contains("{") && input.Contains("}"))
            {
                int index = input.IndexOf("{");
                return input.Substring(0, index - 1).Trim();
            }
            else
            {
                return input;
            }
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

        public static string ResolveReservedNames(string expressionName)
        {
            if (ReservedWords.CSharp.Contains(expressionName))
            {
                return ("@" + expressionName);
            }
            else
            {
                return expressionName;
            }
        }

        public static List<Expression> SanitizeExpressions(Expression expression, RuleSetValidator ruleSetValidator, string parentExpressionType)
        {
            var expressionMembers = expression.DataMembers.Cast<Expression>().ToList();
            var cleanedExpressionMembers = new List<Expression>();

            for (int i = 0; i < expressionMembers.Count; i++)
            {
                Expression currentExpression = expressionMembers[i];

                //Ignore collections
                if (IsTypeOfCollection(currentExpression.Type))
                {
                    cleanedExpressionMembers.Add(currentExpression);
                }
                //Add base type members to the list at the current level
                else if (IsBase(currentExpression))
                {
                    expressionMembers.AddRange(currentExpression.DataMembers.Cast<Expression>());
                }
                else if (IsSerializable(currentExpression.Name))
                {
                    //check accessibility
                    bool isValid = ruleSetValidator.ValidateAllSubRules(parentExpressionType, currentExpression.Name);

                    if (isValid)
                    {
                        cleanedExpressionMembers.Add(currentExpression);
                    }
                }
            }

            return cleanedExpressionMembers;
        }
    }
}
