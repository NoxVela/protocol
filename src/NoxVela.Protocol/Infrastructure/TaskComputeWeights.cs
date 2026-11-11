using System.Collections.Generic;

namespace NoxVela.Protocol.Infrastructure;

/// <summary>
/// Task compute weight classification metadata.
/// </summary>
public static class TaskComputeWeights
{
    /// <summary>
    /// Lightweight compute task (AST parsing, basic metrics, shallow scans).
    /// </summary>
    public const string LIGHTWEIGHT = "lightweight";

    /// <summary>
    /// Heavy compute task (deep dataflow, interprocedural analysis, full call graphs).
    /// </summary>
    public const string HEAVY_COMPUTE = "heavy_compute";

    /// <summary>
    /// Standard balanced compute task.
    /// </summary>
    public const string STANDARD = "standard";

    /// <summary>
    /// Gets all standard compute weight identifiers.
    /// </summary>
    public static readonly IReadOnlyCollection<string> All = new[]
    {
        LIGHTWEIGHT,
        HEAVY_COMPUTE,
        STANDARD
    };
}
