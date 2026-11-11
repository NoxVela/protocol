using System;
using System.Text.Json.Serialization;
using MessagePack;
using NoxVela.Protocol.Infrastructure;
using NoxVela.Protocol.Models.Abstract;

namespace NoxVela.Protocol.Models.Code;

/// <summary>
/// Represents a class definition node within the graph database.
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public record ClassNode : Node
{
    /// <summary> Gets a value indicating whether the class is marked as static. </summary>
    [JsonPropertyName("is_static")]
    [JsonRequired]
    public bool IsStatic { get; init; }

    /// <summary> Gets a value indicating whether the class is abstract. </summary>
    [JsonPropertyName("is_abstract")]
    [JsonRequired]
    public bool IsAbstract { get; init; }

    /// <summary> Gets a value indicating whether the class is sealed (cannot be inherited). </summary>
    [JsonPropertyName("is_sealed")]
    [JsonRequired]
    public bool IsSealed { get; init; }

    /// <summary> Gets a value indicating whether the class has generic parameters. </summary>
    [JsonPropertyName("is_generic")]
    [JsonRequired]
    public bool IsGeneric { get; init; }

    /// <summary> Gets the number of generic type parameters (Arity) defined for this class. </summary>
    [JsonPropertyName("arity")]
    [JsonRequired]
    public int Arity { get; init; }

    /// <summary> Gets the cyclomatic complexity of the class. </summary>
    [JsonPropertyName("cyclomatic_complexity")]
    public int CyclomaticComplexity { get; init; } = 1;

    /// <summary> Gets the lines of code (LOC) of the class. </summary>
    [JsonPropertyName("loc")]
    public int Loc { get; init; } = 1;

    /// <summary> 
    /// Gets the collection of file paths where this class is declared. 
    /// Relevant for partial classes spread across multiple files.
    /// </summary>
    [JsonPropertyName("locations")]
    [JsonRequired]
    public List<CodeLocation> Locations { get; init; } = [];

    /// <summary>
    /// Gets or sets the visibility of the class. Defaults to <see cref="NodeAccessibility.Internal"/>.
    /// </summary>
    [JsonPropertyName("accessibility")]
    [JsonRequired]
    public override NodeAccessibility Accessibility { get; init; } = NodeAccessibility.Internal;

    /// <summary>
    /// Tetermines the type of node.
    /// </summary>
    [JsonPropertyName("type")]
    public override string Type => AllNodesTypes.ClassNodes;
}
