using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace AccretionDynamics.ObjectExporter.VsPackage.Views
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

        }
    }
}
