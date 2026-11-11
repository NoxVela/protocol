using System.Text.Json.Serialization;
using MessagePack;
using NoxVela.Protocol.Infrastructure;
using NoxVela.Protocol.Models.Code;

namespace NoxVela.Protocol.Models.Abstract;

/// <summary>
/// Represents a concrete entity in the graph database. 
/// Inherits core identification from <see cref="NodeBase"/> and adds metadata required for persistence.
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
[Union(0, typeof(ClassNode))]
[Union(1, typeof(InterfaceNode))]
[Union(2, typeof(StructNode))]
[Union(3, typeof(RecordNode))]
[Union(4, typeof(EnumNode))]
[Union(5, typeof(DelegateNode))]
[Union(6, typeof(MethodNode))]
[Union(7, typeof(PropertyNode))]
[Union(8, typeof(FieldNode))]
[Union(9, typeof(EventNode))]
[Union(10, typeof(DummyNode))]
public abstract record Node : NodeBase
{
    /// <summary>
    /// Gets or sets the human-readable identifier.
    /// Used for indexing and searchability within the graph database.
    /// </summary>
    [JsonPropertyName("identifier")]
    [JsonRequired]
    public string Identifier { get; init; }

    /// <summary>
    /// Gets or sets the visibility of the node. 
    /// Must be implemented by derived classes to provide specific mapping from the source symbol.
    /// </summary>
    [JsonPropertyName("accessibility")]
    [JsonRequired]
    public abstract NodeAccessibility Accessibility { get; init; }

    /// <summary> Gets the XML documentation summary associated with the type. </summary>
    [JsonPropertyName("summary")]
    [JsonRequired]
    public string? Summary { get; init; }
}