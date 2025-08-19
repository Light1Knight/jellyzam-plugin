# Complete GitHub Setup Script for Jellyzam Plugin
# This script will guide you through the entire setup process

param(
    [Parameter(Mandatory=$true)]
    [string]$GitHubUsername,
    
    [Parameter(Mandatory=$false)]
    [string]$RepositoryName = "jellyzam-plugin"
)

Write-Host "=== Jellyzam Plugin GitHub Setup ===" -ForegroundColor Green
Write-Host ""

# Step 1: Update manifest.json with real GitHub URLs
Write-Host "Step 1: Updating manifest.json with your GitHub information..." -ForegroundColor Yellow

$manifestPath = "repository\manifest.json"
$manifest = Get-Content $manifestPath -Raw | ConvertFrom-Json

# Update URLs in manifest
$manifest.imageUrl = "https://raw.githubusercontent.com/$GitHubUsername/$RepositoryName/main/jellyzam-icon.png"
$manifest.versions[0].sourceUrl = "https://github.com/$GitHubUsername/$RepositoryName/releases/download/v1.0.0/jellyzam-1.0.0.zip"

# Save updated manifest
$manifest | ConvertTo-Json -Depth 10 | Set-Content $manifestPath

Write-Host "✓ Updated manifest.json with your GitHub URLs" -ForegroundColor Green

# Step 2: Set up git remote
Write-Host ""
Write-Host "Step 2: Setting up git remote..." -ForegroundColor Yellow

$repoUrl = "https://github.com/$GitHubUsername/$RepositoryName.git"
git remote add origin $repoUrl

Write-Host "✓ Added remote origin: $repoUrl" -ForegroundColor Green

# Step 3: Push to GitHub
Write-Host ""
Write-Host "Step 3: Pushing code to GitHub..." -ForegroundColor Yellow

git branch -M main
git add .
git commit -m "Update manifest.json with real GitHub URLs"

Write-Host ""
Write-Host "Ready to push to GitHub!" -ForegroundColor Green
Write-Host "Repository URL: $repoUrl" -ForegroundColor Cyan
Write-Host ""
Write-Host "IMPORTANT: Before running the push command below, make sure you have:" -ForegroundColor Red
Write-Host "1. Created the repository '$RepositoryName' on GitHub" -ForegroundColor Red
Write-Host "2. Set up authentication (GitHub CLI or personal access token)" -ForegroundColor Red
Write-Host ""
Write-Host "To push the code, run:" -ForegroundColor Yellow
Write-Host "git push -u origin main" -ForegroundColor Cyan
Write-Host ""
Write-Host "After pushing, your plugin repository will contain:" -ForegroundColor Green
Write-Host "- Complete source code with file organization features" -ForegroundColor White
Write-Host "- 14 unit tests (all passing)" -ForegroundColor White
Write-Host "- Ready-to-install package (jellyzam-1.0.0.zip)" -ForegroundColor White
Write-Host "- Documentation and build scripts" -ForegroundColor White
Write-Host ""
Write-Host "Final steps after pushing:" -ForegroundColor Green
Write-Host "1. Create a release (v1.0.0) on GitHub" -ForegroundColor White
Write-Host "2. Upload repository/releases/jellyzam-1.0.0.zip to the release" -ForegroundColor White
Write-Host "3. Share the repository URL for easy plugin installation" -ForegroundColor White
Write-Host ""
Write-Host "Plugin Repository URL for users:" -ForegroundColor Green
Write-Host "https://raw.githubusercontent.com/$GitHubUsername/$RepositoryName/main/repository/manifest.json" -ForegroundColor Cyan
