using System;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Design;
using Microsoft.VisualStudio.Shell.Interop;

namespace ObjectExporter.Core.Models
{
    public class TypeRetriever
    {
        private readonly DTE2 _dte2;

        public TypeRetriever(DTE2 dte2)
        {
            _dte2 = dte2;
        }

        public Type GetTypeFromString(string type)
        {
            IServiceProvider serviceProvider = new ServiceProvider(_dte2 as Microsoft.VisualStudio.OLE.Interop.IServiceProvider);
            DynamicTypeService typeService = serviceProvider.GetService(typeof(DynamicTypeService)) as DynamicTypeService;
            IVsSolution solution = serviceProvider.GetService(typeof(IVsSolution)) as IVsSolution;

            if (solution == null)
            {
                return null;
            }

            IVsHierarchy hierarchy;            
            solution.GetProjectOfUniqueName(_dte2.ActiveDocument.ProjectItem.ContainingProject.UniqueName, out hierarchy);
            
            Type returnType = typeService?.GetTypeResolutionService(hierarchy)?.GetType(type, false);

            return returnType;
        }

    }
}
