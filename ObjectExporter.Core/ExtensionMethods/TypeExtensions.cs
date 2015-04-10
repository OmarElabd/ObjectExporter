using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ObjectExporter.Core.ExtensionMethods
{
    public static class TypeExtensions
    {
        public static List<string> GetAccessibleFieldAndPropertyNames(this Type type)
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

        public static List<string> GetAllPropertyNames(this Type type)
        {
            return type.GetProperties().Select(x => x.Name).ToList();
        }
    }
}
