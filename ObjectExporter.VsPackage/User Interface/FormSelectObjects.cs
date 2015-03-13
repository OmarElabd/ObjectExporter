using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EnvDTE;
using EnvDTE80;
using ObjectExporter.Core;
using ObjectExporter.Core.Globals;
using ObjectExporter.Core.Models;
using ObjectExporter.UI;
using Telerik.WinControls.UI;

namespace AccretionDynamics.ObjectExporter.VsPackage.UserInterface
{
    public partial class FormSelectObjects : Form
    {
        private readonly DTE2 dte2;
        private ProgressDialog waitingDialog;

        public FormSelectObjects(DTE2 dte2)
        {
            this.dte2 = dte2;

            InitializeComponent();
            LoadLocals();
        }

        private void LoadLocals()
        {
            if (dte2.Debugger.CurrentMode == EnvDTE.dbgDebugMode.dbgBreakMode &&
                dte2.Debugger != null &&
                dte2.Debugger.CurrentStackFrame != null)
            {
                Expressions localExpresisons = dte2.Debugger.CurrentStackFrame.Locals;

                var expressionList = localExpresisons.Cast<Expression>().ToList();

                ((ListBox)checkedListBoxWatchObjects).DataSource = expressionList;
                ((ListBox)checkedListBoxWatchObjects).DisplayMember = "Name";
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void buttonExport_Click(object sender, EventArgs e)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            waitingDialog = new ProgressDialog(cancellationTokenSource);

            ExportType exportType = GetExportType();
            int maxDepth = (int)numericUpDownMaxDepth.Value;

            List<ExpressionWithSource> expressions = GetAllExpressions();

            if (expressions.Any())
            {
                //Hide and Show Progress Bar
                this.Hide();
                waitingDialog.Show(this);

                var exportGenerator = new ExportGenerator(exportType, expressions, maxDepth);

                try
                {
                    Dictionary<string, string> lookupGeneratedTexts = await exportGenerator.GenerateTextWithKey(cancellationTokenSource.Token);
                    FormDisplayGeneratedText formDisplayGeneratedText = await CreateAndShowFormAsync(lookupGeneratedTexts, exportType, cancellationTokenSource.Token);

                    //Setup event for when the form is shown to close the waiting dialog
                    formDisplayGeneratedText.Shown += formDisplayGeneratedText_Shown;
                    formDisplayGeneratedText.ShowDialog(this);
                }
                catch (ThreadAbortException)
                {
                    waitingDialog.Close();
                }
                catch (ObjectDisposedException ex)
                {
                    waitingDialog.Close();
                }
                catch (Exception ex)
                {
                    waitingDialog.Close();
                    MessageBox.Show("Error: Unable to export all objects");
                }
                finally
                {
                    this.Show();
                }
            }
        }

        private Task<FormDisplayGeneratedText> CreateAndShowFormAsync(Dictionary<string, string> lookupGeneratedTexts, ExportType exportType)
        {
            return Task.Run(() => new FormDisplayGeneratedText(lookupGeneratedTexts, exportType));
        }

        private Task<FormDisplayGeneratedText> CreateAndShowFormAsync(Dictionary<string, string> lookupGeneratedTexts, 
            ExportType exportType, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                try
                {
                    using (cancellationToken.Register(System.Threading.Thread.CurrentThread.Abort))
                    {
                        var formGeneratedText = new FormDisplayGeneratedText(lookupGeneratedTexts, exportType);
                        return formGeneratedText;
                    }
                }
                catch (ThreadAbortException)
                {
                    throw;
                }
            }, cancellationToken);
        }

        void formDisplayGeneratedText_Shown(object sender, EventArgs e)
        {
            if (waitingDialog != null)
            {
                waitingDialog.Close();
            }
        }

        private List<ExpressionWithSource> GetAllExpressions()
        {
            var expressions = new List<ExpressionWithSource>();

            expressions.AddRange(GetSelectedExpressions());
            expressions.AddRange(GetCustomExpressions());

            return expressions;
        }

        private IEnumerable<ExpressionWithSource> GetCustomExpressions()
        {
            var expressions = new List<ExpressionWithSource>();

            foreach (var row in radGridViewCustomExpressions.Rows)
            {
                string expressionName = row.Cells[0].Value.ToString(); //First Column is Expression Name
                Expression customExpression = dte2.Debugger.GetExpression(expressionName);

                if (customExpression.IsValidValue)
                {
                    expressions.Add(new ExpressionWithSource(customExpression, ExpressionSourceType.CustomExpression));
                }
            }

            return expressions;
        }

        private IEnumerable<ExpressionWithSource> GetSelectedExpressions()
        {
            var expressions = new List<ExpressionWithSource>();

            foreach (Expression expression in checkedListBoxWatchObjects.CheckedItems)
            {
                expressions.Add(new ExpressionWithSource(expression, ExpressionSourceType.Locals));
            }

            return expressions;
        }

        private ExportType GetExportType()
        {
            if (radioButtonCSharpObject.Checked)
            {
                return ExportType.CSharpObject;
            }
            if (radioButtonJson.Checked)
            {
                return ExportType.Json;
            }
            if (radioButtonXml.Checked)
            {
                return ExportType.Xml;
            }
            else
            {
                return ExportType.Xml;
            }
        }

        private void radGridViewCustomExpressions_CellValueChanged(object sender, GridViewCellEventArgs e)
        {
            if (e.Column.Index == 0) //Expression Name Column
            {
                string expressionName = e.Value.ToString();
                UpdateIsValidColumnImage(expressionName, e.Row);
            }
        }

        private void UpdateIsValidColumnImage(string expressionName, GridViewRowInfo row)
        {
            try
            {
                Expression addedExpression = dte2.Debugger.GetExpression(expressionName);

                if (addedExpression.IsValidValue)
                {
                    row.Cells[1].Value = ImageResources.CheckCircle;
                }
                else
                {
                    row.Cells[1].Value = ImageResources.ExclamationCircle;
                }
            }
            catch (Exception ex)
            {
                row.Cells[1].Value = ImageResources.ExclamationCircle;
            }
        }
    }
}
