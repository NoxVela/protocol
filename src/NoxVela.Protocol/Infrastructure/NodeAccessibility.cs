namespace NoxVela.Protocol.Infrastructure;

/// <summary>
/// Defines the accessibility levels for code symbols in a database-friendly format.
/// Values are ordered by their restrictiveness, allowing for range-based filtering.
/// </summary>
public enum NodeAccessibility : short
{
    /// <summary> Default state for symbols without accessibility (e.g., static constructors or local variables). </summary>
    NotApplicable = 0,

    /// <summary> Accessible only within the same class. </summary>
    Private = 1,

    /// <summary> Accessible within the same assembly AND by derived classes. </summary>
    ProtectedAndInternal = 2,

    /// <summary> Alias for <see cref="ProtectedAndInternal"/> commonly used in VB.NET context. </summary>
    ProtectedAndFriend = ProtectedAndInternal,

    /// <summary> Accessible within the same class or by derived classes. </summary>
    Protected = 3,

    /// <summary> Accessible within the same assembly. </summary>
    Internal = 4,

    /// <summary> Alias for <see cref="Internal"/> commonly used in VB.NET context. </summary>
    Friend = Internal,

    /// <summary> Accessible within the same assembly OR by derived classes. </summary>
    ProtectedOrInternal = 5,

    /// <summary> Alias for <see cref="ProtectedOrInternal"/> commonly used in VB.NET context. </summary>
    ProtectedOrFriend = ProtectedOrInternal,

    /// <summary> Access is not restricted. </summary>
    Public = 6
}