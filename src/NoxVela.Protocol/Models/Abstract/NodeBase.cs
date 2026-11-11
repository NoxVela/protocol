using System.Text.Json.Serialization;
using MessagePack;
using NoxVela.Protocol.Models.Code;

namespace NoxVela.Protocol.Models.Abstract;

/// <summary>
/// Provides a base implementation for graph nodes, ensuring consistent 
/// identification and change tracking across the system.
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
[Union(10, typeof(AttributeNode))]
[Union(11, typeof(ParameterNode))]
[Union(12, typeof(ExternalTypeNode))]
[Union(13, typeof(NamespaceNode))]
[Union(14, typeof(UnresolvedNode))]
[Union(15, typeof(DummyNode))]
[JsonConverter(typeof(NodeBaseConverter))]
public abstract record NodeBase
{
    /// <summary>
    /// Gets a deterministic identifier based on structural language properties. 
    /// This ID remains stable across builds, allowing for persistent node mapping.
    /// </summary>
    [JsonPropertyName("stable_id")]
    [JsonRequired]
    public string StableId { get; init; }

    /// <summary>
    /// Gets a hash representing the current state of the node's content.
    /// <para>
    /// For example, changes to a class body between commits will result in a 
    /// different hash, signaling that the node's definition has evolved.
    /// </para>
    /// </summary>
    [JsonPropertyName("content_hash")]
    [JsonRequired]
    public string ContentHash { get; init; }

    /// <summary>
    /// Determines the type of node.
    /// </summary>
    [JsonPropertyName("type")]
    public abstract string Type { get; }
}