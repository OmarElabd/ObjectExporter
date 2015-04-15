param($installPath, $toolsPath, $package, $project)

$targetsFile = [System.IO.Path]::Combine($toolsPath, 'PostSharp.targets')

# Need to load MSBuild assembly if it's not loaded yet.
Add-Type -AssemblyName 'Microsoft.Build, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'

# Grab the loaded MSBuild project for the project
$msbuild = [Microsoft.Build.Evaluation.ProjectCollection]::GlobalProjectCollection.GetLoadedProjects($project.FullName) | Select-Object -First 1

$itemsToRemove = @()


# Remove stuff from the project.
$itemsToRemove += $msbuild.Xml.Properties | Where-Object {$_.Name.ToLowerInvariant() -eq "dontimportpostsharp" }
$itemsToRemove += $msbuild.Xml.Imports | Where-Object { $_.Project.ToLowerInvariant().EndsWith("postsharp.targets") }
$itemsToRemove += $msbuild.Xml.Targets | Where-Object {$_.Name.ToLowerInvariant() -eq "ensurepostsharpimported" }
  
if ($itemsToRemove -and $itemsToRemove.length)
{
    foreach ($itemToRemove in $itemsToRemove)
    {
        $itemToRemove.Parent.RemoveChild($itemToRemove) | out-null
    }
     
    $project.Save()
    $project.Object.Refresh()
}