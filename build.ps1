#Restore paket
dotnet tool restore
dotnet paket restore

#Build projects release
dotnet build ./src/Gaspra.Signing.sln -c Release

#Pack as nuget
dotnet pack ./src/Gaspra.Signing.sln -c Release -o ./.pack