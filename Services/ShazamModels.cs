using System.Text.Json.Serialization;

namespace Jellyzam.Services;

/// <summary>
/// Represents a response from the Shazam API.
/// </summary>
public class ShazamResponse
{
    [JsonPropertyName("matches")]
    public List<ShazamMatch> Matches { get; set; } = new();
}

/// <summary>
/// Represents a match from Shazam.
/// </summary>
public class ShazamMatch
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("offset")]
    public double Offset { get; set; }

    [JsonPropertyName("channel")]
    public string Channel { get; set; } = string.Empty;

    [JsonPropertyName("frequencyskew")]
    public double FrequencySkew { get; set; }

    [JsonPropertyName("timeskew")]
    public double TimeSkew { get; set; }

    [JsonPropertyName("track")]
    public ShazamTrack Track { get; set; } = new();
}

/// <summary>
/// Represents track information from Shazam.
/// </summary>
public class ShazamTrack
{
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("subtitle")]
    public string Subtitle { get; set; } = string.Empty;

    [JsonPropertyName("artist")]
    public string Artist { get; set; } = string.Empty;

    [JsonPropertyName("album")]
    public string Album { get; set; } = string.Empty;

    [JsonPropertyName("share")]
    public ShazamShare Share { get; set; } = new();

    [JsonPropertyName("images")]
    public ShazamImages Images { get; set; } = new();

    [JsonPropertyName("hub")]
    public ShazamHub Hub { get; set; } = new();
}

/// <summary>
/// Represents share information from Shazam.
/// </summary>
public class ShazamShare
{
    [JsonPropertyName("subject")]
    public string Subject { get; set; } = string.Empty;

    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("href")]
    public string Href { get; set; } = string.Empty;

    [JsonPropertyName("image")]
    public string Image { get; set; } = string.Empty;

    [JsonPropertyName("twitter")]
    public string Twitter { get; set; } = string.Empty;

    [JsonPropertyName("html")]
    public string Html { get; set; } = string.Empty;

    [JsonPropertyName("snapchat")]
    public string Snapchat { get; set; } = string.Empty;
}

/// <summary>
/// Represents image information from Shazam.
/// </summary>
public class ShazamImages
{
    [JsonPropertyName("background")]
    public string Background { get; set; } = string.Empty;

    [JsonPropertyName("coverart")]
    public string CoverArt { get; set; } = string.Empty;

    [JsonPropertyName("coverarthq")]
    public string CoverArtHq { get; set; } = string.Empty;

    [JsonPropertyName("joecolor")]
    public string JoeColor { get; set; } = string.Empty;
}

/// <summary>
/// Represents hub information from Shazam.
/// </summary>
public class ShazamHub
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("image")]
    public string Image { get; set; } = string.Empty;

    [JsonPropertyName("actions")]
    public List<ShazamAction> Actions { get; set; } = new();

    [JsonPropertyName("options")]
    public List<ShazamOption> Options { get; set; } = new();

    [JsonPropertyName("providers")]
    public List<ShazamProvider> Providers { get; set; } = new();

    [JsonPropertyName("explicit")]
    public bool Explicit { get; set; }

    [JsonPropertyName("displayname")]
    public string DisplayName { get; set; } = string.Empty;
}

/// <summary>
/// Represents an action from Shazam hub.
/// </summary>
public class ShazamAction
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("uri")]
    public string Uri { get; set; } = string.Empty;
}

/// <summary>
/// Represents an option from Shazam hub.
/// </summary>
public class ShazamOption
{
    [JsonPropertyName("caption")]
    public string Caption { get; set; } = string.Empty;

    [JsonPropertyName("actions")]
    public List<ShazamAction> Actions { get; set; } = new();

    [JsonPropertyName("beacondata")]
    public ShazamBeaconData BeaconData { get; set; } = new();

    [JsonPropertyName("image")]
    public string Image { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("listcaption")]
    public string ListCaption { get; set; } = string.Empty;

    [JsonPropertyName("overflowimage")]
    public string OverflowImage { get; set; } = string.Empty;

    [JsonPropertyName("colouroverflowimage")]
    public bool ColourOverflowImage { get; set; }

    [JsonPropertyName("providername")]
    public string ProviderName { get; set; } = string.Empty;
}

/// <summary>
/// Represents beacon data from Shazam.
/// </summary>
public class ShazamBeaconData
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("providername")]
    public string ProviderName { get; set; } = string.Empty;
}

/// <summary>
/// Represents a provider from Shazam hub.
/// </summary>
public class ShazamProvider
{
    [JsonPropertyName("caption")]
    public string Caption { get; set; } = string.Empty;

    [JsonPropertyName("images")]
    public ShazamProviderImages Images { get; set; } = new();

    [JsonPropertyName("actions")]
    public List<ShazamAction> Actions { get; set; } = new();

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;
}

/// <summary>
/// Represents provider images from Shazam.
/// </summary>
public class ShazamProviderImages
{
    [JsonPropertyName("overflow")]
    public string Overflow { get; set; } = string.Empty;

    [JsonPropertyName("default")]
    public string Default { get; set; } = string.Empty;
}
