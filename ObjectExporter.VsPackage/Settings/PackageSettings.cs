using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace ObjectExporter.VsPackage.Settings
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [CLSCompliant(false), ComVisible(true)]
    public class PackageSettings : DialogPage
    {
        private uint _depthSolverTimeOut = Defaults.DepthSolverTimeOut;

        [Category("Depth Solver")]
        [DisplayName("Maximum Depth Time Out")]
        [Description("Sets the timeout (in milliseconds) for calculating the depth of a selected object.")]
        public uint DepthSolverTimeOut
        {
            get { return _depthSolverTimeOut; }
            set { _depthSolverTimeOut = value; }
        }

        private uint _depthSolverCutoff = Defaults.DepthSolverCutOff;

        [Category("Depth Solver")]
        [DisplayName("Maximum Depth Cutoff")]
        [Description("Sets the maximum depth cutofff for calculating the depth of a selected object.")]
        public uint DepthSolverCutoff
        {
            get { return _depthSolverCutoff; }
            set { _depthSolverCutoff = value; }
        }

        private bool _ignoreEntityFrameworkProxyTypes = Defaults.IgnoreEntityFrameworkProxyTypes;

        [Category("Object Generation")]
        [DisplayName("Ignore Entity Framework Proxy Types")]
        [Description("Entity Framework by default will use a proxy class for change tracking and lazy loading, enabling this feature will set Object Exporter to use the actual types and not the generated proxy types.")]
        public bool IgnoreEntityFrameworkProxyTypes
        {
            get { return _ignoreEntityFrameworkProxyTypes; }
            set { _ignoreEntityFrameworkProxyTypes = value; }
        }

        private bool _ignoreDynamicallyAddedProperties = Defaults.IgnoreDynamicallyAddedProperties;

        [Category("Object Generation")]
        [DisplayName("Ignore Dynamically Added Properties")]
        [Description("Some frameworks may add dynamic properties to an object instance. A good example of this is " +
                     "Entity Framework which will add DynamicProxy properties to your object, if you do not wish for " +
                     "these properties to be exported select this option.")]
        public bool IgnoreDynamicallyAddedProperties
        {
            get { return _ignoreDynamicallyAddedProperties; }
            set { _ignoreDynamicallyAddedProperties = value; }
        }

        private bool _errorReportingEnabled = Defaults.ErrorReportingEnabled;

        [Category("Feedback")]
        [DisplayName("Enable Error Reporting")]
        [Description("When enabled, Object Exporter will automatically send exception details to our servers. " +
                     "This will allow us to improve Object Exporter and keep it bug free.")]
        public bool ErrorReportingEnabled
        {
            get { return _errorReportingEnabled; }
            set { _errorReportingEnabled = value; }
        }
    }
}
