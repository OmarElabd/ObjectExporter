// Guids.cs
// MUST match guids.h

using System;

namespace ObjectExporter.VsPackage
{
    static class GuidList
    {
        public const string guidObjectExporter_PkgString = "07fb5b16-f4be-4488-9a19-b4f36d2c05a6";
        public const string guidObjectExporter_CmdSetString = "a81c49da-5682-436f-9c30-06a91886717c";

        public static readonly Guid guidObjectExporter_CmdSet = new Guid(guidObjectExporter_CmdSetString);
    };
}