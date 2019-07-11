namespace ObjectExporter.VsPackage.Views
{
    partial class FormDisplayGeneratedText
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDisplayGeneratedText));
            this.folderBrowserDialogSaveObjects = new System.Windows.Forms.FolderBrowserDialog();
            this.radPageViewGeneratedText = new Telerik.WinControls.UI.RadPageView();
            this.telerikMetroBlueTheme1 = new Telerik.WinControls.Themes.TelerikMetroBlueTheme();
            this.panel1 = new System.Windows.Forms.Panel();
            this.scintillaDisplayObjects = new ScintillaNET.Scintilla();
            this.radButtonSaveObjects = new Telerik.WinControls.UI.RadButton();
            this.radButtonCopyClipboard = new Telerik.WinControls.UI.RadButton();
            this.radButtonCancel = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.radPageViewGeneratedText)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonSaveObjects)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonCopyClipboard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonCancel)).BeginInit();
            this.SuspendLayout();
            // 
            // radPageViewGeneratedText
            // 
            this.radPageViewGeneratedText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radPageViewGeneratedText.Location = new System.Drawing.Point(0, 0);
            this.radPageViewGeneratedText.Name = "radPageViewGeneratedText";
            this.radPageViewGeneratedText.Size = new System.Drawing.Size(864, 414);
            this.radPageViewGeneratedText.TabIndex = 8;
            this.radPageViewGeneratedText.Text = "radPageView1";
            this.radPageViewGeneratedText.ThemeName = "TelerikMetroBlue";
            this.radPageViewGeneratedText.SelectedPageChanged += new System.EventHandler(this.radPageViewGeneratedText_SelectedPageChanged);
            ((Telerik.WinControls.UI.RadPageViewStripElement)(this.radPageViewGeneratedText.GetChildAt(0))).StripButtons = Telerik.WinControls.UI.StripViewButtons.Scroll;
            ((Telerik.WinControls.UI.RadPageViewStripElement)(this.radPageViewGeneratedText.GetChildAt(0))).ItemAlignment = Telerik.WinControls.UI.StripViewItemAlignment.Near;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.scintillaDisplayObjects);
            this.panel1.Controls.Add(this.radButtonSaveObjects);
            this.panel1.Controls.Add(this.radButtonCopyClipboard);
            this.panel1.Controls.Add(this.radButtonCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 420);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(864, 46);
            this.panel1.TabIndex = 10;
            // 
            // scintillaDisplayObjects
            // 
            this.scintillaDisplayObjects.Location = new System.Drawing.Point(0, 0);
            this.scintillaDisplayObjects.Name = "scintillaDisplayObjects";
            this.scintillaDisplayObjects.Size = new System.Drawing.Size(200, 100);
            //this.scintillaDisplayObjects.Styles.BraceBad.FontName = "Verdana\0\0\0\0\0\0\0\0\0\0\0\0\0";
            //this.scintillaDisplayObjects.Styles.BraceLight.FontName = "Verdana\0\0\0\0\0\0\0\0\0\0\0\0\0";
            //this.scintillaDisplayObjects.Styles.CallTip.FontName = "Segoe UI\0\0\0\0\0\0\0\0\0\0\0\0";
            //this.scintillaDisplayObjects.Styles.ControlChar.FontName = "Verdana\0\0\0\0\0\0\0\0\0\0\0\0\0";
            //this.scintillaDisplayObjects.Styles.Default.FontName = "Verdana\0\0\0\0\0\0\0\0\0\0\0\0\0";
            //this.scintillaDisplayObjects.Styles.IndentGuide.FontName = "Verdana\0\0\0\0\0\0\0\0\0\0\0\0\0";
            //this.scintillaDisplayObjects.Styles.LastPredefined.FontName = "Verdana\0\0\0\0\0\0\0\0\0\0\0\0\0";
            //this.scintillaDisplayObjects.Styles.LineNumber.FontName = "Verdana\0\0\0\0\0\0\0\0\0\0\0\0\0";
            //this.scintillaDisplayObjects.Styles.Max.FontName = "Verdana\0\0\0\0\0\0\0\0\0\0\0\0\0";
            this.scintillaDisplayObjects.TabIndex = 0;
            // 
            // radButtonSaveObjects
            // 
            this.radButtonSaveObjects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radButtonSaveObjects.Image = ((System.Drawing.Image)(resources.GetObject("radButtonSaveObjects.Image")));
            this.radButtonSaveObjects.Location = new System.Drawing.Point(502, 10);
            this.radButtonSaveObjects.Name = "radButtonSaveObjects";
            this.radButtonSaveObjects.Size = new System.Drawing.Size(163, 24);
            this.radButtonSaveObjects.TabIndex = 16;
            this.radButtonSaveObjects.Text = "Save Objects To Folder";
            this.radButtonSaveObjects.ThemeName = "TelerikMetroBlue";
            this.radButtonSaveObjects.Click += new System.EventHandler(this.buttonSaveAll_Click);
            // 
            // radButtonCopyClipboard
            // 
            this.radButtonCopyClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radButtonCopyClipboard.Image = ((System.Drawing.Image)(resources.GetObject("radButtonCopyClipboard.Image")));
            this.radButtonCopyClipboard.Location = new System.Drawing.Point(689, 10);
            this.radButtonCopyClipboard.Name = "radButtonCopyClipboard";
            this.radButtonCopyClipboard.Size = new System.Drawing.Size(163, 24);
            this.radButtonCopyClipboard.TabIndex = 15;
            this.radButtonCopyClipboard.Text = "Copy To Clipboard";
            this.radButtonCopyClipboard.ThemeName = "TelerikMetroBlue";
            this.radButtonCopyClipboard.Click += new System.EventHandler(this.buttonCopyToClipboard_Click);
            // 
            // radButtonCancel
            // 
            this.radButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.radButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.radButtonCancel.Image = ((System.Drawing.Image)(resources.GetObject("radButtonCancel.Image")));
            this.radButtonCancel.Location = new System.Drawing.Point(18, 10);
            this.radButtonCancel.Name = "radButtonCancel";
            this.radButtonCancel.Size = new System.Drawing.Size(71, 24);
            this.radButtonCancel.TabIndex = 14;
            this.radButtonCancel.Text = "Exit";
            this.radButtonCancel.ThemeName = "TelerikMetroBlue";
            this.radButtonCancel.Click += new System.EventHandler(this.radButtonCancel_Click);
            // 
            // FormDisplayGeneratedText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.radButtonCancel;
            this.ClientSize = new System.Drawing.Size(864, 466);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.radPageViewGeneratedText);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(880, 505);
            this.Name = "FormDisplayGeneratedText";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Generated Text";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormDisplayGeneratedText_Load);
            this.Shown += new System.EventHandler(this.FormDisplayGeneratedText_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.radPageViewGeneratedText)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radButtonSaveObjects)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonCopyClipboard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonCancel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogSaveObjects;
        private Telerik.WinControls.UI.RadPageView radPageViewGeneratedText;
        private Telerik.WinControls.Themes.TelerikMetroBlueTheme telerikMetroBlueTheme1;
        private System.Windows.Forms.Panel panel1;
        private Telerik.WinControls.UI.RadButton radButtonSaveObjects;
        private Telerik.WinControls.UI.RadButton radButtonCopyClipboard;
        private Telerik.WinControls.UI.RadButton radButtonCancel;
        private ScintillaNET.Scintilla scintillaDisplayObjects;

    }
}