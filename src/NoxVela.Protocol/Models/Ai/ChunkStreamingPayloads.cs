using System;
using System.Text.Json.Serialization;

namespace NoxVela.Protocol.Models.Ai;

/// <summary>
/// Streaming chunk response payload returned by LLM assistant.
/// </summary>
public record ChunkResponsePayload
{
    /// <summary>Partial text chunk received from LLM.</summary>
    [JsonPropertyName("chunk")]
    [JsonRequired]
    public string Chunk { get; init; } = string.Empty;

    /// <summary>SignalR target connection ID to stream chunks back to the client.</summary>
    [JsonPropertyName("targetConnectionId")]
    [JsonRequired]
    public string TargetConnectionId { get; init; } = string.Empty;

    /// <summary>Sequential index of this chunk.</summary>
    [JsonPropertyName("chunkIndex")]
    public int? ChunkIndex { get; init; }

    /// <summary>Optional conversation identifier.</summary>
    [JsonPropertyName("conversationId")]
    public string? ConversationId { get; init; }
}

/// <summary>
/// Completion notification payload returned after final chunk is streamed.
/// </summary>
public record ChunkCompletePayload
{
    /// <summary>Status code, defaults to "complete".</summary>
    [JsonPropertyName("status")]
    public string Status { get; init; } = "complete";

    /// <summary>SignalR target connection ID.</summary>
    [JsonPropertyName("targetConnectionId")]
    [JsonRequired]
    public string TargetConnectionId { get; init; } = string.Empty;

    /// <summary>Optional conversation identifier.</summary>
    [JsonPropertyName("conversationId")]
    public string? ConversationId { get; init; }
}

/// <summary>
/// Retrieved RAG context payload providing relevant AST code snippets and graph nodes.
/// </summary>
public record RagContextPayload
{
    /// <summary>SignalR target connection ID.</summary>
    [JsonPropertyName("targetConnectionId")]
    [JsonRequired]
    public string TargetConnectionId { get; init; } = string.Empty;

    /// <summary>Retrieved RAG context information (nodes, symbols, files).</summary>
    [JsonPropertyName("context")]
    [JsonRequired]
    public object? Context { get; init; }

    /// <summary>Optional conversation identifier.</summary>
    [JsonPropertyName("conversationId")]
    public string? ConversationId { get; init; }
}
