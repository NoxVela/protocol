using System.Collections.Generic;

namespace NoxVela.Protocol.Infrastructure;

/// <summary>
/// Aggregates canonical string constants for analyzer capabilities.
/// Capabilities remain open-ended strings to support custom and community analyzer extensions.
/// </summary>
public static class AnalyzerCapabilities
{
    /// <summary>
    /// Core code structure parsing: classes, methods, fields, properties, and structural relations (CONTAINS, DEFINES).
    /// </summary>
    public const string SYNTAX_TREE_PARSING = "syntax_tree_parsing";

    /// <summary>
    /// Call graph and reference flow analysis (CALLS, READS, WRITES, INSTANTIATES).
    /// </summary>
    public const string CALL_GRAPH = "call_graph";

    /// <summary>
    /// Type hierarchy, interface implementations, and inheritance trees (EXTENDS, IMPLEMENTS, OF_TYPE).
    /// </summary>
    public const string TYPE_HIERARCHY = "type_hierarchy";

    /// <summary>
    /// Code complexity and structural metrics (cyclomatic complexity, maintainability index, lines of code).
    /// </summary>
    public const string METRICS = "metrics";

    /// <summary>
    /// External package, framework, and third-party dependency analysis (external_types, DEPENDS_ON).
    /// </summary>
    public const string DEPENDENCY_ANALYSIS = "dependency_analysis";

    /// <summary>
    /// Static analysis targeting resource and memory leak vulnerabilities (IDisposable, unmanaged handles).
    /// </summary>
    public const string MEMORY_LEAK_DETECTION = "memory_leak_detection";

    /// <summary>
    /// Fundamental vulnerability detection and secure coding pattern scans.
    /// </summary>
    public const string SECURITY_SCAN = "security_scan";

    /// <summary>
    /// Roslyn compiler engine analysis capability.
    /// </summary>
    public const string ENGINE_ROSLYN = "engine:roslyn";

    /// <summary>
    /// Tree-sitter AST parser compiler engine capability.
    /// </summary>
    public const string ENGINE_TREESITTER = "engine:tree_sitter";

    /// <summary>
    /// Lightweight compute task capacity flag.
    /// </summary>
    public const string COMPUTE_LIGHTWEIGHT = "compute:lightweight";

    /// <summary>
    /// Heavy compute task capacity flag.
    /// </summary>
    public const string COMPUTE_HEAVY = "compute:heavy_compute";

    /// <summary>
    /// Gets all standard analyzer capability constants.
    /// </summary>
    public static readonly IReadOnlyCollection<string> All = new[]
    {
        SYNTAX_TREE_PARSING,
        CALL_GRAPH,
        TYPE_HIERARCHY,
        METRICS,
        DEPENDENCY_ANALYSIS,
        MEMORY_LEAK_DETECTION,
        SECURITY_SCAN,
        ENGINE_ROSLYN,
        ENGINE_TREESITTER,
        COMPUTE_LIGHTWEIGHT,
        COMPUTE_HEAVY
    };
}
