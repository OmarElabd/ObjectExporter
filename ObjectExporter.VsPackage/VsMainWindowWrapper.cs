using System;
using System.Windows.Forms;
using EnvDTE80;

namespace ObjectExporter.VsPackage
{
    public class VsMainWindowWrapper : IWin32Window
    {
        private readonly IntPtr _handle;

        public IntPtr Handle
        {
            get { return _handle; }
        }

        public VsMainWindowWrapper(IntPtr handle)
        {
            _handle = handle;
        }

        public VsMainWindowWrapper(DTE2 dte2)
        {
            _handle = new IntPtr(dte2.MainWindow.HWnd);
        }
    }
}
