namespace ObjectExporter.Core.Templates.Converters
{
    public interface IConverter
    {
        string GetDecimal(string expressionValue);
        string GetDecimalWithLiteral(string expressionValue);
        string GetCharWithLiteral(string expressionValue);
        string GetCharAsciiCode(string expressionValue);
        string GetFloat(string expressionValue);
        string GetFloatWithLiteral(string expressionValue);
    }
}