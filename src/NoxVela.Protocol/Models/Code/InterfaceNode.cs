using System.Text.Json.Serialization;
using MessagePack;
using NoxVela.Protocol.Infrastructure;
using NoxVela.Protocol.Models.Abstract;

namespace NoxVela.Protocol.Models.Code;

/// <summary>
/// Represents an interface definition in the graph database.
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public record InterfaceNode : Node
{
    /// <summary>
    /// Gets a value indicating whether the interface has generic parameters (e.g., <c>IReadOnlyList&lt;T&gt;</c>).
    /// </summary>
    [JsonPropertyName("is_generic")]
    [JsonRequired]
    public bool IsGeneric { get; init; }

    /// <summary> Gets the number of generic type parameters (Arity) defined for this interface. </summary>
    [JsonPropertyName("arity")]
    public int Arity { get; init; }

    /// <summary> Gets the cyclomatic complexity of the interface. </summary>
    [JsonPropertyName("cyclomatic_complexity")]
    public int CyclomaticComplexity { get; init; } = 1;

    /// <summary> Gets the lines of code (LOC) of the interface. </summary>
    [JsonPropertyName("loc")]
    public int Loc { get; init; } = 1;

    /// <summary>
    /// Gets or sets the visibility of the interface.
    /// </summary>
    [JsonPropertyName("accessibility")]
    [JsonRequired]
    public override NodeAccessibility Accessibility { get; init; }

    /// <summary>
    /// Gets the collection of file paths where this interface is declared.
    /// Supports partial interface definitions spread across multiple files.
    /// </summary>
    [JsonPropertyName("locations")]
    [JsonRequired]
    public List<CodeLocation> Locations { get; init; } = [];

    /// <summary>
    /// Tetermines the type of node.
    /// </summary>
    [JsonPropertyName("type")]
    public override string Type => AllNodesTypes.InterfaceNodes;
}
