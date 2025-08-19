@echo off
echo =================================
echo Jellyzam GitHub Quick Setup
echo =================================
echo.

set /p username="Enter your GitHub username: "
set reponame=jellyzam-plugin

echo.
echo Setting up repository for: %username%/%reponame%
echo.

REM Update manifest.json with real URLs
powershell -Command "(Get-Content 'repository\manifest.json' -Raw | ConvertFrom-Json) | ForEach-Object { $_.imageUrl = 'https://raw.githubusercontent.com/%username%/%reponame%/main/jellyzam-icon.png'; $_.versions[0].sourceUrl = 'https://github.com/%username%/%reponame%/releases/download/v1.0.0/jellyzam-1.0.0.zip'; $_ } | ConvertTo-Json -Depth 10 | Set-Content 'repository\manifest.json'"

echo Updated manifest.json
git add .
git commit -m "Update manifest URLs for GitHub"

REM Add remote
git remote add origin https://github.com/%username%/%reponame%.git
git branch -M main

echo.
echo ================================
echo NEXT STEPS:
echo ================================
echo 1. Go to https://github.com and create a new repository named: %reponame%
echo 2. Make it PUBLIC (important for plugin installation)
echo 3. Do NOT initialize with README
echo 4. After creating, run: git push -u origin main
echo 5. Create a release v1.0.0 and upload repository\releases\jellyzam-1.0.0.zip
echo.
echo Your plugin repository URL will be:
echo https://raw.githubusercontent.com/%username%/%reponame%/main/repository/manifest.json
echo.
echo Users can add this URL to their Jellyfin plugin repositories!
echo.
pause
