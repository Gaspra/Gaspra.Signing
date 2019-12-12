# Project Name to build and pack
$ProjectName = "Gaspra.Signing"

#Restore paket
dotnet tool restore
dotnet paket restore

#Build projects release
dotnet build ./src/$ProjectName.sln -c Release

#Pack as nuget
dotnet pack ./src/$ProjectName.sln -c Release -o ./.pack
