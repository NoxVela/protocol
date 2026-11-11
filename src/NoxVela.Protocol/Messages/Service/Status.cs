using System.Text.Json.Serialization;

namespace NoxVela.Protocol.Messages.Service;

/// <summary>
/// Message for service status in main application.
/// </summary>
public class Status : Base
{
    /// <summary>
    /// Actual service status. For more information about avaliable statuses see <see cref="ServiceStatuses"/>.
    /// </summary>
    [JsonPropertyName("status")]
    [JsonRequired]
    public short StatusId { get; set; }
}
