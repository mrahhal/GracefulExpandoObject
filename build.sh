dotnet --info
dotnet restore
dotnet test test/GracefulExpandoObject.Tests -f netcoreapp1.1
dotnet test test/GracefulExpandoObject.Json.Tests -f netcoreapp1.1
