using System;
using System.Reflection;
using ObjectExporter.VsPackage.Logging;
using ObjectExporter.VsPackage.Settings;
using PostSharp.Aspects;
using PostSharp.Extensibility;

namespace ObjectExporter.VsPackage.Aspects
{
    [Serializable]
    public sealed class LogUnhandledException : OnMethodBoundaryAspect
    {
        public LogUnhandledException()
        {

        }

        #region Build-Time Logic



        public override bool CompileTimeValidate(MethodBase method)
        {
            // TODO: Check that the aspect has been applied on a proper method.

            if (false)
            {
                Message.Write(method, SeverityType.Error, "MY001", "Cannot apply LogUnhandledException to method '{0}'.", method);
                return false;
            }

            return true;
        }

        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
        {
            // TODO: Initialize any instance field whose value only depends on the method to which the aspect is applied.
        }

        #endregion

        public override void OnException(MethodExecutionArgs args)
        {
            if (GlobalPackageSettings.ErrorReportingEnabled)
            {
                Raygun.LogException(args.Exception);
            }

            //rethrow the exception
            args.FlowBehavior = FlowBehavior.ThrowException;
        }
    }
}
