namespace NoxVela.Protocol.Messages.Server;

/// <summary>
/// Represents the possible statuses for a registration process.
/// </summary>
public enum RegistrationStatuses : short
{
    /// <summary>
    /// Indicates that the registration was successfully accepted.
    /// </summary>
    ACCEPTED = 0,
    /// <summary>
    /// Indicates that the registration was rejected.
    /// </summary>
    REJECTED = 1,
}
