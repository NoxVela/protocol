using System;
using System.Text.Json.Serialization;
using MessagePack;

namespace NoxVela.Protocol.Models.Abstract;

/// <summary>
/// Represents an edge (relationship) between two nodes in the graph.
/// Supports both direct links and "pending" links for out-of-order processing.
/// </summary>
/// <remarks>
/// The <see cref="Id"/> is deterministically generated to ensure idempotency during database synchronization.
/// </remarks>
[MessagePackObject(keyAsPropertyName: true)]
public record RelationNode
{
    /// <summary> 
    /// Unique deterministic identifier for the relation edge.
    /// Computed via <see cref="Utils.StableIdFactory.CreateRelationId(string, string, string, bool)"/>:
    /// <list type="bullet">
    ///   <item><description>Direct: <c>SHA256("{from}|{relation_type}|{to}")</c></description></item>
    ///   <item><description>Pending: <c>SHA256("{from}|{relation_type}|{to}|pending")</c></description></item>
    /// </list>
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary> 
    /// The identifier of the source node (usually the caller or declaring symbol's content hash).
    /// </summary>
    [JsonPropertyName("from")]
    public string From { get; init; } = string.Empty;

    /// <summary> 
    /// The identifier of the target node. 
    /// Contains the target node's ContentHash when direct (<see cref="IsPending"/> is false),
    /// or the target symbol's StableId when pending resolution (<see cref="IsPending"/> is true). 
    /// </summary>
    [JsonPropertyName("to")]
    public string To { get; init; } = string.Empty;

    /// <summary> 
    /// The semantic type of the relationship (e.g., "CONTAINS", "IMPLEMENTS", "CALLS", "OF_TYPE"). 
    /// See <see cref="Infrastructure.NodesRelations"/>.
    /// </summary>
    [JsonPropertyName("relation_type")]
    public string RelationType { get; init; } = string.Empty;

    /// <summary> 
    /// Indicates whether the target node was not yet parsed or resolved during streaming analysis.
    /// When true, allows analyzers to emit edges without buffering entire syntax graphs in memory.
    /// The server resolves pending relations in a post-ingestion query before completion acknowledgement.
    /// </summary>
    [JsonPropertyName("is_pending")]
    public bool IsPending { get; init; }

    /// <summary> Identifier of the current analysis execution session correlated with this edge. </summary>
    [JsonPropertyName("run_id")]
    public string? RunId { get; init; }
}
