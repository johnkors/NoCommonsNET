@echo off
cls
SET MSBUILDDISABLENODEREUSE=1
".\tools\packages\Fake\tools\Fake.exe" build.fsx %*

exit /b %errorlevel%