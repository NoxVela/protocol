using System;

namespace NoxVela.Protocol.Messages;

/// <summary>
/// Defines constant strings representing various types of service messages used in the NoxVela protocol.
/// </summary>
public class ServiceMessagesTypes
{
    /// <summary>
    /// Represents message type for worker registration.
    /// </summary>
    public const string REGISTRATION = "registration";

    /// <summary>
    /// Represents message type for heartbeat/status updates.
    /// </summary>
    public const string STATUS = "status";

    /// <summary>
    /// Represents message type for reporting progress and lifecycle status of an analysis run.
    /// </summary>
    public const string ANALYSIS_STATUS = "analysis_status";

    /// <summary>
    /// Represents message type for streaming an AST node.
    /// </summary>
    public const string NODE = "node";

    /// <summary>
    /// Represents message type for streaming a micro-batch of AST nodes.
    /// </summary>
    public const string NODES_BATCH = "nodes_batch";

    /// <summary>
    /// Represents message type for streaming a graph relation edge.
    /// </summary>
    public const string RELATION = "relation";

    /// <summary>
    /// Represents message type for streaming a micro-batch of graph relation edges.
    /// </summary>
    public const string RELATIONS_BATCH = "relations_batch";

    /// <summary>
    /// Represents message type for reporting analysis execution metrics upon completion.
    /// </summary>
    public const string ANALYSIS_METRICS = "analysis_metrics";

    /// <summary>
    /// Represents message type for acknowledging receipt of a run_analysis instruction.
    /// </summary>
    public const string RUN_ANALYSIS_ACK = "run_analysis_ack";

    /// <summary>
    /// Standardized error reporting frame dispatched by a worker upon encountering fatal or runtime errors.
    /// </summary>
    public const string ERROR = "error";
}
