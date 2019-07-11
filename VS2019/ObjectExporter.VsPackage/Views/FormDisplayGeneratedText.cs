using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ObjectExporter.Core.Globals;
using ObjectExporter.VsPackage.ExtensionMethods;
using ObjectExporter.VsPackage.Settings;
using ScintillaNET;
using Telerik.WinControls.UI;
using Telerik.Windows.Documents.Model.Code;

namespace ObjectExporter.VsPackage.Views
{
    public partial class FormDisplayGeneratedText : Form
    {
        private readonly Dictionary<string, string> _dicTexts;
        private readonly ExportType _type;
        private CodeFormattingSettings _formattingSettings;
        private string _extension;

        public FormDisplayGeneratedText(Dictionary<string, string> dicTexts, ExportType type)
        {
            _dicTexts = dicTexts;
            _type = type;

            InitializeConstructor(_type);
            SetScintillaMargins(dicTexts);
        }

        private void InitializeConstructor(ExportType type)
        {
            switch (type)
            {
                case ExportType.CSharpObject:
                    _formattingSettings = new CodeFormattingSettings(CodeLanguages.CSharp);
                    _extension = ".cs";
                    break;
                case ExportType.Json:
                    _formattingSettings = new CodeFormattingSettings(CodeLanguages.JavaScript);
                    _extension = ".json";
                    break;
                case ExportType.Xml:
                    _formattingSettings = new CodeFormattingSettings(CodeLanguages.Xml);
                    _extension = ".xml";
                    break;
            }

            InitializeComponent();

            foreach (string textKey in _dicTexts.Keys)
            {
                RadPageViewPage pageViewPage = new RadPageViewPage(textKey);
                radPageViewGeneratedText.Pages.Add(pageViewPage);
            }

            scintillaDisplayObjects.Dock = DockStyle.Fill;
            DisplayText(radPageViewGeneratedText.Pages[0]);
        }

        private void FormDisplayGeneratedText_Load(object sender, EventArgs e)
        {
#if DEBUG
            this.Visible = true;
#endif
        }

        private void DisplayText(RadPageViewPage page)
        {
            scintillaDisplayObjects.Parent = page;

            string expressionName = radPageViewGeneratedText.SelectedPage.Text;
            var expressionText = _dicTexts[expressionName];

            scintillaDisplayObjects.Text = expressionText;
            scintillaDisplayObjects.Lexer = GetLexer(_type);

            // Configure the default style

            ScintillaStyles.InitStyle(scintillaDisplayObjects, _type);
            scintillaDisplayObjects.Update();
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
                    string path = saveToPath + "\\" + fileName + _extension;

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
            DisplayText(radPageViewGeneratedText.Pages[0]);
            this.Focus();
        }

        private void SetScintillaMargins(Dictionary<string, string> dicTexts)
        {
            long maxNumberOfLines = dicTexts.Select(x => x.Value).Max(x => x.Lines());

            //Calculater the number of digits
            int numberOfDigits = (int)Math.Floor(Math.Log10(maxNumberOfLines) + 1);

            scintillaDisplayObjects.Margins[0].Width = 10 * numberOfDigits; //10px for every digit
        }

        private string GetLanguage(ExportType type)
        {
            switch (type)
            {
                case ExportType.Json:
                    return "js";
                case ExportType.Xml:
                    return "xml";
                case ExportType.CSharpObject:
                    return "cs";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }
        }

        private Lexer GetLexer(ExportType type)
        {
            switch (type)
            {
                case ExportType.Json:
                    return Lexer.Json;
                case ExportType.Xml:
                    return Lexer.Xml;
                case ExportType.CSharpObject:
                    return Lexer.Cpp;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }
        }
    }
}
