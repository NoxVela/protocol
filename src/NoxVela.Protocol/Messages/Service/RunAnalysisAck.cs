using System;
using System.Text.Json.Serialization;

namespace NoxVela.Protocol.Messages.Service;

/// <summary>
/// Represents the receipt acknowledgement payload sent by the service worker upon receiving a run_analysis instruction.
/// </summary>
public class RunAnalysisAck : Base
{
    /// <summary>
    /// Gets or sets the analysis run identifier being acknowledged.
    /// </summary>
    [JsonPropertyName("run_id")]
    public string RunId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the UTC timestamp when the instruction was received.
    /// </summary>
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
