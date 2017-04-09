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
using ObjectExporter.Core.Models.Expressions;
using ObjectExporter.Core.Models.RuleSets;
using ObjectExporter.VsPackage.Logging;
using ObjectExporter.VsPackage.Settings;
using ObjectExporter.VsPackage.ViewModels;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.UI;
using Task = System.Threading.Tasks.Task;

namespace ObjectExporter.VsPackage.Views
{
    public partial class FormSelectObjects : Form
    {
        private readonly DTE2 _dte2;
        private readonly PackageSettings _settings;
        private ProgressDialog _waitingDialog;

        private readonly RuleSetValidator _ruleSetValidator;

        //TODO: can remove PackageSettings and use GlobalPackageSettings
        public FormSelectObjects(DTE2 dte2, PackageSettings settings)
        {
            _dte2 = dte2;
            _settings = settings;

            InitializeComponent();
            LoadLocals();

            TypeRetriever retriever = new TypeRetriever(dte2);
            List<IRuleSet> ruleSets = new List<IRuleSet>();
            if (settings.IgnoreDynamicallyAddedProperties)
            {
                ruleSets.Add(new PropertyInClassRuleSet(retriever));
            }

            bool excludePrivates = radCheckBoxExcludePrivate.Checked;
            if (excludePrivates)
            {
                ruleSets.Add(new AccessiblePropertiesRuleSet(retriever));
            }

            _ruleSetValidator = new RuleSetValidator(ruleSets);
        }
        
        private void LoadLocals()
        {
            if (_dte2.Debugger != null &&
                _dte2.Debugger.CurrentMode == EnvDTE.dbgDebugMode.dbgBreakMode &&
                _dte2.Debugger.CurrentStackFrame != null)
            {
                Expressions localExpresisons = _dte2.Debugger.CurrentStackFrame.Locals;

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
            bool ignoreDynamicProperties = _settings.IgnoreDynamicallyAddedProperties;
            ExportType exportType = GetExportType();
            int maxDepth = (int)numericUpDownMaxDepth.Value;
            ExportParamaters exportParamaters = new ExportParamaters(excludePrivates, ignoreDynamicProperties, maxDepth, exportType);

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            _waitingDialog = new ProgressDialog(cancellationTokenSource);

            List<ExpressionWithSource> expressions = GetAllExpressions();

            if (expressions.Any())
            {
                //Hide and Show Progress Bar    
                this.Hide();
                _waitingDialog.Show(this);

                TypeRetriever retriever = new TypeRetriever(_dte2);
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
                    _waitingDialog.Close();
                }
                catch (ObjectDisposedException ex)
                {
                    _waitingDialog.Close();
                }
                catch (Exception ex)
                {
                    _waitingDialog.Close();
                    Raygun.LogException(ex);
                    MessageBox.Show("Error when attempting to export objects. If error reporting has not been disabled," +
                                    " then your error has already been logged.");
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
            _waitingDialog?.Close();
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
                Expression customExpression = _dte2.Debugger.GetExpression(expressionName);

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

        private async void radGridViewCustomExpressions_CellValueChanged(object sender, GridViewCellEventArgs e)
        {
            if (e.Column.Index == 0 && e.Value != null) //Expression Name Column
            {
                string expressionName = e.Value.ToString();
                Expression expression = _dte2.Debugger.GetExpression(expressionName);
                bool isValid = expression.IsValidValue;


                UpdateIsValidColumnImage(isValid, e.Row);

                if (isValid)
                {
                    int index = e.Row.Index;
                    e.Row.Cells[1].Value = "(calculating...)";

                    string depth;
                    try
                    {
                        CancellationTokenSource tokenSource = new CancellationTokenSource((int) _settings.DepthSolverTimeOut);
                        depth = await GetDepth(expression, tokenSource.Token);
                    }
                    catch (Exception ex)
                    {
                        depth = "timed out";
                    }

                    if (index == -1) //newly created row
                    {
                        int lastRow = radGridViewCustomExpressions.Rows.Count - 1;
                        radGridViewCustomExpressions.Rows[lastRow].Cells[1].Value = depth;
                    }
                    else
                    {
                        //set x,y as index changes after calculation.
                        e.Row.Cells[1].Value = depth;
                    }
                }
                else
                {
                    e.Row.Cells[1].Value = String.Empty;
                }
            }
        }

        private void UpdateIsValidColumnImage(bool isValid, GridViewRowInfo row)
        {
            try
            {
                if (isValid)
                {
                    row.Cells[2].Value = ImageResources.CheckCircle;
                }
                else
                {
                    row.Cells[2].Value = ImageResources.ExclamationCircle;
                }
            }
            catch (Exception ex)
            {
                row.Cells[2].Value = ImageResources.ExclamationCircle;
            }
        }

        private async void radCheckedListBoxLocals_ItemCheckedChanged(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.CheckState != ToggleState.On) return;

            ExpressionViewModel vm = (ExpressionViewModel)e.Item.DataBoundItem;
            Expression checkedExpression = vm.Expression;
            string expressionName = checkedExpression.Name;

            e.Item.Text = $"{expressionName} (calculating...)";


            CancellationTokenSource tokenSource = new CancellationTokenSource((int) _settings.DepthSolverTimeOut);
            string depth = await GetDepth(checkedExpression, tokenSource.Token);

            string textToDisplay = $"{expressionName} (max depth: {depth})";
            e.Item.Text = textToDisplay;
        }

        private async Task<string> GetDepth(Expression expression, CancellationToken token)
        {
            uint cutoff = _settings.DepthSolverCutoff;
            ObjectDepthFinder depthFinder = new ObjectDepthFinder(_ruleSetValidator, cutoff);

            string depth;
            try
            {
                int maxDepth = await depthFinder.GetMaximumObjectDepthAsync(expression, token);

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

            return depth;
        }
    }
}
