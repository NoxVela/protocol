using System.Text.Json.Serialization;
using MessagePack;
using NoxVela.Protocol.Models.Abstract;

namespace NoxVela.Protocol.Models.Code;

/// <summary>
/// Represents an attribute node in the code model.
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public record class AttributeNode : NodeBase
{
    /// <summary>
    /// Gets or sets the human-readable identifier (e.g., MethodName or FullClassName).
    /// Used for indexing and searchability within the graph database.
    /// </summary>
    [JsonPropertyName("identifier")]
    [JsonRequired]
    public string Identifier { get; init; }
    
    /// <summary>
    /// Gets or sets the list of arguments associated with the attribute.
    /// </summary>
    [JsonPropertyName("arguments")]
    [JsonRequired]
    public List<string> Arguments { get; init; } = [];

    /// <summary>
    /// Tetermines the type of node.
    /// </summary>
    [JsonPropertyName("type")]
    public override string Type => AllNodesTypes.AttributeNodes;
}