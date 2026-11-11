using System;
using NoxVela.Protocol.Infrastructure;
using NoxVela.Protocol.Messages;
using NoxVela.Protocol.Messages.Server;
using NoxVela.Protocol.Models.Code;
using NUnit.Framework;

namespace NoxVela.Protocol.Tests.Constants;

[TestFixture]
public class ProtocolConstantsTests
{
    [Test]
    public void ServerMessagesTypes_StandardConstants_HaveExpectedValues()
    {
        Assert.That(ServerMessagesTypes.RUN_ANALYSIS, Is.EqualTo("run_analysis"));
        Assert.That(ServerMessagesTypes.REGISTRATION_RESULT, Is.EqualTo("registration_result"));
        Assert.That(ServerMessagesTypes.CANCEL_ANALYSIS, Is.EqualTo("cancel_analysis"));
        Assert.That(ServerMessagesTypes.BATCH_ACK, Is.EqualTo("batch_ack"));
        Assert.That(ServerMessagesTypes.STATUS_ACK, Is.EqualTo("status_ack"));
        Assert.That(ServerMessagesTypes.ANALYSIS_COMPLETED_ACK, Is.EqualTo("analysis_completed_ack"));
        Assert.That(ServerMessagesTypes.ERROR, Is.EqualTo("error"));
    }

    [Test]
    public void ServiceMessagesTypes_StandardConstants_HaveExpectedValues()
    {
        Assert.That(ServiceMessagesTypes.REGISTRATION, Is.EqualTo("registration"));
        Assert.That(ServiceMessagesTypes.STATUS, Is.EqualTo("status"));
        Assert.That(ServiceMessagesTypes.NODE, Is.EqualTo("node"));
        Assert.That(ServiceMessagesTypes.NODES_BATCH, Is.EqualTo("nodes_batch"));
        Assert.That(ServiceMessagesTypes.RELATION, Is.EqualTo("relation"));
        Assert.That(ServiceMessagesTypes.RELATIONS_BATCH, Is.EqualTo("relations_batch"));
        Assert.That(ServiceMessagesTypes.ANALYSIS_STATUS, Is.EqualTo("analysis_status"));
        Assert.That(ServiceMessagesTypes.ANALYSIS_METRICS, Is.EqualTo("analysis_metrics"));
        Assert.That(ServiceMessagesTypes.RUN_ANALYSIS_ACK, Is.EqualTo("run_analysis_ack"));
        Assert.That(ServiceMessagesTypes.ERROR, Is.EqualTo("error"));
    }

    [Test]
    public void ProtocolErrorCodes_StandardConstants_HaveExpectedValues()
    {
        Assert.That(NoxVela.Protocol.Errors.ProtocolErrorCodes.DATABASE_UNAVAILABLE, Is.EqualTo("DATABASE_UNAVAILABLE"));
        Assert.That(NoxVela.Protocol.Errors.ProtocolErrorCodes.RATE_LIMITED, Is.EqualTo("RATE_LIMITED"));
        Assert.That(NoxVela.Protocol.Errors.ProtocolErrorCodes.PAYLOAD_VALIDATION_ERROR, Is.EqualTo("PAYLOAD_VALIDATION_ERROR"));
        Assert.That(NoxVela.Protocol.Errors.ProtocolErrorCodes.UNHANDLED_SERVER_ERROR, Is.EqualTo("UNHANDLED_SERVER_ERROR"));
        Assert.That(NoxVela.Protocol.Errors.ProtocolErrorCodes.MALFORMED_FRAME, Is.EqualTo("MALFORMED_FRAME"));

        Assert.That(NoxVela.Protocol.Errors.ProtocolErrorCodes.GIT_CLONE_FAILED, Is.EqualTo("GIT_CLONE_FAILED"));
        Assert.That(NoxVela.Protocol.Errors.ProtocolErrorCodes.COMPILATION_FAILED, Is.EqualTo("COMPILATION_FAILED"));
        Assert.That(NoxVela.Protocol.Errors.ProtocolErrorCodes.OUT_OF_MEMORY, Is.EqualTo("OUT_OF_MEMORY"));
        Assert.That(NoxVela.Protocol.Errors.ProtocolErrorCodes.ANALYSIS_EXECUTION_FAILED, Is.EqualTo("ANALYSIS_EXECUTION_FAILED"));
        Assert.That(NoxVela.Protocol.Errors.ProtocolErrorCodes.TIMEOUT, Is.EqualTo("TIMEOUT"));
    }

    [Test]
    public void NodesRelations_StandardConstants_AreNotEmpty()
    {
        Assert.That(NodesRelations.CONTAINS, Is.EqualTo("CONTAINS"));
        Assert.That(NodesRelations.DEFINES, Is.EqualTo("DEFINES"));
        Assert.That(NodesRelations.CALLS, Is.EqualTo("CALLS"));
        Assert.That(NodesRelations.IMPLEMENTS, Is.EqualTo("IMPLEMENTS"));
        Assert.That(NodesRelations.EXTENDS, Is.EqualTo("EXTENDS"));
        Assert.That(NodesRelations.READS, Is.EqualTo("READS"));
        Assert.That(NodesRelations.WRITES, Is.EqualTo("WRITES"));
        Assert.That(NodesRelations.OF_TYPE, Is.EqualTo("OF_TYPE"));
        Assert.That(NodesRelations.RETURNS, Is.EqualTo("RETURNS"));
    }

