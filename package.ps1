# Jellyzam Plugin Packaging Script
param(
    [string]$Version = "1.0.0"
)

Write-Host "Packaging Jellyzam Plugin v$Version..." -ForegroundColor Green

# Clean and build
Write-Host "Building plugin..." -ForegroundColor Yellow
dotnet clean Jellyzam.csproj --configuration Release
dotnet build Jellyzam.csproj --configuration Release

if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed!" -ForegroundColor Red
    exit 1
}

# Create package directory
$PackageDir = "repository\releases\v$Version"
$ZipName = "jellyzam-$Version.zip"

if (Test-Path $PackageDir) {
    Remove-Item $PackageDir -Recurse -Force
}
New-Item -ItemType Directory -Path $PackageDir -Force | Out-Null

# Copy plugin files
Write-Host "Copying plugin files..." -ForegroundColor Yellow
$SourceDir = "bin\Release\net8.0"
Copy-Item "$SourceDir\Jellyzam.dll" -Destination $PackageDir
Copy-Item "$SourceDir\Jellyzam.pdb" -Destination $PackageDir
Copy-Item "manifest.json" -Destination $PackageDir

# Copy dependencies (if any additional ones are needed)
$Dependencies = @(
    "Newtonsoft.Json.dll"
)

foreach ($dep in $Dependencies) {
    $depPath = "$SourceDir\$dep"
    if (Test-Path $depPath) {
        Copy-Item $depPath -Destination $PackageDir
        Write-Host "Copied dependency: $dep" -ForegroundColor Gray
    }
}

# Create ZIP file
Write-Host "Creating ZIP package..." -ForegroundColor Yellow
$ZipPath = "repository\releases\$ZipName"
if (Test-Path $ZipPath) {
    Remove-Item $ZipPath -Force
}

Add-Type -AssemblyName System.IO.Compression.FileSystem
[System.IO.Compression.ZipFile]::CreateFromDirectory($PackageDir, $ZipPath)

# Calculate checksum
Write-Host "Calculating checksum..." -ForegroundColor Yellow
$Checksum = Get-FileHash $ZipPath -Algorithm MD5 | Select-Object -ExpandProperty Hash

# Update manifest
Write-Host "Updating manifest..." -ForegroundColor Yellow
$ManifestPath = "repository\manifest.json"
$Manifest = Get-Content $ManifestPath | ConvertFrom-Json

# Update the version info
$VersionInfo = $Manifest[0].versions | Where-Object { $_.version -eq "$Version.0" }
if ($VersionInfo) {
    $VersionInfo.checksum = $Checksum.ToLower()
    $VersionInfo.timestamp = (Get-Date).ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ")
} else {
    Write-Host "Version $Version.0 not found in manifest!" -ForegroundColor Red
    exit 1
}

$Manifest | ConvertTo-Json -Depth 10 | Set-Content $ManifestPath

Write-Host "`nPackaging complete!" -ForegroundColor Green
Write-Host "Package: $ZipPath" -ForegroundColor Cyan
Write-Host "Checksum: $Checksum" -ForegroundColor Cyan
Write-Host "Size: $((Get-Item $ZipPath).Length) bytes" -ForegroundColor Cyan

Write-Host "`nNext steps:" -ForegroundColor Yellow
Write-Host "1. Upload the ZIP file to your GitHub releases" -ForegroundColor White
Write-Host "2. Update the sourceUrl in manifest.json with the actual GitHub release URL" -ForegroundColor White
Write-Host "3. Commit and push the updated manifest.json to your repository" -ForegroundColor White
Write-Host "4. Use the raw GitHub URL to manifest.json as your plugin repository URL" -ForegroundColor White
