using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Schema;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using NoxVela.Protocol.Infrastructure;
using NoxVela.Protocol.Messages;
using NoxVela.Protocol.Messages.Server;
using NoxVela.Protocol.Messages.Service;
using NoxVela.Protocol.Models.Abstract;
using NoxVela.Protocol.Models.Code;
using YamlDotNet.Serialization;

namespace NoxVela.Protocol.Generator;

public static class AsyncApiDocumentBuilder
{
    public static void Generate(string rootProtocolDir, bool generateHtml = false)
    {
        Console.WriteLine($"[AsyncApiGenerator] Starting AsyncAPI 3.1 specification generation from C# models...");
        Console.WriteLine($"[AsyncApiGenerator] Target root directory: {rootProtocolDir}");

        XmlDocumentationReader.Initialize(rootProtocolDir);

        var specDir = Path.Combine(rootProtocolDir, "spec");
        var docsDir = Path.Combine(rootProtocolDir, "docs");
        Directory.CreateDirectory(specDir);
        Directory.CreateDirectory(docsDir);

        var specFilePath = Path.Combine(specDir, "asyncapi.yaml");

        var doc = BuildAsyncApiDocument();

        var serializer = new SerializerBuilder()
            .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull)
            .Build();

        var yaml = serializer.Serialize(doc);

        File.WriteAllText(specFilePath, yaml);
        Console.WriteLine($"[AsyncApiGenerator] Successfully generated AsyncAPI specification at: {specFilePath}");