    [Test]
    public void AllNodesTypes_StandardConstants_MatchExpectedIdentifiers()
    {
        Assert.That(AllNodesTypes.AttributeNodes, Is.EqualTo("attributes"));
        Assert.That(AllNodesTypes.ClassNodes, Is.EqualTo("classes"));
        Assert.That(AllNodesTypes.DelegatesNodes, Is.EqualTo("delegates"));
        Assert.That(AllNodesTypes.EnumsNodes, Is.EqualTo("enums"));
        Assert.That(AllNodesTypes.EventsNodes, Is.EqualTo("events"));
        Assert.That(AllNodesTypes.ExternalTypeNodes, Is.EqualTo("external_types"));
        Assert.That(AllNodesTypes.FieldNodes, Is.EqualTo("fields"));
        Assert.That(AllNodesTypes.InterfaceNodes, Is.EqualTo("interfaces"));
        Assert.That(AllNodesTypes.MethodNodes, Is.EqualTo("methods"));
        Assert.That(AllNodesTypes.NamespaceNodes, Is.EqualTo("namespaces"));
        Assert.That(AllNodesTypes.ParameterNodes, Is.EqualTo("parameters"));
        Assert.That(AllNodesTypes.PropertyNodes, Is.EqualTo("properties"));
        Assert.That(AllNodesTypes.RecordNodes, Is.EqualTo("records"));
        Assert.That(AllNodesTypes.StructNodes, Is.EqualTo("structs"));
        Assert.That(AllNodesTypes.UnresolvedNodes, Is.EqualTo("unresolved"));
    }

    [Test]
    public void Enums_NumericValues_MatchSpecification()
    {
        // RegistrationStatuses
        Assert.That((int)RegistrationStatuses.ACCEPTED, Is.EqualTo(0));
        Assert.That((int)RegistrationStatuses.REJECTED, Is.EqualTo(1));

        // ServiceStatuses
        Assert.That((int)ServiceStatuses.IDLE, Is.EqualTo(0));
        Assert.That((int)ServiceStatuses.WORKING, Is.EqualTo(1));
        Assert.That((int)ServiceStatuses.ERROR, Is.EqualTo(2));
        Assert.That((int)ServiceStatuses.COMPLETING, Is.EqualTo(3));
        Assert.That((int)ServiceStatuses.DONE, Is.EqualTo(4));

        // ServiceTypes
        Assert.That(ServiceTypes.ANALYZER, Is.EqualTo(0));
        Assert.That(ServiceTypes.AI, Is.EqualTo(100));
        Assert.That(ServiceTypes.ToName(0), Is.EqualTo("ANALYZER"));
        Assert.That(ServiceTypes.ToName(100), Is.EqualTo("AI"));

        // NodeAccessibility
        Assert.That((int)NodeAccessibility.Public, Is.EqualTo(6));
        Assert.That((int)NodeAccessibility.ProtectedOrInternal, Is.EqualTo(5));
        Assert.That((int)NodeAccessibility.Internal, Is.EqualTo(4));
        Assert.That((int)NodeAccessibility.Protected, Is.EqualTo(3));
        Assert.That((int)NodeAccessibility.ProtectedAndInternal, Is.EqualTo(2));
        Assert.That((int)NodeAccessibility.Private, Is.EqualTo(1));
        Assert.That((int)NodeAccessibility.NotApplicable, Is.EqualTo(0));
    }

