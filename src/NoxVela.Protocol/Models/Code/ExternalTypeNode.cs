using System.Text.Json.Serialization;
using MessagePack;
using NoxVela.Protocol.Models.Abstract;
using NoxVela.Protocol.Utils;

namespace NoxVela.Protocol.Models.Code;

/// <summary>
/// Represents a type definition located in an external assembly (not part of the current source analysis).
/// Acts as a leaf node in the graph to satisfy references from local code to external libraries.
/// </summary>
/// <remarks>
/// Unlike local nodes, this class derives directly from <see cref="NodeBase"/> because 
/// external types often lack source-related metadata like file paths or local accessibility rules.
/// </remarks>
[MessagePackObject(keyAsPropertyName: true)]
public record ExternalTypeNode : NodeBase
{
    /// <summary>
    /// Gets or sets the fully qualified name or a unique string representation of the external type.
    /// </summary>
    [JsonPropertyName("identifier")]
    [JsonRequired]
    public string Identifier { get; init; }

    /// <summary>
    /// Gets the name of the assembly (e.g., "System.Runtime") where this type is defined.
    /// Used to resolve cross-assembly dependencies in the graph.
    /// </summary>
    [JsonPropertyName("assembly_name")]
    [JsonRequired]
    public string AssemblyName { get; init; }

    /// <summary>
    /// Tetermines the type of node.
    /// </summary>
    [JsonPropertyName("type")]
    public override string Type => AllNodesTypes.ExternalTypeNodes;
}