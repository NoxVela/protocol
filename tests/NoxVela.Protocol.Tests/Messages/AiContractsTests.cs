using System.Text.Json;
using NoxVela.Protocol.Infrastructure;
using NoxVela.Protocol.Messages;
using NoxVela.Protocol.Models.Ai;
using NUnit.Framework;

namespace NoxVela.Protocol.Tests.Messages;

[TestFixture]
public class AiContractsTests
{
    [Test]
    public void AiMessageTypes_ContainsExpectedConstants()
    {
        Assert.That(AiMessageTypes.START_EMBEDDING, Is.EqualTo("start_embedding"));
        Assert.That(AiMessageTypes.EMBEDDING_STATUS, Is.EqualTo("embedding_status"));
        Assert.That(AiMessageTypes.FINALIZE_RUN, Is.EqualTo("finalize_run"));
        Assert.That(AiMessageTypes.CHAT_MESSAGE, Is.EqualTo("chat_message"));
        Assert.That(AiMessageTypes.RAG_CONTEXT, Is.EqualTo("rag_context"));
        Assert.That(AiMessageTypes.CHUNK_RESPONSE, Is.EqualTo("chunk_response"));
        Assert.That(AiMessageTypes.CHUNK_RESPONSE_COMPLETE, Is.EqualTo("chunk_response_complete"));
    }

    [Test]
    public void AiCapabilities_ContainsStandardConstants()
    {
        Assert.That(AiCapabilities.All, Does.Contain("continuous_embedding"));
        Assert.That(AiCapabilities.CONTINUOUS_EMBEDDING, Is.EqualTo("continuous_embedding"));
    }

    [Test]
    public void AiDatabaseCredentials_SerializesPassword()
    {
        var jsonWithPassword = "{\"namespace\":\"test_ns\",\"user\":\"admin\",\"password\":\"secret456\"}";
        var creds = JsonSerializer.Deserialize<AiDatabaseCredentials>(jsonWithPassword);
        Assert.That(creds, Is.Not.Null);
        Assert.That(creds!.Namespace, Is.EqualTo("test_ns"));
        Assert.That(creds.User, Is.EqualTo("admin"));
        Assert.That(creds.Password, Is.EqualTo("secret456"));
    }

    [Test]
    public void StartEmbeddingPayload_SerializesAndDeserializes()
    {
        var payload = new StartEmbeddingPayload
        {
            RunId = "run-001",
            ProjectName = "noxvela-core",
            Command = "FINALIZE_RUN",
            DbCredentials = new AiDatabaseCredentials
            {
                Namespace = "ns_core",
                User = "ai",
                Password = "pwd"
            }
        };

        var json = JsonSerializer.Serialize(payload);
        var deserialized = JsonSerializer.Deserialize<StartEmbeddingPayload>(json);

        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.RunId, Is.EqualTo("run-001"));
        Assert.That(deserialized.ProjectName, Is.EqualTo("noxvela-core"));
        Assert.That(deserialized.Command, Is.EqualTo("FINALIZE_RUN"));
        Assert.That(deserialized.DbCredentials.Namespace, Is.EqualTo("ns_core"));
    }

    [Test]
    public void ChunkStreamingPayloads_SerializeCorrectly()
    {
        var chunk = new ChunkResponsePayload
        {
            Chunk = "public class Example",
            TargetConnectionId = "conn-123",
            ChunkIndex = 1
        };

        var json = JsonSerializer.Serialize(chunk);
        Assert.That(json, Does.Contain("\"chunk\":"));
        Assert.That(json, Does.Contain("\"targetConnectionId\":\"conn-123\""));

        var complete = new ChunkCompletePayload
        {
            TargetConnectionId = "conn-123"
        };
        var completeJson = JsonSerializer.Serialize(complete);
        Assert.That(completeJson, Does.Contain("\"status\":\"complete\""));
    }
}
