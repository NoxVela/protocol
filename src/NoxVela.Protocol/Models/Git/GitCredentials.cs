using System.Text.Json.Serialization;

namespace NoxVela.Protocol.Models.Git;

/// <summary>
/// Represents Git credentials.
/// </summary>
public record GitCredentials
{
    /// <summary>
    /// AccessToken.
    /// </summary>
    [JsonPropertyName("access_token")]
    [JsonRequired]
    public string AccessToken { get; set; }
}
