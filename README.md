# Automatic-Timetracking
### Senior Design Project: Spring 2020

This application detects Windows processes and captures logs as well as updates the UI in real time.

This is an enhancement of a previous iteration of this project where this group sucessfully:
- removed Clockify API
  - therefore removing the need for this application to require login/password credentials
- outputted the logs as CSV
- outputted the logs as JSON
  - where the previous iteration only had these processes captured in the UI
- captured the window of each idle process
  - where the previous iteration captured the entire desktop of each idle process
  
  
We utilized Visual Studio and C# to work on this project, and it works locally if you pull this repo and run the "start without debugging" command and are delivering a runnable .msi to Fellows Consulting Group.

### Senior Design Project: Fall 2020
-MongoDB Data Storage
	Uses Johan Terblanche's MongoDB Account and a database provided to store files.

To create MSI make sure that the Microsoft Visual Studio Installer Projects extension is installed.

The thread safety issues that made it so that it would only run in "start without debugging" have been addressed.