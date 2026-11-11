using System.Text.Json.Serialization;

namespace NoxVela.Protocol.Messages.Server;

/// <summary>
/// Payload instructing an analyzer worker to abort an ongoing analysis run and clean up resources.
/// </summary>
public class CancelAnalysis
{
    /// <summary>
    /// Unique identifier of the analysis run to cancel.
    /// </summary>
    [JsonPropertyName("run_id")]
    [JsonRequired]
    public string RunId { get; set; } = string.Empty;

    /// <summary>
    /// Optional reason describing why the analysis was canceled.
    /// </summary>
    [JsonPropertyName("reason")]
    public string? Reason { get; set; }
}
