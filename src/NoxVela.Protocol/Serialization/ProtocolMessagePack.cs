using System;
using MessagePack;
using MessagePack.Resolvers;

namespace NoxVela.Protocol.Serialization;

/// <summary>
/// Provides standardized MessagePack serializer options and utility methods for protocol models.
/// </summary>
public static class ProtocolMessagePack
{
    /// <summary>
    /// Standard serializer options with composite resolvers supporting attribute-based and contractless resolution.
    /// </summary>
    public static readonly MessagePackSerializerOptions Options =
        MessagePackSerializerOptions.Standard
            .WithResolver(CompositeResolver.Create(
                StandardResolver.Instance,
                ContractlessStandardResolver.Instance
            ));

    /// <summary>
    /// Serializes a value of type <typeparamref name="T"/> to a byte array using MessagePack.
    /// </summary>
    public static byte[] Serialize<T>(T value) =>
        MessagePackSerializer.Serialize(value, Options);

    /// <summary>
    /// Deserializes a byte sequence to an instance of type <typeparamref name="T"/> using MessagePack.
    /// </summary>
    public static T Deserialize<T>(ReadOnlyMemory<byte> bytes) =>
        MessagePackSerializer.Deserialize<T>(bytes, Options);

    /// <summary>
    /// Deserializes a byte array to an instance of type <typeparamref name="T"/> using MessagePack.
    /// </summary>
    public static T Deserialize<T>(byte[] bytes) =>
        MessagePackSerializer.Deserialize<T>(bytes, Options);
}
