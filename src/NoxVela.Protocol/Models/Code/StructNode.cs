using System;
using System.Text.Json.Serialization;
using MessagePack;
using NoxVela.Protocol.Infrastructure;
using NoxVela.Protocol.Models.Abstract;

namespace NoxVela.Protocol.Models.Code;

/// <summary>
/// Represents a structure definition (value type) in the graph database.
/// </summary>
/// <remarks>
/// Structs are value types. This node captures modern C# features like 'ref structs' 
/// and 'readonly structs' which are critical for memory safety and performance analysis.
/// </remarks>
[MessagePackObject(keyAsPropertyName: true)]
public record StructNode : Node
{
    /// <summary> 
    /// Gets a value indicating whether the struct is marked as 'readonly'. 
    /// Readonly structs ensure that all fields are immutable and allow for performance optimizations.
    /// </summary>
    [JsonPropertyName("is_read_only")]
    [JsonRequired]
    public bool IsReadOnly { get; init; }

    /// <summary> 
    /// Gets a value indicating whether this is a 'ref struct' (e.g., <see cref="Span{T}"/>). 
    /// Ref-like structs are allocated on the stack and have strict usage constraints.
    /// </summary>
    [JsonPropertyName("is_ref_like")]
    [JsonRequired]
    public bool IsRefLike { get; init; }

    /// <summary> Gets a value indicating whether the struct has generic parameters. </summary>
    [JsonPropertyName("is_generic")]
    [JsonRequired]
    public bool IsGeneric { get; init; }

    /// <summary> Gets the number of generic type parameters (Arity) defined for this struct. </summary>
    [JsonPropertyName("arity")]
    public int Arity { get; init; }

    /// <summary> Gets the cyclomatic complexity of the struct. </summary>
    [JsonPropertyName("cyclomatic_complexity")]
    public int CyclomaticComplexity { get; init; } = 1;

    /// <summary> Gets the lines of code (LOC) of the struct. </summary>
    [JsonPropertyName("loc")]
    public int Loc { get; init; } = 1;

    /// <summary> Gets or sets the visibility of the struct. </summary>
    [JsonPropertyName("accessibility")]
    [JsonRequired]
    public override NodeAccessibility Accessibility { get; init; }

    /// <summary> 
    /// Gets the list of file paths where the struct is declared. 
    /// Supports partial struct definitions across multiple files.
    /// </summary>
    [JsonPropertyName("locations")]
    [JsonRequired]
    public List<CodeLocation> Locations { get; init; } = [];

    /// <summary>
    /// Tetermines the type of node.
    /// </summary>
    [JsonPropertyName("type")]
    public override string Type => AllNodesTypes.StructNodes;
}
