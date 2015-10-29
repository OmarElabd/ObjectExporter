using EnvDTE;
using ObjectExporter.Core.Globals;

namespace ObjectExporter.Core.Models.Expressions
{
    public class ExpressionWithSource
    {
        public Expression Expression { get; set; }
        public ExpressionSourceType Source { get; set; }

        public ExpressionWithSource(Expression expression, ExpressionSourceType source)
        {
            Expression = expression;
            Source = source;
        }
    }
}
