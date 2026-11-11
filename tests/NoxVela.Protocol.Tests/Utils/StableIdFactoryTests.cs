using System;
using NoxVela.Protocol.Utils;
using NUnit.Framework;

namespace NoxVela.Protocol.Tests.Utils;

[TestFixture]
public class StableIdFactoryTests
{
    private Func<string, string> _originalHashAlgorithm = null!;

    [SetUp]
    public void SetUp()
    {
        _originalHashAlgorithm = StableIdFactory.HashAlgorithm;
    }

    [TearDown]
    public void TearDown()
    {
        StableIdFactory.HashAlgorithm = _originalHashAlgorithm;
    }

    [Test]
    public void GenerateHash_NullOrEmpty_ReturnsEmptyString()
    {
        Assert.That(StableIdFactory.GenerateHash(null!), Is.EqualTo(string.Empty));
        Assert.That(StableIdFactory.GenerateHash(string.Empty), Is.EqualTo(string.Empty));
    }

    [Test]
    public void GenerateHash_SameInput_ProducesDeterministicHash()
    {
        const string input = "MyApp.Models.User";
        var hash1 = StableIdFactory.GenerateHash(input);
        var hash2 = StableIdFactory.GenerateHash(input);

        Assert.That(hash1, Is.Not.Null.And.Not.Empty);
        Assert.That(hash1, Is.EqualTo(hash2));
        Assert.That(hash1.Length, Is.EqualTo(64)); // SHA-256 hex string length
    }

    [Test]
    public void CreateDeterministicHashId_DifferentInputs_ProduceDifferentHashes()
    {
        var id1 = StableIdFactory.CreateDeterministicHashId("M:MyApp.Service.DoWork", "MyApp");
        var id2 = StableIdFactory.CreateDeterministicHashId("M:MyApp.Service.DoOtherWork", "MyApp");

        Assert.That(id1, Is.Not.EqualTo(id2));
    }

    [Test]
    public void CreateRelationId_PendingVsNonPending_ProduceDifferentHashes()
    {
        const string from = "source-hash";
        const string to = "target-hash";
        const string rel = "CALLS";

        var directId = StableIdFactory.CreateRelationId(from, to, rel, pending: false);
        var pendingId = StableIdFactory.CreateRelationId(from, to, rel, pending: true);

        Assert.That(directId, Is.Not.EqualTo(pendingId));
        Assert.That(directId.Length, Is.EqualTo(64));
        Assert.That(pendingId.Length, Is.EqualTo(64));
    }

    [Test]
    public void CreateExternalContentHashId_IncludesPrefixInHash()
    {
        const string externalId = "System.String";
        var hash = StableIdFactory.CreateExternalContentHashId(externalId);

        Assert.That(hash, Is.Not.Null.And.Not.Empty);
        Assert.That(hash.Length, Is.EqualTo(64));
        // Must equal GenerateHash("EXTERNAL_CONTENT::" + externalId)
        var expected = StableIdFactory.GenerateHash($"EXTERNAL_CONTENT::{externalId}");
        Assert.That(hash, Is.EqualTo(expected));
    }

    [Test]
    public void CreateAttributeStableId_IncludesIndexAndSignature()
    {
        const string parentId = "parent-stable-id";
        var attr0 = StableIdFactory.CreateAttributeStableId(parentId, 0, "ObsoleteAttribute");
        var attr1 = StableIdFactory.CreateAttributeStableId(parentId, 1, "ObsoleteAttribute");

        Assert.That(attr0, Is.Not.EqualTo(attr1));
        var expected = StableIdFactory.GenerateHash($"ATTRIBUTE_STABLE::{parentId}::ObsoleteAttribute::0");
        Assert.That(attr0, Is.EqualTo(expected));
    }

    [Test]
    public void CreateAttributeContentHashId_IncludesParentContentHashAndIndex()
    {
        const string parentContentHash = "parent-content-hash";
        var content0 = StableIdFactory.CreateAttributeContentHashId(parentContentHash, 0, "[Obsolete]");
        var content1 = StableIdFactory.CreateAttributeContentHashId(parentContentHash, 1, "[Obsolete]");

        Assert.That(content0, Is.Not.EqualTo(content1));
    }

    [Test]
    public void PluggableHashAlgorithm_CanBeOverridden()
    {
        // Arrange
        StableIdFactory.HashAlgorithm = input => $"custom_hash_{input}";

        // Act
        var result = StableIdFactory.GenerateHash("test");

        // Assert
        Assert.That(result, Is.EqualTo("custom_hash_test"));
    }

    [Test]
    public void CanonicalFormulas_MatchExpectedReferenceTestVectors()
    {
        // 1. Symbol Stable ID
        var symbolId = StableIdFactory.CreateDeterministicHashId("M:NoxVela.OrderService.Checkout", "NoxVela");
        Assert.That(symbolId, Is.EqualTo("2f41a79b5b9fa3c6cdcdcbf8936f7002214b369d347dea07771533b0b96925a2"));

        // 2. Direct Relation ID
        var directRelId = StableIdFactory.CreateRelationId("hash_from", "hash_to", "CALLS", pending: false);
        Assert.That(directRelId, Is.EqualTo("e039d779a930cf5dc1d02acd9eeeac8d8d1856d782f1ce46398c8bb3eec7f48b"));

        // 3. Pending Relation ID
        var pendingRelId = StableIdFactory.CreateRelationId("hash_from", "hash_to", "CALLS", pending: true);
        Assert.That(pendingRelId, Is.EqualTo("a29cc1034694ea1e6c6a58339e84838e1892166d9bc50abd228246d977b67810"));

        // 4. External Content Hash
        var extContentHash = StableIdFactory.CreateExternalContentHashId("ext_stable_id_123");
        Assert.That(extContentHash, Is.EqualTo("8a5e1fa17ab4f175e334bdefb83a657930803cd0f7068bbb546ad63e5d114cc0"));

        // 5. Attribute Stable ID
        var attrStableId = StableIdFactory.CreateAttributeStableId("parent_stable_456", 0, "System.ObsoleteAttribute");
        Assert.That(attrStableId, Is.EqualTo("c64a19213c7925dd0fefc0e45f832d0431213902c92fcb21bd6ae770f75c293a"));

        // 6. Attribute Content Hash
        var attrContentHash = StableIdFactory.CreateAttributeContentHashId("parent_content_789", 0, "[Obsolete]");
        Assert.That(attrContentHash, Is.EqualTo("34181500b8af11ca8f0e6485096c8d6402641b408ced1b3004cee0a1f47e2431"));
    }
}
