using Xunit;
using Jellyzam.Configuration;

namespace Jellyzam.Tests.Configuration;

public class PluginConfigurationTests
{
    [Fact]
    public void PluginConfiguration_DefaultValues_AreCorrect()
    {
        // Arrange & Act
        var config = new PluginConfiguration();

        // Assert
        Assert.Equal(string.Empty, config.ShazamApiKey);
        Assert.False(config.AutoIdentifyUnknownTracks);
        Assert.Equal(0.8, config.ConfidenceThreshold);
        Assert.False(config.OverwriteExistingMetadata);
    }

    [Fact]
    public void PluginConfiguration_SetProperties_WorksCorrectly()
    {
        // Arrange
        var config = new PluginConfiguration();

        // Act
        config.ShazamApiKey = "test-api-key";
        config.AutoIdentifyUnknownTracks = true;
        config.ConfidenceThreshold = 0.9;
        config.OverwriteExistingMetadata = true;

        // Assert
        Assert.Equal("test-api-key", config.ShazamApiKey);
        Assert.True(config.AutoIdentifyUnknownTracks);
        Assert.Equal(0.9, config.ConfidenceThreshold);
        Assert.True(config.OverwriteExistingMetadata);
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(0.5)]
    [InlineData(1.0)]
    public void PluginConfiguration_ConfidenceThreshold_AcceptsValidValues(double threshold)
    {
        // Arrange
        var config = new PluginConfiguration();

        // Act
        config.ConfidenceThreshold = threshold;

        // Assert
        Assert.Equal(threshold, config.ConfidenceThreshold);
    }
}
