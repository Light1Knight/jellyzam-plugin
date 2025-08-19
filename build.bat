@echo off
echo Building Jellyzam Plugin...

REM Check if .NET SDK is installed
dotnet --version >nul 2>&1
if %errorlevel% neq 0 (
    echo Error: .NET SDK is not installed.
    echo Please download and install .NET 8.0 SDK from: https://aka.ms/dotnet/download
    echo After installation, restart your command prompt and run this script again.
    pause
    exit /b 1
)

echo Restoring NuGet packages...
dotnet restore
if %errorlevel% neq 0 (
    echo Error: Failed to restore packages
    pause
    exit /b 1
)

echo Building project...
dotnet build --configuration Release
if %errorlevel% neq 0 (
    echo Error: Build failed
    pause
    exit /b 1
)

echo.
echo Build completed successfully!
echo.
echo To install the plugin:
echo 1. Copy the contents of bin\Release\net8.0\ to your Jellyfin plugins directory
echo 2. Restart Jellyfin
echo 3. Configure the plugin in Admin Dashboard -^> Plugins -^> Jellyzam
echo.
pause
