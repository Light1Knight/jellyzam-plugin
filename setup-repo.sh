#!/bin/bash
# Quick setup script for Jellyzam Plugin Repository

echo "🎵 Jellyzam Plugin Repository Setup"
echo "=================================="
echo ""

# Check if we're in the right directory
if [ ! -f "manifest.json" ]; then
    echo "❌ Error: manifest.json not found. Please run this script from the repository root."
    exit 1
fi

echo "📦 Checking repository structure..."

# Check required files
required_files=("repository/manifest.json" "repository/meta.json" "repository/README.md")
for file in "${required_files[@]}"; do
    if [ -f "$file" ]; then
        echo "✅ $file"
    else
        echo "❌ Missing: $file"
        exit 1
    fi
done

echo ""
echo "🚀 Repository is ready for deployment!"
echo ""
echo "Next steps:"
echo "1. Create a new GitHub repository (e.g., 'jellyzam-repo')"
echo "2. Upload the 'repository' folder contents to the repository root"
echo "3. Create a GitHub release and upload the ZIP file from repository/releases/"
echo "4. Update the sourceUrl in manifest.json with the GitHub release URL"
echo "5. Your repository URL will be:"
echo "   https://raw.githubusercontent.com/YOURUSERNAME/jellyzam-repo/main/manifest.json"
echo ""
echo "📋 For detailed instructions, see DEPLOYMENT.md"
