using System;
using System.Text.Json.Serialization;

namespace NoxVela.Protocol.Models.Ai;

/// <summary>
/// Instruction payload dispatched from server to AI worker to trigger micro-batch NLP embedding or finalization.
/// </summary>
public record StartEmbeddingPayload
{
    /// <summary>Unique run identifier.</summary>
    [JsonPropertyName("runId")]
    [JsonRequired]
    public string RunId { get; init; } = string.Empty;

    /// <summary>Name of the project being processed.</summary>
    [JsonPropertyName("projectName")]
    [JsonRequired]
    public string ProjectName { get; init; } = string.Empty;

    /// <summary>Optional project ID.</summary>
    [JsonPropertyName("projectId")]
    public string? ProjectId { get; init; }

    /// <summary>Natural language of code comments/docstrings.</summary>
    [JsonPropertyName("language")]
    public string Language { get; init; } = "en";

    /// <summary>Programming language (e.g. csharp).</summary>
    [JsonPropertyName("programmingLang")]
    public string ProgrammingLang { get; init; } = "csharp";

    /// <summary>Micro-batch size limit for embedding.</summary>
    [JsonPropertyName("limit")]
    public int Limit { get; init; } = 100;

    /// <summary>Action command to perform, e.g. EMBED_BATCH or FINALIZE_RUN.</summary>
    [JsonPropertyName("command")]
    public string Command { get; init; } = "EMBED_BATCH";

    /// <summary>Database connection credentials for the project namespace.</summary>
    [JsonPropertyName("dbCredentials")]
    [JsonRequired]
    public AiDatabaseCredentials DbCredentials { get; init; } = null!;
}
