using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Jellyzam.Services;
using System.Text.Json;

namespace Jellyzam.Tests.Services;

public class ShazamServiceTests
{
    private readonly Mock<ILogger<ShazamService>> _mockLogger;
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private readonly HttpClient _httpClient;
    private readonly ShazamService _shazamService;

    public ShazamServiceTests()
    {
        _mockLogger = new Mock<ILogger<ShazamService>>();
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_mockHttpMessageHandler.Object);
        _shazamService = new ShazamService(_httpClient, _mockLogger.Object);
    }

    [Fact]
    public async Task IdentifySongAsync_WithNullAudioData_ThrowsArgumentException()
    {
        // Arrange
        byte[]? audioData = null;
        string apiKey = "test-api-key";

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => 
            _shazamService.IdentifySongAsync(audioData!, apiKey));
    }

    [Fact]
    public async Task IdentifySongAsync_WithEmptyAudioData_ThrowsArgumentException()
    {
        // Arrange
        byte[] audioData = Array.Empty<byte>();
        string apiKey = "test-api-key";

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => 
            _shazamService.IdentifySongAsync(audioData, apiKey));
    }

    [Fact]
    public async Task IdentifySongAsync_WithNullApiKey_ThrowsArgumentException()
    {
        // Arrange
        byte[] audioData = new byte[] { 1, 2, 3, 4, 5 };
        string? apiKey = null;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => 
            _shazamService.IdentifySongAsync(audioData, apiKey!));
    }

    [Fact]
    public async Task IdentifySongAsync_WithEmptyApiKey_ThrowsArgumentException()
    {
        // Arrange
        byte[] audioData = new byte[] { 1, 2, 3, 4, 5 };
        string apiKey = "";

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => 
            _shazamService.IdentifySongAsync(audioData, apiKey));
    }

    [Fact]
    public async Task GetTrackDetailsAsync_WithNullTrackId_ThrowsArgumentException()
    {
        // Arrange
        string? trackId = null;
        string apiKey = "test-api-key";

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => 
            _shazamService.GetTrackDetailsAsync(trackId!, apiKey));
    }

    [Fact]
    public async Task GetTrackDetailsAsync_WithEmptyTrackId_ThrowsArgumentException()
    {
        // Arrange
        string trackId = "";
        string apiKey = "test-api-key";

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => 
            _shazamService.GetTrackDetailsAsync(trackId, apiKey));
    }
}
