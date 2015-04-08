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
        [DisplayName("Max Depth Time Out")]
        [Description("Sets the timeout for calculating the depth of a selected object")]
        public uint DepthSolverTimeOut
        {
            get { return _depthSolverTimeOut; }
            set { _depthSolverTimeOut = value; }
        }

        private uint _depthSolverCutoff = 25;

        [Category("Depth Solver")]
        [DisplayName("Max Depth Cutoff")]
        [Description("Sets the maximum depth cutofff for calculating the depth of a selected object")]
        public uint DepthSolverCutoff
        {
            get { return _depthSolverCutoff; }
            set { _depthSolverCutoff = value; }
        }
    }
}
