using NoxVela.Protocol.Infrastructure;
using NoxVela.Protocol.Security;
using NUnit.Framework;

namespace NoxVela.Protocol.Tests.Infrastructure;

[TestFixture]
public class ProtocolVersionsTests
{
    [Test]
    public void Constants_MatchExpectedSemanticVersions()
    {
        Assert.That(ProtocolVersions.V1_0_0, Is.EqualTo("1.0.0"));
        Assert.That(ProtocolVersions.V1_1_0, Is.EqualTo("1.1.0"));
        Assert.That(ProtocolVersions.CURRENT, Is.EqualTo("1.1.0"));
        Assert.That(ProtocolVersions.MINIMUM_SUPPORTED, Is.EqualTo("1.0.0"));

        Assert.That(SecurityHeaders.PROTOCOL_VERSION, Is.EqualTo("X-Protocol-Version"));
        Assert.That(SecurityHeaders.DEFAULT_PROTOCOL_VERSION, Is.EqualTo("1.0.0"));
    }

    [TestCase("1.0.0", true)]
    [TestCase("1.1.0", true)]
    [TestCase("1.2.3", true)]
    [TestCase("v1.1.0", true)]
    [TestCase("1.1.0-alpha.1", true)]
    [TestCase("1.0.0-rc1", true)]
    [TestCase("0.9.0", false)]
    [TestCase("0.9.9", false)]
    [TestCase("2.0.0", false)]
    [TestCase("2.1.0", false)]
    [TestCase("", false)]
    [TestCase("   ", false)]
    [TestCase(null, false)]
    [TestCase("invalid", false)]
    public void IsCompatible_EvaluatesVersionCompatibilityCorrectly(string? version, bool expectedCompatible)
    {
        var result = ProtocolVersions.IsCompatible(version);
        Assert.That(result, Is.EqualTo(expectedCompatible));
    }

    [TestCase("1.0.0", false)]
    [TestCase("1.0.9", false)]
    [TestCase("1.1.0", true)]
    [TestCase("1.1.1", true)]
    [TestCase("1.2.0", true)]
    [TestCase("v1.1.0", true)]
    [TestCase("0.9.0", false)]
    [TestCase("2.0.0", false)]
    [TestCase(null, false)]
    public void SupportsBatching_RequiresVersion1_1_OrHigher(string? version, bool expectedSupports)
    {
        var result = ProtocolVersions.SupportsBatching(version);
        Assert.That(result, Is.EqualTo(expectedSupports));
    }

    [Test]
    public void TryParseSemantic_ParsesVariousFormatsSuccessfully()
    {
        Assert.That(ProtocolVersions.TryParseSemantic("1.2.3", out var maj1, out var min1, out var pat1), Is.True);
        Assert.That(maj1, Is.EqualTo(1));
        Assert.That(min1, Is.EqualTo(2));
        Assert.That(pat1, Is.EqualTo(3));

        Assert.That(ProtocolVersions.TryParseSemantic("v2.5.0-preview.2", out var maj2, out var min2, out var pat2), Is.True);
        Assert.That(maj2, Is.EqualTo(2));
        Assert.That(min2, Is.EqualTo(5));
        Assert.That(pat2, Is.EqualTo(0));

        Assert.That(ProtocolVersions.TryParseSemantic("1.0", out var maj3, out var min3, out var pat3), Is.True);
        Assert.That(maj3, Is.EqualTo(1));
        Assert.That(min3, Is.EqualTo(0));
        Assert.That(pat3, Is.EqualTo(0));
    }
}
