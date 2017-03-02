using System.Drawing;
using ObjectExporter.Core.Globals;
using ScintillaNET;

namespace ObjectExporter.VsPackage.Settings
{
    public static class ScintillaStyles
    {
        public static void InitStyle(Scintilla scintilla, ExportType type)
        {
            scintilla.StyleResetDefault();
            scintilla.Styles[Style.Default].Font = "Consolas";
            scintilla.Styles[Style.Default].Size = 10;
            scintilla.Styles[Style.Default].ForeColor = Color.Black;
            scintilla.StyleClearAll();

            switch (type)
            {
                case ExportType.CSharpObject:
                    SetCSharpStyle(scintilla);
                    break;
                case ExportType.Json:
                    SetJsonStyle(scintilla);
                    break;
                case ExportType.Xml:
                    SetXmlStyle(scintilla);
                    break;
            }
        }

        public static void SetCSharpStyle(Scintilla scintilla)
        {
            scintilla.Styles[Style.Cpp.Comment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            scintilla.Styles[Style.Cpp.CommentLine].ForeColor = Color.FromArgb(0, 128, 0); // Green
            scintilla.Styles[Style.Cpp.CommentLineDoc].ForeColor = Color.FromArgb(128, 128, 128); // Gray
            scintilla.Styles[Style.Cpp.Number].ForeColor = Color.Olive;

            scintilla.Styles[Style.Cpp.Default].ForeColor = Color.Black;

            scintilla.Styles[Style.Cpp.Word].ForeColor = Color.Blue;
            scintilla.Styles[Style.Cpp.Word2].ForeColor = Color.Blue;

            scintilla.Styles[Style.Cpp.String].ForeColor = Color.FromArgb(163, 21, 21); // Red
            scintilla.Styles[Style.Cpp.Character].ForeColor = Color.FromArgb(163, 21, 21); // Red
            scintilla.Styles[Style.Cpp.Verbatim].ForeColor = Color.FromArgb(163, 21, 21); // Red
            scintilla.Styles[Style.Cpp.StringEol].BackColor = Color.Pink;
            scintilla.Styles[Style.Cpp.Operator].ForeColor = Color.Purple;
            scintilla.Styles[Style.Cpp.Preprocessor].ForeColor = Color.Maroon;

            // Set the keywords
            // TODO move to ReservedKeywords.cs
            scintilla.SetKeywords(0, "abstract as base break case catch checked continue default delegate do else event explicit extern false finally fixed for foreach goto if implicit in interface internal is lock namespace new null object operator out override params private protected public readonly ref return sealed sizeof stackalloc switch this throw true try typeof unchecked unsafe using virtual while");
            scintilla.SetKeywords(1, "bool byte char class const decimal double enum float int long sbyte short static string struct uint ulong ushort void");
        }

        public static void SetXmlStyle(Scintilla scintilla)
        {
            scintilla.Styles[Style.Xml.Entity].ForeColor = Color.Red;
            scintilla.Styles[Style.Xml.Comment].ForeColor = Color.Green;
            scintilla.Styles[Style.Xml.Tag].ForeColor = Color.Blue;
            scintilla.Styles[Style.Xml.TagEnd].ForeColor = Color.Blue;
            scintilla.Styles[Style.Xml.DoubleString].ForeColor = Color.DeepPink;
            scintilla.Styles[Style.Xml.SingleString].ForeColor = Color.DeepPink;
        }

        public static void SetJsonStyle(Scintilla scintilla)
        {
            scintilla.Styles[Style.Json.Number].ForeColor = Color.Olive;
            scintilla.Styles[Style.Json.String].ForeColor = Color.Maroon;
            scintilla.Styles[Style.Json.Operator].ForeColor = IntToColor(0xE0E0E0);
        }

        private static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }
    }
}
