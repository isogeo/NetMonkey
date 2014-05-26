NetMonkey
=========

A .NET wrapper for the MailChimp API v2.0.

# Build

- Run the following command from this directory: `build.bat`
- Prerequisites:
  * Microsoft .NET Framework 4.5
  * Microsoft Visual Studio 2013
  * [MSBuild.Community.Tasks 1.3](http://msbuildtasks.tigris.org/)
  * FxCop 10.0
  * [PartCover.NET 4.0.20908](http://github.com/sawilde/partcover.net4)



# Development

- Run `build.bat` to resolve NuGet dependencies beforehand, or the Visual Studio solutions will not load.
- The `NetMonkey.Tests.sln` solution is meant to be used for development. This and other solutions are used by the build scripts.
