REM This file looks for .PDF Extension in IE cache and copy it during the test execution
set arg1=%1
dir "%appdata%\..\Local\Microsoft\Windows\INetCache\*.PDF" /S/B > temp.txt
echo %ARG1%
set /p VAR=<temp.txt
copy %VAR% "%arg1%"
del temp.txt