using System.Collections.Generic;
using EnvDTE;

namespace ObjectExporter.Core.Models.Expressions
{
    public class ExpressionNf
    {
        public bool IsValidValue { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public ExpressionNf Parent { get; set; }
        public List<ExpressionNf> Members { get; set; }

        public ExpressionNf(EnvDTE.Expression expression)
        {
            Name = expression.Name;
            Type = expression.Type;
            Value = expression.Value;
            IsValidValue = expression.IsValidValue;
            Members = new List<ExpressionNf>();
        }

        public ExpressionNf(EnvDTE.Expression expression, int depth) : this(expression)
        {
            if (depth > 0 && expression.DataMembers.Count > 0)
            {
                //Recursively call this constructor until depth of 0
                foreach (EnvDTE.Expression dataMember in expression.DataMembers)
                {
                    ExpressionNf convertedDataMember = new ExpressionNf(dataMember, this, depth - 1);
                    Members.Add(convertedDataMember);
                }
            }
        }

        public ExpressionNf(EnvDTE.Expression expression, ExpressionNf parent, int depth) : this(expression, depth)
        {
            Parent = parent;
        }
    }
}
