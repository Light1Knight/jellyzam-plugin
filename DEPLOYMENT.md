# Jellyzam Plugin Repository Deployment Guide

## Step-by-Step Setup for Plugin Repository

### 1. Create a GitHub Repository

1. Go to GitHub and create a new public repository named `jellyzam-repo`
2. Clone the repository to your local machine
3. Copy the `repository` folder contents to the root of your GitHub repository

### 2. Upload the Plugin Release

1. Go to your GitHub repository's "Releases" section
2. Click "Create a new release"
3. Tag version: `v1.0.0`
4. Release title: `Jellyzam v1.0.0`
5. Upload the `jellyzam-1.0.0.zip` file from `repository/releases/`
6. Publish the release

### 3. Update the Manifest

1. After publishing the release, copy the download URL for the ZIP file
2. Update `repository/manifest.json`:
   ```json
   "sourceUrl": "https://github.com/YOURUSERNAME/jellyzam-repo/releases/download/v1.0.0/jellyzam-1.0.0.zip"
   ```
3. Replace `YOURUSERNAME` with your actual GitHub username
4. Also update the `imageUrl` if you want to add a plugin icon

### 4. Commit and Push

```bash
git add .
git commit -m "Add Jellyzam plugin v1.0.0"
git push origin main
```

### 5. Get the Repository URL

Your plugin repository URL will be:
```
https://raw.githubusercontent.com/YOURUSERNAME/jellyzam-repo/main/manifest.json
```

## Adding to Jellyfin

### For End Users:

1. Open Jellyfin Admin Dashboard
2. Navigate to **Plugins** → **Repositories**
3. Click **Add Repository**
4. Fill in:
   - **Repository Name**: `Jellyzam Repository`
   - **Repository URL**: `https://raw.githubusercontent.com/YOURUSERNAME/jellyzam-repo/main/manifest.json`
5. Click **Save**
6. Go to **Plugins** → **Catalog**
7. Find **Jellyzam** and click **Install**
8. Restart Jellyfin

### Configuration:

1. Go to **Admin Dashboard** → **Plugins** → **Jellyzam**
2. Enter your RapidAPI Shazam API key: `d560e35d9amsh278aa105325ae5ap1798c...`
3. Configure settings as needed
4. Save configuration

## Testing Your Plugin

### Test the Repository:

1. Try adding your repository URL to a test Jellyfin instance
2. Verify the plugin appears in the catalog
3. Test installation and functionality

### Verify Package Contents:

The ZIP file should contain:
- `Jellyzam.dll` (main plugin)
- `Jellyzam.pdb` (debug symbols)
- `manifest.json` (plugin metadata)

## Updating the Plugin

When you release a new version:

1. Update the version number in `Jellyzam.csproj`
2. Run the packaging script: `powershell -ExecutionPolicy Bypass -File package.ps1 -Version "1.1.0"`
3. Create a new GitHub release with the new ZIP
4. Add the new version to the `versions` array in `manifest.json`
5. Commit and push the updated manifest

## Example Repository Structure

```
jellyzam-repo/
├── manifest.json          # Main repository manifest
├── meta.json             # Repository metadata
├── README.md             # Repository documentation
└── (releases handled via GitHub Releases)
```

## Security Notes

- Never commit API keys to the repository
- The manifest.json should only contain public information
- Users will configure their API keys through the Jellyfin admin interface

## Troubleshooting

### Common Issues:

1. **Plugin not appearing in catalog**: Check manifest.json syntax and URL accessibility
2. **Installation fails**: Verify ZIP file contents and checksum
3. **Plugin doesn't load**: Check Jellyfin logs for errors

### Validation Tools:

You can validate your manifest.json at: https://jsonlint.com/
