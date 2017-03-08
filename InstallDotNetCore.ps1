mkdir -Force ".\scripts\obtain\" | Out-Null
Invoke-WebRequest "https://raw.githubusercontent.com/dotnet/cli/rel/1.0.0/scripts/obtain/dotnet-install.ps1" -OutFile ".\scripts\obtain\install.ps1"
$env:DOTNET_INSTALL_DIR = "$pwd\.dotnetcli"
.\scripts\obtain\install.ps1 -InstallDir "$pwd\.dotnetcli" -NoPath -version "1.0.1" 

# Installs this atm:
# -Channel "preview"
# -version "1.0.0-preview4-004107"
# https://dotnetcli.azureedge.net/dotnet/Sdk/1.0.0-preview4-004107/dotnet-dev-win-x64.1.0.0-preview4-004107.zip

$env:Path = "$env:DOTNET_INSTALL_DIR;$env:Path"

dotnet --info