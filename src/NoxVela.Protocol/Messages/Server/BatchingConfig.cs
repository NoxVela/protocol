using System.Text.Json.Serialization;

namespace NoxVela.Protocol.Messages.Server;

/// <summary>
/// Configuration for dynamic time-and-count windowed micro-batching during analysis emission.
/// </summary>
public class BatchingConfig
{
    /// <summary>
    /// Maximum number of items aggregated in a single micro-batch before flushing.
    /// Defaults to 100.
    /// </summary>
    [JsonPropertyName("max_batch_size")]
    public int MaxBatchSize { get; set; } = 100;

    /// <summary>
    /// Maximum elapsed milliseconds before flushing buffered items.
    /// Defaults to 100ms.
    /// </summary>
    [JsonPropertyName("flush_interval_ms")]
    public int FlushIntervalMs { get; set; } = 100;
}
