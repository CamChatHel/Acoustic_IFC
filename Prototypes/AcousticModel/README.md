
# Fachmodell Akustik

Diese .sln beinhaltet den Viewer um das Fachmodell Akustik darzustellen und zu Dateien zu verknüpfen. Es baut auf XbimWindowsUI von xBim Toolkit auf. Mehr Informationen sind im Ordner  Prototypes\AcousticModel\XbimWindowsUI-master vorhanden.
(AnsichtFachmodell.png)


# XbimWindowsUI

XbimWindowsUI is part of the [Xbim Toolkit](https://github.com/xBimTeam/XbimEssentials).
It contains libraries and applications that you can use to build applications on Windows desktops. 


## Compilation

**Visual Studio 2017 is recommended.**
Prior versions of Visual Studio may work, but we'd recomments 2017 where possible.
The [free VS 2017 Community Edition](https://visualstudio.microsoft.com/downloads/) will be fine. 
All projects target .NET Framework 4.7

The XBIM toolkit uses the NuGet technology for the management of several packages.
We have custom NuGet feeds for the *master* and *develop* branches of the solution, and use
Myget for CI builds. The [nuget.config](nuget.config) file should automatically add these feeds for you


