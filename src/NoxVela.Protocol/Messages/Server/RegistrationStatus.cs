using System;
using System.Text.Json.Serialization;
using NoxVela.Protocol.Messages.Service;

namespace NoxVela.Protocol.Messages.Server;

/// <summary>
/// Response for <see cref="Registration"/>.
/// </summary>
public class RegistrationStatus
{
    /// <summary>
    /// Status of registration process can have value on of <see cref="RegistrationStatuses"/>.
    /// </summary>
    [JsonPropertyName("status")]
    [JsonRequired]
    public short Status { get; set; }
}
