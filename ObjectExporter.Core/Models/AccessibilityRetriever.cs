using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Design;
using Microsoft.VisualStudio.Shell.Interop;

namespace ObjectExporter.Core.Models
{
    public class AccessibilityRetriever
    {
        private readonly DTE2 _dte2;

        public AccessibilityRetriever(DTE2 dte2)
        {
            _dte2 = dte2;
        }

        public Type GetTypeByName(string name)
        {
            IServiceProvider serviceProvider = new ServiceProvider(_dte2 as Microsoft.VisualStudio.OLE.Interop.IServiceProvider);

            DynamicTypeService typeService = serviceProvider.GetService(typeof(DynamicTypeService)) as DynamicTypeService;

            IVsSolution sln = serviceProvider.GetService(typeof(IVsSolution)) as IVsSolution;

            IVsHierarchy hier;
            sln.GetProjectOfUniqueName(_dte2.ActiveDocument.ProjectItem.ContainingProject.UniqueName, out hier);

            return typeService.GetTypeResolutionService(hier).GetType(name, true);
        }

        public List<string> GetAccessibleFieldsAndProperties(Type type)
        {
            List<string> properties = new List<string>();

            //Add Settable Properties
            properties.AddRange(type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                    .Where(y => y.CanWrite)
                                    .Select(x => x.Name));

            //Add Fields
            properties.AddRange(type.GetFields(BindingFlags.Instance | BindingFlags.Public)
                      .Select(x => x.Name));

            return properties;
        }
    }
}
