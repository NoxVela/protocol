using System.Text.Json.Serialization;
using MessagePack;
using NoxVela.Protocol.Infrastructure;
using NoxVela.Protocol.Models.Abstract;

namespace NoxVela.Protocol.Models.Code;

/// <summary>
/// Represents a record definition (either class or struct) in the graph database.
/// </summary>
/// <remarks>
/// Records provide built-in functionality for encapsulating data, including value-based equality.
/// This node distinguishes between reference-based records and value-based record structs.
/// </remarks>
[MessagePackObject(keyAsPropertyName: true)]
public record RecordNode : Node
{
    /// <summary> Gets or sets the visibility of the record. </summary>
    [JsonPropertyName("accessibility")]
    [JsonRequired]
    public override NodeAccessibility Accessibility { get; init; }

    /// <summary> 
    /// Gets a value indicating whether this is a 'record struct'. 
    /// If false, it represents a 'record class'. 
    /// </summary>
    [JsonPropertyName("is_record_struct")]
    [JsonRequired]
    public bool IsRecordStruct { get; init; }

    /// <summary> 
    /// Gets a value indicating whether the record struct is marked as 'readonly'. 
    /// (Only applicable when <see cref="IsRecordStruct"/> is true).
    /// </summary>
    [JsonPropertyName("is_read_only")]
    [JsonRequired]
    public bool IsReadOnly { get; init; }
    
    /// <summary> Gets a value indicating whether the record is marked as static. </summary>
    [JsonPropertyName("is_static")]
    [JsonRequired]
    public bool IsStatic { get; init; }

    /// <summary> Gets a value indicating whether the record is abstract. </summary>
    [JsonPropertyName("is_abstract")]
    [JsonRequired]
    public bool IsAbstract { get; init; }

    /// <summary> Gets a value indicating whether the record is sealed. </summary>
    [JsonPropertyName("is_sealed")]
    [JsonRequired]
    public bool IsSealed { get; init; }

    /// <summary> Gets a value indicating whether the record has generic parameters. </summary>
    [JsonPropertyName("is_generic")]
    public bool IsGeneric { get; init; }

    /// <summary> Gets the number of generic type parameters (Arity) defined for this record. </summary>
    [JsonPropertyName("arity")]
    public int Arity { get; init; }

    /// <summary> Gets the cyclomatic complexity of the record. </summary>
    [JsonPropertyName("cyclomatic_complexity")]
    public int CyclomaticComplexity { get; init; } = 1;

    /// <summary> Gets the lines of code (LOC) of the record. </summary>
    [JsonPropertyName("loc")]
    public int Loc { get; init; } = 1;

    /// <summary> 
    /// Gets the collection of file paths where this record is declared. 
    /// Records are frequently 'partial' when used with source generators or primary constructors.
    /// </summary>
    [JsonPropertyName("locations")]
    [JsonRequired]
    public List<CodeLocation> Locations { get; init; } = [];

    /// <summary>
    /// Tetermines the type of node.
    /// </summary>
    [JsonPropertyName("type")]
    public override string Type => AllNodesTypes.RecordNodes;
}
