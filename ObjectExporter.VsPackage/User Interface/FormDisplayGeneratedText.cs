using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using ObjectExporter.Core.Globals;
using ObjectExporter.UI;
using Telerik.WinControls.UI;
using Telerik.WinForms.Documents.Model;
using Telerik.WinForms.Documents.Model.Code;

namespace AccretionDynamics.ObjectExporter.VsPackage.UserInterface
{
    public partial class FormDisplayGeneratedText : Form
    {
        private readonly Dictionary<string, string> _dicTexts;
        private CodeFormattingSettings formattingSettings;
        private string extension;

        public FormDisplayGeneratedText(Dictionary<string, string> dicTexts, ExportType type)
        {
            _dicTexts = dicTexts;

            Task.Run(() => InitializeConstructor(type)).Wait();
        }

        private void InitializeConstructor(ExportType type)
        {
            switch (type)
            {
                case ExportType.CSharpObject:
                    formattingSettings = new CodeFormattingSettings(CodeLanguages.CSharp);
                    extension = ".cs";
                    break;
                case ExportType.Json:
                    formattingSettings = new CodeFormattingSettings(CodeLanguages.JavaScript);
                    extension = ".json";
                    break;
                case ExportType.Xml:
                    formattingSettings = new CodeFormattingSettings(CodeLanguages.Xml);
                    extension = ".xml";
                    break;
            }

            InitializeComponent();

            foreach (string textKey in _dicTexts.Keys)
            {
                RadPageViewPage pageViewPage = new RadPageViewPage(textKey);
                radPageViewGeneratedText.Pages.Add(pageViewPage);
            }

            DisplayText(radPageViewGeneratedText.Pages[0]);
            radRichTextEditor1.Dock = DockStyle.Fill;
        }

        private void FormDisplayGeneratedText_Load(object sender, EventArgs e)
        {
#if DEBUG
            this.Visible = true;
#endif
        }

        private void DisplayText(RadPageViewPage page)
        {
            radRichTextEditor1.Document = new RadDocument();
            radRichTextEditor1.Parent = page;

            string expressionName = radPageViewGeneratedText.SelectedPage.Text;
            var expressionText = _dicTexts[expressionName];

            radRichTextEditor1.InsertCodeBlock(expressionText, formattingSettings);
        }

        private async Task DisplayTextAsync(RadPageViewPage page)
        {
            await Task.Run(() => DisplayText(page));
        }

        private void buttonSaveAll_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialogSaveObjects.ShowDialog() == DialogResult.OK)
            {
                string saveToPath = folderBrowserDialogSaveObjects.SelectedPath + "\\Exported Debugger Objects";
                Directory.CreateDirectory(saveToPath);

                foreach (var kvp in _dicTexts)
                {
                    string fileName = kvp.Key;
                    string fileText = kvp.Value;
                    string path = saveToPath + "\\" + fileName + extension;

                    File.WriteAllText(path, fileText);
                }

                FilesCreatedDialog dialog = new FilesCreatedDialog(saveToPath);
                dialog.ShowDialog(this);
            }
        }

        private void buttonCopyToClipboard_Click(object sender, EventArgs e)
        {
            string expressionName = radPageViewGeneratedText.SelectedPage.Text;

            var expressionText = _dicTexts[expressionName];
            Clipboard.SetText(expressionText);
        }

        private void radPageViewGeneratedText_SelectedPageChanged(object sender, EventArgs e)
        {
            DisplayText(radPageViewGeneratedText.SelectedPage);
        }

        private void radButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormDisplayGeneratedText_Shown(object sender, EventArgs e)
        {
            this.Focus();
        }
    }
}
