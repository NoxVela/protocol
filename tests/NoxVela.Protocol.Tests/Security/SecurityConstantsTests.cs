using NoxVela.Protocol.Security;
using NUnit.Framework;

namespace NoxVela.Protocol.Tests.Security;

[TestFixture]
public class SecurityConstantsTests
{
    [Test]
    public void SecurityHeaders_Constants_MatchExpectedProtocolValues()
    {
        Assert.That(SecurityHeaders.SERVICE_KEY, Is.EqualTo("X-Service-Key"));
        Assert.That(SecurityHeaders.INSTANCE_ID, Is.EqualTo("X-Instance-Id"));
        Assert.That(SecurityHeaders.SERVICE_POOL_ID, Is.EqualTo("X-Service-Pool-Id"));
        Assert.That(SecurityHeaders.PROTOCOL_VERSION, Is.EqualTo("X-Protocol-Version"));
        Assert.That(SecurityHeaders.PROTOCOL_ENCODING, Is.EqualTo("X-Protocol-Encoding"));
        Assert.That(SecurityHeaders.AUTHORIZATION, Is.EqualTo("Authorization"));
        Assert.That(SecurityHeaders.BEARER_PREFIX, Is.EqualTo("Bearer "));
        Assert.That(SecurityHeaders.SERVICE_KEY_ENV, Is.EqualTo("NOXVELA_SERVICE_KEY"));
    }

    [Test]
    public void SecurityStatusCodes_Constants_MatchExpectedHttpCodesAndPhrases()
    {
        Assert.That(SecurityStatusCodes.UNAUTHORIZED, Is.EqualTo(401));
        Assert.That(SecurityStatusCodes.FORBIDDEN, Is.EqualTo(403));
        Assert.That(SecurityStatusCodes.TOO_MANY_REQUESTS, Is.EqualTo(429));

        Assert.That(SecurityStatusCodes.UNAUTHORIZED_REASON, Does.Contain("service key"));
        Assert.That(SecurityStatusCodes.FORBIDDEN_REASON, Does.Contain("authorized"));
        Assert.That(SecurityStatusCodes.TOO_MANY_REQUESTS_REASON, Does.Contain("limit"));
    }
}
