@echo off
cls

".\tools\nuget\NuGet.exe" "Install" "FAKE" "-OutputDirectory" "tools/packages" "-ExcludeVersion" "-Version" "4.13.0"
".\tools\nuget\NuGet.exe" "Install" "NUnit.Runners" "-OutputDirectory" "tools/packages" "-ExcludeVersion" 

SET MSBUILDDISABLENODEREUSE=1
".\tools\packages\Fake\tools\Fake.exe" build.fsx %*

exit /b %errorlevel%