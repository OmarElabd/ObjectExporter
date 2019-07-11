using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectExporter.Core.ExtensionMethods
{
    public static class StringExtensions
    {
        public static string Between(this string src, char findfrom, char findto)
        {
            int start = src.IndexOf(findfrom);
            int end = src.IndexOf(findto, start);

            if (start < 0 || end < 0)
            {
                return String.Empty;
            }

            start++;

            string stringBetween = src.Substring(start, end - start);
            return stringBetween;
        }
    }
}
