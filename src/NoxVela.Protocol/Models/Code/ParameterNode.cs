using System;
using System.Text.Json.Serialization;
using MessagePack;
using NoxVela.Protocol.Models.Abstract;

namespace NoxVela.Protocol.Models.Code;

/// <summary>
/// Represents a formal parameter of a method, property, or indexer.
/// </summary>
/// <remarks>
/// Inherits from <see cref="NodeBase"/> as parameters are considered metadata 
/// rather than independent entities with their own accessibility modifiers.
/// </remarks>
[MessagePackObject(keyAsPropertyName: true)]
public record ParameterNode : NodeBase
{
    /// <summary>
    /// Gets the name of the parameter.
    /// </summary>
    [JsonPropertyName("identifier")]
    [JsonRequired]
    public string Identifier { get; init; }

    /// <summary>
    /// Gets the zero-based position of the parameter in the formal parameter list.
    /// </summary>
    [JsonPropertyName("ordinal")]
    [JsonRequired]
    public int Ordinal { get; init; }

    /// <summary>
    /// Gets the ref kind of the parameter (e.g., "None", "Ref", "Out", "In").
    /// </summary>
    [JsonPropertyName("ref_kind")]
    [JsonRequired]
    public string RefKind { get; init; }

    /// <summary>
    /// Gets a value indicating whether the parameter is a parameter array (marked with the 'params' keyword).
    /// </summary>
    [JsonPropertyName("is_params")]
    [JsonRequired]
    public bool IsParams { get; init; }

    /// <summary>
    /// Gets a value indicating whether the parameter is optional (has a default value).
    /// </summary>
    [JsonPropertyName("is_optional")]
    [JsonRequired]
    public bool IsOptional { get; init; }

    /// <summary>
    /// Gets a value indicating whether this is the 'this' parameter of an extension method.
    /// </summary>
    [JsonPropertyName("is_this")]
    [JsonRequired]
    public bool IsThis { get; init; }

    /// <summary>
    /// Gets the string representation of the default value, if <see cref="IsOptional"/> is true.
    /// </summary>
    [JsonPropertyName("default_value")]
    [JsonRequired]
    public string? DefaultValue { get; init; }

    /// <summary>
    /// Tetermines the type of node.
    /// </summary>
    [JsonPropertyName("type")]
    public override string Type => AllNodesTypes.ParameterNodes;
}
