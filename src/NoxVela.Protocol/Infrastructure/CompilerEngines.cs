using System.Collections.Generic;

namespace NoxVela.Protocol.Infrastructure;

/// <summary>
/// Canonical compiler analysis engine identifiers.
/// </summary>
public static class CompilerEngines
{
    /// <summary>
    /// Microsoft .NET Roslyn compiler platform.
    /// </summary>
    public const string ROSLYN = "roslyn";

    /// <summary>
    /// Tree-sitter incremental parsing library.
    /// </summary>
    public const string TREE_SITTER = "tree-sitter";

    /// <summary>
    /// Gets all standard compiler engine identifiers.
    /// </summary>
    public static readonly IReadOnlyCollection<string> All = new[]
    {
        ROSLYN,
        TREE_SITTER
    };
}
