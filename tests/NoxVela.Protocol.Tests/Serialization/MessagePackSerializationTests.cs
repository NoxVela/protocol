using System;
using System.Collections.Generic;
using NoxVela.Protocol.Infrastructure;
using NoxVela.Protocol.Messages;
using NoxVela.Protocol.Messages.Service;
using NoxVela.Protocol.Models.Abstract;
using NoxVela.Protocol.Models.Code;
using NoxVela.Protocol.Security;
using NoxVela.Protocol.Serialization;
using NUnit.Framework;

namespace NoxVela.Protocol.Tests.Serialization;

[TestFixture]
public class MessagePackSerializationTests
{
    [Test]
    public void ProtocolEncodings_And_SecurityHeaders_ConstantsAreCorrect()
    {
        Assert.That(SecurityHeaders.PROTOCOL_ENCODING, Is.EqualTo("X-Protocol-Encoding"));
        Assert.That(ProtocolEncodings.JSON, Is.EqualTo("json"));
        Assert.That(ProtocolEncodings.MSGPACK, Is.EqualTo("msgpack"));
        Assert.That(SecurityHeaders.ENCODING_JSON, Is.EqualTo("json"));
        Assert.That(SecurityHeaders.ENCODING_MSGPACK, Is.EqualTo("msgpack"));
    }

    [Test]
    public void Wrapper_MessagePack_Roundtrip_PreservesData()
    {
        // Arrange
        var messageId = Guid.NewGuid();
        var sourceId = Guid.NewGuid();
        var wrapper = new Wrapper
        {
            MessageId = messageId,
            SourceMessageId = sourceId,
            MessageType = "nodes_batch",
            Payload = "{\"nodes\":[]}"
        };

        // Act
        var bytes = ProtocolMessagePack.Serialize(wrapper);
        var deserialized = ProtocolMessagePack.Deserialize<Wrapper>(bytes);

        // Assert
        Assert.That(bytes, Is.Not.Null);
        Assert.That(bytes.Length, Is.GreaterThan(0));
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized.MessageId, Is.EqualTo(messageId));
        Assert.That(deserialized.SourceMessageId, Is.EqualTo(sourceId));
        Assert.That(deserialized.MessageType, Is.EqualTo("nodes_batch"));
        Assert.That(deserialized.Payload, Is.EqualTo("{\"nodes\":[]}"));
    }

    [Test]
    public void NodesBatch_PolymorphicNodes_Roundtrip_Succeeds()
    {
        // Arrange
        var classNode = new ClassNode
        {
            StableId = "class:NoxVela.Core.Sample",
            ContentHash = "hash123",
            Identifier = "Sample",
            Accessibility = NodeAccessibility.Public,
            IsStatic = false,
            IsAbstract = false,
            IsSealed = true,
            IsGeneric = false,
            Arity = 0,
            Locations = new List<CodeLocation>
            {
                new() { FilePath = "Sample.cs", StartLine = 10, EndLine = 50, StartColumn = 1, EndColumn = 20, Snippet = "class Sample {}" }
            }
        };

        var methodNode = new MethodNode
        {
            StableId = "method:NoxVela.Core.Sample.Run",
            ContentHash = "hash456",
            Identifier = "Run",
            Accessibility = NodeAccessibility.Public,
            MethodKind = "Ordinary",
            IsStatic = false,
            IsAsync = true,
            Complexity = 3,
            Locations = new List<CodeLocation>
            {
                new() { FilePath = "Sample.cs", StartLine = 20, EndLine = 30, StartColumn = 5, EndColumn = 10, Snippet = "void Run() {}" }
            }
        };

        var externalNode = new ExternalTypeNode
        {
            StableId = "ext:System.String",
            ContentHash = "hashExt",
            Identifier = "String",
            AssemblyName = "System.Runtime"
        };

        var nodesBatch = new NodesBatch
        {
            Nodes = new NodeBase[] { classNode, methodNode, externalNode }
        };

        // Act
        var bytes = ProtocolMessagePack.Serialize(nodesBatch);
        var deserialized = ProtocolMessagePack.Deserialize<NodesBatch>(bytes);

        // Assert
        Assert.That(bytes, Is.Not.Null);
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized.Nodes, Is.Not.Null);
        Assert.That(deserialized.Nodes.Count, Is.EqualTo(3));

        Assert.That(deserialized.Nodes[0], Is.TypeOf<ClassNode>());
        var desClass = (ClassNode)deserialized.Nodes[0];
        Assert.That(desClass.Identifier, Is.EqualTo("Sample"));
        Assert.That(desClass.StableId, Is.EqualTo("class:NoxVela.Core.Sample"));
        Assert.That(desClass.IsSealed, Is.True);
        Assert.That(desClass.Locations.Count, Is.EqualTo(1));
        Assert.That(desClass.Locations[0].FilePath, Is.EqualTo("Sample.cs"));

        Assert.That(deserialized.Nodes[1], Is.TypeOf<MethodNode>());
        var desMethod = (MethodNode)deserialized.Nodes[1];
        Assert.That(desMethod.Identifier, Is.EqualTo("Run"));
        Assert.That(desMethod.IsAsync, Is.True);
        Assert.That(desMethod.Complexity, Is.EqualTo(3));

        Assert.That(deserialized.Nodes[2], Is.TypeOf<ExternalTypeNode>());
        var desExternal = (ExternalTypeNode)deserialized.Nodes[2];
        Assert.That(desExternal.Identifier, Is.EqualTo("String"));
        Assert.That(desExternal.AssemblyName, Is.EqualTo("System.Runtime"));
    }

    [Test]
    public void RelationsBatch_Roundtrip_Succeeds()
    {
        // Arrange
        var relation = new RelationNode
        {
            Id = "rel:123",
            From = "class:A",
            To = "class:B",
            RelationType = "INHERITS",
            IsPending = false,
            RunId = "run-1"
        };

        var relationsBatch = new RelationsBatch
        {
            Relations = new[] { relation }
        };

        // Act
        var bytes = ProtocolMessagePack.Serialize(relationsBatch);
        var deserialized = ProtocolMessagePack.Deserialize<RelationsBatch>(bytes);

        // Assert
        Assert.That(bytes, Is.Not.Null);
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized.Relations, Is.Not.Null);
        Assert.That(deserialized.Relations.Count, Is.EqualTo(1));
        Assert.That(deserialized.Relations[0].Id, Is.EqualTo("rel:123"));
        Assert.That(deserialized.Relations[0].From, Is.EqualTo("class:A"));
        Assert.That(deserialized.Relations[0].To, Is.EqualTo("class:B"));
        Assert.That(deserialized.Relations[0].RelationType, Is.EqualTo("INHERITS"));
        Assert.That(deserialized.Relations[0].IsPending, Is.False);
    }
}
