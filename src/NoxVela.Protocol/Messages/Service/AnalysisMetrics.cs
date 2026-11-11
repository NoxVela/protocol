using System;
using System.Text.Json.Serialization;

namespace NoxVela.Protocol.Messages.Service;

/// <summary>
/// Represents the metrics collected during an analysis run, including timing, duration, and generated elements.
/// </summary>
public class AnalysisMetrics : Base
{
    /// <summary>
    /// Gets or sets the unique identifier for the analysis run.
    /// </summary>
    [JsonPropertyName("run_id")]
    [JsonRequired]
    public string RunId { get; set; }
    /// <summary>
    /// Gets or sets the start time of the analysis run.
    /// </summary>
    [JsonPropertyName("started_at")]
    [JsonRequired]
    public DateTime StartedAt { get; set; }
    /// <summary>
    /// Gets or sets the end time of the analysis run.
    /// </summary>
    [JsonPropertyName("ended_at")]
    [JsonRequired]
    public DateTime EndedAt { get; set; }
    /// <summary>
    /// Gets or sets the duration of the analysis run in milliseconds.
    /// </summary>
    [JsonPropertyName("duration_ms")]
    [JsonRequired]
    public double DurationMs { get; set; }
    /// <summary>
    /// Gets or sets the number of nodes generated during the analysis.
    /// </summary>
    [JsonPropertyName("generated_nodes")]
    [JsonRequired]
    public long GeneratedNodes { get; set; }
    /// <summary>
    /// Gets or sets the number of relations generated during the analysis.
    /// </summary>
    [JsonPropertyName("generated_relations")]
    [JsonRequired]
    public long GeneratedRelations { get; set; }

    /// <summary>
    /// Gets or sets the commit hash associated with the analysis run.
    /// </summary>
    [JsonPropertyName("commit_hash")]
    public string? CommitHash { get; set; }

    /// <summary>
    /// Gets or sets the git branch associated with the analysis run.
    /// </summary>
    [JsonPropertyName("branch")]
    public string? Branch { get; set; }

    /// <summary>
    /// Gets or sets the git commit message associated with the analysis run.
    /// </summary>
    [JsonPropertyName("commit_message")]
    public string? CommitMessage { get; set; }
}
