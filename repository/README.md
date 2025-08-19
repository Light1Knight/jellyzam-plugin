# Jellyzam Plugin Repository

This repository contains the Jellyfin plugin distribution for **Jellyzam** - a music identification plugin using Shazam API.

## Installation

### Method 1: Plugin Repository (Recommended)

1. Open Jellyfin Admin Dashboard
2. Go to **Plugins** → **Repositories**
3. Click **Add Repository**
4. Enter the following details:
   - **Repository Name**: `Jellyzam Repository`
   - **Repository URL**: `https://raw.githubusercontent.com/yourusername/jellyzam-repo/main/manifest.json`
5. Click **Save**
6. Go to **Plugins** → **Catalog**
7. Find **Jellyzam** in the list and click **Install**
8. Restart Jellyfin

### Method 2: Manual Installation

1. Download the latest release from [releases](https://github.com/yourusername/jellyzam-repo/releases)
2. Extract the zip file to your Jellyfin plugins directory:
   - **Windows**: `%ProgramData%\Jellyfin\Server\plugins\Jellyzam\`
   - **Linux**: `/var/lib/jellyfin/plugins/Jellyzam/`
   - **Docker**: `/config/plugins/Jellyzam/`
3. Restart Jellyfin

## Configuration

After installation:

1. Go to **Admin Dashboard** → **Plugins** → **Jellyzam**
2. Enter your RapidAPI Shazam API key
3. Configure your preferences:
   - **Auto Identify Unknown Tracks**: Automatically process new tracks
   - **Confidence Threshold**: Minimum confidence for accepting results (0.0-1.0)
   - **Overwrite Existing Metadata**: Whether to replace existing track info

## Getting a Shazam API Key

1. Visit [RapidAPI Shazam](https://rapidapi.com/apidojo/api/shazam/)
2. Sign up for a free account
3. Subscribe to the Shazam API (free tier available)
4. Copy your API key to the plugin configuration

## Features

- 🎵 **Automatic Music Identification** using Shazam's audio fingerprinting
- 📦 **Batch Processing** for multiple unknown tracks
- ⚙️ **Configurable Settings** with confidence thresholds
- 🔒 **Metadata Protection** - choose what to overwrite
- 🌐 **Web Configuration** integrated with Jellyfin admin
- 📝 **Comprehensive Logging** for troubleshooting

## Supported Formats

- MP3, FLAC, M4A/AAC, OGG, WAV
- Any format supported by Jellyfin

## Requirements

- Jellyfin Server 10.8.0 or later
- Active internet connection
- RapidAPI Shazam API key

## Support

- [Plugin Documentation](https://github.com/yourusername/jellyzam)
- [Report Issues](https://github.com/yourusername/jellyzam/issues)
- [Jellyfin Community](https://discord.gg/zHBxVSXdBV)

## Version History

### v1.0.0 (2025-08-19)
- Initial release
- Shazam API integration
- Automatic music identification
- Web configuration interface
- Batch processing support
