using System.Drawing;
using System.Windows.Forms;

namespace AccretionDynamics.ObjectExporter.VsPackage.UserInterface
{
    public static class UIExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
}
