using System;
using System.Text.Json.Serialization;
using NoxVela.Protocol.Models.Git;

namespace NoxVela.Protocol.Messages.Server;

/// <summary>
/// Represents a message to run analysis on a target repository context.
/// </summary>
public class RunAnalysis
{
    /// <summary>
    /// Unique execution run identifier.
    /// </summary>
    [JsonPropertyName("run_id")]
    [JsonRequired]
    public string RunId { get; set; } = string.Empty;

    /// <summary>
    /// Target repository and branch/commit context.
    /// </summary>
    [JsonPropertyName("code_context")]
    [JsonRequired]
    public GitContext CodeContext { get; set; } = null!;

    /// <summary>
    /// Optional collection of specific capabilities requested for this execution run.
    /// When null or empty, the analyzer executes its default capabilities.
    /// </summary>
    [JsonPropertyName("requested_capabilities")]
    public IReadOnlyCollection<string>? RequestedCapabilities { get; set; }

    /// <summary>
    /// Optional micro-batching configuration controlling maximum batch size and flush interval.
    /// </summary>
    [JsonPropertyName("batching_config")]
    public BatchingConfig? BatchingConfig { get; set; }

    /// <summary>
    /// Optional compute weight requirement for this analysis run ("lightweight" vs "heavy_compute").
    /// </summary>
    [JsonPropertyName("compute_weight")]
    public string? ComputeWeight { get; set; }

    /// <summary>
    /// Optional required compiler engine (e.g. "roslyn", "tree-sitter").
    /// </summary>
    [JsonPropertyName("compiler_engine")]
    public string? CompilerEngine { get; set; }

    /// <summary>
    /// Optional required compiler engine version.
    /// </summary>
    [JsonPropertyName("compiler_version")]
    public string? CompilerVersion { get; set; }
}
