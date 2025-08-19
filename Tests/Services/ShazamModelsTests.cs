using Xunit;
using Jellyzam.Services;
using System.Text.Json;

namespace Jellyzam.Tests.Services;

public class ShazamModelsTests
{
    [Fact]
    public void ShazamResponse_Deserialization_WorksCorrectly()
    {
        // Arrange
        var json = """
        {
            "matches": [
                {
                    "id": "123456789",
                    "offset": 0.0,
                    "channel": "stereo",
                    "frequencyskew": 0.1,
                    "timeskew": 0.2,
                    "track": {
                        "key": "track123",
                        "title": "Test Song",
                        "subtitle": "Test Subtitle",
                        "artist": "Test Artist",
                        "album": "Test Album"
                    }
                }
            ]
        }
        """;

        // Act
        var response = JsonSerializer.Deserialize<ShazamResponse>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        // Assert
        Assert.NotNull(response);
        Assert.Single(response.Matches);
        
        var match = response.Matches[0];
        Assert.Equal("123456789", match.Id);
        Assert.Equal(0.0, match.Offset);
        Assert.Equal("stereo", match.Channel);
        Assert.Equal(0.1, match.FrequencySkew);
        Assert.Equal(0.2, match.TimeSkew);
        
        var track = match.Track;
        Assert.Equal("track123", track.Key);
        Assert.Equal("Test Song", track.Title);
        Assert.Equal("Test Subtitle", track.Subtitle);
        Assert.Equal("Test Artist", track.Artist);
        Assert.Equal("Test Album", track.Album);
    }

    [Fact]
    public void ShazamTrack_DefaultValues_AreEmpty()
    {
        // Arrange & Act
        var track = new ShazamTrack();

        // Assert
        Assert.Equal(string.Empty, track.Key);
        Assert.Equal(string.Empty, track.Title);
        Assert.Equal(string.Empty, track.Subtitle);
        Assert.Equal(string.Empty, track.Artist);
        Assert.Equal(string.Empty, track.Album);
        Assert.NotNull(track.Share);
        Assert.NotNull(track.Images);
        Assert.NotNull(track.Hub);
    }

    [Fact]
    public void ShazamMatch_DefaultValues_AreCorrect()
    {
        // Arrange & Act
        var match = new ShazamMatch();

        // Assert
        Assert.Equal(string.Empty, match.Id);
        Assert.Equal(0.0, match.Offset);
        Assert.Equal(string.Empty, match.Channel);
        Assert.Equal(0.0, match.FrequencySkew);
        Assert.Equal(0.0, match.TimeSkew);
        Assert.NotNull(match.Track);
    }
}
