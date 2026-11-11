using System.Text.Json.Serialization;
using MessagePack;
using NoxVela.Protocol.Infrastructure;
using NoxVela.Protocol.Models.Abstract;

namespace NoxVela.Protocol.Models.Code;

/// <summary>
/// Represents an enumeration type definition in the graph database.
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public record EnumNode : Node
{
    /// <summary>
    /// Gets or sets the visibility of the enum.
    /// </summary>
    [JsonPropertyName("accessibility")]
    [JsonRequired]
    public override NodeAccessibility Accessibility { get; init; }

    /// <summary>
    /// Gets the source file path where the enum is defined.
    /// </summary>
    [JsonPropertyName("location")]
    [JsonRequired]
    public CodeLocation? Location { get; init; }

    /// <summary>
    /// Tetermines the type of node.
    /// </summary>
    [JsonPropertyName("type")]
    public override string Type => AllNodesTypes.EnumsNodes;
}
