using System.Collections.Generic;
using System.Text.Json.Serialization;
using MessagePack;
using NoxVela.Protocol.Models.Abstract;

namespace NoxVela.Protocol.Messages.Service;

/// <summary>
/// Strongly-typed payload representing a micro-batch of graph relations.
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public class RelationsBatch
{
    /// <summary>
    /// Collection of graph relations emitted in this micro-batch.
    /// </summary>
    [JsonPropertyName("relations")]
    [JsonRequired]
    public IReadOnlyList<RelationNode> Relations { get; set; } = [];
}
