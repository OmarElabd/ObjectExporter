using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectExporter.VsPackage.Settings
{
    public static class Defaults
    {
        public const uint DepthSolverTimeOut = 20000;
        public const uint DepthSolverCutOff = 25;
        public const bool IgnoreDynamicallyAddedProperties = true;
        public const bool ErrorReportingEnabled = true;
        public const bool IgnoreEntityFrameworkProxyTypes = true;
    }
}
