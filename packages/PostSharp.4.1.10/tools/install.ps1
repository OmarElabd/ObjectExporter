param($installPath, $toolsPath, $package, $project)


function PathToUri([string] $path)
{
    return new-object Uri('file://' + $path.Replace("%","%25").Replace("#","%23").Replace("$","%24").Replace("+","%2B").Replace(",","%2C").Replace("=","%3D").Replace("@","%40").Replace("~","%7E").Replace("^","%5E"))
}

function UriToPath([System.Uri] $uri)
{
    return [System.Uri]::UnescapeDataString( $uri.ToString() ).Replace([System.IO.Path]::AltDirectorySeparatorChar, [System.IO.Path]::DirectorySeparatorChar)
}

$targetsFile = [System.IO.Path]::Combine($toolsPath, 'PostSharp.targets')

# Need to load MSBuild assembly if it's not loaded yet.
Add-Type -AssemblyName 'Microsoft.Build, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'

# Grab the loaded MSBuild project for the project
$msbuild = [Microsoft.Build.Evaluation.ProjectCollection]::GlobalProjectCollection.GetLoadedProjects($project.FullName) | Select-Object -First 1

# Make the path to the targets file relative.
$projectUri = PathToUri $project.FullName
$targetUri = PathToUri $targetsFile

$relativePath = UriToPath $projectUri.MakeRelativeUri($targetUri)

# Remove elements from previous installations or versions.
$itemsToRemove = @()
$itemsToRemove += $msbuild.Xml.Properties | Where-Object {$_.Name.ToLowerInvariant() -eq "dontimportpostsharp" }
# $itemsToRemove += $msbuild.Xml.Properties | Where-Object {$_.Name.ToLowerInvariant().EndsWith("postsharpignoredpackages") } # Don't remove this so that it stays during upgrades.
$itemsToRemove += $msbuild.Xml.Imports | Where-Object {$_.Project.ToLowerInvariant().EndsWith("postsharp.targets") } 
$itemsToRemove += $msbuild.Xml.Targets | Where-Object {$_.Name.ToLowerInvariant() -eq "ensurepostsharpimported" }


if ($itemsToRemove -and $itemsToRemove.length)
{
    foreach ($itemToRemove in $itemsToRemove)
    {
        $itemToRemove.Parent.RemoveChild($itemToRemove) | out-null
    }
}

# Remove references from PostSharp 1.* and 2.*.
$referencesToRemove = @()
$referencesToRemove += $project.Object.References | Where-Object {$_.Identity.ToLowerInvariant().StartsWith("postsharp.public") } 
$referencesToRemove += $project.Object.References | Where-Object {$_.Identity.ToLowerInvariant().StartsWith("postsharp.laos") } 

if ($referencesToRemove -and $referencesToRemove.length)
{
    foreach ($referenceToRemove in $referencesToRemove)
    {
        $referenceToRemove.Remove()
    }
}



# Set property DontImportPostSharp to prevent locally-installed previous versions of PostSharp to interfere.
$msbuild.Xml.AddProperty( "DontImportPostSharp", "True" ) | Out-Null

# Add import to PostSharp.targets
$import = $msbuild.Xml.AddImport($relativePath)
$import.set_Condition( "Exists('$relativePath')" ) | Out-Null
[string]::Format("Added import of '{0}'.", $relativePath )

 # Add a target to fail the build when our targets are not imported
$target = $msbuild.Xml.AddTarget("EnsurePostSharpImported")
$target.BeforeTargets = "BeforeBuild"
$target.Condition = "'`$(PostSharp30Imported)' == ''"

# if the targets don't exist at the time the target runs, package restore didn't run
$errorTask = $target.AddTask("Error")
$errorTask.Condition = "!Exists('$relativePath')"
$errorTask.SetParameter("Text", "This project references NuGet package(s) that are missing on this computer. Enable NuGet Package Restore to download them.  For more information, see http://www.postsharp.net/links/nuget-restore.");

# if the targets exist at the time the target runs, package restore ran but the build didn't import the targets.
$errorTask = $target.AddTask("Error")
$errorTask.Condition = "Exists('$relativePath')"
$errorTask.SetParameter("Text", "The build restored NuGet packages. Build the project again to include these packages in the build. For more information, see http://www.postsharp.net/links/nuget-restore.");

# For all the assembly references installed by this package - set CopyLocal = true
$pakcageRefs = $package.AssemblyReferences | %{$_.Name}
foreach ($reference in $project.Object.References)
{
    if ($pakcageRefs -contains $reference.Name + ".dll")
    {
        # To persist the CopyLocal value we have to change it from true to false first
        $reference.CopyLocal = $false;
        $reference.CopyLocal = $true;
    }
}

$project.Save()
$project.Object.Refresh()

# Asynchronously run setup wizard if necessary. Since the setup wizard is compressed in PostSharp-Tools.exe, the easiest is to run it through MSBuild.
$msbuildExe = [System.IO.Path]::Combine( [System.Runtime.InteropServices.RuntimeEnvironment]::GetRuntimeDirectory(), "msbuild.exe")
"Starting $msbuildExe"
Start-Process -FilePath $msbuildExe -ArgumentList @("""$toolsPath\PostSharp.targets""", "/t:PostSharp30InstallVsx /p:BuildingInsideVisualStudio=True") -WindowStyle Hidden
	