namespace NoxVela.Protocol.Errors;

/// <summary>
/// Standardized catalog of error codes used across the NoxVela protocol ecosystem.
/// </summary>
public static class ProtocolErrorCodes
{
    /// <summary>Database is unreachable or failed to respond.</summary>
    public const string DATABASE_UNAVAILABLE = "DATABASE_UNAVAILABLE";

    /// <summary>Operation rate limit has been exceeded.</summary>
    public const string RATE_LIMITED = "RATE_LIMITED";

    /// <summary>Payload failed validation or could not be deserialized.</summary>
    public const string PAYLOAD_VALIDATION_ERROR = "PAYLOAD_VALIDATION_ERROR";

    /// <summary>Unexpected internal server error occurred.</summary>
    public const string UNHANDLED_SERVER_ERROR = "UNHANDLED_SERVER_ERROR";

    /// <summary>WebSocket message frame structure is malformed.</summary>
    public const string MALFORMED_FRAME = "MALFORMED_FRAME";

    // Analyzer error codes

    /// <summary>Git clone or checkout operation failed.</summary>
    public const string GIT_CLONE_FAILED = "GIT_CLONE_FAILED";

    /// <summary>Project compilation or workspace loading failed.</summary>
    public const string COMPILATION_FAILED = "COMPILATION_FAILED";

    /// <summary>Process exceeded available system memory limit.</summary>
    public const string OUT_OF_MEMORY = "OUT_OF_MEMORY";

    /// <summary>Analysis execution encountered a fatal unhandled error.</summary>
    public const string ANALYSIS_EXECUTION_FAILED = "ANALYSIS_EXECUTION_FAILED";

    /// <summary>Operation exceeded allowed execution time window.</summary>
    public const string TIMEOUT = "TIMEOUT";
}
