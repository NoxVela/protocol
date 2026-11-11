using System.Collections.Generic;
using System.Text.Json.Serialization;
using MessagePack;
using NoxVela.Protocol.Models.Abstract;

namespace NoxVela.Protocol.Messages.Service;

/// <summary>
/// Strongly-typed payload representing a micro-batch of AST nodes.
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class NodesBatch
{
    /// <summary>
    /// Collection of AST nodes emitted in this micro-batch.
    /// </summary>
    [JsonPropertyName("nodes")]
    [JsonRequired]
    public IReadOnlyList<NodeBase> Nodes { get; set; } = [];
}
