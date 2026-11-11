using System;
using System.Text.Json.Serialization;

namespace NoxVela.Protocol.Messages.Service;

/// <summary>
/// Represents the analysis status within the service response.
/// </summary>
/// <remarks>
/// Contains information about the state and a descriptive message.
/// </remarks>
public class AnalysisStatus : Status
{
    /// <summary>
    /// Textual message of the analysis status.
    /// </summary>
    [JsonPropertyName("message")]
    [JsonRequired]
    public required string Message { get; set; }

    /// <summary>
    /// Overall percentage of analysis completion (0.0 to 100.0).
    /// </summary>
    [JsonPropertyName("progress_percentage")]
    public double? ProgressPercentage { get; set; }

    /// <summary>
    /// Number of items processed so far (e.g. syntax trees, files, or nodes).
    /// </summary>
    [JsonPropertyName("processed_items")]
    public int? ProcessedItems { get; set; }

    /// <summary>
    /// Total number of items to process in the current phase or run.
    /// </summary>
    [JsonPropertyName("total_items")]
    public int? TotalItems { get; set; }

    /// <summary>
    /// Path or name of the file currently being processed.
    /// </summary>
    [JsonPropertyName("current_file")]
    public string? CurrentFile { get; set; }

    /// <summary>
    /// Unique run identifier associated with this status update.
    /// </summary>
    [JsonPropertyName("run_id")]
    public string? RunId { get; set; }

    /// <summary>
    /// Commit hash associated with this analysis run.
    /// </summary>
    [JsonPropertyName("commit_hash")]
    public string? CommitHash { get; set; }

    /// <summary>
    /// Git branch associated with this analysis run.
    /// </summary>
    [JsonPropertyName("branch")]
    public string? Branch { get; set; }

    /// <summary>
    /// Git commit message associated with this analysis run.
    /// </summary>
    [JsonPropertyName("commit_message")]
    public string? CommitMessage { get; set; }
}

