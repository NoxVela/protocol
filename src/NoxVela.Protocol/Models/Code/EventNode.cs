using System.Text.Json.Serialization;
using MessagePack;
using NoxVela.Protocol.Infrastructure;
using NoxVela.Protocol.Models.Abstract;

namespace NoxVela.Protocol.Models.Code;

/// <summary>
/// Represents an event member within a class, interface, or struct.
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public record EventNode : Node
{
    /// <summary> Gets or sets the visibility of the event. </summary>
    [JsonPropertyName("accessibility")]
    [JsonRequired]
    public override NodeAccessibility Accessibility { get; init; }

    /// <summary> Gets a value indicating whether the event is shared across all instances of the type. </summary>
    [JsonPropertyName("is_static")]
    [JsonRequired]
    public bool IsStatic { get; init; }

    /// <summary> Gets a value indicating whether the event is declared without an implementation. </summary>
    [JsonPropertyName("is_abstract")]
    [JsonRequired]
    public bool IsAbstract { get; init; }

    /// <summary> Gets a value indicating whether the event can be overridden in derived classes. </summary>
    [JsonPropertyName("is_virtual")]
    [JsonRequired]
    public bool IsVirtual { get; init; }

    /// <summary> Gets a value indicating whether this event overrides a base class event. </summary>
    [JsonPropertyName("is_override")]
    [JsonRequired]
    public bool IsOverride { get; init; }

    /// <summary> Gets a value indicating whether this event cannot be further overridden. </summary>
    [JsonPropertyName("is_sealed")]
    [JsonRequired]
    public bool IsSealed { get; init; }

    /// <summary>
    /// Gets a value indicating whether the event explicitly defines 'add' and 'remove' accessors.
    /// True if the event is not a simple field-like event.
    /// </summary>
    [JsonPropertyName("has_custom_accessors")]
    [JsonRequired]
    public bool HasCustomAccessors { get; init; } 

    /// <summary> Gets the source file path where the event is declared. </summary>
    [JsonPropertyName("location")]
    [JsonRequired]
    public CodeLocation? Location { get; init; }

    /// <summary>
    /// Tetermines the type of node.
    /// </summary>
    [JsonPropertyName("type")]
    public override string Type => AllNodesTypes.EventsNodes;
}
