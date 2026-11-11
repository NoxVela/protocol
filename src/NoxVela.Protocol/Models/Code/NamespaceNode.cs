using System.Text.Json.Serialization;
using MessagePack;
using NoxVela.Protocol.Models.Abstract;

namespace NoxVela.Protocol.Models.Code;

/// <summary>
/// Represents a logical container for types and other namespaces.
/// </summary>
/// <remarks>
/// Inherits from <see cref="NodeBase"/> instead of <see cref="Node"/> because namespaces 
/// do not have accessibility modifiers or file-specific content hashes.
/// </remarks>
[MessagePackObject(keyAsPropertyName: true)]
public record NamespaceNode: NodeBase
{
    /// <summary>
    /// Gets or sets the fully qualified name of the namespace (e.g., "System.Collections.Generic").
    /// Used as the primary label for grouping types within the graph.
    /// </summary>
    [JsonPropertyName("identifier")]
    [JsonRequired]
    public string Identifier { get; init; }

    /// <summary>
    /// Tetermines the type of node.
    /// </summary>
    [JsonPropertyName("type")]
    public override string Type => AllNodesTypes.NamespaceNodes;
}
