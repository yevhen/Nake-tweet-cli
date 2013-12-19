@ECHO OFF
SET DIR=%~dp0%

IF "%~1"=="install" GOTO install
%DIR%\Bin\Nake.exe -f %DIR%\Source\Tweet.csx -d %DIR% -r tweet %*
GOTO end

:install
ECHO Installing dependencies ..."
%DIR%\Bin\NuGet.exe install %DIR%\Packages\Packages.config -o %DIR%\Packages

:end