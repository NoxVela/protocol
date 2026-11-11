using System;
using System.Text.Json.Serialization;
using MessagePack;
using NoxVela.Protocol.Infrastructure;
using NoxVela.Protocol.Models.Abstract;

namespace NoxVela.Protocol.Models.Code;

/// <summary>
/// Represents a delegate definition node in the graph database.
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public record DelegateNode : Node
{
    /// <summary>
    /// Gets or sets the visibility of the delegate.
    /// </summary>
    [JsonPropertyName("accessibility")]
    [JsonRequired]
    public override NodeAccessibility Accessibility { get; init; }

    /// <summary>
    /// Gets the file path where the delegate is defined. 
    /// <para>
    /// Unlike classes, delegates cannot be partial, so they are typically 
    /// associated with a single source location.
    /// </para>
    /// </summary>
    [JsonPropertyName("location")]
    [JsonRequired]
    public CodeLocation? Location { get; init; }

    /// <summary>
    /// Tetermines the type of node.
    /// </summary>
    [JsonPropertyName("type")]
    public override string Type => AllNodesTypes.DelegatesNodes;
}
