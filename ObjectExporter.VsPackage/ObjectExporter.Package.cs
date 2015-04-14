using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using System.IO;
using System.Reflection;
using AccretionDynamics.ObjectExporter.VsPackage.Views;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
using Mindscape.Raygun4Net;

namespace AccretionDynamics.ObjectExporter.VsPackage
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the information needed to show this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    // This attribute is needed to let the shell know that this package exposes some menus.
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.guidObjectExporter_PkgString)]
    //Used for the hidden menu item
    [ProvideAutoLoad(UIContextGuids80.SolutionExists)]
    [ProvideOptionPage(typeof(PackageSettings),
    "Object Exporter", "General", 0, 0, true)]
    public sealed class ObjectExporter : Package
    {
        readonly DTE2 dte2 = GetGlobalService(typeof(DTE)) as DTE2;


        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public ObjectExporter()
        {

                //const string apiKey = ApiKeys.Raygun;
                //RaygunClient client = new RaygunClient(apiKey);
                //client.Send(e);

            string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            IntPtr pDll = NativeMethods.LoadLibrary(Path.Combine(assemblyPath, "SciLexer.dll"));

            if (pDll == IntPtr.Zero)
            {
                throw new DllNotFoundException("Could not find SciLexer.dll - This is unmanaged assembly is required by ScintillaNet.dll");
            }
        }

        /////////////////////////////////////////////////////////////////////////////
        // Overridden Package Implementation
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", ToString()));
            base.Initialize();

            // Add our command handlers for menu (commands must exist in the .vsct file)
            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (null != mcs)
            {
                // Create the command for the menu item.
                CommandID menuCommandID = new CommandID(GuidList.guidObjectExporter_CmdSet, (int)PkgCmdIDList.cmdidExportObjects);
                var menuItem = new OleMenuCommand(MenuItemCallback, menuCommandID);

                menuItem.BeforeQueryStatus += menuItem_BeforeQueryStatus;
                mcs.AddCommand(menuItem);
            }
        }

        void menuItem_BeforeQueryStatus(object sender, EventArgs e)
        {
            // get the menu that fired the event
            var menuCommand = sender as OleMenuCommand;
            if (menuCommand != null)
            {
                // start by assuming that the menu will not be shown
                menuCommand.Visible = false;
                menuCommand.Enabled = false;

                if (dte2.Debugger != null &&
                    dte2.Debugger.CurrentMode == dbgDebugMode.dbgBreakMode &&
                    dte2.Debugger.CurrentStackFrame != null)
                {
                    menuCommand.Visible = true;
                    menuCommand.Enabled = true;
                }
            }
        }
        #endregion

        /// <summary>
        /// This function is the callback used to execute a command when the a menu item is clicked.
        /// See the Initialize method to see how the menu item is associated to this function using
        /// the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            if (dte2.Debugger != null && 
                dte2.Debugger.CurrentMode == dbgDebugMode.dbgBreakMode &&
                dte2.Debugger.CurrentStackFrame != null)
            {
                PackageSettings settings = (PackageSettings) GetDialogPage(typeof (PackageSettings));
                FormSelectObjects objForm = new FormSelectObjects(dte2, settings);
                objForm.Show(new VsMainWindowWrapper(dte2));
            }
        }
    }
}
