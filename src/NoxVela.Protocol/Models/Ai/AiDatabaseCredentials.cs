using System;
using System.Text.Json.Serialization;

namespace NoxVela.Protocol.Models.Ai;

/// <summary>
/// Credentials used by worker services to authenticate against project database namespaces.
/// </summary>
public record AiDatabaseCredentials
{
    /// <summary>Database namespace name.</summary>
    [JsonPropertyName("namespace")]
    [JsonRequired]
    public string Namespace { get; init; } = string.Empty;

    /// <summary>Database user name.</summary>
    [JsonPropertyName("user")]
    [JsonRequired]
    public string User { get; init; } = string.Empty;

    /// <summary>Database user password.</summary>
    [JsonPropertyName("password")]
    public string Password { get; init; } = string.Empty;
}
