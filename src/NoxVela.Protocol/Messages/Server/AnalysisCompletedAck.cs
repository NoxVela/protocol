using System;
using System.Text.Json.Serialization;

namespace NoxVela.Protocol.Messages.Server;

/// <summary>
/// Represents the payload for an analysis completion acknowledgement frame dispatched by the server
/// once all nodes, relations, and metrics have been verified and persisted.
/// </summary>
public class AnalysisCompletedAck
{
    /// <summary>
    /// Gets or sets the analysis run identifier.
    /// </summary>
    [JsonPropertyName("run_id")]
    public string RunId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the UTC timestamp when the analysis completion was acknowledged.
    /// </summary>
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
