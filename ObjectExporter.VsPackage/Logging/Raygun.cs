using System;
using System.Collections;
using System.Collections.Generic;
using Mindscape.Raygun4Net;
using ObjectExporter.Core;
using ObjectExporter.VsPackage.Settings;

namespace ObjectExporter.VsPackage.Logging
{
    public static class Raygun
    {       
        private static UserInfo _info;

        public static void InitializeUserInfo(UserInfo info)
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
                    ApplicationVersion = "1.4.0"
                };

                client.Send(ex, null, userInfo);
            }
        }
    }
}
