using System;
using System.Threading;
using Telerik.WinControls.UI;

namespace ObjectExporter.VsPackage.Views
{
    public partial class ProgressDialog : RadForm
    {
        private readonly CancellationTokenSource _ctSource;

        public ProgressDialog(CancellationTokenSource ctSource)
        {
            _ctSource = ctSource;
            InitializeComponent();
            radWaitingBar1.StartWaiting();
        }

        private void radButtonCancel_Click(object sender, EventArgs e)
        {
            _ctSource.Cancel();
            this.Close();
        }

        private void ProgressDialog_Shown(object sender, EventArgs e)
        {
            this.Focus();
        }
    }
}
