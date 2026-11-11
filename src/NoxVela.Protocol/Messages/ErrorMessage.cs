using System;
using System.Text.Json.Serialization;

namespace NoxVela.Protocol.Messages;

/// <summary>
/// Represents a standardized diagnostic error envelope exchanged bidirectionally across the NoxVela protocol.
/// </summary>
public class ErrorMessage
{
    /// <summary>
    /// Machine-readable standardized error code. See <see cref="Errors.ProtocolErrorCodes"/>.
    /// </summary>
    [JsonPropertyName("code")]
    [JsonRequired]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Human-readable descriptive error message.
    /// </summary>
    [JsonPropertyName("message")]
    [JsonRequired]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Optional analysis run identifier correlated with the failure.
    /// </summary>
    [JsonPropertyName("run_id")]
    public string? RunId { get; set; }

    /// <summary>
    /// Indicates whether the error was fatal and aborted the operation or worker process.
    /// </summary>
    [JsonPropertyName("is_fatal")]
    public bool IsFatal { get; set; }

    /// <summary>
    /// Optional additional technical details or stack trace for diagnostics.
    /// </summary>
    [JsonPropertyName("details")]
    public string? Details { get; set; }

    /// <summary>
    /// UTC timestamp when the error was recorded.
    /// </summary>
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
