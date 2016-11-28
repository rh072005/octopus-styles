@echo off

SET DIR=%~dp0%

if '%1'=='build' goto build
if '%1'=='version' goto version
if '%1'=='test' goto test
if '%1'=='package' goto package

@PowerShell -ExecutionPolicy unrestricted -Command "[System.Threading.Thread]::CurrentThread.CurrentCulture = ''; [System.Threading.Thread]::CurrentThread.CurrentUICulture = '';& '%DIR%build.ps1' %*"
goto :end

:build
@PowerShell -ExecutionPolicy unrestricted -Command "[System.Threading.Thread]::CurrentThread.CurrentCulture = ''; [System.Threading.Thread]::CurrentThread.CurrentUICulture = '';& '%DIR%build.ps1' -target build"
goto :end

:version
@PowerShell -ExecutionPolicy unrestricted -Command "[System.Threading.Thread]::CurrentThread.CurrentCulture = ''; [System.Threading.Thread]::CurrentThread.CurrentUICulture = '';& '%DIR%build.ps1' -Target version -VersionType=%2"
goto :end

:test
@PowerShell -ExecutionPolicy unrestricted -Command "[System.Threading.Thread]::CurrentThread.CurrentCulture = ''; [System.Threading.Thread]::CurrentThread.CurrentUICulture = '';& '%DIR%build.ps1' -target test"
goto :end

:package
@PowerShell -ExecutionPolicy unrestricted -Command "[System.Threading.Thread]::CurrentThread.CurrentCulture = ''; [System.Threading.Thread]::CurrentThread.CurrentUICulture = '';& '%DIR%build.ps1' -target package"
goto :end

:end
pause