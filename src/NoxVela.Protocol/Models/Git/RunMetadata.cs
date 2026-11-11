using System.Text.Json.Serialization;

namespace NoxVela.Protocol.Models.Git;

/// <summary>
/// Encapsulates execution and Git metadata for an analysis run.
/// </summary>
public record class RunMetadata
{
    /// <summary>
    /// Unique run identifier.
    /// </summary>
    [JsonPropertyName("run_id")]
    [JsonRequired]
    public string RunId { get; set; } = string.Empty;

    /// <summary>
    /// Commit hash.
    /// </summary>
    [JsonPropertyName("commit_hash")]
    [JsonRequired]
    public string CommitHash { get; set; } = string.Empty;

    /// <summary>
    /// Branch name.
    /// </summary>
    [JsonPropertyName("branch_name")]
    public string? BranchName { get; set; }

    /// <summary>
    /// Repository URL.
    /// </summary>
    [JsonPropertyName("repository_url")]
    public string? RepositoryUrl { get; set; }

    /// <summary>
    /// Commit message.
    /// </summary>
    [JsonPropertyName("commit_message")]
    public string? CommitMessage { get; set; }

    /// <summary>
    /// Commit author.
    /// </summary>
    [JsonPropertyName("author")]
    public string? Author { get; set; }
}
