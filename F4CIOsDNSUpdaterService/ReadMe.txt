F4CIOsDNSUpdater 1.1

What does it do?
This tool runs in background and periodicaly calls (pings) URL. Log file and LastResponse.htm are being written. Initially I wrote this tool to periodically ping DNS server and inform it about my new dynamic IP (DNS client) but it can be used to keep awake your website or to periodically trigger some operation.

Note: If you need mail alert in case when website or server is down or if server is low on disk space use Periodic Health Checker 
(http://www.f4cio.com/periodic-website-health-checker-pinger)

How does it do it?
Program have two parts:
1. F4CIOsDNSUpdater <-- A windows service. 
   It periodicaly reads F4CIOsDNSUpdater.ini
   First line contains interval in miliseconds and second line contains Uri (with http:// part!).
   Every time between given interval it makes http request to url specified in second line.
2. F4CIOsDNSUpdaterManager <-- Windows tray manager application for probing and configuring .ini file.
   Note that changes are reflected by F4CIOsDNSUpdaterService on next reading from .ini file or on next restart of F4CIOsDNSUpdaterService.
   Since F4CIOsDNSUpdater service runs in background after every system start you don't need manager application once you configure it.

System requirements?
-Windows OS
-Microsoft .Net Framework 2.0 (installed by default on Windows 7 and later OS-es)

How to install?
1. Extract all files to desired folder.
2. Run InstallService.bat
3. Run F4CIOsDNSManager.exe
4. In F4CIOsDNSManager set interval and uri and start service.

More info?
http://www.f4cio.com/periodic-url-pinger  