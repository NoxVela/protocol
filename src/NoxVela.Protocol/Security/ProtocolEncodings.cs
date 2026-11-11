namespace NoxVela.Protocol.Security;

/// <summary>
/// Standard protocol wire format encoding identifiers negotiated during the WebSocket HTTP handshake.
/// </summary>
public static class ProtocolEncodings
{
    /// <summary>
    /// Standard UTF-8 JSON text frame encoding.
    /// </summary>
    public const string JSON = "json";

    /// <summary>
    /// Binary MessagePack frame encoding.
    /// </summary>
    public const string MSGPACK = "msgpack";
}