        if (generateHtml)
        {
            TryRegenerateHtmlDocs(specFilePath, docsDir);
        }
        else
        {
            Console.WriteLine("[AsyncApiGenerator] HTML generation skipped (pass --html or set GENERATE_ASYNCAPI_HTML=true to regenerate docs/).");
        }
    }

    private static Dictionary<string, object> BuildAsyncApiDocument()
    {
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            TypeInfoResolver = new DefaultJsonTypeInfoResolver()
        };

        var schemas = new Dictionary<string, object>();

        // Register Core Payloads
        RegisterSchema(schemas, "RegistrationPayload", typeof(Registration), options, "Payload for registering a service with the server.");
        RegisterSchema(schemas, "StatusPayload", typeof(Status), options, "Periodic status heartbeat payload.");
        RegisterSchema(schemas, "AnalysisStatusPayload", typeof(AnalysisStatus), options, "Analysis progress and lifecycle status payload.");
        RegisterSchema(schemas, "AnalysisMetricsPayload", typeof(AnalysisMetrics), options, "Summary metrics emitted at the conclusion of an analysis run.");
        RegisterSchema(schemas, "RunAnalysisPayload", typeof(RunAnalysis), options, "Task dispatch payload commanding an analyzer to analyze a code repository.");
        RegisterSchema(schemas, "RunAnalysisAckPayload", typeof(RunAnalysisAck), options, "Acknowledgment payload sent by worker upon accepting a run_analysis task.");
        RegisterSchema(schemas, "CancelAnalysisPayload", typeof(CancelAnalysis), options, "Payload for commanding an analyzer to cancel an active analysis task.");
        RegisterSchema(schemas, "StatusAckPayload", typeof(StatusAck), options, "Server heartbeat acknowledgment payload.");
        RegisterSchema(schemas, "BatchAckPayload", typeof(BatchAck), options, "Server acknowledgment for a processed micro-batch of AST nodes or relations.");
        RegisterSchema(schemas, "AnalysisCompletedAckPayload", typeof(AnalysisCompletedAck), options, "Server acknowledgment confirming that all AST nodes and relations have been finalized in the database.");
        RegisterSchema(schemas, "NodesBatchPayload", typeof(NodesBatch), options, "Micro-batch array payload containing multiple AST nodes.");
        RegisterSchema(schemas, "RelationsBatchPayload", typeof(RelationsBatch), options, "Micro-batch array payload containing multiple AST relation edges.");
        RegisterSchema(schemas, "ErrorMessagePayload", typeof(ErrorMessage), options, "Standardized error message payload for reporting failures.");
        RegisterSchema(schemas, "RegistrationStatusPayload", typeof(RegistrationStatus), options, "Server registration response payload indicating whether registration was accepted.");

        // Register NodeBase as base schema object
        schemas["NodeBase"] = new Dictionary<string, object>
        {
            ["type"] = "object",
            ["description"] = "Base schema for all code AST entities.",
            ["required"] = new List<string> { "stable_id", "content_hash", "type" },
            ["properties"] = new Dictionary<string, object>
            {
                ["stable_id"] = new Dictionary<string, object>
                {
                    ["type"] = "string",
                    ["description"] = "Deterministic ID based on structural language properties, invariant across builds.",
                    ["example"] = "MyApp.Services.UserService"
                },
                ["content_hash"] = new Dictionary<string, object>
                {
                    ["type"] = "string",
                    ["description"] = "Hash representing node content/definition state.",
                    ["example"] = "8f4b237c9a1e05d2"
                },
                ["type"] = new Dictionary<string, object>
                {
                    ["type"] = "string",
                    ["description"] = "AST entity type: classes, methods, fields, properties, interfaces, enums, records, structs, namespaces, parameters, delegates, events, attributes, etc."
                },
                ["parent_id"] = new Dictionary<string, object>
                {
                    ["type"] = "string",
                    ["nullable"] = true,
                    ["description"] = "Identifier of parent container node."
                },
                ["language"] = new Dictionary<string, object>
                {
                    ["type"] = "string",
                    ["nullable"] = true,
                    ["description"] = "Programming language (e.g. csharp, python)."
                }
            }
        };

        // Register AST and Code Models
        RegisterSchema(schemas, "ClassNode", typeof(ClassNode), options, "Represents a class definition in the AST graph.");
        RegisterSchema(schemas, "InterfaceNode", typeof(InterfaceNode), options, "Represents an interface definition in the AST graph.");
        RegisterSchema(schemas, "RecordNode", typeof(RecordNode), options, "Represents a record (class or struct) definition in the AST graph.");
        RegisterSchema(schemas, "StructNode", typeof(StructNode), options, "Represents a struct definition in the AST graph.");
        RegisterSchema(schemas, "EnumNode", typeof(EnumNode), options, "Represents an enum definition in the AST graph.");
        RegisterSchema(schemas, "MethodNode", typeof(MethodNode), options, "Represents a method or constructor in the AST graph.");
        RegisterSchema(schemas, "FieldNode", typeof(FieldNode), options, "Represents a field in the AST graph.");
        RegisterSchema(schemas, "PropertyNode", typeof(PropertyNode), options, "Represents a property in the AST graph.");
        RegisterSchema(schemas, "EventNode", typeof(EventNode), options, "Represents an event member in the AST graph.");
        RegisterSchema(schemas, "DelegateNode", typeof(DelegateNode), options, "Represents a delegate definition in the AST graph.");
        RegisterSchema(schemas, "NamespaceNode", typeof(NamespaceNode), options, "Represents a namespace container in the AST graph.");
        RegisterSchema(schemas, "ParameterNode", typeof(ParameterNode), options, "Represents a method parameter in the AST graph.");
        RegisterSchema(schemas, "AttributeNode", typeof(AttributeNode), options, "Represents a code attribute/decorator in the AST graph.");
        RegisterSchema(schemas, "RelationNode", typeof(RelationNode), options, "Represents a directed relationship edge between two AST entities.");
        RegisterSchema(schemas, "CodeLocation", typeof(CodeLocation), options, "Source code file location coordinates.");

        // Define Messages (Envelopes)
        var messages = new Dictionary<string, object>
        {
            ["RegistrationMessage"] = CreateEnvelopeMessage("RegistrationMessage", "Service Registration", "Service registers its capabilities and metadata upon connecting.", ServiceMessagesTypes.REGISTRATION, "RegistrationPayload"),
            ["StatusMessage"] = CreateEnvelopeMessage("StatusMessage", "Heartbeat Status", "Periodic heartbeat report from worker to server.", ServiceMessagesTypes.STATUS, "StatusPayload"),
            ["AnalysisStatusMessage"] = CreateEnvelopeMessage("AnalysisStatusMessage", "Analysis Status", "Lifecycle progress and state reports during an analysis run.", ServiceMessagesTypes.ANALYSIS_STATUS, "AnalysisStatusPayload"),
            ["AnalysisMetricsMessage"] = CreateEnvelopeMessage("AnalysisMetricsMessage", "Analysis Metrics", "Telemetry metrics emitted at the conclusion of an analysis run.", ServiceMessagesTypes.ANALYSIS_METRICS, "AnalysisMetricsPayload"),
            ["NodeMessage"] = CreateEnvelopeMessage("NodeMessage", "AST Node", "Single AST node streamed during code analysis.", ServiceMessagesTypes.NODE, "NodeBase"),
            ["RelationMessage"] = CreateEnvelopeMessage("RelationMessage", "AST Relation", "Single AST relation edge streamed during code analysis.", ServiceMessagesTypes.RELATION, "RelationNode"),
            ["NodesBatchMessage"] = CreateEnvelopeMessage("NodesBatchMessage", "AST Nodes Batch", "Micro-batch containing multiple AST nodes streamed concurrently.", ServiceMessagesTypes.NODES_BATCH, "NodesBatchPayload"),
            ["RelationsBatchMessage"] = CreateEnvelopeMessage("RelationsBatchMessage", "AST Relations Batch", "Micro-batch containing multiple AST relations streamed concurrently.", ServiceMessagesTypes.RELATIONS_BATCH, "RelationsBatchPayload"),
            ["RunAnalysisAckMessage"] = CreateEnvelopeMessage("RunAnalysisAckMessage", "Run Analysis Acknowledgment", "Worker confirms receipt and acceptance of an analysis task.", ServiceMessagesTypes.RUN_ANALYSIS_ACK, "RunAnalysisAckPayload"),
            ["ErrorMessage"] = CreateEnvelopeMessage("ErrorMessage", "Error Message", "Standardized error notification frame.", "error", "ErrorMessagePayload"),

            // Server Messages
            ["RegistrationResultMessage"] = CreateEnvelopeMessage("RegistrationResultMessage", "Registration Result", "Server accepts or rejects service registration.", ServerMessagesTypes.REGISTRATION_RESULT, "RegistrationStatusPayload"),
            ["RunAnalysisMessage"] = CreateEnvelopeMessage("RunAnalysisMessage", "Run Analysis Command", "Server dispatches a repository analysis task to an idle worker.", ServerMessagesTypes.RUN_ANALYSIS, "RunAnalysisPayload"),
            ["CancelAnalysisMessage"] = CreateEnvelopeMessage("CancelAnalysisMessage", "Cancel Analysis Command", "Server commands a worker to abort an active analysis task.", ServerMessagesTypes.CANCEL_ANALYSIS, "CancelAnalysisPayload"),
            ["StatusAckMessage"] = CreateEnvelopeMessage("StatusAckMessage", "Status Acknowledgment", "Server acknowledges heartbeat status to detect half-open sockets.", ServerMessagesTypes.STATUS_ACK, "StatusAckPayload"),
            ["BatchAckMessage"] = CreateEnvelopeMessage("BatchAckMessage", "Batch Acknowledgment", "Server confirms persistence of a micro-batch.", ServerMessagesTypes.BATCH_ACK, "BatchAckPayload"),
            ["AnalysisCompletedAckMessage"] = CreateEnvelopeMessage("AnalysisCompletedAckMessage", "Analysis Completed Acknowledgment", "Server confirms all nodes and relations are reconciled in database.", ServerMessagesTypes.ANALYSIS_COMPLETED_ACK, "AnalysisCompletedAckPayload")
        };

        // Channel Messages references
        var channelMessages = new Dictionary<string, object>();
        foreach (var msgKey in messages.Keys)
        {
            var camelKey = char.ToLowerInvariant(msgKey[0]) + msgKey.Substring(1);
            channelMessages[camelKey] = new Dictionary<string, string> { ["$ref"] = $"#/components/messages/{msgKey}" };
        }

        var channels = new Dictionary<string, object>
        {
            ["wsChannel"] = new Dictionary<string, object>
            {
                ["address"] = "/ws",
                ["description"] = "Bi-directional WebSocket communication channel for analyzer/service registration, task dispatch, and AST graph streaming.",
                ["messages"] = channelMessages
            }
        };

        // Operations
        var operations = new Dictionary<string, object>
        {
            ["registerService"] = CreateOperation("send", "Service registers itself with the server upon connection.", "registrationMessage"),
            ["reportStatus"] = CreateOperation("send", "Worker sends periodic heartbeat status.", "statusMessage"),
            ["reportAnalysisStatus"] = CreateOperation("send", "Worker reports analysis lifecycle progress.", "analysisStatusMessage"),
            ["reportAnalysisMetrics"] = CreateOperation("send", "Worker emits summary metrics after analysis.", "analysisMetricsMessage"),
            ["streamNode"] = CreateOperation("send", "Worker streams a single AST node.", "nodeMessage"),
            ["streamRelation"] = CreateOperation("send", "Worker streams a single AST relation edge.", "relationMessage"),
            ["streamNodesBatch"] = CreateOperation("send", "Worker flushes a micro-batch of AST nodes.", "nodesBatchMessage"),
            ["streamRelationsBatch"] = CreateOperation("send", "Worker flushes a micro-batch of AST relations.", "relationsBatchMessage"),
            ["acknowledgeRunAnalysis"] = CreateOperation("send", "Worker confirms acceptance of an analysis task.", "runAnalysisAckMessage"),
            ["reportError"] = CreateOperation("send", "Worker or server emits an error message.", "errorMessage"),

            ["acknowledgeRegistration"] = CreateOperation("receive", "Server acknowledges or rejects service registration.", "registrationResultMessage"),
            ["dispatchAnalysis"] = CreateOperation("receive", "Server commands worker to run analysis.", "runAnalysisMessage"),
            ["cancelAnalysis"] = CreateOperation("receive", "Server commands worker to abort analysis.", "cancelAnalysisMessage"),
            ["acknowledgeStatus"] = CreateOperation("receive", "Server acknowledges heartbeat status.", "statusAckMessage"),
            ["acknowledgeBatch"] = CreateOperation("receive", "Server acknowledges batch persistence.", "batchAckMessage"),
            ["acknowledgeAnalysisCompleted"] = CreateOperation("receive", "Server confirms graph reconciliation in database.", "analysisCompletedAckMessage")
        };

        var envVersion = Environment.GetEnvironmentVariable("PROTOCOL_RELEASE_VERSION")?.Trim().TrimStart('v');
        var specVersion = !string.IsNullOrWhiteSpace(envVersion) ? envVersion : ProtocolVersions.CURRENT;

        var root = new Dictionary<string, object>
        {
            ["asyncapi"] = "3.1.0",
            ["info"] = new Dictionary<string, object>
            {
                ["title"] = "NoxVela Service Protocol",
                ["version"] = specVersion,
                ["description"] = "Bilateral WebSocket specification for communication between NoxVela Server and external worker services (Code Analyzers, AI services, etc.).\n\nAll messages across the WebSocket connection use UTF-8 text frames adhering to the `Wrapper` envelope.\nNotice that the `payload` property within the wrapper is a serialized JSON string.",
                ["license"] = new Dictionary<string, string>
                {
                    ["name"] = "MIT",
                    ["url"] = "https://opensource.org/licenses/MIT"
                }
            },
            ["defaultContentType"] = "application/json",
            ["channels"] = channels,
            ["operations"] = operations,
            ["components"] = new Dictionary<string, object>
            {
                ["messages"] = messages,
                ["schemas"] = schemas
            }
        };

        return root;
    }

    private static void RegisterSchema(Dictionary<string, object> schemas, string name, Type type, JsonSerializerOptions options, string? description = null)
    {
        var schemaNode = options.GetJsonSchemaAsNode(type);
        var typeDescription = description ?? XmlDocumentationReader.GetTypeDescription(type);
        NormalizeSchema(schemaNode, typeDescription);
        EnrichSchemaProperties(schemaNode, type, options);

        if (schemaNode is JsonObject rootObj)
        {
            // Remove root-level nullable flag for components.schemas definitions
            rootObj.Remove("nullable");
        }
        var deserializer = new DeserializerBuilder().Build();
        var yamlObj = deserializer.Deserialize<object>(schemaNode.ToJsonString());
        schemas[name] = yamlObj;
    }

    private static void EnrichSchemaProperties(JsonNode? node, Type type, JsonSerializerOptions options)
    {
        if (node is not JsonObject obj) return;

        if (obj.TryGetPropertyValue("properties", out var propsNode) && propsNode is JsonObject propsObj)
        {
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in properties)
            {
                var jsonPropName = GetJsonPropertyName(prop, options);
                if (propsObj.TryGetPropertyValue(jsonPropName, out var propNode) && propNode is JsonObject propSchemaObj)
                {
                    if (!propSchemaObj.ContainsKey("description"))
                    {
                        var propDoc = XmlDocumentationReader.GetPropertyDescription(type, prop.Name);
                        if (!string.IsNullOrWhiteSpace(propDoc))
                        {
                            propSchemaObj["description"] = propDoc;
                        }
                    }

                    // Recurse into complex nested objects or arrays of complex objects within the protocol assembly
                    var targetType = prop.PropertyType;
                    if (targetType.IsGenericType && (targetType.GetGenericTypeDefinition() == typeof(List<>) || targetType.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
                    {
                        var itemType = targetType.GetGenericArguments()[0];
                        if (itemType.Assembly == type.Assembly && propSchemaObj.TryGetPropertyValue("items", out var itemsNode))
                        {
                            EnrichSchemaProperties(itemsNode, itemType, options);
                        }
                    }
                    else if (targetType.Assembly == type.Assembly && !targetType.IsEnum)
                    {
                        EnrichSchemaProperties(propSchemaObj, targetType, options);
                    }
                }
            }
        }
    }

    private static string GetJsonPropertyName(PropertyInfo prop, JsonSerializerOptions options)
    {
        var attr = prop.GetCustomAttribute<JsonPropertyNameAttribute>();
        if (attr != null && !string.IsNullOrEmpty(attr.Name))
        {
            return attr.Name;
        }

        if (options.PropertyNamingPolicy != null)
        {
            return options.PropertyNamingPolicy.ConvertName(prop.Name);
        }

        if (char.IsUpper(prop.Name[0]))
        {
            return char.ToLowerInvariant(prop.Name[0]) + prop.Name.Substring(1);
        }

        return prop.Name;
    }

    private static void NormalizeSchema(JsonNode? node, string? description = null)
    {
        if (node is JsonObject obj)
        {
            if (description != null && !obj.ContainsKey("description"))
            {
                obj["description"] = description;
            }

            // Clean up ["type", "null"] to cleaner schemas
            if (obj.TryGetPropertyValue("type", out var typeNode) && typeNode is JsonArray arr)
            {
                var nonNullTypes = new List<string>();
                bool hasNull = false;
                foreach (var item in arr)
                {
                    var val = item?.GetValue<string>();
                    if (val == "null") hasNull = true;
                    else if (val != null) nonNullTypes.Add(val);
                }

                if (nonNullTypes.Contains("number"))
                {
                    obj["type"] = "number";
                    obj.Remove("pattern");
                    if (hasNull) obj["nullable"] = true;
                }
                else if (nonNullTypes.Contains("integer"))
                {
                    obj["type"] = "integer";
                    obj.Remove("pattern");
                    if (hasNull) obj["nullable"] = true;
                }
                else if (nonNullTypes.Count == 1)
                {
                    obj["type"] = nonNullTypes[0];
                    if (hasNull) obj["nullable"] = true;
                }
                else if (nonNullTypes.Count > 1)
                {
                    obj["type"] = nonNullTypes[0];
                    if (hasNull) obj["nullable"] = true;
                }
            }
            else if (obj.TryGetPropertyValue("type", out var singleType) && singleType is JsonValue val)
            {
                var strVal = val.GetValue<string>();
                if (strVal == "number" || strVal == "integer")
                {
                    obj.Remove("pattern");
                }
            }

            if (obj.TryGetPropertyValue("properties", out var propsNode) && propsNode is JsonObject propsObj)
            {
                foreach (var prop in propsObj)
                {
                    NormalizeSchema(prop.Value);
                }
            }

            if (obj.TryGetPropertyValue("items", out var itemsNode))
            {
                NormalizeSchema(itemsNode);
            }
        }
    }

    private static Dictionary<string, object> CreateEnvelopeMessage(string name, string title, string summary, string messageType, string payloadSchemaRef)
    {
        return new Dictionary<string, object>
        {
            ["name"] = name,
            ["title"] = title,
            ["summary"] = summary,
            ["contentType"] = "application/json",
            ["payload"] = new Dictionary<string, object>
            {
                ["type"] = "object",
                ["description"] = "Wrapper envelope framing WebSocket text messages.",
                ["required"] = new List<string> { "messageId", "messageType", "payload" },
                ["properties"] = new Dictionary<string, object>
                {
                    ["messageId"] = new Dictionary<string, object>
                    {
                        ["type"] = "string",
                        ["format"] = "uuid",
                        ["description"] = "Unique message identifier (UUIDv7 recommended).",
                        ["example"] = "0191ebc2-8400-7c23-881e-123456789abc"
                    },
                    ["sourceMessageId"] = new Dictionary<string, object>
                    {
                        ["type"] = "string",
                        ["format"] = "uuid",
                        ["description"] = "Identifier of the request message this frame responds to, or 00000000-0000-0000-0000-000000000000 for original frames.",
                        ["example"] = "00000000-0000-0000-0000-000000000000"
                    },
                    ["messageType"] = new Dictionary<string, object>
                    {
                        ["type"] = "string",
                        ["const"] = messageType,
                        ["description"] = $"Message discriminator set to '{messageType}'."
                    },
                    ["payload"] = new Dictionary<string, object>
                    {
                        ["type"] = "string",
                        ["description"] = $"JSON-stringified `{payloadSchemaRef}` payload."
                    }
                }
            }
        };
    }

    private static Dictionary<string, object> CreateOperation(string action, string summary, string channelMessageKey)
    {
        return new Dictionary<string, object>
        {
            ["action"] = action,
            ["channel"] = new Dictionary<string, string> { ["$ref"] = "#/channels/wsChannel" },
            ["summary"] = summary,
            ["messages"] = new List<object>
            {
                new Dictionary<string, string> { ["$ref"] = $"#/channels/wsChannel/messages/{channelMessageKey}" }
            }
        };
    }

    private static void TryRegenerateHtmlDocs(string specPath, string docsDir)
    {
        try
        {
            Console.WriteLine("[AsyncApiGenerator] Regenerating HTML documentation portal via @asyncapi/cli...");
            var psi = new ProcessStartInfo
            {
                FileName = OperatingSystem.IsWindows() ? "cmd.exe" : "asyncapi",
                Arguments = OperatingSystem.IsWindows() 
                    ? $"/c asyncapi generate fromTemplate \"{specPath}\" @asyncapi/html-template -o \"{docsDir}\" --force-write"
                    : $"generate fromTemplate \"{specPath}\" @asyncapi/html-template -o \"{docsDir}\" --force-write",
                RedirectStandardOutput = false,
                RedirectStandardError = false,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi);
            if (process != null)
            {
                if (process.WaitForExit(90000))
                {
                    if (process.ExitCode == 0)
                    {
                        Console.WriteLine("[AsyncApiGenerator] HTML documentation successfully generated at: " + docsDir);
                    }
                    else
                    {
                        Console.WriteLine("[AsyncApiGenerator] Warning: asyncapi command exited with code " + process.ExitCode);
                    }
                }
                else
                {
                    process.Kill();
                    Console.WriteLine("[AsyncApiGenerator] Warning: asyncapi command timed out.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("[AsyncApiGenerator] Note: @asyncapi/cli was not invoked or failed: " + ex.Message);
        }
    }
}
