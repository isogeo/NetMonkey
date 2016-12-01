[![Build status](https://ci.appveyor.com/api/projects/status/e83vlonrwpuhmq5m/branch/master?svg=true)](https://ci.appveyor.com/project/mcartoixa/netmonkey/branch/master)

NetMonkey
=========

A .NET wrapper for the MailChimp API v3.0.

## Description

NetMonkey is [another .NET wrapper](http://apidocs.mailchimp.com/api/downloads/#microsoft-net-framework) for the MailChimp API. Its characteristics are:

- All the API calls are asynchronous (hence the dependency on .NET 4.5).
- The generated assemblies are strongly signed.
- Only 4 verbs are implemented thus far, but we accept contributions ;-)

## Build

- Run the following command from this directory: `build.bat`
- Prerequisites:
  * Microsoft .NET Framework 4.5
  * Microsoft Visual Studio 2015
  * [MSBuild.Community.Tasks 1.5.0](https://github.com/loresoft/msbuildtasks)
  * [OpenCover 4.6](https://github.com/OpenCover/opencover)



## Development

- Run `build.bat` to resolve NuGet dependencies beforehand, or the Visual Studio solutions will not load.
- The `NetMonkey.Tests.sln` solution is meant to be used for development. This and other solutions are used by the build scripts.