    [Test]
    public void AnalyzerCapabilities_StandardConstants_HaveExpectedValues()
    {
        Assert.That(AnalyzerCapabilities.SYNTAX_TREE_PARSING, Is.EqualTo("syntax_tree_parsing"));
        Assert.That(AnalyzerCapabilities.CALL_GRAPH, Is.EqualTo("call_graph"));
        Assert.That(AnalyzerCapabilities.TYPE_HIERARCHY, Is.EqualTo("type_hierarchy"));
        Assert.That(AnalyzerCapabilities.METRICS, Is.EqualTo("metrics"));
        Assert.That(AnalyzerCapabilities.DEPENDENCY_ANALYSIS, Is.EqualTo("dependency_analysis"));
        Assert.That(AnalyzerCapabilities.MEMORY_LEAK_DETECTION, Is.EqualTo("memory_leak_detection"));
        Assert.That(AnalyzerCapabilities.SECURITY_SCAN, Is.EqualTo("security_scan"));
        Assert.That(AnalyzerCapabilities.ENGINE_ROSLYN, Is.EqualTo("engine:roslyn"));
        Assert.That(AnalyzerCapabilities.ENGINE_TREESITTER, Is.EqualTo("engine:tree_sitter"));
        Assert.That(AnalyzerCapabilities.COMPUTE_LIGHTWEIGHT, Is.EqualTo("compute:lightweight"));
        Assert.That(AnalyzerCapabilities.COMPUTE_HEAVY, Is.EqualTo("compute:heavy_compute"));

        Assert.That(AnalyzerCapabilities.All, Has.Count.EqualTo(11));
        Assert.That(AnalyzerCapabilities.All, Does.Contain(AnalyzerCapabilities.SYNTAX_TREE_PARSING));
        Assert.That(AnalyzerCapabilities.All, Does.Contain(AnalyzerCapabilities.CALL_GRAPH));
        Assert.That(AnalyzerCapabilities.All, Does.Contain(AnalyzerCapabilities.TYPE_HIERARCHY));
        Assert.That(AnalyzerCapabilities.All, Does.Contain(AnalyzerCapabilities.METRICS));
        Assert.That(AnalyzerCapabilities.All, Does.Contain(AnalyzerCapabilities.DEPENDENCY_ANALYSIS));
        Assert.That(AnalyzerCapabilities.All, Does.Contain(AnalyzerCapabilities.MEMORY_LEAK_DETECTION));
        Assert.That(AnalyzerCapabilities.All, Does.Contain(AnalyzerCapabilities.SECURITY_SCAN));
        Assert.That(AnalyzerCapabilities.All, Does.Contain(AnalyzerCapabilities.ENGINE_ROSLYN));
        Assert.That(AnalyzerCapabilities.All, Does.Contain(AnalyzerCapabilities.ENGINE_TREESITTER));
        Assert.That(AnalyzerCapabilities.All, Does.Contain(AnalyzerCapabilities.COMPUTE_LIGHTWEIGHT));
        Assert.That(AnalyzerCapabilities.All, Does.Contain(AnalyzerCapabilities.COMPUTE_HEAVY));
    }

    [Test]
    public void CompilerEngines_StandardConstants_HaveExpectedValues()
    {
        Assert.That(CompilerEngines.ROSLYN, Is.EqualTo("roslyn"));
        Assert.That(CompilerEngines.TREE_SITTER, Is.EqualTo("tree-sitter"));

        Assert.That(CompilerEngines.All, Has.Count.EqualTo(2));
        Assert.That(CompilerEngines.All, Does.Contain(CompilerEngines.ROSLYN));
        Assert.That(CompilerEngines.All, Does.Contain(CompilerEngines.TREE_SITTER));
    }

    [Test]
    public void TaskComputeWeights_StandardConstants_HaveExpectedValues()
    {
        Assert.That(TaskComputeWeights.LIGHTWEIGHT, Is.EqualTo("lightweight"));
        Assert.That(TaskComputeWeights.HEAVY_COMPUTE, Is.EqualTo("heavy_compute"));
        Assert.That(TaskComputeWeights.STANDARD, Is.EqualTo("standard"));

        Assert.That(TaskComputeWeights.All, Has.Count.EqualTo(3));
        Assert.That(TaskComputeWeights.All, Does.Contain(TaskComputeWeights.LIGHTWEIGHT));
        Assert.That(TaskComputeWeights.All, Does.Contain(TaskComputeWeights.HEAVY_COMPUTE));
        Assert.That(TaskComputeWeights.All, Does.Contain(TaskComputeWeights.STANDARD));
    }

    [Test]
    public void AiCapabilities_StandardConstants_HaveExpectedValues()
    {
        Assert.That(AiCapabilities.CHAT, Is.EqualTo("chat"));
        Assert.That(AiCapabilities.CONTINUOUS_EMBEDDING, Is.EqualTo("continuous_embedding"));
        Assert.That(AiCapabilities.RAG_SEMANTIC, Is.EqualTo("rag_semantic"));
        Assert.That(AiCapabilities.RAG_HYBRID, Is.EqualTo("rag_hybrid"));
        Assert.That(AiCapabilities.GNN_EMBEDDING, Is.EqualTo("gnn_embedding"));
        Assert.That(AiCapabilities.ARCHITECTURAL_SMELLS, Is.EqualTo("architectural_smells"));

        Assert.That(AiCapabilities.All, Has.Count.EqualTo(6));
        Assert.That(AiCapabilities.All, Does.Contain(AiCapabilities.CHAT));
        Assert.That(AiCapabilities.All, Does.Contain(AiCapabilities.CONTINUOUS_EMBEDDING));
        Assert.That(AiCapabilities.All, Does.Contain(AiCapabilities.RAG_SEMANTIC));
        Assert.That(AiCapabilities.All, Does.Contain(AiCapabilities.RAG_HYBRID));
        Assert.That(AiCapabilities.All, Does.Contain(AiCapabilities.GNN_EMBEDDING));
        Assert.That(AiCapabilities.All, Does.Contain(AiCapabilities.ARCHITECTURAL_SMELLS));
    }
}
