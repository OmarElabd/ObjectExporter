namespace ObjectExporter.VsPackage.Views
{
    partial class FormSelectObjects
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSelectObjects));
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewImageColumn gridViewImageColumn1 = new Telerik.WinControls.UI.GridViewImageColumn();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.radioButtonCSharpObject = new System.Windows.Forms.RadioButton();
            this.radioButtonJson = new System.Windows.Forms.RadioButton();
            this.radioButtonXml = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownMaxDepth = new System.Windows.Forms.NumericUpDown();
            this.telerikMetroBlueTheme1 = new Telerik.WinControls.Themes.TelerikMetroBlueTheme();
            this.radButtonCancel = new Telerik.WinControls.UI.RadButton();
            this.radButtonExport = new Telerik.WinControls.UI.RadButton();
            this.radPageViewExport = new Telerik.WinControls.UI.RadPageView();
            this.radPageViewPageLocals = new Telerik.WinControls.UI.RadPageViewPage();
            this.radCheckedListBoxLocals = new Telerik.WinControls.UI.RadCheckedListBox();
            this.radPageViewPageCustomExpression = new Telerik.WinControls.UI.RadPageViewPage();
            this.radGridViewCustomExpressions = new Telerik.WinControls.UI.RadGridView();
            this.radCheckBoxExcludePrivate = new Telerik.WinControls.UI.RadCheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPageViewExport)).BeginInit();
            this.radPageViewPageLocals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radCheckedListBoxLocals)).BeginInit();
            this.radPageViewPageCustomExpression.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGridViewCustomExpressions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridViewCustomExpressions.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCheckBoxExcludePrivate)).BeginInit();
            this.SuspendLayout();
            // 
            // radioButtonCSharpObject
            // 
            this.radioButtonCSharpObject.AutoSize = true;
            this.radioButtonCSharpObject.Location = new System.Drawing.Point(25, 19);
            this.radioButtonCSharpObject.Name = "radioButtonCSharpObject";
            this.radioButtonCSharpObject.Size = new System.Drawing.Size(73, 17);
            this.radioButtonCSharpObject.TabIndex = 6;
            this.radioButtonCSharpObject.TabStop = true;
            this.radioButtonCSharpObject.Text = "C# Object";
            this.radioButtonCSharpObject.UseVisualStyleBackColor = true;
            // 
            // radioButtonJson
            // 
            this.radioButtonJson.AutoSize = true;
            this.radioButtonJson.Location = new System.Drawing.Point(162, 19);
            this.radioButtonJson.Name = "radioButtonJson";
            this.radioButtonJson.Size = new System.Drawing.Size(53, 17);
            this.radioButtonJson.TabIndex = 7;
            this.radioButtonJson.TabStop = true;
            this.radioButtonJson.Text = "JSON";
            this.radioButtonJson.UseVisualStyleBackColor = true;
            // 
            // radioButtonXml
            // 
            this.radioButtonXml.AutoSize = true;
            this.radioButtonXml.Checked = true;
            this.radioButtonXml.Location = new System.Drawing.Point(279, 19);
            this.radioButtonXml.Name = "radioButtonXml";
            this.radioButtonXml.Size = new System.Drawing.Size(47, 17);
            this.radioButtonXml.TabIndex = 8;
            this.radioButtonXml.TabStop = true;
            this.radioButtonXml.Text = "XML";
            this.radioButtonXml.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonCSharpObject);
            this.groupBox1.Controls.Add(this.radioButtonXml);
            this.groupBox1.Controls.Add(this.radioButtonJson);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(358, 45);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Output Format";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Maximum Object Depth";
            // 
            // numericUpDownMaxDepth
            // 
            this.numericUpDownMaxDepth.Location = new System.Drawing.Point(39, 85);
            this.numericUpDownMaxDepth.Name = "numericUpDownMaxDepth";
            this.numericUpDownMaxDepth.Size = new System.Drawing.Size(69, 20);
            this.numericUpDownMaxDepth.TabIndex = 13;
            this.numericUpDownMaxDepth.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // radButtonCancel
            // 
            this.radButtonCancel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.radButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.radButtonCancel.Image = ((System.Drawing.Image)(resources.GetObject("radButtonCancel.Image")));
            this.radButtonCancel.Location = new System.Drawing.Point(12, 423);
            this.radButtonCancel.Name = "radButtonCancel";
            // 
            // 
            // 
            this.radButtonCancel.RootElement.AccessibleDescription = null;
            this.radButtonCancel.RootElement.AccessibleName = null;
            this.radButtonCancel.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 110, 24);
            this.radButtonCancel.Size = new System.Drawing.Size(86, 24);
            this.radButtonCancel.TabIndex = 16;
            this.radButtonCancel.Text = "Exit";
            this.radButtonCancel.ThemeName = "TelerikMetroBlue";
            this.radButtonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // radButtonExport
            // 
            this.radButtonExport.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.radButtonExport.Image = ((System.Drawing.Image)(resources.GetObject("radButtonExport.Image")));
            this.radButtonExport.Location = new System.Drawing.Point(284, 423);
            this.radButtonExport.Name = "radButtonExport";
            // 
            // 
            // 
            this.radButtonExport.RootElement.AccessibleDescription = null;
            this.radButtonExport.RootElement.AccessibleName = null;
            this.radButtonExport.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 110, 24);
            this.radButtonExport.Size = new System.Drawing.Size(86, 24);
            this.radButtonExport.TabIndex = 17;
            this.radButtonExport.Text = "Export";
            this.radButtonExport.ThemeName = "TelerikMetroBlue";
            this.radButtonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // radPageViewExport
            // 
            this.radPageViewExport.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.radPageViewExport.Controls.Add(this.radPageViewPageLocals);
            this.radPageViewExport.Controls.Add(this.radPageViewPageCustomExpression);
            this.radPageViewExport.Location = new System.Drawing.Point(12, 116);
            this.radPageViewExport.Name = "radPageViewExport";
            // 
            // 
            // 
            this.radPageViewExport.RootElement.AccessibleDescription = null;
            this.radPageViewExport.RootElement.AccessibleName = null;
            this.radPageViewExport.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 400, 300);
            this.radPageViewExport.SelectedPage = this.radPageViewPageLocals;
            this.radPageViewExport.Size = new System.Drawing.Size(358, 293);
            this.radPageViewExport.TabIndex = 18;
            this.radPageViewExport.ThemeName = "TelerikMetroBlue";
            // 
            // radPageViewPageLocals
            // 
            this.radPageViewPageLocals.Controls.Add(this.radCheckedListBoxLocals);
            this.radPageViewPageLocals.ItemSize = new System.Drawing.SizeF(113F, 25F);
            this.radPageViewPageLocals.Location = new System.Drawing.Point(5, 31);
            this.radPageViewPageLocals.Name = "radPageViewPageLocals";
            this.radPageViewPageLocals.Size = new System.Drawing.Size(348, 257);
            this.radPageViewPageLocals.Text = "Select From Locals";
            // 
            // radCheckedListBoxLocals
            // 
            this.radCheckedListBoxLocals.AllowArbitraryItemWidth = true;
            this.radCheckedListBoxLocals.AllowColumnReorder = false;
            this.radCheckedListBoxLocals.AllowColumnResize = false;
            this.radCheckedListBoxLocals.AutoScroll = true;
            this.radCheckedListBoxLocals.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.radCheckedListBoxLocals.Location = new System.Drawing.Point(0, 0);
            this.radCheckedListBoxLocals.Name = "radCheckedListBoxLocals";
            // 
            // 
            // 
            this.radCheckedListBoxLocals.RootElement.AccessibleDescription = null;
            this.radCheckedListBoxLocals.RootElement.AccessibleName = null;
            this.radCheckedListBoxLocals.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 120, 95);
            this.radCheckedListBoxLocals.Size = new System.Drawing.Size(348, 257);
            this.radCheckedListBoxLocals.TabIndex = 0;
            this.radCheckedListBoxLocals.ThemeName = "TelerikMetroBlue";
            this.radCheckedListBoxLocals.ItemCheckedChanged += new Telerik.WinControls.UI.ListViewItemEventHandler(this.radCheckedListBoxLocals_ItemCheckedChanged);
            // 
            // radPageViewPageCustomExpression
            // 
            this.radPageViewPageCustomExpression.Controls.Add(this.radGridViewCustomExpressions);
            this.radPageViewPageCustomExpression.ItemSize = new System.Drawing.SizeF(121F, 25F);
            this.radPageViewPageCustomExpression.Location = new System.Drawing.Point(5, 31);
            this.radPageViewPageCustomExpression.Name = "radPageViewPageCustomExpression";
            this.radPageViewPageCustomExpression.Size = new System.Drawing.Size(348, 257);
            this.radPageViewPageCustomExpression.Text = "Custom Expressions";
            // 
            // radGridViewCustomExpressions
            // 
            this.radGridViewCustomExpressions.BackColor = System.Drawing.Color.White;
            this.radGridViewCustomExpressions.Cursor = System.Windows.Forms.Cursors.Default;
            this.radGridViewCustomExpressions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGridViewCustomExpressions.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.radGridViewCustomExpressions.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radGridViewCustomExpressions.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radGridViewCustomExpressions.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.radGridViewCustomExpressions.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom;
            this.radGridViewCustomExpressions.MasterTemplate.AllowColumnChooser = false;
            this.radGridViewCustomExpressions.MasterTemplate.AllowColumnHeaderContextMenu = false;
            this.radGridViewCustomExpressions.MasterTemplate.AllowColumnReorder = false;
            this.radGridViewCustomExpressions.MasterTemplate.AllowColumnResize = false;
            this.radGridViewCustomExpressions.MasterTemplate.AllowDragToGroup = false;
            this.radGridViewCustomExpressions.MasterTemplate.AllowRowResize = false;
            this.radGridViewCustomExpressions.MasterTemplate.AutoGenerateColumns = false;
            this.radGridViewCustomExpressions.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            gridViewTextBoxColumn1.AllowResize = false;
            gridViewTextBoxColumn1.EnableExpressionEditor = false;
            gridViewTextBoxColumn1.HeaderText = "Expression Name";
            gridViewTextBoxColumn1.IsPinned = true;
            gridViewTextBoxColumn1.Name = "columnExpressionName";
            gridViewTextBoxColumn1.PinPosition = Telerik.WinControls.UI.PinnedColumnPosition.Left;
            gridViewTextBoxColumn1.Width = 200;
            gridViewTextBoxColumn2.HeaderText = "Depth";
            gridViewTextBoxColumn2.MaxWidth = 85;
            gridViewTextBoxColumn2.Name = "columnDepth";
            gridViewTextBoxColumn2.ReadOnly = true;
            gridViewTextBoxColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewTextBoxColumn2.Width = 85;
            gridViewImageColumn1.AllowResize = false;
            gridViewImageColumn1.EnableExpressionEditor = false;
            gridViewImageColumn1.HeaderText = "Valid";
            gridViewImageColumn1.MaxWidth = 45;
            gridViewImageColumn1.Name = "columnIsExpressionValid";
            gridViewImageColumn1.Width = 45;
            this.radGridViewCustomExpressions.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewImageColumn1});
            this.radGridViewCustomExpressions.MasterTemplate.EnableGrouping = false;
            this.radGridViewCustomExpressions.MasterTemplate.EnableSorting = false;
            this.radGridViewCustomExpressions.MasterTemplate.ShowRowHeaderColumn = false;
            this.radGridViewCustomExpressions.MasterTemplate.VerticalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysHide;
            this.radGridViewCustomExpressions.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.radGridViewCustomExpressions.Name = "radGridViewCustomExpressions";
            this.radGridViewCustomExpressions.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // 
            // 
            this.radGridViewCustomExpressions.RootElement.AccessibleDescription = null;
            this.radGridViewCustomExpressions.RootElement.AccessibleName = null;
            this.radGridViewCustomExpressions.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 240, 150);
            this.radGridViewCustomExpressions.ShowGroupPanel = false;
            this.radGridViewCustomExpressions.Size = new System.Drawing.Size(348, 257);
            this.radGridViewCustomExpressions.TabIndex = 0;
            this.radGridViewCustomExpressions.ThemeName = "TelerikMetroBlue";
            this.radGridViewCustomExpressions.CellValueChanged += new Telerik.WinControls.UI.GridViewCellEventHandler(this.radGridViewCustomExpressions_CellValueChanged);
            // 
            // radCheckBoxExcludePrivate
            // 
            this.radCheckBoxExcludePrivate.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.radCheckBoxExcludePrivate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.radCheckBoxExcludePrivate.Location = new System.Drawing.Point(141, 85);
            this.radCheckBoxExcludePrivate.Name = "radCheckBoxExcludePrivate";
            // 
            // 
            // 
            this.radCheckBoxExcludePrivate.RootElement.AccessibleDescription = null;
            this.radCheckBoxExcludePrivate.RootElement.AccessibleName = null;
            this.radCheckBoxExcludePrivate.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 100, 18);
            this.radCheckBoxExcludePrivate.RootElement.StretchHorizontally = true;
            this.radCheckBoxExcludePrivate.RootElement.StretchVertically = true;
            this.radCheckBoxExcludePrivate.Size = new System.Drawing.Size(221, 19);
            this.radCheckBoxExcludePrivate.TabIndex = 19;
            this.radCheckBoxExcludePrivate.Text = "Exclude private properties and fields";
            this.radCheckBoxExcludePrivate.ThemeName = "TelerikMetroBlue";
            this.radCheckBoxExcludePrivate.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            // 
            // FormSelectObjects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.radButtonCancel;
            this.ClientSize = new System.Drawing.Size(380, 461);
            this.Controls.Add(this.radCheckBoxExcludePrivate);
            this.Controls.Add(this.radPageViewExport);
            this.Controls.Add(this.radButtonExport);
            this.Controls.Add(this.radButtonCancel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDownMaxDepth);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSelectObjects";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " Object Exporter";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonExport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPageViewExport)).EndInit();
            this.radPageViewPageLocals.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radCheckedListBoxLocals)).EndInit();
            this.radPageViewPageCustomExpression.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radGridViewCustomExpressions.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridViewCustomExpressions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCheckBoxExcludePrivate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonCSharpObject;
        private System.Windows.Forms.RadioButton radioButtonJson;
        private System.Windows.Forms.RadioButton radioButtonXml;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxDepth;
        private Telerik.WinControls.Themes.TelerikMetroBlueTheme telerikMetroBlueTheme1;
        private Telerik.WinControls.UI.RadButton radButtonCancel;
        private Telerik.WinControls.UI.RadButton radButtonExport;
        private Telerik.WinControls.UI.RadPageView radPageViewExport;
        private Telerik.WinControls.UI.RadPageViewPage radPageViewPageLocals;
        private Telerik.WinControls.UI.RadPageViewPage radPageViewPageCustomExpression;
        private Telerik.WinControls.UI.RadGridView radGridViewCustomExpressions;
        private Telerik.WinControls.UI.RadCheckBox radCheckBoxExcludePrivate;
        private Telerik.WinControls.UI.RadCheckedListBox radCheckedListBoxLocals;
    }
}