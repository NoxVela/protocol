using System;
using System.Text.Json.Serialization;
using MessagePack;

namespace NoxVela.Protocol.Messages;

/// <summary>
/// Represents a wrapper for protocol messages, containing essential metadata and payload.
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class Wrapper
{
    /// <summary>
    /// Gets or sets the unique identifier for the message.
    /// </summary>
    [JsonPropertyName("messageId")]
    [JsonRequired]
    public Guid MessageId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the source message, if applicable.
    /// </summary>
    [JsonPropertyName("sourceMessageId")]
    [JsonRequired]
    public Guid SourceMessageId { get; set; }

    /// <summary>
    /// Gets or sets the type of the message, indicating its purpose or category.
    /// </summary>
    [JsonPropertyName("messageType")]
    [JsonRequired]
    public required string MessageType { get; set; }

    /// <summary>
    /// Gets or sets the payload of the message, typically containing the actual data.
    /// </summary>
    [JsonPropertyName("payload")]
    [JsonRequired]
    public required string Payload { get; set; }
}
