dotnet restore NoCommons
dotnet restore NoCommons.Tests

dotnet build NoCommons
dotnet build NoCommons.Tests

dotnet test NoCommons.Tests

dotnet pack NoCommons -c Release -o build --no-build
