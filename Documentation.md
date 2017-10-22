# Overview
LogExpert is a Windows tail program (a GUI replacement for the Unix tail command).

Summary of features:
* Tail mode
* MDI-Interface with Tabs
* Search function (including RegEx)
* Bookmarks
* A very flexible filter view
* Highlighting lines via search criteria
* Columnizers: This means splitting log lines into columns for some well defined logfile formats
* Unicode support
* log4j XML file support
* 3rd party plugin support
* SFTP support (loading files directly from SFTP)
* Plugin API for more log file data sources

# Building the sources
You need Visual Studio 2008 (maybe 2005 works as well). LogExpert is written in C# and needs .NET 2.0. 

# Check out the source (SVN)
# Open LogExpert.sln in the LogExpert sub folder
# Press F6 :) It should compile out of the box
# Compiled application and all plugins will be compiled to bin/Debug or bin/Release. There's no build script or deployment yet. Just copy the needed stuff.

I made the whole project using Visual Studio 2008 Standard Edition. The express editions may also work, but I haven't tried yet.

# 3rd Party dependencies
* All dependency DLLs for LogExpert core functions are located in the **lib** folder. A text file with URLs and license info is also located in the folder.
* For the NUnit tests you need a NUnit runtime environment
* The CSV-Columnizer uses a CSV reader from http://www.codeproject.com/KB/database/CsvReader.aspx
* MDI Tab Control is included as source code, because I made some minor changes. It's a Visual Basic project.

# Help file
* All help file resources are in the sub folder **HelpSmith**
* The help file is created with HelpSmith ([http://www.helpsmith.com/](http://www.helpsmith.com/))

# SDK
* SDK for columnizer and plugin development is located in the sub folder **SDK**
* The SDK contains a general help file (created with HelpSmith) and an API doc created with NDoc [http://ndoc.sourceforge.net/](http://ndoc.sourceforge.net/)

# Where to start?
* [Code overview](Code-overview)

# Future?
* [Future plans/ideas for LogExpert](Future-plans_ideas-for-LogExpert)