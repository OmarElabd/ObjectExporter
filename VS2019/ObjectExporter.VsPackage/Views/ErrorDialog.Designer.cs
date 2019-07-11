namespace ObjectExporter.VsPackage.Views
{
    partial class ErrorDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorDialog));
            this.telerikMetroBlueTheme1 = new Telerik.WinControls.Themes.TelerikMetroBlueTheme();
            this.radLabelErrorMessage = new Telerik.WinControls.UI.RadLabel();
            this.radButtonCancel = new Telerik.WinControls.UI.RadButton();
            this.radButtonSendError = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.radLabelErrorMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonSendError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radLabelErrorMessage
            // 
            this.radLabelErrorMessage.Location = new System.Drawing.Point(12, 12);
            this.radLabelErrorMessage.Name = "radLabelErrorMessage";
            this.radLabelErrorMessage.Size = new System.Drawing.Size(207, 18);
            this.radLabelErrorMessage.TabIndex = 2;
            this.radLabelErrorMessage.Text = "Error occured during object exportation.";
            // 
            // radButtonCancel
            // 
            this.radButtonCancel.Image = ((System.Drawing.Image)(resources.GetObject("radButtonCancel.Image")));
            this.radButtonCancel.Location = new System.Drawing.Point(12, 74);
            this.radButtonCancel.Name = "radButtonCancel";
            this.radButtonCancel.Size = new System.Drawing.Size(91, 24);
            this.radButtonCancel.TabIndex = 1;
            this.radButtonCancel.Text = "Cancel";
            this.radButtonCancel.ThemeName = "TelerikMetroBlue";
            this.radButtonCancel.Click += new System.EventHandler(this.radButtonCancel_Click);
            // 
            // radButtonSendError
            // 
            this.radButtonSendError.Image = global::ObjectExporter.VsPackage.ImageResources.paper_plane;
            this.radButtonSendError.Location = new System.Drawing.Point(320, 74);
            this.radButtonSendError.Name = "radButtonSendError";
            this.radButtonSendError.Size = new System.Drawing.Size(149, 24);
            this.radButtonSendError.TabIndex = 0;
            this.radButtonSendError.Text = "Send Error Report";
            this.radButtonSendError.ThemeName = "TelerikMetroBlue";
            this.radButtonSendError.Click += new System.EventHandler(this.radButtonSendError_Click);
            // 
            // ErrorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 105);
            this.Controls.Add(this.radLabelErrorMessage);
            this.Controls.Add(this.radButtonCancel);
            this.Controls.Add(this.radButtonSendError);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ErrorDialog";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Object Exporting Failed!";
            this.ThemeName = "TelerikMetroBlue";
            ((System.ComponentModel.ISupportInitialize)(this.radLabelErrorMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonSendError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.Themes.TelerikMetroBlueTheme telerikMetroBlueTheme1;
        private Telerik.WinControls.UI.RadButton radButtonSendError;
        private Telerik.WinControls.UI.RadButton radButtonCancel;
        private Telerik.WinControls.UI.RadLabel radLabelErrorMessage;
    }
}
