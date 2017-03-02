using System;
using System.Collections.Generic;
using System.Linq;
using EnvDTE;
using EnvDTE80;
using ObjectExporter.Core.ExtensionMethods;
using ObjectExporter.Core.Globals;
using ObjectExporter.Core.Models.RuleSets;

namespace ObjectExporter.Core.Templates
{
    public static class GeneratorHelper
    {
        public static bool CanBeExpressedAsSingleType(string expressionType)
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

        public static bool IsBase(Expression expression)
        {
            return (expression.Name == "base" && expression.Type.Contains("{"));
        }

        private static List<string> SimpleTypes = new List<string>()
        {
            "bool",
            "bool?",
            "byte",
            "byte?",
            "sbyte",
            "sbyte?",
            "char",
            "char?",
            "decimal",
            "decimal?",
            "double",
            "double?",
            "float",
            "float?",
            "int",
            "int?",
            "uint",
            "uint?",
            "long",
            "long?",
            "ulong",
            "ulong?",
            "object",
            "object?",
            "short",
            "short?",
            "ushort",
            "ushort?",
            "string"
        };

        public static string WriteCommaIfNotLast(bool isLast)
        {
            if (isLast)
            {
                return "";
            }
            else
            {
                return ",";
            }
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

        /// <returns></returns>
        public static string StripCurleyBraces(string input)
        {
            return input.Replace("{", "").Replace("}", "");
        }

        /// <summary>
        /// Strips the subclass from a string.
        /// <example>e.g. baseclass { subclass } returns subclass</example>
        /// </summary>
        /// <param name="inputType">input String</param>
        /// <returns>subclass type</returns>
        public static string GetSubClassFromType(string inputType)
        {
            if (inputType.Contains("{") && inputType.Contains("}"))
            {
                return inputType.Between('{', '}').Trim();
            }
            else
            {
                return inputType;
            }
        }

        /// <summary>
        /// Strips the base class from a string.
        /// <example>e.g. baseclass { subclass } returns baseclass</example>
        /// </summary>
        /// <param name="inputType">input string</param>
        /// <returns>base class type</returns>
        public static string GetBaseClassFromType(string inputType)
        {
            if (inputType.Contains("{") && inputType.Contains("}"))
            {
                int index = inputType.IndexOf("{");
                return inputType.Substring(0, index - 1).Trim();
            }
            else
            {
                return inputType;
            }
        }

        public static bool HasEfDynamicProxiesReference(string inputType)
        {
            //TODO should probably check that it also ends with "_GUID" (of length 64)

            if (inputType.Contains("{") && inputType.Contains("}"))
            {
                string subclassType = GetSubClassFromType(inputType);
                return subclassType.StartsWith("System.Data.Entity.DynamicProxies.");

            }
            else
            {
                return inputType.StartsWith("System.Data.Entity.DynamicProxies.");
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
