using MediaBrowser.Controller.Entities.Audio;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace Jellyzam.Services;

/// <summary>
/// Service for organizing music files into structured directories.
/// </summary>
public class FileOrganizationService
{
    private readonly ILogger<FileOrganizationService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileOrganizationService"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public FileOrganizationService(ILogger<FileOrganizationService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Organizes a music file into the artist/album folder structure.
    /// </summary>
    /// <param name="audioItem">The audio item to organize.</param>
    /// <param name="basePath">The base path for organized music.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The new file path if moved, or the original path if not moved.</returns>
    public async Task<string> OrganizeFileAsync(Audio audioItem, string basePath, CancellationToken cancellationToken = default)
    {
        if (audioItem == null)
        {
            throw new ArgumentNullException(nameof(audioItem));
        }

        if (string.IsNullOrWhiteSpace(basePath))
        {
            basePath = Path.GetDirectoryName(audioItem.Path) ?? "";
        }

        try
        {
            var artist = SanitizeFileName(audioItem.Artists?.FirstOrDefault() ?? "Unknown Artist");
            var album = SanitizeFileName(audioItem.Album ?? "Unknown Album");
            var title = SanitizeFileName(audioItem.Name ?? Path.GetFileNameWithoutExtension(audioItem.Path));
            var extension = Path.GetExtension(audioItem.Path);

            // Create the organized path structure
            var artistFolder = Path.Combine(basePath, artist);
            var albumFolder = Path.Combine(artistFolder, album);
            var fileName = $"{title}{extension}";
            var newFilePath = Path.Combine(albumFolder, fileName);

            // Skip if file is already in the correct location
            if (string.Equals(audioItem.Path, newFilePath, StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogDebug("File already organized: {Path}", audioItem.Path);
                return audioItem.Path;
            }

            // Create directories if they don't exist
            Directory.CreateDirectory(albumFolder);

            // Handle file name conflicts
            newFilePath = GetUniqueFilePath(newFilePath);

            // Move the file
            _logger.LogInformation("Moving file from {OldPath} to {NewPath}", audioItem.Path, newFilePath);
            File.Move(audioItem.Path, newFilePath);

            return newFilePath;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error organizing file: {Path}", audioItem.Path);
            return audioItem.Path; // Return original path if organization fails
        }
    }

    /// <summary>
    /// Organizes multiple files in batches.
    /// </summary>
    /// <param name="audioItems">The audio items to organize.</param>
    /// <param name="basePath">The base path for organized music.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A dictionary mapping original paths to new paths.</returns>
    public async Task<Dictionary<string, string>> OrganizeFilesAsync(
        IEnumerable<Audio> audioItems, 
        string basePath, 
        CancellationToken cancellationToken = default)
    {
        var results = new Dictionary<string, string>();
        var items = audioItems.ToList();

        _logger.LogInformation("Starting organization of {Count} files", items.Count);

        int processed = 0;
        foreach (var item in items)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                break;
            }

            processed++;
            _logger.LogInformation("Organizing file {Current}/{Total}: {Name}", processed, items.Count, item.Name);

            try
            {
                var newPath = await OrganizeFileAsync(item, basePath, cancellationToken);
                results[item.Path] = newPath;

                // Small delay to avoid overwhelming the file system
                await Task.Delay(100, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to organize file: {Path}", item.Path);
                results[item.Path] = item.Path; // Keep original path on failure
            }
        }

        _logger.LogInformation("File organization completed. Processed {Processed}/{Total} files", processed, items.Count);
        return results;
    }

    /// <summary>
    /// Sanitizes a filename by removing invalid characters.
    /// </summary>
    /// <param name="fileName">The filename to sanitize.</param>
    /// <returns>The sanitized filename.</returns>
    private static string SanitizeFileName(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            return "Unknown";
        }

        // Remove invalid characters for file/folder names
        var invalidChars = Path.GetInvalidFileNameChars().Concat(Path.GetInvalidPathChars()).ToArray();
        var sanitized = invalidChars.Aggregate(fileName, (current, c) => current.Replace(c, '_'));

        // Remove multiple consecutive spaces and underscores
        sanitized = Regex.Replace(sanitized, @"[\s_]+", " ").Trim();

        // Limit length to avoid path too long errors
        if (sanitized.Length > 100)
        {
            sanitized = sanitized.Substring(0, 100).Trim();
        }

        return string.IsNullOrWhiteSpace(sanitized) ? "Unknown" : sanitized;
    }

    /// <summary>
    /// Gets a unique file path by appending a number if the file already exists.
    /// </summary>
    /// <param name="filePath">The desired file path.</param>
    /// <returns>A unique file path.</returns>
    private static string GetUniqueFilePath(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return filePath;
        }

        var directory = Path.GetDirectoryName(filePath) ?? "";
        var fileName = Path.GetFileNameWithoutExtension(filePath);
        var extension = Path.GetExtension(filePath);

        int counter = 1;
        string newPath;
        do
        {
            newPath = Path.Combine(directory, $"{fileName} ({counter}){extension}");
            counter++;
        } while (File.Exists(newPath));

        return newPath;
    }

    /// <summary>
    /// Cleans up empty directories after file organization.
    /// </summary>
    /// <param name="basePath">The base path to clean up.</param>
    public void CleanupEmptyDirectories(string basePath)
    {
        if (!Directory.Exists(basePath))
        {
            return;
        }

        try
        {
            foreach (var directory in Directory.GetDirectories(basePath, "*", SearchOption.AllDirectories)
                .OrderByDescending(d => d.Length)) // Start with deepest directories
            {
                try
                {
                    if (!Directory.EnumerateFileSystemEntries(directory).Any())
                    {
                        _logger.LogDebug("Removing empty directory: {Directory}", directory);
                        Directory.Delete(directory);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Could not remove directory: {Directory}", directory);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during directory cleanup: {BasePath}", basePath);
        }
    }
}
