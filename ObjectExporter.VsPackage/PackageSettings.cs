using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace AccretionDynamics.ObjectExporter.VsPackage
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [CLSCompliant(false), ComVisible(true)]
    public class PackageSettings : DialogPage
    {
        private uint _depthSolverTimeOut = 20000;

        [Category("Depth Solver")]
        [DisplayName("Maximum Depth Time Out")]
        [Description("Sets the timeout (in milliseconds) for calculating the depth of a selected object.")]
        public uint DepthSolverTimeOut
        {
            get { return _depthSolverTimeOut; }
            set { _depthSolverTimeOut = value; }
        }

        private uint _depthSolverCutoff = 25;

        [Category("Depth Solver")]
        [DisplayName("Maximum Depth Cutoff")]
        [Description("Sets the maximum depth cutofff for calculating the depth of a selected object.")]
        public uint DepthSolverCutoff
        {
            get { return _depthSolverCutoff; }
            set { _depthSolverCutoff = value; }
        }

        private bool _ignoreDynamicallyAddedProperties = true;

        [Category("Object Generation")]
        [DisplayName("Ignore Dynamically Added Properties")]
        [Description("Some frameworks may add dynamic properties to an object instance. A good example of this is Entity Framework which will add DynamicProxy properties" +
                     "to your object, if you do not wish for these properties to be exported select this option.")]
        public bool IgnoreDynamicallyAddedProperties
        {
            get { return _ignoreDynamicallyAddedProperties; }
            set { _ignoreDynamicallyAddedProperties = value; }
        }

    }
}
