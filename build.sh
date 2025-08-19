#!/bin/bash

echo "Building Jellyzam Plugin..."

# Check if .NET SDK is installed
if ! command -v dotnet &> /dev/null; then
    echo "Error: .NET SDK is not installed."
    echo "Please download and install .NET 8.0 SDK from: https://aka.ms/dotnet/download"
    exit 1
fi

echo "Restoring NuGet packages..."
dotnet restore
if [ $? -ne 0 ]; then
    echo "Error: Failed to restore packages"
    exit 1
fi

echo "Building project..."
dotnet build --configuration Release
if [ $? -ne 0 ]; then
    echo "Error: Build failed"
    exit 1
fi

echo ""
echo "Build completed successfully!"
echo ""
echo "To install the plugin:"
echo "1. Copy the contents of bin/Release/net8.0/ to your Jellyfin plugins directory"
echo "2. Restart Jellyfin"
echo "3. Configure the plugin in Admin Dashboard -> Plugins -> Jellyzam"
echo ""
