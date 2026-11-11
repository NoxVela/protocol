using System.ComponentModel;

namespace System.Runtime.CompilerServices;

#if !NET5_0_OR_GREATER
[EditorBrowsable(EditorBrowsableState.Never)]
internal static class IsExternalInit {}
#endif

// Polyfill attributes for 'required' keyword and C# 11 features on .NET Standard 2.1
#if !NET7_0_OR_GREATER
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
internal sealed class RequiredMemberAttribute : Attribute {}

[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
internal sealed class CompilerFeatureRequiredAttribute : Attribute
{
    public CompilerFeatureRequiredAttribute(string featureName) => FeatureName = featureName;
    public string FeatureName { get; }
    public bool IsOptional { get; set; }
    public const string RequiredMembers = nameof(RequiredMembers);
}
#endif