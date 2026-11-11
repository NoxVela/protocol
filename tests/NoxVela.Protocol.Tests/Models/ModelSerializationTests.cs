using System.Collections.Generic;
using System.Text.Json;
using NoxVela.Protocol.Infrastructure;
using NoxVela.Protocol.Models.Abstract;
using NoxVela.Protocol.Models.Code;
using NoxVela.Protocol.Models.Git;
using NUnit.Framework;

namespace NoxVela.Protocol.Tests.Models;

[TestFixture]
public class ModelSerializationTests
{
    [Test]
    public void ClassNode_RoundTrip_PreservesAllProperties()
    {
        // Arrange
        var classNode = new ClassNode
        {
            StableId = "MyApp.Models.User",
            ContentHash = "hash-12345",
            IsStatic = false,
            IsAbstract = false,
            IsSealed = true,
            IsGeneric = false,
            Arity = 0,
            Accessibility = NodeAccessibility.Public,
            Locations = new List<CodeLocation>
            {
                new()
                {
                    FilePath = "Models/User.cs",
                    StartLine = 10,
                    EndLine = 45,
                    StartColumn = 1,
                    EndColumn = 2,
                    Snippet = "public sealed class User { }"
                }
            }
        };

        // Act
        var json = JsonSerializer.Serialize(classNode);
        var deserialized = JsonSerializer.Deserialize<ClassNode>(json);

        // Assert
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.StableId, Is.EqualTo("MyApp.Models.User"));
        Assert.That(deserialized.ContentHash, Is.EqualTo("hash-12345"));
        Assert.That(deserialized.Type, Is.EqualTo(AllNodesTypes.ClassNodes));
        Assert.That(deserialized.IsStatic, Is.False);
        Assert.That(deserialized.IsAbstract, Is.False);
        Assert.That(deserialized.IsSealed, Is.True);
        Assert.That(deserialized.IsGeneric, Is.False);
        Assert.That(deserialized.Arity, Is.EqualTo(0));
        Assert.That(deserialized.Accessibility, Is.EqualTo(NodeAccessibility.Public));
        Assert.That(deserialized.Locations, Has.Count.EqualTo(1));
        Assert.That(deserialized.Locations[0].FilePath, Is.EqualTo("Models/User.cs"));
        Assert.That(deserialized.Locations[0].Snippet, Is.EqualTo("public sealed class User { }"));
    }

    [Test]
    public void MethodNode_RoundTrip_PreservesMethodMetadata()
    {
        // Arrange
        var methodNode = new MethodNode
        {
            StableId = "MyApp.Models.User.Authenticate(string,string)",
            ContentHash = "hash-method-6789",
            Accessibility = NodeAccessibility.Public,
            MethodKind = "Ordinary",
            IsStatic = false,
            IsAbstract = false,
            IsVirtual = false,
            IsOverride = false,
            IsSealed = false,
            IsAsync = true,
            IsExtensionMethod = false,
            IsGeneric = false,
            Arity = 0,
            Complexity = 3,
            Locations = new List<CodeLocation>
            {
                new()
                {
                    FilePath = "Models/User.cs",
                    StartLine = 20,
                    EndLine = 35,
                    StartColumn = 5,
                    EndColumn = 6,
                    Snippet = "public async Task Authenticate(string u, string p)"
                }
            }
        };

        // Act
        var json = JsonSerializer.Serialize(methodNode);
        var deserialized = JsonSerializer.Deserialize<MethodNode>(json);

        // Assert
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.StableId, Is.EqualTo("MyApp.Models.User.Authenticate(string,string)"));
        Assert.That(deserialized.Type, Is.EqualTo(AllNodesTypes.MethodNodes));
        Assert.That(deserialized.MethodKind, Is.EqualTo("Ordinary"));
        Assert.That(deserialized.IsAsync, Is.True);
        Assert.That(deserialized.Complexity, Is.EqualTo(3));
    }

    [Test]
    public void InterfaceNode_RoundTrip_PreservesInterfaceProperties()
    {
        // Arrange
        var ifaceNode = new InterfaceNode
        {
            StableId = "MyApp.Contracts.IUserService",
            ContentHash = "hash-iface-1111",
            Accessibility = NodeAccessibility.Public,
            IsGeneric = false,
            Locations = new List<CodeLocation>
            {
                new()
                {
                    FilePath = "Contracts/IUserService.cs",
                    StartLine = 5,
                    EndLine = 15,
                    StartColumn = 1,
                    EndColumn = 2,
                    Snippet = "public interface IUserService { }"
                }
            }
        };

        // Act
        var json = JsonSerializer.Serialize(ifaceNode);
        var deserialized = JsonSerializer.Deserialize<InterfaceNode>(json);

        // Assert
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.StableId, Is.EqualTo("MyApp.Contracts.IUserService"));
        Assert.That(deserialized.Type, Is.EqualTo(AllNodesTypes.InterfaceNodes));
        Assert.That(deserialized.IsGeneric, Is.False);
        Assert.That(deserialized.Accessibility, Is.EqualTo(NodeAccessibility.Public));
        Assert.That(deserialized.Locations, Has.Count.EqualTo(1));
    }

    [Test]
    public void PropertyNode_RoundTrip_PreservesPropertyModifiers()
    {
        // Arrange
        var propNode = new PropertyNode
        {
            StableId = "MyApp.Models.User.Id",
            ContentHash = "hash-prop-2222",
            Accessibility = NodeAccessibility.Public,
            IsIndexer = false,
            IsStatic = false,
            IsAbstract = false,
            IsVirtual = false,
            IsOverride = false,
            IsSealed = false,
            IsRequired = true,
            HasGet = true,
            HasSet = true,
            IsInitOnly = true,
            Locations = new List<CodeLocation>
            {
                new()
                {
                    FilePath = "Models/User.cs",
                    StartLine = 12,
                    EndLine = 12,
                    StartColumn = 5,
                    EndColumn = 45,
                    Snippet = "public required string Id { get; init; }"
                }
            }
        };

        // Act
        var json = JsonSerializer.Serialize(propNode);
        var deserialized = JsonSerializer.Deserialize<PropertyNode>(json);

        // Assert
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.StableId, Is.EqualTo("MyApp.Models.User.Id"));
        Assert.That(deserialized.Type, Is.EqualTo(AllNodesTypes.PropertyNodes));
        Assert.That(deserialized.IsRequired, Is.True);
        Assert.That(deserialized.HasGet, Is.True);
        Assert.That(deserialized.HasSet, Is.True);
        Assert.That(deserialized.IsInitOnly, Is.True);
    }

    [Test]
    public void RecordNode_TypeDiscriminator_AndRoundTrip_PreservesProperties()
    {
        // Arrange
        var recordNode = new RecordNode
        {
            StableId = "MyApp.Models.UserRecord",
            ContentHash = "hash-record-4444",
            Identifier = "UserRecord",
            Accessibility = NodeAccessibility.Public,
            IsRecordStruct = false,
            IsReadOnly = false,
            IsStatic = false,
            IsAbstract = false,
            IsSealed = true,
            Summary = "User record summary",
            Locations = new List<CodeLocation>
            {
                new()
                {
                    FilePath = "Models/UserRecord.cs",
                    StartLine = 5,
                    EndLine = 15,
                    StartColumn = 1,
                    EndColumn = 2,
                    Snippet = "public record UserRecord(string Name);"
                }
            }
        };

        // Assert type discriminator directly on new instance and populated model
        Assert.That(new RecordNode().Type, Is.EqualTo(AllNodesTypes.RecordNodes));
        Assert.That(recordNode.Type, Is.EqualTo(AllNodesTypes.RecordNodes));

        // Act
        var json = JsonSerializer.Serialize(recordNode);
        var deserialized = JsonSerializer.Deserialize<RecordNode>(json);

        // Assert JSON contains discriminator and matches deserialized object
        Assert.That(json, Does.Contain("\"type\":\"records\""));
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.StableId, Is.EqualTo("MyApp.Models.UserRecord"));
        Assert.That(deserialized.Type, Is.EqualTo(AllNodesTypes.RecordNodes));
        Assert.That(deserialized.Identifier, Is.EqualTo("UserRecord"));
        Assert.That(deserialized.Accessibility, Is.EqualTo(NodeAccessibility.Public));
        Assert.That(deserialized.IsRecordStruct, Is.False);
        Assert.That(deserialized.IsReadOnly, Is.False);
        Assert.That(deserialized.IsStatic, Is.False);
        Assert.That(deserialized.IsAbstract, Is.False);
        Assert.That(deserialized.IsSealed, Is.True);
        Assert.That(deserialized.Summary, Is.EqualTo("User record summary"));
        Assert.That(deserialized.Locations, Has.Count.EqualTo(1));
        Assert.That(deserialized.Locations[0].FilePath, Is.EqualTo("Models/UserRecord.cs"));
    }

    [Test]
    public void EnumNode_RoundTrip_PreservesSingleLocation()
    {
        // Arrange
        var enumNode = new EnumNode
        {
            StableId = "MyApp.Models.UserRole",
            ContentHash = "hash-enum-3333",
            Accessibility = NodeAccessibility.Public,
            Location = new CodeLocation
            {
                FilePath = "Models/UserRole.cs",
                StartLine = 1,
                EndLine = 10,
                StartColumn = 1,
                EndColumn = 2,
                Snippet = "public enum UserRole { Admin, User }"
            }
        };

        // Act
        var json = JsonSerializer.Serialize(enumNode);
        var deserialized = JsonSerializer.Deserialize<EnumNode>(json);

        // Assert
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.StableId, Is.EqualTo("MyApp.Models.UserRole"));
        Assert.That(deserialized.Type, Is.EqualTo(AllNodesTypes.EnumsNodes));
        Assert.That(deserialized.Location, Is.Not.Null);
        Assert.That(deserialized.Location!.FilePath, Is.EqualTo("Models/UserRole.cs"));
    }

    [Test]
    public void RelationNode_RoundTrip_PreservesGraphEdgeProperties()
    {
        // Arrange
        var relation = new RelationNode
        {
            Id = "rel-001",
            From = "hash-source-1",
            To = "hash-target-2",
            RelationType = NodesRelations.CALLS,
            IsPending = false,
            RunId = "run-0191"
        };

        // Act
        var json = JsonSerializer.Serialize(relation);
        var deserialized = JsonSerializer.Deserialize<RelationNode>(json);

        // Assert
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.Id, Is.EqualTo("rel-001"));
        Assert.That(deserialized.From, Is.EqualTo("hash-source-1"));
        Assert.That(deserialized.To, Is.EqualTo("hash-target-2"));
        Assert.That(deserialized.RelationType, Is.EqualTo(NodesRelations.CALLS));
        Assert.That(deserialized.IsPending, Is.False);
        Assert.That(deserialized.RunId, Is.EqualTo("run-0191"));
        Assert.That(json, Does.Contain("\"relation_type\":\"CALLS\""));
    }

    [Test]
    public void CodeLocation_RoundTrip_PreservesSnippetAndCoordinates()
    {
        // Arrange
        var location = new CodeLocation
        {
            FilePath = "src/Service.cs",
            StartLine = 15,
            EndLine = 25,
            StartColumn = 1,
            EndColumn = 10,
            Snippet = "var x = 42;"
        };

        // Act
        var json = JsonSerializer.Serialize(location);
        var deserialized = JsonSerializer.Deserialize<CodeLocation>(json);

        // Assert
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.FilePath, Is.EqualTo("src/Service.cs"));
        Assert.That(deserialized.StartLine, Is.EqualTo(15));
        Assert.That(deserialized.EndLine, Is.EqualTo(25));
        Assert.That(deserialized.StartColumn, Is.EqualTo(1));
        Assert.That(deserialized.EndColumn, Is.EqualTo(10));
        Assert.That(deserialized.Snippet, Is.EqualTo("var x = 42;"));
        Assert.That(json, Does.Contain("\"code_snippet\":\"var x = 42;\""));
    }

    [Test]
    public void GitContext_WithCredentials_RoundTrip()
    {
        // Arrange
        var gitContext = new GitContext
        {
            RepositoryUrl = "https://github.com/private/repo.git",
            BranchName = "develop",
            CommitHash = "1234567890abcdef",
            Provider = "GitHub",
            IsPublic = false,
            Credentials = new GitCredentials
            {
                AccessToken = "mock_access_token_12345"
            }
        };

        // Act
        var json = JsonSerializer.Serialize(gitContext);
        var deserialized = JsonSerializer.Deserialize<GitContext>(json);

        // Assert
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.RepositoryUrl, Is.EqualTo("https://github.com/private/repo.git"));
        Assert.That(deserialized.IsPublic, Is.False);
        Assert.That(deserialized.Credentials, Is.Not.Null);
        Assert.That(deserialized.Credentials!.AccessToken, Is.EqualTo("mock_access_token_12345"));
        Assert.That(json, Does.Contain("\"access_token\":\"mock_access_token_12345\""));
    }

    [Test]
    public void GitContext_WithDiffMetadata_RoundTrip_PreservesBaseCommitAndModifiedFiles()
    {
        // Arrange
        var gitContext = new GitContext
        {
            RepositoryUrl = "https://github.com/org/repo.git",
            BranchName = "feature/pr-scan",
            CommitHash = "abcdef1122334455667788990011223344556677",
            BaseCommitHash = "00112233445566778899aabbccddeeff00112233",
            Provider = "GitHub",
            IsPublic = true,
            ModifiedFiles = new List<string> { "src/Service.cs", "src/Models/User.cs" }
        };

        // Act
        var json = JsonSerializer.Serialize(gitContext);
        var deserialized = JsonSerializer.Deserialize<GitContext>(json);

        // Assert
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.BaseCommitHash, Is.EqualTo("00112233445566778899aabbccddeeff00112233"));
        Assert.That(deserialized.ModifiedFiles, Is.EquivalentTo(new[] { "src/Service.cs", "src/Models/User.cs" }));
        Assert.That(json, Does.Contain("\"base_commit_hash\":\"00112233445566778899aabbccddeeff00112233\""));
        Assert.That(json, Does.Contain("\"modified_files\":[\"src/Service.cs\",\"src/Models/User.cs\"]"));
    }

    [Test]
    public void RunMetadata_RoundTrip_PreservesAllProperties()
    {
        // Arrange
        var metadata = new RunMetadata
        {
            RunId = "run-0191-ef90",
            CommitHash = "abcdef0123456789abcdef0123456789abcdef01",
            BranchName = "feature/time-travel",
            RepositoryUrl = "https://github.com/NoxVela/protocol.git",
            CommitMessage = "feat: implement time-travel architecture",
            Author = "Developer <dev@noxvela.io>"
        };

        // Act
        var json = JsonSerializer.Serialize(metadata);
        var deserialized = JsonSerializer.Deserialize<RunMetadata>(json);

        // Assert
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.RunId, Is.EqualTo("run-0191-ef90"));
        Assert.That(deserialized.CommitHash, Is.EqualTo("abcdef0123456789abcdef0123456789abcdef01"));
        Assert.That(deserialized.BranchName, Is.EqualTo("feature/time-travel"));
        Assert.That(deserialized.RepositoryUrl, Is.EqualTo("https://github.com/NoxVela/protocol.git"));
        Assert.That(deserialized.CommitMessage, Is.EqualTo("feat: implement time-travel architecture"));
        Assert.That(deserialized.Author, Is.EqualTo("Developer <dev@noxvela.io>"));
        Assert.That(json, Does.Contain("\"commit_hash\":\"abcdef0123456789abcdef0123456789abcdef01\""));
        Assert.That(json, Does.Contain("\"branch_name\":\"feature/time-travel\""));
    }
}
