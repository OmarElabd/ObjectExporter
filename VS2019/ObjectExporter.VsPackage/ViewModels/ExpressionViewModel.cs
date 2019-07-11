using EnvDTE;

namespace ObjectExporter.VsPackage.ViewModels
{
    public class ExpressionViewModel
    {
        public string DisplayName { get; set; }
        public Expression Expression { get; set; }

        public ExpressionViewModel(Expression expression, string displayName)
        {
            DisplayName = displayName;
            Expression = expression;
        }
    }
}
