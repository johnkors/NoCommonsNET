powershell .\InstallDotNetCore.ps1

.\.dotnetcli\dotnet restore NoCommons\NoCommons.csproj
.\.dotnetcli\dotnet restore NoCommons.Tests\NoCommons.Tests.csproj

.\.dotnetcli\dotnet build NoCommons\NoCommons.csproj
.\.dotnetcli\dotnet build NoCommons.Tests\NoCommons.Tests.csproj

.\.dotnetcli\dotnet test NoCommons.Tests\NoCommons.Tests.csproj

.\.dotnetcli\dotnet pack NoCommons\NoCommons.csproj -c Release -o build 
