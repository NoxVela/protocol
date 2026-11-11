using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using NoxVela.Protocol.Models.Code;

namespace NoxVela.Protocol.Models.Abstract;

/// <summary>
/// Polymorphic JSON converter for <see cref="NodeBase"/> hierarchies using the "type" discriminator.
/// </summary>
public class NodeBaseConverter : JsonConverter<NodeBase>
{
    /// <inheritdoc />
    public override NodeBase? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;
        if (!root.TryGetProperty("type", out var typeProp))
        {
            throw new JsonException("Missing required 'type' discriminator on NodeBase JSON object.");
        }

        var typeStr = typeProp.GetString();
        Type targetType = typeStr switch
        {
            AllNodesTypes.ClassNodes => typeof(ClassNode),
            AllNodesTypes.InterfaceNodes => typeof(InterfaceNode),
            AllNodesTypes.StructNodes => typeof(StructNode),
            AllNodesTypes.RecordNodes => typeof(RecordNode),
            AllNodesTypes.EnumsNodes => typeof(EnumNode),
            AllNodesTypes.DelegatesNodes => typeof(DelegateNode),
            AllNodesTypes.MethodNodes => typeof(MethodNode),
            AllNodesTypes.PropertyNodes => typeof(PropertyNode),
            AllNodesTypes.FieldNodes => typeof(FieldNode),
            AllNodesTypes.EventsNodes => typeof(EventNode),
            AllNodesTypes.AttributeNodes => typeof(AttributeNode),
            AllNodesTypes.ParameterNodes => typeof(ParameterNode),
            AllNodesTypes.ExternalTypeNodes => typeof(ExternalTypeNode),
            AllNodesTypes.NamespaceNodes => typeof(NamespaceNode),
            AllNodesTypes.UnresolvedNodes => typeof(UnresolvedNode),
            _ => throw new JsonException($"Unknown or unsupported node type discriminator: '{typeStr}'.")
        };

        var rawJson = root.GetRawText();
        return (NodeBase?)JsonSerializer.Deserialize(rawJson, targetType, options);
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, NodeBase value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}
