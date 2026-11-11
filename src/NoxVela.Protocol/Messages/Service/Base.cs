using System;
using System.Text.Json.Serialization;

namespace NoxVela.Protocol.Messages.Service;

/// <summary>
/// Base message structure applicable for all messages.
/// </summary>
public abstract class Base
{
    /// <summary>
    /// Instance id.
    /// </summary>
    [JsonPropertyName("instanceId")]
    [JsonRequired]
    public required string InstanceId { get; set; }
}
