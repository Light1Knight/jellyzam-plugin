using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Jellyzam.Services;

/// <summary>
/// Service for interacting with the Shazam API.
/// </summary>
public class ShazamService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ShazamService> _logger;
    private const string BaseUrl = "https://shazam.p.rapidapi.com";

    /// <summary>
    /// Initializes a new instance of the <see cref="ShazamService"/> class.
    /// </summary>
    /// <param name="httpClient">The HTTP client.</param>
    /// <param name="logger">The logger.</param>
    public ShazamService(HttpClient httpClient, ILogger<ShazamService> logger)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Identifies a song from audio data.
    /// </summary>
    /// <param name="audioData">The audio data as a byte array.</param>
    /// <param name="apiKey">The Shazam API key.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The Shazam response containing identified tracks.</returns>
    public async Task<ShazamResponse?> IdentifySongAsync(byte[] audioData, string apiKey, CancellationToken cancellationToken = default)
    {
        if (audioData == null || audioData.Length == 0)
        {
            throw new ArgumentException("Audio data cannot be null or empty.", nameof(audioData));
        }

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new ArgumentException("API key cannot be null or empty.", nameof(apiKey));
        }

        try
        {
            _logger.LogInformation("Attempting to identify song using Shazam API");

            using var request = new HttpRequestMessage(HttpMethod.Post, $"{BaseUrl}/recognize");
            request.Headers.Add("X-RapidAPI-Key", apiKey);
            request.Headers.Add("X-RapidAPI-Host", "shazam.p.rapidapi.com");
            request.Content = new ByteArrayContent(audioData);
            request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

            using var response = await _httpClient.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Shazam API returned unsuccessful status code: {StatusCode}", response.StatusCode);
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogDebug("Error response: {ErrorContent}", errorContent);
                return null;
            }

            var jsonContent = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogDebug("Received response from Shazam API: {Response}", jsonContent);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var shazamResponse = JsonSerializer.Deserialize<ShazamResponse>(jsonContent, options);
            
            if (shazamResponse?.Matches?.Count > 0)
            {
                _logger.LogInformation("Successfully identified {MatchCount} potential matches", shazamResponse.Matches.Count);
            }
            else
            {
                _logger.LogInformation("No matches found for the provided audio");
            }

            return shazamResponse;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Network error occurred while calling Shazam API");
            throw;
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Error deserializing Shazam API response");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred while identifying song");
            throw;
        }
    }

    /// <summary>
    /// Gets track details by Shazam track ID.
    /// </summary>
    /// <param name="trackId">The Shazam track ID.</param>
    /// <param name="apiKey">The Shazam API key.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The track details.</returns>
    public async Task<ShazamTrack?> GetTrackDetailsAsync(string trackId, string apiKey, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(trackId))
        {
            throw new ArgumentException("Track ID cannot be null or empty.", nameof(trackId));
        }

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new ArgumentException("API key cannot be null or empty.", nameof(apiKey));
        }

        try
        {
            _logger.LogInformation("Fetching track details for ID: {TrackId}", trackId);

            using var request = new HttpRequestMessage(HttpMethod.Get, $"{BaseUrl}/tracks/details?track_id={trackId}");
            request.Headers.Add("X-RapidAPI-Key", apiKey);
            request.Headers.Add("X-RapidAPI-Host", "shazam.p.rapidapi.com");

            using var response = await _httpClient.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Shazam API returned unsuccessful status code: {StatusCode} for track ID: {TrackId}", response.StatusCode, trackId);
                return null;
            }

            var jsonContent = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogDebug("Received track details from Shazam API: {Response}", jsonContent);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var trackDetails = JsonSerializer.Deserialize<ShazamTrack>(jsonContent, options);
            
            if (trackDetails != null)
            {
                _logger.LogInformation("Successfully retrieved track details for: {Title} by {Artist}", trackDetails.Title, trackDetails.Artist);
            }

            return trackDetails;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching track details for ID: {TrackId}", trackId);
            throw;
        }
    }
}
