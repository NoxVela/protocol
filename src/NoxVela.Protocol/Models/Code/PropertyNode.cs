using System;
using System.Text.Json.Serialization;
using MessagePack;
using NoxVela.Protocol.Infrastructure;
using NoxVela.Protocol.Models.Abstract;

namespace NoxVela.Protocol.Models.Code;

/// <summary>
/// Represents a property or indexer member within a type.
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public record PropertyNode : Node
{
    /// <summary> Gets or sets the visibility of the property. </summary>
    [JsonPropertyName("accessibility")]
    [JsonRequired]
    public override NodeAccessibility Accessibility { get; init; }

    /// <summary> 
    /// Gets a value indicating whether this property is an indexer (e.g., <c>this[int index]</c>). 
    /// </summary>
    [JsonPropertyName("is_indexer")]
    [JsonRequired]
    public bool IsIndexer { get; init; }

    /// <summary> Gets a value indicating whether the property is shared across all instances. </summary>
    [JsonPropertyName("is_static")]
    [JsonRequired]
    public bool IsStatic { get; init; }

    /// <summary> Gets a value indicating whether the property is declared without implementation. </summary>
    [JsonPropertyName("is_abstract")]
    [JsonRequired]
    public bool IsAbstract { get; init; }

    /// <summary> Gets a value indicating whether the property can be overridden in derived classes. </summary>
    [JsonPropertyName("is_virtual")]
    [JsonRequired]
    public bool IsVirtual { get; init; }

    /// <summary> Gets a value indicating whether this property overrides a base implementation. </summary>
    [JsonPropertyName("is_override")]
    [JsonRequired]
    public bool IsOverride { get; init; }

    /// <summary> Gets a value indicating whether this property cannot be further overridden. </summary>
    [JsonPropertyName("is_sealed")]
    [JsonRequired]
    public bool IsSealed { get; init; }

    /// <summary> Gets a value indicating whether the property has a 'get' accessor. </summary>
    [JsonPropertyName("has_get")]
    [JsonRequired]
    public bool HasGet { get; init; }

    /// <summary> Gets a value indicating whether the property has a 'set' or 'init' accessor. </summary>
    [JsonPropertyName("has_set")]
    [JsonRequired]
    public bool HasSet { get; init; }

    /// <summary> 
    /// Gets a value indicating whether the 'set' accessor is 'init-only'. 
    /// Relevant for C# 9.0+ immutable object initialization.
    /// </summary>
    [JsonPropertyName("is_init_only")]
    [JsonRequired]
    public bool IsInitOnly { get; init; }

    /// <summary> 
    /// Gets a value indicating whether the property is marked with the 'required' keyword (C# 11+). 
    /// </summary>
    [JsonPropertyName("is_required")]
    [JsonRequired]
    public bool IsRequired { get; init; }

    /// <summary> 
    /// Gets the collection of file paths where this property is declared. 
    /// Supports partial types where properties might be defined in different locations.
    /// </summary>
    [JsonPropertyName("locations")]
    [JsonRequired]
    public List<CodeLocation> Locations { get; init; } = [];

    /// <summary>
    /// Tetermines the type of node.
    /// </summary>
    [JsonPropertyName("type")]
    public override string Type => AllNodesTypes.PropertyNodes;
}
