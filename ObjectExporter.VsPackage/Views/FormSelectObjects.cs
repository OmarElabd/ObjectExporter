using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AccretionDynamics.ObjectExporter.VsPackage.ViewModels;
using EnvDTE;
using EnvDTE80;
using ObjectExporter.Core;
using ObjectExporter.Core.Globals;
using ObjectExporter.Core.Models;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.UI;
using Task = System.Threading.Tasks.Task;

//NOTE: bug inradCheckListBox: http://feedback.telerik.com/Project/154/Feedback/Details/155730-fix-radcheckedlistbox-when-the-allowarbitraryitemwidth-property-is-set-to-true

namespace AccretionDynamics.ObjectExporter.VsPackage.Views
{
    public partial class FormSelectObjects : Form
    {
        private readonly DTE2 dte2;
        private readonly PackageSettings _settings;
        private ProgressDialog waitingDialog;

        public FormSelectObjects(DTE2 dte2, PackageSettings settings)
        {
            this.dte2 = dte2;
            _settings = settings;

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

                List<ExpressionViewModel> expressionViewModels = expressionList.Select(x => new ExpressionViewModel(x, x.Name))
                                                                               .ToList();

                radCheckedListBoxLocals.DataSource = expressionViewModels;
                radCheckedListBoxLocals.DisplayMember = "DisplayName";
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void buttonExport_Click(object sender, EventArgs e)
        {
            //Create Export Paramaters
            bool excludePrivates = radCheckBoxExcludePrivate.Checked;
            ExportType exportType = GetExportType();
            int maxDepth = (int)numericUpDownMaxDepth.Value;
            ExportParamaters exportParamaters = new ExportParamaters(excludePrivates, maxDepth, exportType);

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            waitingDialog = new ProgressDialog(cancellationTokenSource);

            List<ExpressionWithSource> expressions = GetAllExpressions();

            if (expressions.Any())
            {
                //Hide and Show Progress Bar    
                this.Hide();
                waitingDialog.Show(this);

                AccessibilityRetriever retriever = new AccessibilityRetriever(dte2);
                var exportGenerator = new ExportGenerator(expressions, retriever, exportParamaters);

                try
                {
                    Dictionary<string, string> lookupGeneratedTexts = await exportGenerator.GenerateTextWithKey(cancellationTokenSource.Token);

                    //Setup event for when the form is shown to close the waiting dialog
                    FormDisplayGeneratedText formDisplayGeneratedText = new FormDisplayGeneratedText(lookupGeneratedTexts, exportType);
                    formDisplayGeneratedText.Shown += formDisplayGeneratedText_Shown;
                    formDisplayGeneratedText.ShowDialog(this);
                }
                catch (ThreadAbortException ex)
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
                    //MessageBox.Show("Error: Unable to export all objects");
                    MessageBox.Show(ex.ToString());
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

            foreach (ListViewDataItem lvItem in radCheckedListBoxLocals.CheckedItems)
            {
                ExpressionViewModel expressionVM = (ExpressionViewModel)lvItem.DataBoundItem;
                Expression expression = expressionVM.Expression;
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
            if (e.Column.Index == 0 && e.Value != null) //Expression Name Column
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

        private async void radCheckedListBoxLocals_ItemCheckedChanged(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.CheckState != ToggleState.On) return;

            ExpressionViewModel vm = (ExpressionViewModel)e.Item.DataBoundItem;
            Expression checkedExpression = vm.Expression;
            string expressionName = checkedExpression.Name;

            e.Item.Text = String.Format("{0} (calculating...)", expressionName);

            uint cutoff = _settings.DepthSolverCutoff;
            ObjectDepthFinder depthFinder = new ObjectDepthFinder(cutoff);

            int timeoutMills = (int)_settings.DepthSolverTimeOut;
            CancellationTokenSource tokenSource = new CancellationTokenSource(timeoutMills);

            string depth;
            try
            {
                int maxDepth = await depthFinder.GetMaximumObjectDepthAsync(checkedExpression, tokenSource.Token);

                if (maxDepth == -1)
                {
                    depth = "∞";
                }
                else if (maxDepth == cutoff)
                {
                    depth = "> " + maxDepth;
                }
                else
                {
                    depth = maxDepth.ToString();
                }
            }
            catch (TypeLoadException)
            {
                depth = "timed out";
            }

            string textToDisplay = String.Format("{0} (max depth: {1})", expressionName, depth);
            e.Item.Text = textToDisplay;
        }
    }
}
