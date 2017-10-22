# Future plans for LogExpert
Some guy asked me about my plans for LogExpert. My answer was quite long, so I decided to share the info to the public.

You can apply for jobs! ;)

## #1 - File handling
I would like to change the file handling so that LE will not need to load/scan the complete file. Currently all files will be loaded completely at first. The actual content is purged and reloaded when needed (depending on mem settings). This kind of file handling allows me to count the lines in the file. This makes some things easier. But now I realized that there are very large log files out there (several 100MB). LogExpert can handle this stuff but it takes too much time for initial loading.

Rewriting the file handling has a massive impact on all parts of LogExpert. Currently many internal and external (Plugin APIs) functions use line numbers (search results, filters, Columnizers, bookmarks, ...). This needs to be changed to use file positions instead of line numbers. This is a lot of work. It may also be needed to make some operations async.  

I am not sure if I really want to do this. But it would be cool because then there would be no need to use other tools when dealing with very large files.

## #2 - MultiFile
Currently the MultiFile feature cannot handle all kinds of rotating log files. E.g. Glassfish creates file names with kind of random numbers in it. LogExpert expects continuous index numbers (e.g. log1, log2, log3, ....). To handle such names I need to change the way of file handling in MultiFile mode. Currently LogExpert builds a name (by using the rules from configuration), then it checks if the file exists. Handling should be changed to create a sorted list of the directory and then apply the rules to check if there are more files to be loaded. 

## #3 - Replace the DataGridView
I use a Windows Form control called DataGridView for displaying the file content. This has some disadvantages:
* performance problems on large files (many lines)
* performance problems on long lines (that's the reason why I cut very long lines)
* line wrapping seems not to work (frequently requested feature)
* several internal bugs in the control itself
* Mono (the open .NET implementation, e.g. for Linux) does not support the virtual mode of the DataGridView. 

So I consider to write my own control or to find some custom control written by someone other.

## #4 - Replace the TabViewControl
The MDITabControl also has some disadvantages:
* written in VisualBasic. So it's hard for me to find/fix bugs - I hate this language ;)
* It has problems with higher DPI settings (frequently reported in the last months, as more and more people use better screens)
* It has no split/dock feature. View splitting would be very cool. Especially when using the time sync feature (syncing file scroll positions of 2 or more files by timestamps).

## #5 - SFTP plugin
I'd like to add more authentication methods (key exchange). And eventually a simple file browser.

## #6 - Settings export/import
Nearly complete but I had to time and mood to finish.

## #7 - Bugs....
There are some "most wanted" bugs :)
- Exception caused by toolbar ("A generic error occurred in GDI+")
- Some random "index out of range" exceptions. 
- Exceptions like "Requested Clipboard operation did not succeed"

BTW: I never got these exceptions. So it's hard to fix for me.

## #8 - Highlighting 
There are several feature requests about better highlight support. Some people want to highlight multiple lines between a range (e.g. opening and closing tags). When thinking about this I found that highlighting is very much the same like filtering (from perspective of the user). I actually wrote a test version with "unified" filtering/highlighting. But it was too slow and not usable. 

## #9 - Content analysis
I always dreamed of a cool algorithm that scans through a log file and find "suspect" locations. E.g. stack traces or error messages. In the source code there's still a (disabled) piece of experimental code. The idea was to let the user select a range of lines that contains the logs of an operation that runs OK. E.g. all log messages of a payment transaction. Then LogExpert searches for text blocks that looks similar to the selected one. If it finds anomalies in the blocks it will display this (assuming that something is wrong then). :)


I think number #1 is the hardest problem. It will also break all plugin APIs. #3 and #4 may be needed in the near future. 
