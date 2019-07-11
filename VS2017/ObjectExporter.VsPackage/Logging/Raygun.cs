using System;
using System.Collections;
using System.Collections.Generic;
using Mindscape.Raygun4Net;
using ObjectExporter.Core;
using ObjectExporter.VsPackage.Settings;

namespace ObjectExporter.VsPackage.Logging
{
    //TODO: Could move into core, or possibly into it's own project. NOTE: PostSharp needs to be on the project using the aspect.
    public static class Raygun
    {       
        private static UserInfo _info;

        public static void Initialize(UserInfo info)
        {
            _info = info;
        }

        public static void LogException(Exception ex)
        {
            if (GlobalPackageSettings.ErrorReportingEnabled)
            {
                IDictionary userInfo = new Dictionary<string, string>()
                {
                    { "visual studio version", _info.VisualStudioVersion }
                };

                RaygunClient client = new RaygunClient(ApiKeys.RayGun)
                {
                    ApplicationVersion = "1.7.0"
                };

                client.Send(ex, null, userInfo);
            }
        }
    }
}
