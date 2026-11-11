using System;
using System.Collections.Generic;
using System.Text.Json;
using NoxVela.Protocol.Infrastructure;
using NoxVela.Protocol.Messages;
using NoxVela.Protocol.Messages.Server;
using NoxVela.Protocol.Messages.Service;
using NoxVela.Protocol.Models.Abstract;
using NoxVela.Protocol.Models.Code;
using NoxVela.Protocol.Models.Git;
using NUnit.Framework;

namespace NoxVela.Protocol.Tests.Messages;

[TestFixture]
public class MessageSerializationTests
{
    [Test]
    public void Registration_RoundTrip_PreservesAllProperties()
    {
        // Arrange
        var reg = new Registration
        {
            InstanceId = "instance-uuid-1234",
            PoolId = "pool-us-east-1",
            ServiceKey = "secret-pool-key-999",
            ServiceName = "csharp-analyzer",
            Version = "1.0.0",
            Capabilities = new List<string> { "AST", "Metrics" },
            SupportedLanguages = new List<string> { "csharp" },
            ServiceType = ServiceTypes.ANALYZER
        };

        // Act
        var json = JsonSerializer.Serialize(reg);
        var deserialized = JsonSerializer.Deserialize<Registration>(json);

        // Assert
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.InstanceId, Is.EqualTo("instance-uuid-1234"));
        Assert.That(deserialized.PoolId, Is.EqualTo("pool-us-east-1"));
        Assert.That(deserialized.ServiceKey, Is.EqualTo("secret-pool-key-999"));
        Assert.That(deserialized.ServiceName, Is.EqualTo("csharp-analyzer"));
        Assert.That(deserialized.Version, Is.EqualTo("1.0.0"));
        Assert.That(deserialized.Capabilities, Is.EquivalentTo(new[] { "AST", "Metrics" }));
        Assert.That(deserialized.SupportedLanguages, Is.EquivalentTo(new[] { "csharp" }));
        Assert.That(deserialized.ServiceType, Is.EqualTo(ServiceTypes.ANALYZER));
    }

    [Test]
    public void Registration_Deserialization_SupportsPoolIdAndServiceKey()
    {
        // Arrange
        var json = """
        {
            "instanceId": "replica-worker-42",
            "poolId": "analyzer-cluster-prod",
            "serviceKey": "super-secret-key-123",
            "serviceName": "csharp-analyzer",
            "version": "1.1.0",
            "capabilities": ["syntax_tree_parsing"],
            "supportedLanguages": ["csharp"],
            "serviceType": 0
        }
        """;

        // Act
        var deserialized = JsonSerializer.Deserialize<Registration>(json);

        // Assert
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.InstanceId, Is.EqualTo("replica-worker-42"));
        Assert.That(deserialized.PoolId, Is.EqualTo("analyzer-cluster-prod"));
        Assert.That(deserialized.ServiceKey, Is.EqualTo("super-secret-key-123"));
    }

    [Test]
    public void Registration_WithCompilerEngineAndComputeWeight_RoundTrip_PreservesAllProperties()
    {
        // Arrange
        var reg = new Registration
        {
            InstanceId = "instance-uuid-5678",
            ServiceName = "csharp-analyzer",
            Version = "1.1.0",
            Capabilities = new List<string> { AnalyzerCapabilities.SYNTAX_TREE_PARSING, AnalyzerCapabilities.ENGINE_ROSLYN },
            SupportedLanguages = new List<string> { "csharp" },
            ServiceType = ServiceTypes.ANALYZER,
            CompilerEngine = CompilerEngines.ROSLYN,
            CompilerVersion = "4.12.0",
            ComputeWeight = TaskComputeWeights.HEAVY_COMPUTE
        };

        // Act
        var json = JsonSerializer.Serialize(reg);
        var deserialized = JsonSerializer.Deserialize<Registration>(json);

        // Assert
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.CompilerEngine, Is.EqualTo("roslyn"));
        Assert.That(deserialized.CompilerVersion, Is.EqualTo("4.12.0"));
        Assert.That(deserialized.ComputeWeight, Is.EqualTo("heavy_compute"));
        Assert.That(json, Does.Contain("\"compilerEngine\":\"roslyn\""));
        Assert.That(json, Does.Contain("\"compilerVersion\":\"4.12.0\""));
        Assert.That(json, Does.Contain("\"computeWeight\":\"heavy_compute\""));
    }

    [Test]
    public void Registration_WithProtocolVersion_RoundTrip_PreservesCamelCase()
    {
        // Arrange
        var reg = new Registration
        {
            InstanceId = "instance-proto-001",
            ServiceName = "csharp-analyzer",
            Version = "1.0.0",
            Capabilities = new List<string> { "syntax_tree_parsing" },
            SupportedLanguages = new List<string> { "csharp" },
            ServiceType = ServiceTypes.ANALYZER,
            ProtocolVersion = "1.1.0"
        };

        // Act
        var json = JsonSerializer.Serialize(reg);
        var deserialized = JsonSerializer.Deserialize<Registration>(json);

        // Assert
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.ProtocolVersion, Is.EqualTo("1.1.0"));
        Assert.That(json, Does.Contain("\"protocolVersion\":\"1.1.0\""));
    }

    [Test]
    public void Registration_Deserialization_SupportsProtocolVersionCamelCase()
    {
        var camelCaseJson = """
        {
            "instanceId": "instance-camel-02",
            "serviceName": "go-analyzer",
            "version": "1.0.0",
            "capabilities": ["syntax_tree_parsing"],
            "supportedLanguages": ["go"],
            "serviceType": 0,
            "protocolVersion": "1.1.0"
        }
        """;
        var res = JsonSerializer.Deserialize<Registration>(camelCaseJson);
        Assert.That(res, Is.Not.Null);
        Assert.That(res!.ProtocolVersion, Is.EqualTo("1.1.0"));
    }

    [Test]
    public void Status_RoundTrip_PreservesStatusId()
    {
        // Arrange
        var status = new Status
        {
            InstanceId = "instance-uuid-1234",
            StatusId = (short)ServiceStatuses.WORKING
        };

        // Act
        var json = JsonSerializer.Serialize(status);
        var deserialized = JsonSerializer.Deserialize<Status>(json);

        // Assert
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.InstanceId, Is.EqualTo("instance-uuid-1234"));
        Assert.That(deserialized.StatusId, Is.EqualTo((short)ServiceStatuses.WORKING));
        Assert.That(json, Does.Contain("\"status\":1"));
    }

    [Test]
    public void AnalysisStatus_RoundTrip_PreservesMessageAndStatus()
    {
        // Arrange
        var analysisStatus = new AnalysisStatus
        {
            InstanceId = "instance-uuid-1234",
            StatusId = (short)ServiceStatuses.WORKING,
            Message = "Parsing syntax tree"
        };

        // Act
        var json = JsonSerializer.Serialize(analysisStatus);
        var deserialized = JsonSerializer.Deserialize<AnalysisStatus>(json);

        // Assert
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.InstanceId, Is.EqualTo("instance-uuid-1234"));
        Assert.That(deserialized.StatusId, Is.EqualTo((short)ServiceStatuses.WORKING));
        Assert.That(deserialized.Message, Is.EqualTo("Parsing syntax tree"));
    }

    [Test]
    public void AnalysisStatus_WithCommitMetadata_RoundTrip_PreservesCommitAndBranch()
    {
        // Arrange
        var analysisStatus = new AnalysisStatus
        {
            InstanceId = "instance-uuid-1234",
            RunId = "run-0191",
            StatusId = (short)ServiceStatuses.WORKING,
            Message = "Analyzing commit",
            CommitHash = "0123456789abcdef0123456789abcdef01234567",
            Branch = "feature/ast-parser",
            CommitMessage = "feat: initial commit"
        };

        // Act
        var json = JsonSerializer.Serialize(analysisStatus);
        var deserialized = JsonSerializer.Deserialize<AnalysisStatus>(json);

        // Assert
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.RunId, Is.EqualTo("run-0191"));
        Assert.That(deserialized.CommitHash, Is.EqualTo("0123456789abcdef0123456789abcdef01234567"));
        Assert.That(deserialized.Branch, Is.EqualTo("feature/ast-parser"));
        Assert.That(deserialized.CommitMessage, Is.EqualTo("feat: initial commit"));
        Assert.That(json, Does.Contain("\"commit_hash\":\"0123456789abcdef0123456789abcdef01234567\""));
        Assert.That(json, Does.Contain("\"branch\":\"feature/ast-parser\""));
    }

    [Test]
    public void AnalysisMetrics_RoundTrip_PreservesTelemetryMetrics()
    {
        // Arrange
        var startTime = new DateTime(2026, 9, 15, 18, 0, 0, DateTimeKind.Utc);
        var endTime = new DateTime(2026, 9, 15, 18, 0, 5, DateTimeKind.Utc);
        var metrics = new AnalysisMetrics
        {
            InstanceId = "instance-uuid-1234",
            RunId = "run-0191",
            StartedAt = startTime,
            EndedAt = endTime,
            DurationMs = 5000.5,
            GeneratedNodes = 1420,
            GeneratedRelations = 3890
        };

        // Act
        var json = JsonSerializer.Serialize(metrics);
        var deserialized = JsonSerializer.Deserialize<AnalysisMetrics>(json);

        // Assert
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.InstanceId, Is.EqualTo("instance-uuid-1234"));
        Assert.That(deserialized.RunId, Is.EqualTo("run-0191"));
        Assert.That(deserialized.StartedAt, Is.EqualTo(startTime));
        Assert.That(deserialized.EndedAt, Is.EqualTo(endTime));
        Assert.That(deserialized.DurationMs, Is.EqualTo(5000.5));
        Assert.That(deserialized.GeneratedNodes, Is.EqualTo(1420));
        Assert.That(deserialized.GeneratedRelations, Is.EqualTo(3890));
    }

    [Test]
    public void AnalysisMetrics_WithCommitMetadata_RoundTrip_PreservesCommitAndBranch()
    {
        // Arrange
        var startTime = new DateTime(2026, 9, 15, 18, 0, 0, DateTimeKind.Utc);
        var endTime = new DateTime(2026, 9, 15, 18, 0, 5, DateTimeKind.Utc);
        var metrics = new AnalysisMetrics
        {
            InstanceId = "instance-uuid-1234",
            RunId = "run-0191",
            StartedAt = startTime,
            EndedAt = endTime,
            DurationMs = 5000.5,
            GeneratedNodes = 1420,
            GeneratedRelations = 3890,
            CommitHash = "abcdef0123456789abcdef0123456789abcdef01",
            Branch = "main",
            CommitMessage = "chore: release 1.0"
        };

        // Act
        var json = JsonSerializer.Serialize(metrics);
        var deserialized = JsonSerializer.Deserialize<AnalysisMetrics>(json);

        // Assert
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.CommitHash, Is.EqualTo("abcdef0123456789abcdef0123456789abcdef01"));
        Assert.That(deserialized.Branch, Is.EqualTo("main"));
        Assert.That(deserialized.CommitMessage, Is.EqualTo("chore: release 1.0"));
        Assert.That(json, Does.Contain("\"commit_hash\":\"abcdef0123456789abcdef0123456789abcdef01\""));
        Assert.That(json, Does.Contain("\"branch\":\"main\""));
    }

    [Test]
    public void RegistrationStatus_RoundTrip_PreservesStatus()
    {
        // Arrange
        var regStatus = new RegistrationStatus
        {
            Status = (short)RegistrationStatuses.ACCEPTED
        };

        // Act
        var json = JsonSerializer.Serialize(regStatus);
        var deserialized = JsonSerializer.Deserialize<RegistrationStatus>(json);

        // Assert
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.Status, Is.EqualTo((short)RegistrationStatuses.ACCEPTED));
        Assert.That(json, Does.Contain("\"status\":0"));
    }

    [Test]
    public void RunAnalysis_RoundTrip_PreservesCodeContextAndRunId()
    {
        // Arrange
        var runAnalysis = new RunAnalysis
        {
            RunId = "run-0191-ef90",
            CodeContext = new GitContext
            {
                RepositoryUrl = "https://github.com/example/repo.git",
                BranchName = "main",
                CommitHash = "7f8b91a2b3c4d5e6f7a8b9c0d1e2f3a4b5c6d7e8",
                Provider = "GitHub",
                IsPublic = true,
                Credentials = null
            }
        };

        // Act
        var json = JsonSerializer.Serialize(runAnalysis);
        var deserialized = JsonSerializer.Deserialize<RunAnalysis>(json);

        // Assert
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.RunId, Is.EqualTo("run-0191-ef90"));
        Assert.That(deserialized.CodeContext, Is.Not.Null);
        Assert.That(deserialized.CodeContext.RepositoryUrl, Is.EqualTo("https://github.com/example/repo.git"));
        Assert.That(deserialized.CodeContext.BranchName, Is.EqualTo("main"));
        Assert.That(deserialized.CodeContext.CommitHash, Is.EqualTo("7f8b91a2b3c4d5e6f7a8b9c0d1e2f3a4b5c6d7e8"));
        Assert.That(deserialized.CodeContext.Provider, Is.EqualTo("GitHub"));
        Assert.That(deserialized.CodeContext.IsPublic, Is.True);
    }

    [Test]
    public void RunAnalysis_WithCompilerEngineAndComputeWeight_RoundTrip_PreservesAllProperties()
    {
        // Arrange
        var runAnalysis = new RunAnalysis
        {
            RunId = "run-weight-123",
            CodeContext = new GitContext
            {
                RepositoryUrl = "https://github.com/example/repo.git",
                BranchName = "main",
                CommitHash = "7f8b91a2b3c4d5e6f7a8b9c0d1e2f3a4b5c6d7e8",
                BaseCommitHash = "11223344556677889900aabbccddeeff11223344",
                ModifiedFiles = new List<string> { "src/App.cs" },
                Provider = "GitHub",
                IsPublic = true
            },
            CompilerEngine = CompilerEngines.ROSLYN,
            CompilerVersion = "4.12.0",
            ComputeWeight = TaskComputeWeights.LIGHTWEIGHT
        };

        // Act
        var json = JsonSerializer.Serialize(runAnalysis);
        var deserialized = JsonSerializer.Deserialize<RunAnalysis>(json);

        // Assert
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.CompilerEngine, Is.EqualTo("roslyn"));
        Assert.That(deserialized.CompilerVersion, Is.EqualTo("4.12.0"));
        Assert.That(deserialized.ComputeWeight, Is.EqualTo("lightweight"));
        Assert.That(deserialized.CodeContext.BaseCommitHash, Is.EqualTo("11223344556677889900aabbccddeeff11223344"));
        Assert.That(deserialized.CodeContext.ModifiedFiles, Has.Count.EqualTo(1));
        Assert.That(json, Does.Contain("\"compiler_engine\":\"roslyn\""));
        Assert.That(json, Does.Contain("\"compiler_version\":\"4.12.0\""));
        Assert.That(json, Does.Contain("\"compute_weight\":\"lightweight\""));
    }

    [Test]
    public void RunAnalysis_WithRequestedCapabilities_RoundTrip_PreservesCapabilities()
    {
        // Arrange
        var runAnalysis = new RunAnalysis
        {
            RunId = "run-caps-001",
            CodeContext = new GitContext
            {
                RepositoryUrl = "https://github.com/example/repo.git",
                BranchName = "main",
                CommitHash = "abcdef0123456789",
                Provider = "GitHub",
                IsPublic = true
            },
            RequestedCapabilities = new[] { "syntax_tree_parsing", "call_graph", "metrics" }
        };

        // Act
        var json = JsonSerializer.Serialize(runAnalysis);
        var deserialized = JsonSerializer.Deserialize<RunAnalysis>(json);

        // Assert
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.RequestedCapabilities, Is.Not.Null);
        Assert.That(deserialized.RequestedCapabilities, Is.EquivalentTo(new[] { "syntax_tree_parsing", "call_graph", "metrics" }));
        Assert.That(json, Does.Contain("\"requested_capabilities\":[\"syntax_tree_parsing\",\"call_graph\",\"metrics\"]"));
    }

    [Test]
    public void RunAnalysis_WithoutRequestedCapabilities_DeserializesWithNull()
    {
        // Arrange - JSON without requested_capabilities field
        var json = """
        {
            "run_id": "run-no-caps",
            "code_context": {
                "repository_url": "https://github.com/example/repo.git",
                "branch_name": "main",
                "commit_hash": "123456",
                "provider": "GitHub",
                "is_public": true
            }
        }
        """;

        // Act
        var deserialized = JsonSerializer.Deserialize<RunAnalysis>(json);

        // Assert
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.RunId, Is.EqualTo("run-no-caps"));
        Assert.That(deserialized.RequestedCapabilities, Is.Null);
    }

    [Test]
    public void CancelAnalysis_RoundTrip_WithReason_PreservesAllProperties()
    {
        // Arrange
        var cancel = new CancelAnalysis
        {
            RunId = "run-0191-cancel-test",
            Reason = "User requested cancellation"
        };

        // Act
        var json = JsonSerializer.Serialize(cancel);
        var deserialized = JsonSerializer.Deserialize<CancelAnalysis>(json);

        // Assert
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.RunId, Is.EqualTo("run-0191-cancel-test"));
        Assert.That(deserialized.Reason, Is.EqualTo("User requested cancellation"));
        Assert.That(json, Does.Contain("\"run_id\":\"run-0191-cancel-test\""));
        Assert.That(json, Does.Contain("\"reason\":\"User requested cancellation\""));
    }

    [Test]
    public void CancelAnalysis_RoundTrip_WithoutReason_PreservesRunIdAndNullReason()
    {
        // Arrange
        var cancel = new CancelAnalysis
        {
            RunId = "run-0191-cancel-no-reason"
        };

        // Act
        var json = JsonSerializer.Serialize(cancel);
        var deserialized = JsonSerializer.Deserialize<CancelAnalysis>(json);

        // Assert
        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.RunId, Is.EqualTo("run-0191-cancel-no-reason"));
        Assert.That(deserialized.Reason, Is.Null);
    }

    [Test]
    public void CancelAnalysis_MissingRunId_ThrowsJsonException()
    {
        // Arrange - JSON without run_id
        var json = """{"reason": "Only reason provided"}""";

        // Act & Assert
        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<CancelAnalysis>(json));
    }

    [Test]
    public void BatchingConfig_DefaultValues_AreExpected()
    {
        var config = new BatchingConfig();

        Assert.That(config.MaxBatchSize, Is.EqualTo(100));
        Assert.That(config.FlushIntervalMs, Is.EqualTo(100));
    }

    [Test]
    public void BatchingConfig_RoundTrip_PreservesValues()
    {
        var config = new BatchingConfig
        {
            MaxBatchSize = 250,
            FlushIntervalMs = 50
        };

        var json = JsonSerializer.Serialize(config);
        var deserialized = JsonSerializer.Deserialize<BatchingConfig>(json);

        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.MaxBatchSize, Is.EqualTo(250));
        Assert.That(deserialized.FlushIntervalMs, Is.EqualTo(50));
        Assert.That(json, Does.Contain("\"max_batch_size\":250"));
        Assert.That(json, Does.Contain("\"flush_interval_ms\":50"));
    }

    [Test]
    public void RunAnalysis_WithBatchingConfig_SerializesAndDeserializesCorrectly()
    {
        var runAnalysis = new RunAnalysis
        {
            RunId = "run-batch-test",
            CodeContext = new GitContext
            {
                RepositoryUrl = "https://github.com/org/repo.git",
                BranchName = "main",
                CommitHash = "abcdef1234567890",
                Provider = "GitHub"
            },
            BatchingConfig = new BatchingConfig
            {
                MaxBatchSize = 50,
                FlushIntervalMs = 200
            }
        };

        var json = JsonSerializer.Serialize(runAnalysis);
        var deserialized = JsonSerializer.Deserialize<RunAnalysis>(json);

        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.RunId, Is.EqualTo("run-batch-test"));
        Assert.That(deserialized.BatchingConfig, Is.Not.Null);
        Assert.That(deserialized.BatchingConfig!.MaxBatchSize, Is.EqualTo(50));
        Assert.That(deserialized.BatchingConfig.FlushIntervalMs, Is.EqualTo(200));
        Assert.That(json, Does.Contain("\"batching_config\":{"));
    }

    [Test]
    public void NodesBatch_RoundTrip_SerializesAndDeserializesNodes()
    {
        var batch = new NodesBatch
        {
            Nodes = new List<NodeBase>
            {
                new ClassNode
                {
                    StableId = "cls-1",
                    Identifier = "OrderService",
                    ContentHash = "hash1",
                    IsStatic = false,
                    IsAbstract = false,
                    IsSealed = false,
                    IsGeneric = false,
                    Arity = 0
                }
            }
        };

        var json = JsonSerializer.Serialize(batch);
        var deserialized = JsonSerializer.Deserialize<NodesBatch>(json);

        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.Nodes, Has.Count.EqualTo(1));
        Assert.That(deserialized.Nodes[0].StableId, Is.EqualTo("cls-1"));
        Assert.That(deserialized.Nodes[0].ContentHash, Is.EqualTo("hash1"));
        Assert.That(json, Does.Contain("\"nodes\":["));
    }

    [Test]
    public void RelationsBatch_RoundTrip_SerializesAndDeserializesRelations()
    {
        var batch = new RelationsBatch
        {
            Relations = new List<RelationNode>
            {
                new RelationNode
                {
                    Id = "rel-1",
                    From = "hash-from",
                    To = "hash-to",
                    RelationType = "defines",
                    RunId = "run-1"
                }
            }
        };

        var json = JsonSerializer.Serialize(batch);
        var deserialized = JsonSerializer.Deserialize<RelationsBatch>(json);

        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.Relations, Has.Count.EqualTo(1));
        Assert.That(deserialized.Relations[0].Id, Is.EqualTo("rel-1"));
        Assert.That(deserialized.Relations[0].RelationType, Is.EqualTo("defines"));
        Assert.That(json, Does.Contain("\"relations\":["));
    }

    [Test]
    public void BatchAck_RoundTrip_SerializesAndDeserializesCorrectly()
    {
        var runId = Guid.NewGuid();
        var ack = new BatchAck
        {
            BatchCount = 42,
            RunId = runId,
            Timestamp = new DateTime(2026, 10, 10, 14, 0, 0, DateTimeKind.Utc)
        };

        var json = JsonSerializer.Serialize(ack);
        var deserialized = JsonSerializer.Deserialize<BatchAck>(json);

        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.BatchCount, Is.EqualTo(42));
        Assert.That(deserialized.RunId, Is.EqualTo(runId));
        Assert.That(deserialized.Timestamp, Is.EqualTo(ack.Timestamp));
        Assert.That(json, Does.Contain("\"batch_count\":42"));
        Assert.That(json, Does.Contain("\"run_id\":\"" + runId.ToString() + "\""));
        Assert.That(json, Does.Contain("\"timestamp\":"));
    }

    [Test]
    public void StatusAck_RoundTrip_SerializesAndDeserializesCorrectly()
    {
        var timestamp = new DateTime(2026, 10, 10, 16, 30, 0, DateTimeKind.Utc);
        var ack = new StatusAck
        {
            InstanceId = "worker-abc-123",
            Timestamp = timestamp
        };

        var json = JsonSerializer.Serialize(ack);
        var deserialized = JsonSerializer.Deserialize<StatusAck>(json);

        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.InstanceId, Is.EqualTo("worker-abc-123"));
        Assert.That(deserialized.Timestamp, Is.EqualTo(timestamp));
        Assert.That(json, Does.Contain("\"instance_id\":\"worker-abc-123\""));
        Assert.That(json, Does.Contain("\"timestamp\":"));
    }

    [Test]
    public void AnalysisCompletedAck_RoundTrip_SerializesAndDeserializesCorrectly()
    {
        var timestamp = new DateTime(2026, 10, 10, 16, 35, 0, DateTimeKind.Utc);
        var ack = new AnalysisCompletedAck
        {
            RunId = "run-uuid-9999",
            Timestamp = timestamp
        };

        var json = JsonSerializer.Serialize(ack);
        var deserialized = JsonSerializer.Deserialize<AnalysisCompletedAck>(json);

        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.RunId, Is.EqualTo("run-uuid-9999"));
        Assert.That(deserialized.Timestamp, Is.EqualTo(timestamp));
        Assert.That(json, Does.Contain("\"run_id\":\"run-uuid-9999\""));
        Assert.That(json, Does.Contain("\"timestamp\":"));
    }

    [Test]
    public void RunAnalysisAck_RoundTrip_SerializesAndDeserializesCorrectly()
    {
        var timestamp = new DateTime(2026, 10, 10, 16, 40, 0, DateTimeKind.Utc);
        var ack = new RunAnalysisAck
        {
            InstanceId = "analyzer-worker-7",
            RunId = "run-uuid-8888",
            Timestamp = timestamp
        };

        var json = JsonSerializer.Serialize(ack);
        var deserialized = JsonSerializer.Deserialize<RunAnalysisAck>(json);

        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.InstanceId, Is.EqualTo("analyzer-worker-7"));
        Assert.That(deserialized.RunId, Is.EqualTo("run-uuid-8888"));
        Assert.That(deserialized.Timestamp, Is.EqualTo(timestamp));
        Assert.That(json, Does.Contain("\"instanceId\":\"analyzer-worker-7\""));
        Assert.That(json, Does.Contain("\"run_id\":\"run-uuid-8888\""));
        Assert.That(json, Does.Contain("\"timestamp\":"));
    }

    [Test]
    public void ErrorMessage_RoundTrip_PreservesAllProperties()
    {
        var timestamp = new DateTime(2026, 10, 10, 17, 0, 0, DateTimeKind.Utc);
        var error = new ErrorMessage
        {
            Code = NoxVela.Protocol.Errors.ProtocolErrorCodes.GIT_CLONE_FAILED,
            Message = "Authentication failed for remote repository",
            RunId = "run-uuid-5555",
            IsFatal = true,
            Details = "fatal: Authentication failed for 'https://github.com/secret/repo.git'",
            Timestamp = timestamp
        };

        var json = JsonSerializer.Serialize(error);
        var deserialized = JsonSerializer.Deserialize<ErrorMessage>(json);

        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.Code, Is.EqualTo(NoxVela.Protocol.Errors.ProtocolErrorCodes.GIT_CLONE_FAILED));
        Assert.That(deserialized.Message, Is.EqualTo("Authentication failed for remote repository"));
        Assert.That(deserialized.RunId, Is.EqualTo("run-uuid-5555"));
        Assert.That(deserialized.IsFatal, Is.True);
        Assert.That(deserialized.Details, Is.EqualTo("fatal: Authentication failed for 'https://github.com/secret/repo.git'"));
        Assert.That(deserialized.Timestamp, Is.EqualTo(timestamp));
        Assert.That(json, Does.Contain("\"code\":\"GIT_CLONE_FAILED\""));
        Assert.That(json, Does.Contain("\"is_fatal\":true"));
    }

    [Test]
    public void ErrorMessage_WithoutOptionalFields_DeserializesWithDefaults()
    {
        var json = """
        {
            "code": "DATABASE_UNAVAILABLE",
            "message": "SurrealDB instance unreachable"
        }
        """;

        var deserialized = JsonSerializer.Deserialize<ErrorMessage>(json);

        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.Code, Is.EqualTo("DATABASE_UNAVAILABLE"));
        Assert.That(deserialized.Message, Is.EqualTo("SurrealDB instance unreachable"));
        Assert.That(deserialized.RunId, Is.Null);
        Assert.That(deserialized.IsFatal, Is.False);
        Assert.That(deserialized.Details, Is.Null);
    }

    [Test]
    public void AnalysisStatus_WithTelemetryFields_RoundTrip_PreservesAllProperties()
    {
        var status = new AnalysisStatus
        {
            InstanceId = "worker-1",
            StatusId = (short)ServiceStatuses.WORKING,
            Message = "Parsing syntax tree: 45/100 files",
            ProgressPercentage = 45.0,
            ProcessedItems = 45,
            TotalItems = 100,
            CurrentFile = "Services/AnalysisPipeline.cs",
            RunId = "run-uuid-1234"
        };

        var json = JsonSerializer.Serialize(status);
        var deserialized = JsonSerializer.Deserialize<AnalysisStatus>(json);

        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.InstanceId, Is.EqualTo("worker-1"));
        Assert.That(deserialized.StatusId, Is.EqualTo((short)ServiceStatuses.WORKING));
        Assert.That(deserialized.Message, Is.EqualTo("Parsing syntax tree: 45/100 files"));
        Assert.That(deserialized.ProgressPercentage, Is.EqualTo(45.0));
        Assert.That(deserialized.ProcessedItems, Is.EqualTo(45));
        Assert.That(deserialized.TotalItems, Is.EqualTo(100));
        Assert.That(deserialized.CurrentFile, Is.EqualTo("Services/AnalysisPipeline.cs"));
        Assert.That(deserialized.RunId, Is.EqualTo("run-uuid-1234"));

        Assert.That(json, Does.Contain("\"progress_percentage\":45"));
        Assert.That(json, Does.Contain("\"processed_items\":45"));
        Assert.That(json, Does.Contain("\"total_items\":100"));
        Assert.That(json, Does.Contain("\"current_file\":\"Services/AnalysisPipeline.cs\""));
        Assert.That(json, Does.Contain("\"run_id\":\"run-uuid-1234\""));
    }

    [Test]
    public void AnalysisStatus_WithoutTelemetryFields_DeserializesWithNullDefaults()
    {
        var json = """
        {
            "instanceId": "worker-1",
            "status": 1,
            "message": "Idle"
        }
        """;

        var deserialized = JsonSerializer.Deserialize<AnalysisStatus>(json);

        Assert.That(deserialized, Is.Not.Null);
        Assert.That(deserialized!.Message, Is.EqualTo("Idle"));
        Assert.That(deserialized.StatusId, Is.EqualTo((short)1));
        Assert.That(deserialized.ProgressPercentage, Is.Null);
        Assert.That(deserialized.ProcessedItems, Is.Null);
        Assert.That(deserialized.TotalItems, Is.Null);
        Assert.That(deserialized.CurrentFile, Is.Null);
        Assert.That(deserialized.RunId, Is.Null);
    }
}




