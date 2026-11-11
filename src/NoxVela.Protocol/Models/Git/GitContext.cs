using System.Text.Json.Serialization;

namespace NoxVela.Protocol.Models.Git;

/// <summary>
/// Describes Git repository context for NoxVela protocol.
/// </summary>
public record class GitContext
{
    /// <summary>
    /// Commit hash.
    /// </summary>
    [JsonPropertyName("commit_hash")]
    [JsonRequired]
    public string CommitHash { get; set; }

    /// <summary>
    /// Branch name.
    /// </summary>
    [JsonPropertyName("branch_name")]
    [JsonRequired]
    public string BranchName { get; set; }

    /// <summary>
    /// Repository URL.
    /// </summary>
    [JsonPropertyName("repository_url")]
    [JsonRequired]
    public string RepositoryUrl { get; set; }

    /// <summary>
    /// Provider of AccesToken.
    /// </summary>
    [JsonPropertyName("provider")]
    [JsonRequired]
    public string Provider { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the repository is publicly accessible.
    /// </summary>
    public bool IsPublic { get; set; }

    /// <summary>
    /// Optional credentials for Git access.
    /// </summary>
    [JsonPropertyName("credentials")]
    public GitCredentials? Credentials { get; set; }

    /// <summary>
    /// Optional base commit hash for diff / incremental CI/CD scans.
    /// </summary>
    [JsonPropertyName("base_commit_hash")]
    public string? BaseCommitHash { get; set; }

    /// <summary>
    /// Optional collection of modified file paths between base commit and target commit.
    /// </summary>
    [JsonPropertyName("modified_files")]
    public List<string>? ModifiedFiles { get; set; }
}
