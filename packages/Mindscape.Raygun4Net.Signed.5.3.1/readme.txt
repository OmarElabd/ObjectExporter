Raygun4Net - Raygun Provider for .NET Framework
===================

Using Raygun4Net in an Mvc or WebApi project?
====================
If so, then this is not the NuGet package you are looking for.

If you have an MVC project, please uninstall this package, and install the Raygun4Net.Mvc package instead.
The Raygun4Net.Mvc package includes all the functionality of this package + MVC specific support.

If you have a WebApi project, please uninstall this package, and install the Raygun4Net.WebApi package instead.
The Raygun4Net.WebApi package only includes WebApi specific support and does not reference System.Web

NOTE: the Mvc and WebApi packages can work side-by-side, so install both if you have an Mvc WebApi project.

Where is my app API key?
====================
When you create a new application in your Raygun dashboard, your app API key is displayed at the top of the instructions page.
You can also find the API key by clicking the "Application Settings" button in the side bar of the Raygun dashboard.

Namespace
====================
The main classes can be found in the Mindscape.Raygun4Net namespace.

Supported platforms/frameworks
====================

Projects built with the following frameworks are supported:

* .NET 2.0, 3.5, 4.0, 4.5
* .NET 3.5 and 4.0 Client Profile
* ASP.NET
* WinForms, WPF, console apps etc.
* Windows Store apps (universal) for Windows 8.1 and Windows Phone 8.1
* Windows 8
* Windows Phone 7.1 and 8
* WinRT
* Xamarin.iOS and Xamarin.Mac (Both unified and classic)
* Xamarin.Android

The NuGet package will select the appropriate dll to use for your project.

Usage
====================

The Raygun4Net provider includes support for many .NET frameworks.
Scroll down to find information about using Raygun for your type of application.

ASP.NET
====================
Add a section to configSections:

<section name="RaygunSettings" type="Mindscape.Raygun4Net.RaygunSettings, Mindscape.Raygun4Net"/>

Add the Raygun settings configuration block from above:

<RaygunSettings apikey="YOUR_APP_API_KEY" />

Now you can either setup Raygun to send unhandled exceptions automatically or/and send exceptions manually.

To send unhandled exceptions automatically, use the RaygunHttpModule in web.config in the appropriate way for your application:

For system.web:

<httpModules>
  <add name="RaygunErrorModule" type="Mindscape.Raygun4Net.RaygunHttpModule"/>
</httpModules>

For system.webServer:

<modules>
  <add name="RaygunErrorModule" type="Mindscape.Raygun4Net.RaygunHttpModule"/>
</modules>

Anywhere in you code, you can also send exception reports manually simply by creating a new instance of the RaygunClient and call one of the Send or SendInBackground methods.
This is most commonly used to send exceptions caught in a try/catch block.

try
{
  
}
catch (Exception e)
{
  new RaygunClient().SendInBackground(e);
}

Or to send exceptions in your own handlers rather than using the http module described above.

protected void Application_Error()
{
  var exception = Server.GetLastError();
  new RaygunClient().Send(exception);
}

Additional ASP.NET configuration options
========================================

Exclude errors by HTTP status code
----------------------------------

