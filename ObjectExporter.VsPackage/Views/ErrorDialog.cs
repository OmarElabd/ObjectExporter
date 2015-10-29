using System;

namespace ObjectExporter.VsPackage.Views
{
    public partial class ErrorDialog : Telerik.WinControls.UI.RadForm
    {
        public ErrorDialog()
        {
            InitializeComponent();
        }

        private void radButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radButtonSendError_Click(object sender, EventArgs e)
        {
            //TODO
        }
    }
}
