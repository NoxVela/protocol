using System;

namespace NoxVela.Protocol.Infrastructure;

/// <summary>
/// Constants and negotiation logic for NoxVela protocol versions.
/// </summary>
public static class ProtocolVersions
{
    /// <summary>
    /// Initial baseline protocol release (1.0.0).
    /// </summary>
    public const string V1_0_0 = "1.0.0";

    /// <summary>
    /// Protocol version 1.1.0 introducing micro-batching (nodes_batch, relations_batch), ACK frames, and service pool keys.
    /// </summary>
    public const string V1_1_0 = "1.1.0";

    /// <summary>
    /// Current standard protocol version emitted by NoxVela components.
    /// </summary>
    public const string CURRENT = V1_1_0;

    /// <summary>
    /// Minimum protocol version required to connect to the NoxVela Server.
    /// </summary>
    public const string MINIMUM_SUPPORTED = V1_0_0;

    /// <summary>
    /// Evaluates whether a reported protocol version string is semantically compatible with the minimum required version.
    /// Compatible versions share the same major version and are greater than or equal to the minimum.
    /// </summary>
    /// <param name="reportedVersion">Reported version string (e.g. "1.1.0", "v1.0.0", "1.1.0-beta").</param>
    /// <param name="minimumSupported">Minimum supported version (defaults to MINIMUM_SUPPORTED).</param>
    /// <returns>True if compatible; otherwise false.</returns>
    public static bool IsCompatible(string? reportedVersion, string minimumSupported = MINIMUM_SUPPORTED)
    {
        if (string.IsNullOrWhiteSpace(reportedVersion))
            return false;

        if (!TryParseSemantic(reportedVersion, out var reportedMajor, out var reportedMinor, out var reportedPatch))
            return false;

        if (!TryParseSemantic(minimumSupported, out var minMajor, out var minMinor, out var minPatch))
            return false;

        if (reportedMajor != minMajor)
            return false;

        if (reportedMajor > minMajor)
            return true;

        if (reportedMinor > minMinor)
            return true;

        if (reportedMinor < minMinor)
            return false;

        return reportedPatch >= minPatch;
    }

    /// <summary>
    /// Checks whether the specified version supports micro-batching (nodes_batch and relations_batch),
    /// which requires protocol version 1.1.0 or higher.
    /// </summary>
    /// <param name="reportedVersion">Reported version string.</param>
    /// <returns>True if version >= 1.1.0 with major 1.</returns>
    public static bool SupportsBatching(string? reportedVersion)
    {
        return IsCompatible(reportedVersion, V1_1_0);
    }

    /// <summary>
    /// Parses a semantic version string into major, minor, and patch numbers.
    /// Handles optional 'v' prefix and prerelease tags (e.g. "v1.1.0-alpha.1").
    /// </summary>
    /// <param name="input">Version string to parse.</param>
    /// <param name="major">Parsed major version.</param>
    /// <param name="minor">Parsed minor version.</param>
    /// <param name="patch">Parsed patch version.</param>
    /// <returns>True if parsing succeeded; otherwise false.</returns>
    public static bool TryParseSemantic(string? input, out int major, out int minor, out int patch)
    {
        major = 0;
        minor = 0;
        patch = 0;

        if (string.IsNullOrWhiteSpace(input))
            return false;

        var span = input.Trim().AsSpan();
        if (span.StartsWith("v", StringComparison.OrdinalIgnoreCase))
            span = span.Slice(1);

        var hyphenIdx = span.IndexOf('-');
        if (hyphenIdx >= 0)
            span = span.Slice(0, hyphenIdx);

        var firstDot = span.IndexOf('.');
        if (firstDot <= 0)
            return false;

        if (!int.TryParse(span.Slice(0, firstDot).ToString(), out major) || major < 0)
            return false;

        var rest = span.Slice(firstDot + 1);
        var secondDot = rest.IndexOf('.');
        if (secondDot < 0)
        {
            if (!int.TryParse(rest.ToString(), out minor) || minor < 0)
                return false;
            patch = 0;
            return true;
        }

        if (!int.TryParse(rest.Slice(0, secondDot).ToString(), out minor) || minor < 0)
            return false;

        var patchSpan = rest.Slice(secondDot + 1);
        if (!int.TryParse(patchSpan.ToString(), out patch) || patch < 0)
            return false;

        return true;
    }
}
