using System;
using System.Text.Json.Serialization;

namespace NoxVela.Protocol.Messages.Server;

/// <summary>
/// Represents the payload for a heartbeat acknowledgement frame dispatched by the server upon receiving status.
/// </summary>
public class StatusAck
{
    /// <summary>
    /// Gets or sets the instance identifier of the worker being acknowledged.
    /// </summary>
    [JsonPropertyName("instance_id")]
    public string InstanceId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the UTC timestamp when the heartbeat was acknowledged.
    /// </summary>
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
