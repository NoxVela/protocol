using System;
using System.Text.Json;
using NoxVela.Protocol.Messages;
using NUnit.Framework;

namespace NoxVela.Protocol.Tests.Messages;

[TestFixture]
public class WrapperTests
{
    [Test]
    public void SerializeAndDeserialize_ValidEnvelope_PreservesAllFields()
    {
        // Arrange
        var messageId = Guid.NewGuid();
        var sourceMessageId = Guid.NewGuid();
        var wrapper = new Wrapper
        {
            MessageId = messageId,
            SourceMessageId = sourceMessageId,
            MessageType = "test_message",
            Payload = "{\"key\":\"value\"}"
        };

        // Act
        var json = JsonSerializer.Serialize(wrapper);
        var deserialized = JsonSerializer.Deserialize<Wrapper>(json);

        // Assert
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.MessageId, Is.EqualTo(messageId));
        Assert.That(deserialized.SourceMessageId, Is.EqualTo(sourceMessageId));
        Assert.That(deserialized.MessageType, Is.EqualTo("test_message"));
        Assert.That(deserialized.Payload, Is.EqualTo("{\"key\":\"value\"}"));
    }

    [Test]
    public void Serialize_UsesExpectedJsonPropertyNames()
    {
        // Arrange
        var wrapper = new Wrapper
        {
            MessageId = Guid.Parse("0191ebc2-8400-7c23-881e-123456789abc"),
            SourceMessageId = Guid.Empty,
            MessageType = "status",
            Payload = "{}"
        };

        // Act
        var json = JsonSerializer.Serialize(wrapper);

        // Assert
        Assert.That(json, Does.Contain("\"messageId\":"));
        Assert.That(json, Does.Contain("\"sourceMessageId\":"));
        Assert.That(json, Does.Contain("\"messageType\":"));
        Assert.That(json, Does.Contain("\"payload\":"));
    }

    [Test]
    public void Deserialize_ValidJsonString_ParsesCorrectly()
    {
        // Arrange
        const string json = "{" +
            "\"messageId\":\"0191ebc2-8400-7c23-881e-123456789abc\"," +
            "\"sourceMessageId\":\"00000000-0000-0000-0000-000000000000\"," +
            "\"messageType\":\"registration\"," +
            "\"payload\":\"{\\\"instanceId\\\":\\\"123\\\"}\"" +
            "}";

        // Act
        var wrapper = JsonSerializer.Deserialize<Wrapper>(json);

        // Assert
        Assert.That(wrapper, Is.Not.Null);
        Assert.That(wrapper!.MessageId, Is.EqualTo(Guid.Parse("0191ebc2-8400-7c23-881e-123456789abc")));
        Assert.That(wrapper.SourceMessageId, Is.EqualTo(Guid.Empty));
        Assert.That(wrapper.MessageType, Is.EqualTo("registration"));
        Assert.That(wrapper.Payload, Is.EqualTo("{\"instanceId\":\"123\"}"));
    }

    [Test]
    public void Deserialize_MissingRequiredField_ThrowsJsonException()
    {
        // Arrange - missing 'payload' which has [JsonRequired]
        const string invalidJson = "{" +
            "\"messageId\":\"0191ebc2-8400-7c23-881e-123456789abc\"," +
            "\"sourceMessageId\":\"00000000-0000-0000-0000-000000000000\"," +
            "\"messageType\":\"registration\"" +
            "}";

        // Act & Assert
        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Wrapper>(invalidJson));
    }
}
