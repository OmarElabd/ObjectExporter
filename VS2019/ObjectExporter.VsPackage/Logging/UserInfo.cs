using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectExporter.VsPackage.Logging
{
    //NOTE: can include information such as, type of projects
    public class UserInfo
    {
        private string _visualStudioVersion = "NotDefined";

        public string VisualStudioVersion
        {
            get { return _visualStudioVersion; }
            set { _visualStudioVersion = value; }
        }
    }
}
