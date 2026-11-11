using System.Text.Json.Serialization;
using MessagePack;
using NoxVela.Protocol.Infrastructure;
using NoxVela.Protocol.Models.Abstract;

namespace NoxVela.Protocol.Models.Code;

/// <summary>
/// Represents a field member within a class, struct, or enum.
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public record FieldNode : Node
{
    /// <summary> Gets or sets the visibility of the field. </summary>
    [JsonPropertyName("accessibility")]
    [JsonRequired]
    public override NodeAccessibility Accessibility { get; init; }

    /// <summary> Gets a value indicating whether the field is a constant (evaluated at compile-time). </summary>
    [JsonPropertyName("is_const")]
    [JsonRequired]
    public bool IsConst { get; init; }

    /// <summary> Gets a value indicating whether the field is read-only (can only be set in a constructor). </summary>
    [JsonPropertyName("is_read_only")]
    [JsonRequired]
    public bool IsReadOnly { get; init; }

    /// <summary> Gets a value indicating whether the field is shared across all instances. </summary>
    [JsonPropertyName("is_static")]
    [JsonRequired]
    public bool IsStatic { get; init; }

    /// <summary> 
    /// Gets a value indicating whether the field is volatile, 
    /// signaling that it may be modified by multiple threads. 
    /// </summary>
    [JsonPropertyName("is_volatile")]
    [JsonRequired]
    public bool IsVolatile { get; init; }

    /// <summary> 
    /// Gets the string representation of the constant value, if <see cref="IsConst"/> is true. 
    /// </summary>
    [JsonPropertyName("constant_value")]
    [JsonRequired]
    public string? ConstantValue { get; init; } 

    /// <summary> Gets the source file path where the field is declared. </summary>
    [JsonPropertyName("location")]
    [JsonRequired]
    public CodeLocation? Location { get; init; }

    /// <summary>
    /// Tetermines the type of node.
    /// </summary>
    [JsonPropertyName("type")]
    public override string Type => AllNodesTypes.FieldNodes;
}
