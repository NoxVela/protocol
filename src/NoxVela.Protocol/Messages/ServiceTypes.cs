using System;

namespace NoxVela.Protocol.Messages;

/// <summary>
/// Enumeration of service types in the NoxVela ecosystem.
/// </summary>
public enum ServiceType
{
    /// <summary>
    /// Service that analyzes code and provides AST nodes and relations.
    /// </summary>
    ANALYZER = 0,

    /// <summary>
    /// Service that provides AI/GNN model analysis, embeddings, and intelligent code queries.
    /// </summary>
    AI = 100,
}

/// <summary>
/// Types of services constants and helpers.
/// </summary>
public class ServiceTypes
{
    /// <summary>
    /// Service that analyzes code and provides nodes and edges.
    /// </summary>
    public const int ANALYZER = (int)ServiceType.ANALYZER;

    /// <summary>
    /// Service that provides AI/GNN model analysis and embeddings.
    /// </summary>
    public const int AI = (int)ServiceType.AI;

    /// <summary>
    /// Converts numeric service type to its uppercase canonical string representation.
    /// </summary>
    public static string ToName(int serviceType) => (ServiceType)serviceType switch
    {
        ServiceType.ANALYZER => nameof(ServiceType.ANALYZER),
        ServiceType.AI => nameof(ServiceType.AI),
        _ => serviceType.ToString()
    };

    /// <summary>
    /// Tries to parse service type name or numeric string into integer value.
    /// </summary>
    public static bool TryParse(string name, out int serviceType)
    {
        if (Enum.TryParse<ServiceType>(name, true, out var parsed))
        {
            serviceType = (int)parsed;
            return true;
        }

        if (int.TryParse(name, out serviceType))
        {
            return true;
        }

        serviceType = default;
        return false;
    }
}
