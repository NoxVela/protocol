using System;
using System.Text.Json.Serialization;

namespace NoxVela.Protocol.Models.Ai;

/// <summary>
/// User query message payload dispatched to the AI assistant worker.
/// </summary>
public record ChatMessagePayload
{
    /// <summary>Natural language question or request from user.</summary>
    [JsonPropertyName("userQuery")]
    [JsonRequired]
    public string UserQuery { get; init; } = string.Empty;

    /// <summary>Associated project identifier.</summary>
    [JsonPropertyName("projectId")]
    [JsonRequired]
    public string ProjectId { get; init; } = string.Empty;

    /// <summary>Optional user session identifier.</summary>
    [JsonPropertyName("sessionId")]
    public string? SessionId { get; init; }

    /// <summary>SignalR connection ID of the initiating client.</summary>
    [JsonPropertyName("sourceConnectionId")]
    public string? SourceConnectionId { get; init; }

    /// <summary>Target SignalR connection ID for routing response chunks.</summary>
    [JsonPropertyName("targetConnectionId")]
    public string? TargetConnectionId { get; init; }

    /// <summary>Language preference (e.g. en, pl).</summary>
    [JsonPropertyName("language")]
    public string Language { get; init; } = "en";

    /// <summary>Optional LLM model override name.</summary>
    [JsonPropertyName("model")]
    public string? Model { get; init; }

    /// <summary>Sampling temperature for generation.</summary>
    [JsonPropertyName("temperature")]
    public double? Temperature { get; init; }

    /// <summary>Max tokens to generate.</summary>
    [JsonPropertyName("maxTokens")]
    public int? MaxTokens { get; init; }

    /// <summary>Database credentials for namespace isolation.</summary>
    [JsonPropertyName("dbCredentials")]
    public AiDatabaseCredentials? DbCredentials { get; init; }
}
