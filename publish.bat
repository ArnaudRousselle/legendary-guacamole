@echo off

rmdir /q /s publish

dotnet publish .\LegendaryGuacamole.ConsoleApp\LegendaryGuacamole.ConsoleApp.csproj -c Release -o .\publish
IF %ERRORLEVEL% NEQ 0 exit 1

dotnet publish .\LegendaryGuacamole.WebApi\LegendaryGuacamole.WebApi.csproj -c Release -o .\publish\webapi
IF %ERRORLEVEL% NEQ 0 exit 1
