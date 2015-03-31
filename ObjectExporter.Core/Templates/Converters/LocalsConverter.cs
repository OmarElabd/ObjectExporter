namespace ObjectExporter.Core.Templates.Converters  
{
    public class LocalsConverter : IConverter
    {
        public string GetCharWithLiteral(string expressionValue)
        {
            return expressionValue.Split(' ')[1];
        }

        public string GetCharAsciiCode(string expressionValue)
        {
            return expressionValue.Split(' ')[0];
        }

        public string GetDecimal(string expressionValue)
        {
            return expressionValue;
        }

        public string GetDecimalWithLiteral(string expressionValue)
        {
            return expressionValue + "m";
        }

        public string GetFloat(string expressionValue)
        {
            return expressionValue;
        }

        public string GetFloatWithLiteral(string expressionValue)
        {
            return expressionValue + "f";
        }
    }
}
