using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ObjectExporter.VsPackage.Views
{
    public partial class FilesCreatedDialog : Form
    {
        public FilesCreatedDialog(string filePath)
        {
            InitializeComponent();
            richTextBoxOutput.AppendText("Files Generated Succesfully!\n\n", Color.Green);
            richTextBoxOutput.AppendText("Exported objects have been outputted to\n: file://" + filePath.Replace(" ", "%20"));
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void richTextBoxOutput_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                var linkText = e.LinkText.Replace("%20", " ");
                Process.Start(linkText);
            }
            catch (Exception)
            {
                //Do Nothing, eat the exception
            }
        }
    }
}
