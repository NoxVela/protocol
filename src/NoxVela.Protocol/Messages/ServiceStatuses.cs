namespace NoxVela.Protocol.Messages;

/// <summary>
/// Represents the status of a service in the NoxVela protocol.
/// </summary>
public enum ServiceStatuses : short
{
    /// <summary>
    /// The service is idle and not performing any tasks.
    /// </summary>
    IDLE = 0,

    /// <summary>
    /// The service is actively working on a task.
    /// </summary>
    WORKING = 1,

    /// <summary>
    /// The service has encountered an error.
    /// </summary>
    ERROR = 2,

    /// <summary>
    /// The service is in the process of completing a task.
    /// </summary>
    COMPLETING = 3,

    /// <summary>
    /// The service has successfully completed its task.
    /// </summary>
    DONE = 4,

    /// <summary>
    /// The status of the service is unknown.
    /// </summary>
    UNKNOWN = 5,

    /// <summary>
    /// Service disconnected from the server.
    /// <para>
    /// Assinged by server in registry for service status, after disconnection.
    /// </para>
    /// </summary>
    DISCONNECTED = 6
}
