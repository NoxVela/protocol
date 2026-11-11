using System;
using System.Text.Json.Serialization;
using MessagePack;
using NoxVela.Protocol.Infrastructure;
using NoxVela.Protocol.Models.Abstract;

namespace NoxVela.Protocol.Models.Code;

/// <summary>
/// Represents a fallback or placeholder node for symbols that do not have a specialized 
/// implementation or for symbols that are referenced but not fully analyzed.
/// </summary>
/// <remarks>
/// Used to maintain graph integrity when a relation points to a symbol that 
/// falls outside the current analysis scope or is an unhandled.
/// </remarks>
[MessagePackObject(keyAsPropertyName: true)]
public record DummyNode : Node
{
    /// <summary>
    /// Gets or sets the accessibility. 
    /// For dummy nodes, this often defaults to <see cref="NodeAccessibility.NotApplicable"/>.
    /// </summary>
    [JsonPropertyName("accessibility")]
    [JsonRequired]
    public override NodeAccessibility Accessibility { get; init; } = NodeAccessibility.NotApplicable;

    /// <summary>
    /// Tetermines the type of node.
    /// </summary>
    [JsonPropertyName("type")]
    public override string Type => string.Empty;
}
