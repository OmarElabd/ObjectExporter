using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using ObjectExporter.Core.Templates;

namespace ObjectExporter.Core.Models
{
    public class ObjectDepthFinder
    {
        private List<Expression> _visitedExpressions = new List<Expression>();
        private int _maxDepth = 0;

        private readonly int _cutoff;

        public ObjectDepthFinder(int cutoff = 100)
        {
            _cutoff = cutoff;
        }

        public int GetMaximumObjectDepth(Expression expression)
        {
            _maxDepth = 0;
            return GetMaxObjectDepth(expression, 0);
        }

        private int GetMaxObjectDepth(Expression expression, int currentDepth)
        {
            string expressionType = GeneratorHelper.StripObjectReference(expression.Type);

            //No members and can't be resolved to a single type (equivalent of having no members)
            if (expression.DataMembers.Count > 0 && GeneratorHelper.IsSerializable(expression.Name) &&
                !GeneratorHelper.CanBeExpressedAsSingleType(expressionType))
            {
                foreach (Expression dataMember in expression.DataMembers)
                {
                    if (_maxDepth >= _cutoff) return _maxDepth; //Stop calculating
                    if (_visitedExpressions.Contains(dataMember)) return -1; //Infinite (Circular reference)
                    _visitedExpressions.Add(dataMember);

                    if (currentDepth > _maxDepth)
                    {
                        _maxDepth = currentDepth;
                    }

                    GetMaxObjectDepth(dataMember, currentDepth + 1);
                }
            }

            return _maxDepth;
        }
    }
}
