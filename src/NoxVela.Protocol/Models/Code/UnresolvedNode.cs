using System;
using System.Text.Json.Serialization;
using MessagePack;
using NoxVela.Protocol.Models.Abstract;

namespace NoxVela.Protocol.Models.Code;

/// <summary>
/// Represents a node in the graph for a symbol that the compiler could not successfully resolve 
/// (e.g., due to missing assembly references, ambiguous names, or accessibility issues).
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public record UnresolvedNode : NodeBase
{
    /// <summary> 
    /// Gets the name or text identifier of the unresolved symbol exactly as it appears in the source code.
    /// </summary>
    [JsonPropertyName("identifier")]
    [JsonRequired]
    public string Identifier { get; init; }

    /// <summary> 
    /// Gets the underlying reason why the compiler considered the symbol unresolved 
    /// (e.g., "Ambiguous", "Inaccessible", "NotCreatable", "None").
    /// </summary>
    [JsonPropertyName("candidate_reason")]
    public string CandidateReason { get; init; }

    /// <summary> 
    /// Gets the precise source code location where the unresolved symbol was encountered or attempted to be used.
    /// </summary>
    [JsonPropertyName("location")]
    [JsonRequired]
    public CodeLocation? Location { get; init; }

    /// <summary>
    /// Tetermines the type of node.
    /// </summary>
    [JsonPropertyName("type")]
    public override string Type => AllNodesTypes.UnresolvedNodes;
}