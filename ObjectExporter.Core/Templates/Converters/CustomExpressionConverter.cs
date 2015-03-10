using System.Text.RegularExpressions;

namespace ObjectExporter.Core.Templates.Converters
{
    public class CustomExpressionConverter : IConverter
    {
        public string GetCharWithLiteral(string expressionValue)
        {
            return expressionValue;
        }

        public string GetCharAsciiCode(string expressionValue)
        {
            string strippedQuotes = expressionValue.Replace("'", "");
            string unEscaped = Regex.Unescape(strippedQuotes);
            char convertedChar = char.Parse(unEscaped);

            return ((int) convertedChar).ToString(); // return numeric value as string
        }

        public string GetDecimal(string expressionValue)
        {
            return expressionValue.Replace("m", "").Replace("M", "");
        }

        public string GetDecimalWithLiteral(string expressionValue)
        {
            return expressionValue;
        }

        public string GetFloat(string expressionValue)
        {
            return expressionValue.Replace("F", "").Replace("f", "");
        }

        public string GetFloatWithLiteral(string expressionValue)
        {
            return expressionValue;
        }
    }
}
