# Dexter

Dexter is a command line executable that will translate the Unix Timestamp in a given log file to a more readable one. Instead of overwriting over the provided log file Dexter will create a new file and write the previous text to the new log file.

## Example

Calling the following command from command line will cause Dexter to read from "SampleLog.txt" and create and write to "Samplelogout.txt"
> Dexter.exe "-i:SampleLog.txt" "-o:Samplelogout.txt"