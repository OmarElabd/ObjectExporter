using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EnvDTE;
using ObjectExporter.Core.Templates;

namespace ObjectExporter.Core.Models
{
    public class ObjectDepthFinder
    {
        private int _maxDepth = 0;

        private readonly uint _cutoff;

        public ObjectDepthFinder(uint cutoff = 100)
        {
            _cutoff = cutoff;
        }

        public int GetMaximumObjectDepth(Expression expression)
        {
            _maxDepth = 0;
            return GetMaxObjectDepth(expression, 0);
        }

        public Task<int> GetMaximumObjectDepthAsync(Expression expression, CancellationToken token)
        {
            _maxDepth = 0;
            return Task.Run(() => GetMaxObjectDepth(expression, 0), token);
        }

        private int GetMaxObjectDepth(Expression expression, int currentDepth)
        {
            string expressionType = GeneratorHelper.StripObjectReference(expression.Type);

            //No members and can't be resolved to a single type (equivalent of having no members)
            if (expression.DataMembers.Count > 0 && GeneratorHelper.IsSerializable(expression.Name) &&
                !GeneratorHelper.CanBeExpressedAsSingleType(expressionType))
            {
                List<Expression> dataMembers = expression.DataMembers.Cast<Expression>().ToList();

                for (int i = 0; i < dataMembers.Count; i++)
                {
                    Expression currentMember = dataMembers[i];

                    //Add to current list, bring base members up one level
                    if (GeneratorHelper.IsBase(currentMember))
                    {
                        dataMembers.AddRange(currentMember.DataMembers.Cast<Expression>());
                    }
                    else
                    {
                        if (_maxDepth >= _cutoff) return _maxDepth; //Stop calculating

                        if (currentDepth > _maxDepth)
                        {
                            _maxDepth = currentDepth;
                        }

                        GetMaxObjectDepth(currentMember, currentDepth + 1);
                    }
                }
            }

            return _maxDepth;
        }
    }
}