If using the HTTP module then you can exclude errors by their HTTP status code by providing a comma separated list of status codes to ignore in the configuration. For example if you wanted to exclude errors that return the [I'm a teapot](http://tools.ietf.org/html/rfc2324) response code, you could use the configuration below.

<RaygunSettings apikey="YOUR_APP_API_KEY" excludeHttpStatusCodes="418" />

Exclude errors that originate from a local origin
-------------------------------------------------

Toggle this boolean and the HTTP module will not send errors to Raygun if the request originated from a local origin. i.e. A way to prevent local debug/development from notifying Raygun without having to resort to Web.config transforms.

<RaygunSettings apikey="YOUR_APP_API_KEY" excludeErrorsFromLocal="true" />

Remove sensitive request data
-----------------------------

If you have sensitive data in an HTTP request that you wish to prevent being transmitted to Raygun, you can provide lists of possible keys (names) to remove.
Keys to ignore can be specified on the RaygunSettings tag in web.config, (or you can use the equivalent methods on RaygunClient if you are setting things up in code).
The available options are:

ignoreFormFieldNames
ignoreHeaderNames
ignoreCookieNames
ignoreServerVariableNames

These can be set to be a comma separated list of keys to ignore. Setting an option as * will indicate that all the keys will not be sent to Raygun.
Placing * before, after or at both ends of a key will perform an ends-with, starts-with or contains operation respectively.
For example, ignoreFormFieldNames="*password*" will cause Raygun to ignore all form fields that contain "password" anywhere in the name.
These options are not case sensitive.

Providing a custom RaygunClient to the http module
--------------------------------------------------

Sometimes when setting up Raygun using the http module to send exceptions automatically, you may need to provide the http module with a custom RaygunClient instance in order to use some of the optional feature described at the end of this file.
To do this, get your Http Application to implement the IRaygunApplication interface. Implement the GenerateRaygunClient method to return a new (or previously created) RaygunClient instance.
The http module will use the RaygunClient returned from this method to send the unhandled exceptions.
In this method you can setup any additional options on the RaygunClient instance that you need - more information about each feature is described at the end of this file.

MVC
====================

As of version 4.0.0, Mvc support has been moved into a new NuGet package.
If you have an Mvc project, please uninstall this NuGet package and install the Mindscape.Raygun4Net.Mvc NuGet package instead.
The NuGet package will include a readme containing everything you need to know about using it.

The Mvc and WebApi NuGet packages can be installed in the same project.

Web Api
====================

As of version 4.0.0, WebApi support has been moved into a new NuGet package.
If you have a WebApi project, please uninstall this NuGet package and install the Mindscape.Raygun4Net.WebApi NuGet package instead.
The NuGet package will include a readme containing everything you need to know about using it.

The Mvc and WebApi NuGet packages can be installed in the same project.

WPF
====================
Create an instance of RaygunClient by passing your app API key in the constructor.
Attach an event handler to the DispatcherUnhandledException event of your application.
In the event handler, use the RaygunClient.Send method to send the Exception.

private RaygunClient _client = new RaygunClient("YOUR_APP_API_KEY");

public App()
{
  DispatcherUnhandledException += OnDispatcherUnhandledException;
}

void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
{
  _client.Send(e.Exception);
}

WinForms
====================
Create an instance of RaygunClient by passing your app API key in the constructor.
Attach an event handler to the Application.ThreadException event BEFORE calling Application.Run(...).
In the event handler, use the RaygunClient.Send method to send the Exception.

private static readonly RaygunClient _raygunClient = new RaygunClient("YOUR_APP_API_KEY");

[STAThread]
static void Main()
{
  Application.EnableVisualStyles();
  Application.SetCompatibleTextRenderingDefault(false);

  Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

  Application.Run(new Form1());
}

private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
{
  _raygunClient.Send(e.Exception);
}

Windows Store Apps (Windows 8.1 and Windows Phone 8.1)
====================

In the App.xaml.cs constructor (or any central entry point in your application), call the static RaygunClient.Attach method using your API key. This will catch and send all unhandled exception to Raygun for you.

public App()
{
  RaygunClient.Attach("YOUR_APP_API_KEY");
}

At any point after calling the Attach method, you can use RaygunClient.Current to get the static instance. This can be used for manually sending messages (via the Send methods) or changing options such as the User identity string.

You can manually send exceptions with the SendAsync method. When manually sending, currently the compiler does not allow you to use `await` in a catch block. You can however call SendAsync in a blocking way:

try
{
  throw new Exception("foo");
}
catch (Exception e)
{
  RaygunClient.Current.SendAsync(e);
}

WinRT
====================
In the App.xaml.cs constructor (or any main entry point to your application), call the static RaygunClient.Attach method using your API key.

public App()
{
  RaygunClient.Attach("YOUR_APP_API_KEY");
}

At any point after calling the Attach method, you can use RaygunClient.Current to get the static instance. This can be used for manually sending messages (via the Send methods) or changing options such as the User identity string.

Limitations of WinRT UnhandledException event and Wrap() workarounds
====================
The options available in WinRT for catching unhandled exceptions at this point in time are more limited
compared to the options in the more mature .NET framework. The UnhandledException event will be raised when
invalid XAML is parsed, in addition to other runtime exceptions that happen on the main UI thread. While
many errors will be picked up this way and therefore be able to be sent to Raygun, others will be missed by
this exception handler. In particular asynchronous code or Tasks that execute on background threads will
not have their exceptions caught.

A workaround for this issue is provided with the Wrap() method. These allow you to pass the code you want
to execute to an instance of the Raygun client - it will simply call it surrounded by a try-catch block.
If the method you pass in does result in an exception being thrown this will be transmitted to Raygun, and
the exception will again be thrown. Two overloads are available; one for methods that return void and
another for methods that return an object.

Windows Phone 7.1 and 8
=======================
In the App.xaml.cs constructor (or any main entry point to your application), call the static RaygunClient.Attach method using your API key.

RaygunClient.Attach("YOUR_APP_API_KEY");

At any point after calling the Attach method, you can use RaygunClient.Current to get the static instance. This can be used for manually sending messages (via the Send methods) or changing options such as the User identity string.

Xamarin for Android
====================
In the main/entry Activity of your application, use the static RaygunClient.Attach method using your app API key.
There is also an overload for the Attach method that lets you pass in a user-identity string which is useful for tracking affected users in your Raygun dashboard.

RaygunClient.Attach("YOUR_APP_API_KEY");

At any point after calling the Attach method, you can use RaygunClient.Current to get the static instance. This can be used for manually sending messages or changing options such as the User identity string.

Xamarin for iOS
====================
In the main entry point of the application, use the static RaygunClient.Attach method using your app API key.
There is also an overload for the Attach method that lets you pass in a user-identity string which is useful for tracking affected users in your Raygun dashboard.

static void Main(string[] args)
{
  RaygunClient.Attach("YOUR_APP_API_KEY");

  UIApplication.Main(args, null, "AppDelegate");
}

At any point after calling the Attach method, you can use RaygunClient.Current to get the static instance. This can be used for manually sending messages or changing options such as the User identity string.

Xamarin for Mac
====================
Xamarin for Mac support is not included in the NuGet package or the Raygun4Net Xamarin Component. Instead, download the .zip of assemblies from the latest release on GitHub: https://github.com/MindscapeHQ/raygun4net/releases (Click the green button). Then copy and reference the Mindscape.Raygun4Net.Xamarin.Mac.dll into your Xamarin.Mac project.

In the main entry point of the application, use the static RaygunClient.Attach method using your app API key.

static void Main(string[] args)
{
  RaygunClient.Attach("YOUR_APP_API_KEY");

  NSApplication.Init();
  NSApplication.Main(args);
}

At any point after calling the Attach method, you can use RaygunClient.Current to get the static instance. This can be used for manually sending messages or changing options such as the User identity string.

Additional features for all .Net frameworks:
============================================

Modify or cancel message
------------------------

On a RaygunClient instance, attach an event handler to the SendingMessage event. This event handler will be called just before the RaygunClient sends an exception - either automatically or manually.
The event arguments provide the RaygunMessage object that is about to be sent. One use for this event handler is to add or modify any information on the RaygunMessage.
Another use for this method is to identify exceptions that you never want to send to raygun, and if so, set e.Cancel = true to cancel the send.

Strip wrapper exceptions
------------------------

If you have common outer exceptions that wrap a valuable inner exception which you'd prefer to group by, you can specify these by using the multi-parameter method:

raygunClient.AddWrapperExceptions(typeof(TargetInvocationException));

In this case, if a TargetInvocationException occurs, it will be removed and replaced with the actual InnerException that was the cause.
Note that HttpUnhandledException and TargetInvocationException are already added to the wrapper exception list; you do not have to add these manually.
This method is useful if you have your own custom wrapper exceptions, or a framework is throwing exceptions using its own wrapper.

Unique (affected) user tracking
-------------------------------

There is a property named *User* on RaygunClient which you can set to be the current user's ID or email address.
This allows you to see the count of affected users for each error in the Raygun dashboard.
If you provide an email address, and the user has an associated Gravatar, you will see their avatar in the error instance page.

Make sure to abide by any privacy policies that your company follows when using this feature.

Version numbering
-----------------

By default, Raygun will send the assembly version of your project with each report.
If you are using WinRT, the transmitted version number will be that of the Windows Store package, set in Package.appxmanifest (under Packaging).

If you need to provide your own custom version value, you can do so by setting the ApplicationVersion property of the RaygunClient (in the format x.x.x.x where x is a positive integer).

Tags and custom data
--------------------

When sending exceptions manually, you can also send an arbitrary list of tags (an array of strings), and a collection of custom data (a dictionary of any objects).
This can be done using the various Send and SendInBackground method overloads.

Custom grouping keys
--------------------
You can provide your own grouping key if you wish. We only recommend this you're having issues with errors not being grouped properly.

On a RaygunClient instance, attach an event handler to the CustomGroupingKey event. This event handler will be called after Raygun has built the RaygunMessage object, but before the SendingMessage event is called.
The event arguments provide the RaygunMessage object that is about to be sent, and the original exception that triggered it. You can use anything you like to generate the key, and set it by `CustomGroupingKey`
property on the event arguments. Setting it to null or empty string will leave the exception to be grouped by Raygun, setting it to something will cause Raygun to group it with other exceptions you've sent with that key.

The key has a maximum length of 100.