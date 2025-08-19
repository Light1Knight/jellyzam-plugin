using MediaBrowser.Model.Plugins;

namespace Jellyzam.Configuration;

/// <summary>
/// Plugin configuration class for Jellyzam.
/// </summary>
public class PluginConfiguration : BasePluginConfiguration
{
    /// <summary>
    /// Gets or sets the Shazam API key.
    /// </summary>
    public string ShazamApiKey { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether to automatically identify unknown tracks.
    /// </summary>
    public bool AutoIdentifyUnknownTracks { get; set; } = false;

    /// <summary>
    /// Gets or sets the minimum confidence threshold for accepting Shazam results.
    /// </summary>
    public double ConfidenceThreshold { get; set; } = 0.8;

    /// <summary>
    /// Gets or sets a value indicating whether to overwrite existing metadata.
    /// </summary>
    public bool OverwriteExistingMetadata { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether to organize files into artist/album folders.
    /// </summary>
    public bool OrganizeFiles { get; set; } = true;

    /// <summary>
    /// Gets or sets the base path for organizing music files.
    /// </summary>
    public string OrganizedMusicPath { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether to run initial scan on first install.
    /// </summary>
    public bool RunInitialScan { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether the initial scan has been completed.
    /// </summary>
    public bool InitialScanCompleted { get; set; } = false;
}
