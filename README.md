# Object Exporter
Object Exporter creates serializations of in memory objects from your various debugging windows. Currently supported output formats are: CSharp Object Initialization Code, JSON and XML.

# Release Notes
https://raw.githubusercontent.com/OmarElabd/ObjectExporter/master/ObjectExporter.VsPackage/Documentation/Release%20Notes.txt

# Use Cases
1. Persisting an object state for debugging comparisons.

2. Searching for information within objects.

3. Generating C# object initialization code for unit testing.

# Output
The currently supported output formats are: C# Object Initialization Code, JSON and XML.

# Instructions

Object Exporter is accessed through the tools menu and is only visible when you are debugging.

![Select from tool menu](https://raw.githubusercontent.com/OmarElabd/ObjectExporter/master/ObjectExporter.VsPackage/Documentation/Object%20Exporter%20-%20Tools%20Menu.png)


Once the menu option is selected a dialog is shown with settings for the Object Export.

Object exporter has two modes for selecting objects to export, one is by selecting from a checklist which is populated with your locals.

![Select locals](https://raw.githubusercontent.com/OmarElabd/ObjectExporter/master/ObjectExporter.VsPackage/Documentation/Object%20Exporter%20-%20Select%20From%20Locals%201.png)

The other mode is by writing a custom expression as you would in the watch window.

![Custom Expression](https://github.com/OmarElabd/ObjectExporter/blob/master/ObjectExporter.VsPackage/Documentation/Object%20Exporter%20-%20Custom%20Expressions.png)

Once an object is written or selected, object exporter will attempt to calculate it's depth in the background. This depth will give you an indication of what cutoff would need to be specified to export the entire object. Note some objects may contain circular references.

![Select Locals with calculated depth](https://raw.githubusercontent.com/OmarElabd/ObjectExporter/master/ObjectExporter.VsPackage/Documentation/Object%20Exporter%20-%20Select%20From%20Locals%202.png)

Once your objects and settings are selected, you may export them in your desired format. A dialog will be displayed with the generated ouput for each of your objects.

C#:

![Generated C#](https://raw.githubusercontent.com/OmarElabd/ObjectExporter/master/ObjectExporter.VsPackage/Documentation/Object%20Exporter%20-%20Generated%20C%23.png)

JSON:

![Generated JSON](https://raw.githubusercontent.com/OmarElabd/ObjectExporter/master/ObjectExporter.VsPackage/Documentation/Object%20Exporter%20-%20Generated%20JSON.png)

XML:

![Generated XML](https://raw.githubusercontent.com/OmarElabd/ObjectExporter/master/ObjectExporter.VsPackage/Documentation/Object%20Exporter%20-%20Generated%20XML.png)

# Settings

Object exporter settings is access through Tools -> Options -> Object Exporter

![Settings](https://raw.githubusercontent.com/OmarElabd/ObjectExporter/master/ObjectExporter.VsPackage/Documentation/Object%20Exporter%20-%20Options.png)

# Info

Object Exporter is powered by [RayGun](https://raygun.io/).

![Raygun](https://brandfolder.com/raygun/assets/14l8cwle)
