using System;

namespace NoxVela.Protocol.Infrastructure;

/// <summary>
/// Static collection of standardized relationship types used within the graph.
/// These constants define the edges between various nodes in the database.
/// </summary>
public class NodesRelations
{
    // --- Structural & Hierarchical Layer ---

    /// <summary> A container that contains a type. </summary>
    public const string CONTAINS = nameof(CONTAINS);

    /// <summary> A type defines a member. </summary>
    public const string DEFINES = nameof(DEFINES);

    /// <summary> A method or property has a specific formal parameter. </summary>
    public const string HAS_PARAMETER = nameof(HAS_PARAMETER);

    /// <summary> A method or delegate returns a specific type. </summary>
    public const string RETURNS = nameof(RETURNS);

    // --- Typology Layer (Inheritance & Implementation) ---

    /// <summary> A class inherits from a base class. </summary>
    public const string EXTENDS = nameof(EXTENDS);

    /// <summary> A class, struct, or interface implements an interface. </summary>
    public const string IMPLEMENTS = nameof(IMPLEMENTS);

    // --- Behavioral Layer (Functional logic) ---

    /// <summary> A method invokes another method or local function. </summary>
    public const string CALLS = nameof(CALLS);

    /// <summary> A method or property accessor reads from a field. </summary>
    public const string READS = nameof(READS);

    /// <summary> A method or property accessor writes to a field. </summary>
    public const string WRITES = nameof(WRITES);

    /// <summary> A method raises/fires an event. </summary>
    public const string FIRES = nameof(FIRES);

    /// <summary> A method or block explicitly throws an exception type. </summary>
    public const string THROWS = nameof(THROWS);

    /// <summary> A method or try-catch block handles a specific exception type. </summary>
    public const string CATCHES = nameof(CATCHES);

    // --- Metadata & Typing Layer ---

    /// <summary> Defines the data type of a field, property, or parameter. </summary>
    public const string OF_TYPE = nameof(OF_TYPE);

    /// <summary> A method creates a new instance of a type (eg. using 'new' operator). </summary>
    public const string INSTANTIATES = nameof(INSTANTIATES);

    /// <summary> An extension method extends a specific type. </summary>
    public const string EXTENDS_TYPE = nameof(EXTENDS_TYPE);

    /// <summary> Defines relation of for attributes. </summary>
    public const string HAS_ATTRIBUTE = nameof(HAS_ATTRIBUTE);

    /// <summary> Reference to a specific type. </summary>
    public const string REFERENCES_TYPE = nameof(REFERENCES_TYPE);

    /// <summary> Dependence. </summary>
    public const string DEPENDS_ON = nameof(DEPENDS_ON);

    /// <summary> Represents unresolved dependency. </summary>
    public const string UNRESOLVED_DEPENDS_ON = nameof(UNRESOLVED_DEPENDS_ON);
}
