namespace NoxVela.Protocol.Security;

/// <summary>
/// HTTP status codes and standard reason descriptions for protocol security and authentication failures.
/// </summary>
public static class SecurityStatusCodes
{
    /// <summary>
    /// Missing or invalid service authentication key or bearer token (HTTP 401).
    /// </summary>
    public const int UNAUTHORIZED = 401;

    /// <summary>
    /// Authenticated identity lacks permission to access the requested pool or endpoint (HTTP 403).
    /// </summary>
    public const int FORBIDDEN = 403;

    /// <summary>
    /// Worker pool or client exceeded rate limits or concurrency quotas (HTTP 429).
    /// </summary>
    public const int TOO_MANY_REQUESTS = 429;

    /// <summary>
    /// Default reason phrase for unauthorized service connection attempts.
    /// </summary>
    public const string UNAUTHORIZED_REASON = "Missing or invalid service key.";

    /// <summary>
    /// Default reason phrase for forbidden service connection attempts.
    /// </summary>
    public const string FORBIDDEN_REASON = "Service identity not authorized for this worker pool.";

    /// <summary>
    /// Default reason phrase for rate limited or quota exceeded connection attempts.
    /// </summary>
    public const string TOO_MANY_REQUESTS_REASON = "Worker pool concurrency limit exceeded.";
}
