using System;
using System.Text.Json.Serialization;

namespace NoxVela.Protocol.Models.Ai;

/// <summary>
/// Status and progress update payload emitted by AI embedding workers.
/// </summary>
public record EmbeddingStatusPayload
{
    /// <summary>Unique instance identifier of the AI worker reporting status.</summary>
    [JsonPropertyName("instanceId")]
    [JsonRequired]
    public string InstanceId { get; init; } = string.Empty;

    /// <summary>Status code (e.g. IN_PROGRESS, FINISHED, ERROR).</summary>
    [JsonPropertyName("status")]
    [JsonRequired]
    public string Status { get; init; } = string.Empty;

    /// <summary>Descriptive message accompanying the status.</summary>
    [JsonPropertyName("message")]
    [JsonRequired]
    public string Message { get; init; } = string.Empty;

    /// <summary>Associated project identifier.</summary>
    [JsonPropertyName("projectId")]
    [JsonRequired]
    public string ProjectId { get; init; } = string.Empty;

    /// <summary>Associated run identifier, if any.</summary>
    [JsonPropertyName("runId")]
    public string? RunId { get; init; }

    /// <summary>Number of items processed so far.</summary>
    [JsonPropertyName("processed")]
    public long? Processed { get; init; }

    /// <summary>Number of items remaining to process.</summary>
    [JsonPropertyName("left")]
    public long? Left { get; init; }
}
