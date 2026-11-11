using System.Collections.Generic;

namespace NoxVela.Protocol.Infrastructure;

/// <summary>
/// Aggregates canonical string constants for AI subsystem capabilities.
/// Capabilities remain open-ended strings to support modular AI services and future neural extensions.
/// </summary>
public static class AiCapabilities
{
    /// <summary>
    /// Interactive programming assistant and streaming RAG conversational responses.
    /// </summary>
    public const string CHAT = "chat";

    /// <summary>
    /// Background asynchronous text and identifier vectorization (e.g., Nomic, KeyBERT).
    /// </summary>
    public const string CONTINUOUS_EMBEDDING = "continuous_embedding";


    /// <summary>
    /// Semantic vector space retrieval and code search.
    /// </summary>
    public const string RAG_SEMANTIC = "rag_semantic";

    /// <summary>
    /// Hybrid retrieval fusion combining BM25 keyword matching, dense vector embeddings, and graph topology.
    /// </summary>
    public const string RAG_HYBRID = "rag_hybrid";

    /// <summary>
    /// Structural graph neural network evaluation and node representation compression into HGT vectors.
    /// </summary>
    public const string GNN_EMBEDDING = "gnn_embedding";

    /// <summary>
    /// Unsupervised architectural anomaly and smell detection (e.g., Feature Envy, God Class, Layer Violation).
    /// </summary>
    public const string ARCHITECTURAL_SMELLS = "architectural_smells";

    /// <summary>
    /// Gets all standard AI capability constants.
    /// </summary>
    public static readonly IReadOnlyCollection<string> All = new[]
    {
        CHAT,
        CONTINUOUS_EMBEDDING,
        RAG_SEMANTIC,
        RAG_HYBRID,
        GNN_EMBEDDING,
        ARCHITECTURAL_SMELLS
    };
}
