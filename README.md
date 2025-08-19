# Jellyzam - Jellyfin Music Identification Plugin

Jellyzam is a Jellyfin plugin that uses the Shazam API to automatically identify and update metadata for music files in your library.

## Features

- **Automatic Music Identification**: Uses Shazam's powerful audio fingerprinting technology to identify unknown tracks
- **File Organization**: Automatically organizes your music library into a structured folder hierarchy (Artist/Album/Song)
- **Initial Library Scan**: Option to scan and organize your entire music library on first installation
- **Batch Processing**: Process multiple unknown tracks automatically
- **Configurable Confidence Threshold**: Set minimum confidence levels for accepting identification results
- **Metadata Protection**: Choose whether to overwrite existing metadata or only fill in missing information
- **Flexible Organization**: Choose whether to enable file organization and specify the target directory
- **Web Configuration**: Easy-to-use web interface for plugin configuration

## Prerequisites

- .NET 8.0 SDK or later
- Jellyfin Server 10.8.0 or later
- RapidAPI account for Shazam API access

## Installation

### Option 1: Plugin Repository (Recommended)
1. Open Jellyfin Admin Dashboard
2. Navigate to **Plugins** → **Repositories**
3. Click **Add Repository**
4. Enter the repository details:
   - **Repository Name**: `Jellyzam Repository`
   - **Repository URL**: `https://raw.githubusercontent.com/YOURUSERNAME/jellyzam-repo/main/manifest.json`
   - *(Replace YOURUSERNAME with the actual GitHub username)*
5. Click **Save**
6. Go to **Plugins** → **Catalog**
7. Find **Jellyzam** in the list and click **Install**
8. Restart Jellyfin

### Option 2: From Release (Manual)
1. Download the latest release from the [releases page](https://github.com/yourusername/jellyzam-repo/releases)
2. Extract the plugin files to your Jellyfin plugins directory:
   - Windows: `%ProgramData%\Jellyfin\Server\plugins\Jellyzam`
   - Linux: `/var/lib/jellyfin/plugins/Jellyzam`
   - Docker: `/config/plugins/Jellyzam`
3. Restart your Jellyfin server
4. Navigate to Admin Dashboard → Plugins → Jellyzam to configure the plugin

### Option 3: Build from Source
1. Ensure .NET 8.0 SDK is installed: [Download .NET](https://aka.ms/dotnet/download)
2. Clone or download this repository
3. Run the build script:
   - Windows: Double-click `build.bat`
   - Linux/macOS: Run `chmod +x build.sh && ./build.sh`
4. Copy the contents of `bin/Release/net8.0/` to your Jellyfin plugins directory
5. Restart Jellyfin

## Installation

1. Download the latest release from the [releases page](https://github.com/yourusername/jellyzam/releases)
2. Extract the plugin files to your Jellyfin plugins directory:
   - Windows: `%ProgramData%\Jellyfin\Server\plugins\Jellyzam`
   - Linux: `/var/lib/jellyfin/plugins/Jellyzam`
   - Docker: `/config/plugins/Jellyzam`
3. Restart your Jellyfin server
4. Navigate to Admin Dashboard → Plugins → Jellyzam to configure the plugin

## Configuration

### Required Settings

- **Shazam API Key**: You'll need a RapidAPI key for the Shazam API
  1. Visit [RapidAPI Shazam](https://rapidapi.com/apidojo/api/shazam/)
  2. Subscribe to the API (free tier available)
  3. Copy your API key to the plugin configuration

### Optional Settings

- **Auto Identify Unknown Tracks**: Automatically process tracks with missing metadata
- **Confidence Threshold**: Minimum confidence level (0.0-1.0) for accepting results
- **Overwrite Existing Metadata**: Whether to replace existing track information
- **Organize Files**: Enable automatic file organization into Artist/Album/Song structure
- **Organized Music Path**: Target directory for organized files (leave empty to use current library path)
- **Run Initial Scan**: Perform a complete library scan and organization on first run
- **Initial Scan Completed**: Shows whether the initial scan has been completed

## Usage

### Manual Identification

1. Navigate to your music library in Jellyfin
2. Select tracks you want to identify
3. The plugin will attempt to identify and update metadata automatically

### Automatic Processing

Enable "Auto Identify Unknown Tracks" in the plugin configuration to automatically process new tracks as they're added to your library.

## How It Works

1. **Audio Sampling**: The plugin extracts a small audio sample from each track
2. **Shazam Analysis**: The sample is sent to Shazam's API for identification
3. **Confidence Evaluation**: Results are evaluated against your confidence threshold
4. **Metadata Update**: Matching tracks have their metadata updated in Jellyfin

## Supported Audio Formats

The plugin works with any audio format supported by Jellyfin, including:
- MP3
- FLAC
- M4A/AAC
- OGG
- WAV

## Limitations

- Requires an active internet connection
- Limited by Shazam API rate limits and quotas
- Identification accuracy depends on audio quality and Shazam's database coverage
- Classical music and very obscure tracks may not be identified

## Privacy

- Audio samples are sent to Shazam's servers for identification
- No audio data is stored permanently by the plugin
- Only metadata is updated in your Jellyfin library

## Development

### Building from Source

1. Clone the repository
2. Ensure you have .NET 8.0 SDK installed
3. Build the project:
   ```bash
   dotnet build
   ```

### Contributing

Contributions are welcome! Please feel free to submit pull requests or open issues for bugs and feature requests.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

- [Jellyfin](https://jellyfin.org/) for the amazing media server platform
- [Shazam](https://www.shazam.com/) for their music identification API
- The Jellyfin plugin development community

## Support

If you encounter any issues or have questions:

1. Check the [issues page](https://github.com/yourusername/jellyzam/issues) for existing solutions
2. Create a new issue with detailed information about your problem
3. Join the [Jellyfin Discord](https://discord.gg/zHBxVSXdBV) for community support

## Changelog

### Version 1.0.0
- Initial release
- Basic Shazam integration
- Web configuration interface
- Batch processing support
