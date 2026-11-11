using System.Text.Json.Serialization;
using MessagePack;

namespace NoxVela.Protocol.Models.Code;

/// <summary>
/// Represents a specific range within a source code file.
/// Used to provide precise navigation and highlighting in the UI or during reporting.
/// </summary>
[MessagePackObject(keyAsPropertyName: true)]
public record CodeLocation
{
    /// <summary> Gets the absolute or relative path to the source file. </summary>
    [JsonPropertyName("file_path")]
    [JsonRequired]
    public string FilePath { get; init; }

    /// <summary> Gets the 1-based start line number. </summary>
    [JsonPropertyName("start_line")]
    [JsonRequired]
    public int StartLine { get; init; }

    /// <summary> Gets the 1-based end line number. </summary>
    [JsonPropertyName("end_line")]
    [JsonRequired]
    public int EndLine { get; init; }

    /// <summary> Gets the 1-based start column position. </summary>
    [JsonPropertyName("start_column")]
    [JsonRequired]
    public int StartColumn { get; init; }

    /// <summary> Gets the 1-based end column position. </summary>
    [JsonPropertyName("end_column")]
    [JsonRequired]
    public int EndColumn { get; init; }

    /// <summary> Gets the source code snippet representing this location. </summary>
    [JsonPropertyName("code_snippet")]
    [JsonRequired]
    public string Snippet { get; init; } = string.Empty;
}


