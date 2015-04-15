using System;
using Mindscape.Raygun4Net;
using ObjectExporter.Core;
using ObjectExporter.VsPackage.Settings;

namespace ObjectExporter.VsPackage
{
    public static class Raygun
    {
        public static void LogException(Exception ex)
        {
            if (GlobalPackageSettings.ErrorReportingEnabled)
            {
                RaygunClient client = new RaygunClient(ApiKeys.Raygun);
                client.Send(ex);
            }
        }
    }
}
