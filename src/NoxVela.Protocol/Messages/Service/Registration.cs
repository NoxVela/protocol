using System;
using System.Text.Json.Serialization;

namespace NoxVela.Protocol.Messages.Service;

/// <summary>
/// Message for registration service in main application.
/// </summary>
public class Registration : Base
{
    /// <summary>
    /// Name of a service.
    /// </summary>
    [JsonPropertyName("serviceName")]
    [JsonRequired]
    public string ServiceName { get; set; }
    /// <summary>
    /// Legimitive version of service.
    /// </summary>
    [JsonPropertyName("version")]
    [JsonRequired]
    public string Version { get; set; }
    /// <summary>
    /// Capabilities of a service
    /// </summary>
    [JsonPropertyName("capabilities")]
    [JsonRequired]
    public IEnumerable<string> Capabilities { get; set; }
    /// <summary>
    /// Supported languages for service. 
    /// <para>
    /// Applicable for analyzers.
    /// </para>
    /// </summary>
    [JsonPropertyName("supportedLanguages")]
    [JsonRequired]
    public IEnumerable<string>? SupportedLanguages { get; set; }
    /// <summary>
    /// Type of a service.
    /// </summary>
    [JsonPropertyName("serviceType")]
    [JsonRequired]
    public int ServiceType { get; set; }

    /// <summary>
    /// Optional logical pool identifier grouping multiple worker instances.
    /// </summary>
    [JsonPropertyName("poolId")]
    public string? PoolId { get; set; }

    /// <summary>
    /// Optional service pool authentication key passed in registration payload.
    /// </summary>
    [JsonPropertyName("serviceKey")]
    public string? ServiceKey { get; set; }

    /// <summary>
    /// Optional compiler engine declaration (e.g. "roslyn", "tree-sitter").
    /// </summary>
    [JsonPropertyName("compilerEngine")]
    public string? CompilerEngine { get; set; }

    /// <summary>
    /// Optional compiler engine version (e.g. "4.8.0", "0.22.0").
    /// </summary>
    [JsonPropertyName("compilerVersion")]
    public string? CompilerVersion { get; set; }

    /// <summary>
    /// Optional compute weight capacity declaration ("lightweight" vs "heavy_compute").
    /// </summary>
    [JsonPropertyName("computeWeight")]
    public string? ComputeWeight { get; set; }

    /// <summary>
    /// Negotiated protocol version supported by this service (e.g. "1.0.0", "1.1.0").
    /// </summary>
    [JsonPropertyName("protocolVersion")]
    public string? ProtocolVersion { get; set; }
}
