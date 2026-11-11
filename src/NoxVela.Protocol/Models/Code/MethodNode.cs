using System.Text.Json.Serialization;
using MessagePack;
using NoxVela.Protocol.Infrastructure;
using NoxVela.Protocol.Models.Abstract;

namespace NoxVela.Protocol.Models.Code;

/// <summary>
/// Represents a method, constructor, or destructor within the graph.
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public record MethodNode : Node
{
    /// <summary> Gets or sets the visibility of the method. </summary>
    [JsonPropertyName("accessibility")]
    [JsonRequired]
    public override NodeAccessibility Accessibility { get; init; }

    /// <summary> 
    /// Gets the specific kind of the method (e.g., "Ordinary", "Constructor", "UserDefinedOperator").
    /// </summary>
    [JsonPropertyName("method_kind")]
    [JsonRequired]
    public string MethodKind { get; init; }

    /// <summary> Gets a value indicating whether the method is shared across all instances. </summary>
    [JsonPropertyName("is_static")]
    [JsonRequired]
    public bool IsStatic { get; init; }

    /// <summary> Gets a value indicating whether the method is declared without an implementation. </summary>
    [JsonPropertyName("is_abstract")]
    [JsonRequired]
    public bool IsAbstract { get; init; }

    /// <summary> Gets a value indicating whether the method can be overridden in derived classes. </summary>
    [JsonPropertyName("is_virtual")]
    [JsonRequired]
    public bool IsVirtual { get; init; }

    /// <summary> Gets a value indicating whether this method overrides a base implementation. </summary>
    [JsonPropertyName("is_override")]
    [JsonRequired]
    public bool IsOverride { get; init; }

    /// <summary> Gets a value indicating whether this method cannot be further overridden. </summary>
    [JsonPropertyName("is_sealed")]
    [JsonRequired]
    public bool IsSealed { get; init; }

    /// <summary> Gets a value indicating whether the method is marked with the 'async' modifier. </summary>
    [JsonPropertyName("is_async")]
    [JsonRequired]
    public bool IsAsync { get; init; }

    /// <summary> Gets a value indicating whether this is an extension method. </summary>
    [JsonPropertyName("is_extension_method")]
    [JsonRequired]
    public bool IsExtensionMethod { get; init; }

    /// <summary> Gets a value indicating whether the method has type parameters. </summary>
    [JsonPropertyName("is_generic")]
    [JsonRequired]
    public bool IsGeneric { get; init; }

    /// <summary> Gets the number of generic type parameters (Arity) of the method. </summary>
    [JsonPropertyName("arity")]
    [JsonRequired]
    public int Arity { get; init; }

    /// <summary> 
    /// Gets the list of files where the method is defined. 
    /// Usually contains one file, unless the method is a partial method with both definition and implementation.
    /// </summary>
    [JsonPropertyName("locations")]
    [JsonRequired]
    public List<CodeLocation> Locations { get; init; } = [];

    /// <summary> Contains the results of the cyclometric analysis </summary>
    [JsonPropertyName("complexity")]
    [JsonRequired]
    public int Complexity { get; set; }

    /// <summary>
    /// Tetermines the type of node.
    /// </summary>
    [JsonPropertyName("type")]
    public override string Type => AllNodesTypes.MethodNodes;
}
