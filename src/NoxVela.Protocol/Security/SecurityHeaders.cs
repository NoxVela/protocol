namespace NoxVela.Protocol.Security;

/// <summary>
/// Standard HTTP and WebSocket security and handshake header constants.
/// </summary>
public static class SecurityHeaders
{
    /// <summary>
    /// Service pool pre-shared authentication key passed during WebSocket handshake.
    /// </summary>
    public const string SERVICE_KEY = "X-Service-Key";

    /// <summary>
    /// Unique worker instance identifier for horizontal replica pools.
    /// </summary>
    public const string INSTANCE_ID = "X-Instance-Id";

    /// <summary>
    /// Logical pool identifier grouping multiple worker instances.
    /// </summary>
    public const string SERVICE_POOL_ID = "X-Service-Pool-Id";

    /// <summary>
    /// Protocol version negotiated during handshake.
    /// </summary>
    public const string PROTOCOL_VERSION = "X-Protocol-Version";

    /// <summary>
    /// Default protocol version used during handshake if not explicitly specified.
    /// </summary>
    public const string DEFAULT_PROTOCOL_VERSION = "1.0.0";

    /// <summary>
    /// Protocol encoding negotiated during handshake (e.g., application/json or application/x-msgpack).
    /// </summary>
    public const string PROTOCOL_ENCODING = "X-Protocol-Encoding";

    /// <summary>
    /// Standard JSON text frame encoding value.
    /// </summary>
    public const string ENCODING_JSON = ProtocolEncodings.JSON;

    /// <summary>
    /// Binary MessagePack frame encoding value.
    /// </summary>
    public const string ENCODING_MSGPACK = ProtocolEncodings.MSGPACK;

    /// <summary>
    /// Standard HTTP Authorization header for bearer tokens or basic credentials.
    /// </summary>
    public const string AUTHORIZATION = "Authorization";

    /// <summary>
    /// Standard Bearer prefix for Authorization header tokens.
    /// </summary>
    public const string BEARER_PREFIX = "Bearer ";

    /// <summary>
    /// Canonical environment variable name for configuring the service pool key.
    /// </summary>
    public const string SERVICE_KEY_ENV = "NOXVELA_SERVICE_KEY";
}
