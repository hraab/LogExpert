Here are known contributions from other people.

## Generic Columnizer
It works without further implementation and supports all line based logs with fixed size columns. Including source code.

[http://www.toolhouse-westware.de/projects.htm](http://www.toolhouse-westware.de/projects.htm)

## RegExColumnizer

A generic RegEx columnizer contributed by Eric Meltzer. This regex columnizer is capable of parsing time stamps. Some RegEx skills required ;)

To use it you have to provide named groups in the regex term. Example for splitting into 2 columns:

Example log line:
{"2012-08-23 18:34:00,985[JBoss Shutdown Hook](JBoss-Shutdown-Hook)[INFO, org.jboss.system.server.Server](INFO,-org.jboss.system.server.Server) Runtime shutdown hook called, forceHalt: true"}

RegEx:
{"(?<date>.+?)(?<text>\[.*)"}

Binary: [http://www.log-expert.de/RegexColumnizer.zip](http://www.log-expert.de/RegexColumnizer.zip)
Source: [http://www.log-expert.de/RegexColumnizer_src.rar](http://www.log-expert.de/RegexColumnizer_src.rar)

Eric has given permissions to publish and/or change the source. So feel free to use it!

## SharePoint Columnizer
A columnizer for SharePoint ULS Logs by Thomas Meckel. Includes source code.

[http://www.log-expert.de/SharePointULSColumnizer.zip](http://www.log-expert.de/SharePointULSColumnizer.zip)

## LogCat Columnizer
Columnizer for Android logcat logs.
[https://github.com/Bhiefer/LogCatColumnizer](https://github.com/Bhiefer/LogCatColumnizer)
