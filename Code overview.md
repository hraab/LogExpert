# Project structure
* **LogExpert** ist the main project. 
* **ColumnizerLib** is the API for columnizers and plugins (known as the Columnizer SDK)
* **UnitTest** contains some NUnit tests for the MultiFile feature
* All other projects are Columnizers

# Source
## GUI
* The main class is **Program**. It uses IPC to check for an already running instance. There's always only one instance, even if multiple main windows are shown! This allows communication between the main windows.
* The main window is **LogTabWindow**. The class has grown up by the time and contains lots of stuff that should be moved to specialized classes in a nice refactoring session... ;)
* For every log file an instance of **LogWindow** is created. This class should be refactored too...

## File handling
**LogFileReader** handles the file loading, tailing, buffer flushing, re-loading and MultiFile rollover stuff. It sends various events to LogWindow whenever a file has changed. A log file is read completely on startup into so-called _buffers_. A buffer contains a fixed amount of log lines and information about file positions etc. 

When the file is too large, a garbage collection thread removes the line content from the least used buffers. The buffers itself will be kept. If removed lines are needed again, the LogFileReader will re-read the content from disk.

The buffer size and count can be adjusted in LogExpert's settings. Larger buffer sizes or counts will result in much faster filter/search operations.

Multithread synchronization of buffer access is done by using ReaderWriterLocks. Please handle with care when doing changes here. It's easy to run into deadlocks or not synchronized accesses. Read locks are granted to multiple threads at the same time. Write locks are exclusive.

## Logging
* LogExpert writes a log file when running a **debug build**
* The log file is located in <MyDocuments>\LogExpert and is called **logfile.txt**
* Logger class is **Logger**
* There are 4 different log levels (error, warn, info, debug). Default level on startup is info. The current level can be changed in the Debug menu (only available in debug builds).

## Automatic Bugzilla posting
* In case you want to experiment with the Bugzilla bug entry creation: Please create an own account on LogExpert's bugzilla and assign the bugs to yourself while doing your tests.
* For changing the assignee you have to change the **BugSender** class