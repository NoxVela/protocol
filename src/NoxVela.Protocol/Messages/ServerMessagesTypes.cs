using System;

namespace NoxVela.Protocol.Messages;

/// <summary>
/// Represents the types of messages sent by the server in the NoxVela protocol.
/// This class contains constant strings defining various server message types.
/// </summary>
public class ServerMessagesTypes
{
    /// <summary>
    /// Represents message type for the orchestrator registration result response.
    /// </summary>
    public const string REGISTRATION_RESULT = "registration_result";

    /// <summary>
    /// Represents message type for triggering code analysis.
    /// </summary>
    public const string RUN_ANALYSIS = "run_analysis";

    /// <summary>
    /// Server instruction requesting immediate cancellation of an ongoing analysis run.
    /// </summary>
    public const string CANCEL_ANALYSIS = "cancel_analysis";

    /// <summary>
    /// Acknowledgement frame returned by the server upon successfully persisting a batch.
    /// </summary>
    public const string BATCH_ACK = "batch_ack";

    /// <summary>
    /// Acknowledgement frame returned by the server upon receiving a worker heartbeat status frame.
    /// </summary>
    public const string STATUS_ACK = "status_ack";

    /// <summary>
    /// Acknowledgement frame returned by the server once all nodes, relations, and metrics of an analysis run are persisted.
    /// </summary>
    public const string ANALYSIS_COMPLETED_ACK = "analysis_completed_ack";

    /// <summary>
    /// Standardized error reporting frame dispatched upon encountering critical server-side errors.
    /// </summary>
    public const string ERROR = "error";
}