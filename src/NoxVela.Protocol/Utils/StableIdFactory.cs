using System;
using System.Security.Cryptography;
using System.Text;

namespace NoxVela.Protocol.Utils;

/// <summary>
/// Provides factory methods for generating deterministic, stable identifiers and content hashes
/// used by the protocol. Supports a pluggable hash algorithm and constructs IDs for symbols,
/// relationships, external references, and attributes.
/// </summary>
public static class StableIdFactory
{
    /// <summary>
    /// The hashing strategy used by the protocol.
    /// This can be overridden by the hosting application for better performance (e.g., using .NET 10 SHA256.HashData).
    /// </summary>
    public static Func<string, string> HashAlgorithm { get; set; } = DefaultSha256Hash;

    /// <summary>
    /// Computes a canonical SHA-256 hash of the input string using the currently configured <see cref="HashAlgorithm"/>.
    /// The standard format is a 64-character lowercase hexadecimal string encoded from UTF-8 bytes.
    /// </summary>
    /// <param name="input">The input string to hash.</param>
    /// <returns>A deterministic lowercase 64-character hex hash, or an empty string when <paramref name="input"/> is null or empty.</returns>
    public static string GenerateHash(string input) => HashAlgorithm(input);

    private static string DefaultSha256Hash(string input)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;

#if NET6_0_OR_GREATER || NET10_0_OR_GREATER
        int byteCount = Encoding.UTF8.GetByteCount(input);
        byte[]? rented = null;
        Span<byte> sourceBytes = byteCount <= 512
            ? stackalloc byte[byteCount]
            : (rented = System.Buffers.ArrayPool<byte>.Shared.Rent(byteCount)).AsSpan(0, byteCount);

        try
        {
            Encoding.UTF8.GetBytes(input, sourceBytes);
            Span<byte> hash = stackalloc byte[32];
            SHA256.HashData(sourceBytes, hash);
            return Convert.ToHexStringLower(hash);
        }
        finally
        {
            if (rented != null)
            {
                System.Buffers.ArrayPool<byte>.Shared.Return(rented);
            }
        }
#else
        using var sha256 = SHA256.Create();
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        byte[] hashBytes = sha256.ComputeHash(inputBytes);

        var sb = new StringBuilder(hashBytes.Length * 2);

        foreach (byte b in hashBytes)
        {
            sb.Append(b.ToString("x2"));
        }

        return sb.ToString();
#endif
    }

    /// <summary>
    /// Generates a stable identifier for a symbol based on its documentation ID and assembly name.
    /// Canonical formula: <c>SHA256("{assemblyName}::{documentationId}")</c>.
    /// </summary>
    /// <param name="documentationId">The documentation ID of the symbol (e.g. "M:Namespace.Class.Method").</param>
    /// <param name="assemblyName">The name of the assembly containing the symbol. Defaults to "Unknown".</param>
    /// <returns>A deterministic 64-character lowercase hex hash identifier for the symbol.</returns>
    public static string CreateDeterministicHashId(string documentationId, string assemblyName = "Unknown")
    {
        return GenerateHash($"{assemblyName}::{documentationId}");
    }

    /// <summary>
    /// Generates a stable content hash for the provided source data or body text.
    /// Canonical formula: <c>SHA256("{data}")</c>.
    /// </summary>
    /// <param name="data">The source data or code text to hash.</param>
    /// <returns>A deterministic 64-character lowercase hex content hash.</returns>
    public static string CreateDeterministicContentHashId(string data)
    {
        return GenerateHash(data);
    }

    /// <summary>
    /// Generates a unique canonical identifier for a directional relationship between two nodes.
    /// <list type="bullet">
    ///   <item><description>Direct relation (<paramref name="pending"/> = false): <c>SHA256("{fromContentHash}|{relation}|{toContentHash}")</c></description></item>
    ///   <item><description>Pending relation (<paramref name="pending"/> = true): <c>SHA256("{fromContentHash}|{relation}|{toContentHash}|pending")</c></description></item>
    /// </list>
    /// </summary>
    /// <param name="fromContentHash">The content hash of the source node.</param>
    /// <param name="toContentHash">The content hash (direct) or stable ID (pending) of the target node.</param>
    /// <param name="relation">The semantic type of the relationship (e.g. "CALLS", "CONTAINS", "IMPLEMENTS").</param>
    /// <param name="pending">If true, marks the relation as unresolved/pending resolution during graph streaming.</param>
    /// <returns>A deterministic 64-character lowercase hex composite hash identifying the unique edge.</returns>
    public static string CreateRelationId(string fromContentHash, string toContentHash, string relation, bool pending = false)
    {
        string suffix = pending ? "|pending" : string.Empty;
        return GenerateHash($"{fromContentHash}|{relation}|{toContentHash}{suffix}");
    }

    /// <summary>
    /// Generates a content hash for symbols that do not have source code available (e.g., compiled standard libraries or external packages).
    /// Canonical formula: <c>SHA256("EXTERNAL_CONTENT::{externalStableId}")</c>.
    /// </summary>
    /// <param name="externalStableId">The stable ID of the external symbol or artifact.</param>
    /// <returns>A deterministic 64-character lowercase hex external content hash.</returns>
    public static string CreateExternalContentHashId(string externalStableId)
    {
        return GenerateHash($"EXTERNAL_CONTENT::{externalStableId}");
    }

    /// <summary>
    /// Generates a stable identifier for an attribute based on its parent's stable ID and its signature.
    /// Canonical formula: <c>SHA256("ATTRIBUTE_STABLE::{parentStableId}::{attributeSignature}::{index}")</c>.
    /// </summary>
    /// <param name="parentStableId">The stable ID of the parent symbol declaration.</param>
    /// <param name="index">The zero-based positional index of the attribute instance on the symbol.</param>
    /// <param name="attributeSignature">The signature or type name of the attribute.</param>
    /// <returns>A deterministic 64-character lowercase hex attribute stable ID.</returns>
    public static string CreateAttributeStableId(string parentStableId, int index, string attributeSignature = "UnknownAttribute")
    {
        return GenerateHash($"ATTRIBUTE_STABLE::{parentStableId}::{attributeSignature}::{index}");
    }

    /// <summary>
    /// Generates a unique content hash for an attribute, distinct from its parent symbol.
    /// Incorporates the attribute's source code or metadata string plus an index for multiples.
    /// Canonical formula: <c>SHA256("ATTRIBUTE_CONTENT::{parentContentHash}::{attributeContent}::{index}")</c>.
    /// </summary>
    /// <param name="parentContentHash">The content hash of the parent symbol.</param>
    /// <param name="index">The zero-based positional index of this attribute within the parent context.</param>
    /// <param name="attributeContent">The attribute content, argument expressions, or signature string.</param>
    /// <returns>A deterministic 64-character lowercase hex attribute content hash.</returns>
    public static string CreateAttributeContentHashId(string parentContentHash, int index, string attributeContent = "UnknownAttribute")
    {
        return GenerateHash($"ATTRIBUTE_CONTENT::{parentContentHash}::{attributeContent}::{index}");
    }
}
