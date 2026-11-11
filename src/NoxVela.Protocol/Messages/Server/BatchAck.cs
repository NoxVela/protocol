using System;
using System.Text.Json.Serialization;

namespace NoxVela.Protocol.Messages.Server;

/// <summary>
/// Represents the payload for a batch acknowledgement frame dispatched by the server upon persisting a batch.
/// </summary>
public class BatchAck
{
    /// <summary>
    /// Gets or sets the count of items in the batch that was persisted.
    /// </summary>
    [JsonPropertyName("batch_count")]
    public int BatchCount { get; set; }

    /// <summary>
    /// Gets or sets the run identifier associated with the batch.
    /// </summary>
    [JsonPropertyName("run_id")]
    public Guid? RunId { get; set; }

    /// <summary>
    /// Gets or sets the UTC timestamp when the batch was acknowledged.
    /// </summary>
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
