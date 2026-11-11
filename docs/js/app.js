
    const schema = {
  "asyncapi": "3.1.0",
  "info": {
    "title": "NoxVela Service Protocol",
    "version": "1.1.0",
    "description": "Bilateral WebSocket specification for communication between NoxVela Server and external worker services (Code Analyzers, AI services, etc.).\n\nAll messages across the WebSocket connection use UTF-8 text frames adhering to the `Wrapper` envelope.\nNotice that the `payload` property within the wrapper is a serialized JSON string.",
    "license": {
      "name": "MIT",
      "url": "https://opensource.org/licenses/MIT"
    }
  },
  "defaultContentType": "application/json",
  "channels": {
    "wsChannel": {
      "address": "/ws",
      "description": "Bi-directional WebSocket communication channel for analyzer/service registration, task dispatch, and AST graph streaming.",
      "messages": {
        "registrationMessage": {
          "name": "RegistrationMessage",
          "title": "Service Registration",
          "summary": "Service registers its capabilities and metadata upon connecting.",
          "contentType": "application/json",
          "payload": {
            "type": "object",
            "description": "Wrapper envelope framing WebSocket text messages.",
            "required": [
              "messageId",
              "messageType",
              "payload"
            ],
            "properties": {
              "messageId": {
                "type": "string",
                "format": "uuid",
                "description": "Unique message identifier (UUIDv7 recommended).",
                "example": "0191ebc2-8400-7c23-881e-123456789abc",
                "x-parser-schema-id": "<anonymous-schema-2>"
              },
              "sourceMessageId": {
                "type": "string",
                "format": "uuid",
                "description": "Identifier of the request message this frame responds to, or 00000000-0000-0000-0000-000000000000 for original frames.",
                "example": "00000000-0000-0000-0000-000000000000",
                "x-parser-schema-id": "<anonymous-schema-3>"
              },
              "messageType": {
                "type": "string",
                "const": "registration",
                "description": "Message discriminator set to 'registration'.",
                "x-parser-schema-id": "<anonymous-schema-4>"
              },
              "payload": {
                "type": "string",
                "description": "JSON-stringified `RegistrationPayload` payload.",
                "x-parser-schema-id": "<anonymous-schema-5>"
              }
            },
            "x-parser-schema-id": "<anonymous-schema-1>"
          },
          "x-parser-unique-object-id": "registrationMessage"
        },
        "statusMessage": {
          "name": "StatusMessage",
          "title": "Heartbeat Status",
          "summary": "Periodic heartbeat report from worker to server.",
          "contentType": "application/json",
          "payload": {
            "type": "object",
            "description": "Wrapper envelope framing WebSocket text messages.",
            "required": [
              "messageId",
              "messageType",
              "payload"
            ],
            "properties": {
              "messageId": {
                "type": "string",
                "format": "uuid",
                "description": "Unique message identifier (UUIDv7 recommended).",
                "example": "0191ebc2-8400-7c23-881e-123456789abc",
                "x-parser-schema-id": "<anonymous-schema-7>"
              },
              "sourceMessageId": {
                "type": "string",
                "format": "uuid",
                "description": "Identifier of the request message this frame responds to, or 00000000-0000-0000-0000-000000000000 for original frames.",
                "example": "00000000-0000-0000-0000-000000000000",
                "x-parser-schema-id": "<anonymous-schema-8>"
              },
              "messageType": {
                "type": "string",
                "const": "status",
                "description": "Message discriminator set to 'status'.",
                "x-parser-schema-id": "<anonymous-schema-9>"
              },
              "payload": {
                "type": "string",
                "description": "JSON-stringified `StatusPayload` payload.",
                "x-parser-schema-id": "<anonymous-schema-10>"
              }
            },
            "x-parser-schema-id": "<anonymous-schema-6>"
          },
          "x-parser-unique-object-id": "statusMessage"
        },
        "analysisStatusMessage": {
          "name": "AnalysisStatusMessage",
          "title": "Analysis Status",
          "summary": "Lifecycle progress and state reports during an analysis run.",
          "contentType": "application/json",
          "payload": {
            "type": "object",
            "description": "Wrapper envelope framing WebSocket text messages.",
            "required": [
              "messageId",
              "messageType",
              "payload"
            ],
            "properties": {
              "messageId": {
                "type": "string",
                "format": "uuid",
                "description": "Unique message identifier (UUIDv7 recommended).",
                "example": "0191ebc2-8400-7c23-881e-123456789abc",
                "x-parser-schema-id": "<anonymous-schema-12>"
              },
              "sourceMessageId": {
                "type": "string",
                "format": "uuid",
                "description": "Identifier of the request message this frame responds to, or 00000000-0000-0000-0000-000000000000 for original frames.",
                "example": "00000000-0000-0000-0000-000000000000",
                "x-parser-schema-id": "<anonymous-schema-13>"
              },
              "messageType": {
                "type": "string",
                "const": "analysis_status",
                "description": "Message discriminator set to 'analysis_status'.",
                "x-parser-schema-id": "<anonymous-schema-14>"
              },
              "payload": {
                "type": "string",
                "description": "JSON-stringified `AnalysisStatusPayload` payload.",
                "x-parser-schema-id": "<anonymous-schema-15>"
              }
            },
            "x-parser-schema-id": "<anonymous-schema-11>"
          },
          "x-parser-unique-object-id": "analysisStatusMessage"
        },
        "analysisMetricsMessage": {
          "name": "AnalysisMetricsMessage",
          "title": "Analysis Metrics",
          "summary": "Telemetry metrics emitted at the conclusion of an analysis run.",
          "contentType": "application/json",
          "payload": {
            "type": "object",
            "description": "Wrapper envelope framing WebSocket text messages.",
            "required": [
              "messageId",
              "messageType",
              "payload"
            ],
            "properties": {
              "messageId": {
                "type": "string",
                "format": "uuid",
                "description": "Unique message identifier (UUIDv7 recommended).",
                "example": "0191ebc2-8400-7c23-881e-123456789abc",
                "x-parser-schema-id": "<anonymous-schema-17>"
              },
              "sourceMessageId": {
                "type": "string",
                "format": "uuid",
                "description": "Identifier of the request message this frame responds to, or 00000000-0000-0000-0000-000000000000 for original frames.",
                "example": "00000000-0000-0000-0000-000000000000",
                "x-parser-schema-id": "<anonymous-schema-18>"
              },
              "messageType": {
                "type": "string",
                "const": "analysis_metrics",
                "description": "Message discriminator set to 'analysis_metrics'.",
                "x-parser-schema-id": "<anonymous-schema-19>"
              },
              "payload": {
                "type": "string",
                "description": "JSON-stringified `AnalysisMetricsPayload` payload.",
                "x-parser-schema-id": "<anonymous-schema-20>"
              }
            },
            "x-parser-schema-id": "<anonymous-schema-16>"
          },
          "x-parser-unique-object-id": "analysisMetricsMessage"
        },
        "nodeMessage": {
          "name": "NodeMessage",
          "title": "AST Node",
          "summary": "Single AST node streamed during code analysis.",
          "contentType": "application/json",
          "payload": {
            "type": "object",
            "description": "Wrapper envelope framing WebSocket text messages.",
            "required": [
              "messageId",
              "messageType",
              "payload"
            ],
            "properties": {
              "messageId": {
                "type": "string",
                "format": "uuid",
                "description": "Unique message identifier (UUIDv7 recommended).",
                "example": "0191ebc2-8400-7c23-881e-123456789abc",
                "x-parser-schema-id": "<anonymous-schema-22>"
              },
              "sourceMessageId": {
                "type": "string",
                "format": "uuid",
                "description": "Identifier of the request message this frame responds to, or 00000000-0000-0000-0000-000000000000 for original frames.",
                "example": "00000000-0000-0000-0000-000000000000",
                "x-parser-schema-id": "<anonymous-schema-23>"
              },
              "messageType": {
                "type": "string",
                "const": "node",
                "description": "Message discriminator set to 'node'.",
                "x-parser-schema-id": "<anonymous-schema-24>"
              },
              "payload": {
                "type": "string",
                "description": "JSON-stringified `NodeBase` payload.",
                "x-parser-schema-id": "<anonymous-schema-25>"
              }
            },
            "x-parser-schema-id": "<anonymous-schema-21>"
          },
          "x-parser-unique-object-id": "nodeMessage"
        },
        "relationMessage": {
          "name": "RelationMessage",
          "title": "AST Relation",
          "summary": "Single AST relation edge streamed during code analysis.",
          "contentType": "application/json",
          "payload": {
            "type": "object",
            "description": "Wrapper envelope framing WebSocket text messages.",
            "required": [
              "messageId",
              "messageType",
              "payload"
            ],
            "properties": {
              "messageId": {
                "type": "string",
                "format": "uuid",
                "description": "Unique message identifier (UUIDv7 recommended).",
                "example": "0191ebc2-8400-7c23-881e-123456789abc",
                "x-parser-schema-id": "<anonymous-schema-27>"
              },
              "sourceMessageId": {
                "type": "string",
                "format": "uuid",
                "description": "Identifier of the request message this frame responds to, or 00000000-0000-0000-0000-000000000000 for original frames.",
                "example": "00000000-0000-0000-0000-000000000000",
                "x-parser-schema-id": "<anonymous-schema-28>"
              },
              "messageType": {
                "type": "string",
                "const": "relation",
                "description": "Message discriminator set to 'relation'.",
                "x-parser-schema-id": "<anonymous-schema-29>"
              },
              "payload": {
                "type": "string",
                "description": "JSON-stringified `RelationNode` payload.",
                "x-parser-schema-id": "<anonymous-schema-30>"
              }
            },
            "x-parser-schema-id": "<anonymous-schema-26>"
          },
          "x-parser-unique-object-id": "relationMessage"
        },
        "nodesBatchMessage": {
          "name": "NodesBatchMessage",
          "title": "AST Nodes Batch",
          "summary": "Micro-batch containing multiple AST nodes streamed concurrently.",
          "contentType": "application/json",
          "payload": {
            "type": "object",
            "description": "Wrapper envelope framing WebSocket text messages.",
            "required": [
              "messageId",
              "messageType",
              "payload"
            ],
            "properties": {
              "messageId": {
                "type": "string",
                "format": "uuid",
                "description": "Unique message identifier (UUIDv7 recommended).",
                "example": "0191ebc2-8400-7c23-881e-123456789abc",
                "x-parser-schema-id": "<anonymous-schema-32>"
              },
              "sourceMessageId": {
                "type": "string",
                "format": "uuid",
                "description": "Identifier of the request message this frame responds to, or 00000000-0000-0000-0000-000000000000 for original frames.",
                "example": "00000000-0000-0000-0000-000000000000",
                "x-parser-schema-id": "<anonymous-schema-33>"
              },
              "messageType": {
                "type": "string",
                "const": "nodes_batch",
                "description": "Message discriminator set to 'nodes_batch'.",
                "x-parser-schema-id": "<anonymous-schema-34>"
              },
              "payload": {
                "type": "string",
                "description": "JSON-stringified `NodesBatchPayload` payload.",
                "x-parser-schema-id": "<anonymous-schema-35>"
              }
            },
            "x-parser-schema-id": "<anonymous-schema-31>"
          },
          "x-parser-unique-object-id": "nodesBatchMessage"
        },
        "relationsBatchMessage": {
          "name": "RelationsBatchMessage",
          "title": "AST Relations Batch",
          "summary": "Micro-batch containing multiple AST relations streamed concurrently.",
          "contentType": "application/json",
          "payload": {
            "type": "object",
            "description": "Wrapper envelope framing WebSocket text messages.",
            "required": [
              "messageId",
              "messageType",
              "payload"
            ],
            "properties": {
              "messageId": {
                "type": "string",
                "format": "uuid",
                "description": "Unique message identifier (UUIDv7 recommended).",
                "example": "0191ebc2-8400-7c23-881e-123456789abc",
                "x-parser-schema-id": "<anonymous-schema-37>"
              },
              "sourceMessageId": {
                "type": "string",
                "format": "uuid",
                "description": "Identifier of the request message this frame responds to, or 00000000-0000-0000-0000-000000000000 for original frames.",
                "example": "00000000-0000-0000-0000-000000000000",
                "x-parser-schema-id": "<anonymous-schema-38>"
              },
              "messageType": {
                "type": "string",
                "const": "relations_batch",
                "description": "Message discriminator set to 'relations_batch'.",
                "x-parser-schema-id": "<anonymous-schema-39>"
              },
              "payload": {
                "type": "string",
                "description": "JSON-stringified `RelationsBatchPayload` payload.",
                "x-parser-schema-id": "<anonymous-schema-40>"
              }
            },
            "x-parser-schema-id": "<anonymous-schema-36>"
          },
          "x-parser-unique-object-id": "relationsBatchMessage"
        },
        "runAnalysisAckMessage": {
          "name": "RunAnalysisAckMessage",
          "title": "Run Analysis Acknowledgment",
          "summary": "Worker confirms receipt and acceptance of an analysis task.",
          "contentType": "application/json",
          "payload": {
            "type": "object",
            "description": "Wrapper envelope framing WebSocket text messages.",
            "required": [
              "messageId",
              "messageType",
              "payload"
            ],
            "properties": {
              "messageId": {
                "type": "string",
                "format": "uuid",
                "description": "Unique message identifier (UUIDv7 recommended).",
                "example": "0191ebc2-8400-7c23-881e-123456789abc",
                "x-parser-schema-id": "<anonymous-schema-42>"
              },
              "sourceMessageId": {
                "type": "string",
                "format": "uuid",
                "description": "Identifier of the request message this frame responds to, or 00000000-0000-0000-0000-000000000000 for original frames.",
                "example": "00000000-0000-0000-0000-000000000000",
                "x-parser-schema-id": "<anonymous-schema-43>"
              },
              "messageType": {
                "type": "string",
                "const": "run_analysis_ack",
                "description": "Message discriminator set to 'run_analysis_ack'.",
                "x-parser-schema-id": "<anonymous-schema-44>"
              },
              "payload": {
                "type": "string",
                "description": "JSON-stringified `RunAnalysisAckPayload` payload.",
                "x-parser-schema-id": "<anonymous-schema-45>"
              }
            },
            "x-parser-schema-id": "<anonymous-schema-41>"
          },
          "x-parser-unique-object-id": "runAnalysisAckMessage"
        },
        "errorMessage": {
          "name": "ErrorMessage",
          "title": "Error Message",
          "summary": "Standardized error notification frame.",
          "contentType": "application/json",
          "payload": {
            "type": "object",
            "description": "Wrapper envelope framing WebSocket text messages.",
            "required": [
              "messageId",
              "messageType",
              "payload"
            ],
            "properties": {
              "messageId": {
                "type": "string",
                "format": "uuid",
                "description": "Unique message identifier (UUIDv7 recommended).",
                "example": "0191ebc2-8400-7c23-881e-123456789abc",
                "x-parser-schema-id": "<anonymous-schema-47>"
              },
              "sourceMessageId": {
                "type": "string",
                "format": "uuid",
                "description": "Identifier of the request message this frame responds to, or 00000000-0000-0000-0000-000000000000 for original frames.",
                "example": "00000000-0000-0000-0000-000000000000",
                "x-parser-schema-id": "<anonymous-schema-48>"
              },
              "messageType": {
                "type": "string",
                "const": "error",
                "description": "Message discriminator set to 'error'.",
                "x-parser-schema-id": "<anonymous-schema-49>"
              },
              "payload": {
                "type": "string",
                "description": "JSON-stringified `ErrorMessagePayload` payload.",
                "x-parser-schema-id": "<anonymous-schema-50>"
              }
            },
            "x-parser-schema-id": "<anonymous-schema-46>"
          },
          "x-parser-unique-object-id": "errorMessage"
        },
        "registrationResultMessage": {
          "name": "RegistrationResultMessage",
          "title": "Registration Result",
          "summary": "Server accepts or rejects service registration.",
          "contentType": "application/json",
          "payload": {
            "type": "object",
            "description": "Wrapper envelope framing WebSocket text messages.",
            "required": [
              "messageId",
              "messageType",
              "payload"
            ],
            "properties": {
              "messageId": {
                "type": "string",
                "format": "uuid",
                "description": "Unique message identifier (UUIDv7 recommended).",
                "example": "0191ebc2-8400-7c23-881e-123456789abc",
                "x-parser-schema-id": "<anonymous-schema-52>"
              },
              "sourceMessageId": {
                "type": "string",
                "format": "uuid",
                "description": "Identifier of the request message this frame responds to, or 00000000-0000-0000-0000-000000000000 for original frames.",
                "example": "00000000-0000-0000-0000-000000000000",
                "x-parser-schema-id": "<anonymous-schema-53>"
              },
              "messageType": {
                "type": "string",
                "const": "registration_result",
                "description": "Message discriminator set to 'registration_result'.",
                "x-parser-schema-id": "<anonymous-schema-54>"
              },
              "payload": {
                "type": "string",
                "description": "JSON-stringified `RegistrationStatusPayload` payload.",
                "x-parser-schema-id": "<anonymous-schema-55>"
              }
            },
            "x-parser-schema-id": "<anonymous-schema-51>"
          },
          "x-parser-unique-object-id": "registrationResultMessage"
        },
        "runAnalysisMessage": {
          "name": "RunAnalysisMessage",
          "title": "Run Analysis Command",
          "summary": "Server dispatches a repository analysis task to an idle worker.",
          "contentType": "application/json",
          "payload": {
            "type": "object",
            "description": "Wrapper envelope framing WebSocket text messages.",
            "required": [
              "messageId",
              "messageType",
              "payload"
            ],
            "properties": {
              "messageId": {
                "type": "string",
                "format": "uuid",
                "description": "Unique message identifier (UUIDv7 recommended).",
                "example": "0191ebc2-8400-7c23-881e-123456789abc",
                "x-parser-schema-id": "<anonymous-schema-57>"
              },
              "sourceMessageId": {
                "type": "string",
                "format": "uuid",
                "description": "Identifier of the request message this frame responds to, or 00000000-0000-0000-0000-000000000000 for original frames.",
                "example": "00000000-0000-0000-0000-000000000000",
                "x-parser-schema-id": "<anonymous-schema-58>"
              },
              "messageType": {
                "type": "string",
                "const": "run_analysis",
                "description": "Message discriminator set to 'run_analysis'.",
                "x-parser-schema-id": "<anonymous-schema-59>"
              },
              "payload": {
                "type": "string",
                "description": "JSON-stringified `RunAnalysisPayload` payload.",
                "x-parser-schema-id": "<anonymous-schema-60>"
              }
            },
            "x-parser-schema-id": "<anonymous-schema-56>"
          },
          "x-parser-unique-object-id": "runAnalysisMessage"
        },
        "cancelAnalysisMessage": {
          "name": "CancelAnalysisMessage",
          "title": "Cancel Analysis Command",
          "summary": "Server commands a worker to abort an active analysis task.",
          "contentType": "application/json",
          "payload": {
            "type": "object",
            "description": "Wrapper envelope framing WebSocket text messages.",
            "required": [
              "messageId",
              "messageType",
              "payload"
            ],
            "properties": {
              "messageId": {
                "type": "string",
                "format": "uuid",
                "description": "Unique message identifier (UUIDv7 recommended).",
                "example": "0191ebc2-8400-7c23-881e-123456789abc",
                "x-parser-schema-id": "<anonymous-schema-62>"
              },
              "sourceMessageId": {
                "type": "string",
                "format": "uuid",
                "description": "Identifier of the request message this frame responds to, or 00000000-0000-0000-0000-000000000000 for original frames.",
                "example": "00000000-0000-0000-0000-000000000000",
                "x-parser-schema-id": "<anonymous-schema-63>"
              },
              "messageType": {
                "type": "string",
                "const": "cancel_analysis",
                "description": "Message discriminator set to 'cancel_analysis'.",
                "x-parser-schema-id": "<anonymous-schema-64>"
              },
              "payload": {
                "type": "string",
                "description": "JSON-stringified `CancelAnalysisPayload` payload.",
                "x-parser-schema-id": "<anonymous-schema-65>"
              }
            },
            "x-parser-schema-id": "<anonymous-schema-61>"
          },
          "x-parser-unique-object-id": "cancelAnalysisMessage"
        },
        "statusAckMessage": {
          "name": "StatusAckMessage",
          "title": "Status Acknowledgment",
          "summary": "Server acknowledges heartbeat status to detect half-open sockets.",
          "contentType": "application/json",
          "payload": {
            "type": "object",
            "description": "Wrapper envelope framing WebSocket text messages.",
            "required": [
              "messageId",
              "messageType",
              "payload"
            ],
            "properties": {
              "messageId": {
                "type": "string",
                "format": "uuid",
                "description": "Unique message identifier (UUIDv7 recommended).",
                "example": "0191ebc2-8400-7c23-881e-123456789abc",
                "x-parser-schema-id": "<anonymous-schema-67>"
              },
              "sourceMessageId": {
                "type": "string",
                "format": "uuid",
                "description": "Identifier of the request message this frame responds to, or 00000000-0000-0000-0000-000000000000 for original frames.",
                "example": "00000000-0000-0000-0000-000000000000",
                "x-parser-schema-id": "<anonymous-schema-68>"
              },
              "messageType": {
                "type": "string",
                "const": "status_ack",
                "description": "Message discriminator set to 'status_ack'.",
                "x-parser-schema-id": "<anonymous-schema-69>"
              },
              "payload": {
                "type": "string",
                "description": "JSON-stringified `StatusAckPayload` payload.",
                "x-parser-schema-id": "<anonymous-schema-70>"
              }
            },
            "x-parser-schema-id": "<anonymous-schema-66>"
          },
          "x-parser-unique-object-id": "statusAckMessage"
        },
        "batchAckMessage": {
          "name": "BatchAckMessage",
          "title": "Batch Acknowledgment",
          "summary": "Server confirms persistence of a micro-batch.",
          "contentType": "application/json",
          "payload": {
            "type": "object",
            "description": "Wrapper envelope framing WebSocket text messages.",
            "required": [
              "messageId",
              "messageType",
              "payload"
            ],
            "properties": {
              "messageId": {
                "type": "string",
                "format": "uuid",
                "description": "Unique message identifier (UUIDv7 recommended).",
                "example": "0191ebc2-8400-7c23-881e-123456789abc",
                "x-parser-schema-id": "<anonymous-schema-72>"
              },
              "sourceMessageId": {
                "type": "string",
                "format": "uuid",
                "description": "Identifier of the request message this frame responds to, or 00000000-0000-0000-0000-000000000000 for original frames.",
                "example": "00000000-0000-0000-0000-000000000000",
                "x-parser-schema-id": "<anonymous-schema-73>"
              },
              "messageType": {
                "type": "string",
                "const": "batch_ack",
                "description": "Message discriminator set to 'batch_ack'.",
                "x-parser-schema-id": "<anonymous-schema-74>"
              },
              "payload": {
                "type": "string",
                "description": "JSON-stringified `BatchAckPayload` payload.",
                "x-parser-schema-id": "<anonymous-schema-75>"
              }
            },
            "x-parser-schema-id": "<anonymous-schema-71>"
          },
          "x-parser-unique-object-id": "batchAckMessage"
        },
        "analysisCompletedAckMessage": {
          "name": "AnalysisCompletedAckMessage",
          "title": "Analysis Completed Acknowledgment",
          "summary": "Server confirms all nodes and relations are reconciled in database.",
          "contentType": "application/json",
          "payload": {
            "type": "object",
            "description": "Wrapper envelope framing WebSocket text messages.",
            "required": [
              "messageId",
              "messageType",
              "payload"
            ],
            "properties": {
              "messageId": {
                "type": "string",
                "format": "uuid",
                "description": "Unique message identifier (UUIDv7 recommended).",
                "example": "0191ebc2-8400-7c23-881e-123456789abc",
                "x-parser-schema-id": "<anonymous-schema-77>"
              },
              "sourceMessageId": {
                "type": "string",
                "format": "uuid",
                "description": "Identifier of the request message this frame responds to, or 00000000-0000-0000-0000-000000000000 for original frames.",
                "example": "00000000-0000-0000-0000-000000000000",
                "x-parser-schema-id": "<anonymous-schema-78>"
              },
              "messageType": {
                "type": "string",
                "const": "analysis_completed_ack",
                "description": "Message discriminator set to 'analysis_completed_ack'.",
                "x-parser-schema-id": "<anonymous-schema-79>"
              },
              "payload": {
                "type": "string",
                "description": "JSON-stringified `AnalysisCompletedAckPayload` payload.",
                "x-parser-schema-id": "<anonymous-schema-80>"
              }
            },
            "x-parser-schema-id": "<anonymous-schema-76>"
          },
          "x-parser-unique-object-id": "analysisCompletedAckMessage"
        }
      },
      "x-parser-unique-object-id": "wsChannel"
    }
  },
  "operations": {
    "registerService": {
      "action": "send",
      "channel": "$ref:$.channels.wsChannel",
      "summary": "Service registers itself with the server upon connection.",
      "messages": [
        "$ref:$.channels.wsChannel.messages.registrationMessage"
      ],
      "x-parser-unique-object-id": "registerService"
    },
    "reportStatus": {
      "action": "send",
      "channel": "$ref:$.channels.wsChannel",
      "summary": "Worker sends periodic heartbeat status.",
      "messages": [
        "$ref:$.channels.wsChannel.messages.statusMessage"
      ],
      "x-parser-unique-object-id": "reportStatus"
    },
    "reportAnalysisStatus": {
      "action": "send",
      "channel": "$ref:$.channels.wsChannel",
      "summary": "Worker reports analysis lifecycle progress.",
      "messages": [
        "$ref:$.channels.wsChannel.messages.analysisStatusMessage"
      ],
      "x-parser-unique-object-id": "reportAnalysisStatus"
    },
    "reportAnalysisMetrics": {
      "action": "send",
      "channel": "$ref:$.channels.wsChannel",
      "summary": "Worker emits summary metrics after analysis.",
      "messages": [
        "$ref:$.channels.wsChannel.messages.analysisMetricsMessage"
      ],
      "x-parser-unique-object-id": "reportAnalysisMetrics"
    },
    "streamNode": {
      "action": "send",
      "channel": "$ref:$.channels.wsChannel",
      "summary": "Worker streams a single AST node.",
      "messages": [
        "$ref:$.channels.wsChannel.messages.nodeMessage"
      ],
      "x-parser-unique-object-id": "streamNode"
    },
    "streamRelation": {
      "action": "send",
      "channel": "$ref:$.channels.wsChannel",
      "summary": "Worker streams a single AST relation edge.",
      "messages": [
        "$ref:$.channels.wsChannel.messages.relationMessage"
      ],
      "x-parser-unique-object-id": "streamRelation"
    },
    "streamNodesBatch": {
      "action": "send",
      "channel": "$ref:$.channels.wsChannel",
      "summary": "Worker flushes a micro-batch of AST nodes.",
      "messages": [
        "$ref:$.channels.wsChannel.messages.nodesBatchMessage"
      ],
      "x-parser-unique-object-id": "streamNodesBatch"
    },
    "streamRelationsBatch": {
      "action": "send",
      "channel": "$ref:$.channels.wsChannel",
      "summary": "Worker flushes a micro-batch of AST relations.",
      "messages": [
        "$ref:$.channels.wsChannel.messages.relationsBatchMessage"
      ],
      "x-parser-unique-object-id": "streamRelationsBatch"
    },
    "acknowledgeRunAnalysis": {
      "action": "send",
      "channel": "$ref:$.channels.wsChannel",
      "summary": "Worker confirms acceptance of an analysis task.",
      "messages": [
        "$ref:$.channels.wsChannel.messages.runAnalysisAckMessage"
      ],
      "x-parser-unique-object-id": "acknowledgeRunAnalysis"
    },
    "reportError": {
      "action": "send",
      "channel": "$ref:$.channels.wsChannel",
      "summary": "Worker or server emits an error message.",
      "messages": [
        "$ref:$.channels.wsChannel.messages.errorMessage"
      ],
      "x-parser-unique-object-id": "reportError"
    },
    "acknowledgeRegistration": {
      "action": "receive",
      "channel": "$ref:$.channels.wsChannel",
      "summary": "Server acknowledges or rejects service registration.",
      "messages": [
        "$ref:$.channels.wsChannel.messages.registrationResultMessage"
      ],
      "x-parser-unique-object-id": "acknowledgeRegistration"
    },
    "dispatchAnalysis": {
      "action": "receive",
      "channel": "$ref:$.channels.wsChannel",
      "summary": "Server commands worker to run analysis.",
      "messages": [
        "$ref:$.channels.wsChannel.messages.runAnalysisMessage"
      ],
      "x-parser-unique-object-id": "dispatchAnalysis"
    },
    "cancelAnalysis": {
      "action": "receive",
      "channel": "$ref:$.channels.wsChannel",
      "summary": "Server commands worker to abort analysis.",
      "messages": [
        "$ref:$.channels.wsChannel.messages.cancelAnalysisMessage"
      ],
      "x-parser-unique-object-id": "cancelAnalysis"
    },
    "acknowledgeStatus": {
      "action": "receive",
      "channel": "$ref:$.channels.wsChannel",
      "summary": "Server acknowledges heartbeat status.",
      "messages": [
        "$ref:$.channels.wsChannel.messages.statusAckMessage"
      ],
      "x-parser-unique-object-id": "acknowledgeStatus"
    },
    "acknowledgeBatch": {
      "action": "receive",
      "channel": "$ref:$.channels.wsChannel",
      "summary": "Server acknowledges batch persistence.",
      "messages": [
        "$ref:$.channels.wsChannel.messages.batchAckMessage"
      ],
      "x-parser-unique-object-id": "acknowledgeBatch"
    },
    "acknowledgeAnalysisCompleted": {
      "action": "receive",
      "channel": "$ref:$.channels.wsChannel",
      "summary": "Server confirms graph reconciliation in database.",
      "messages": [
        "$ref:$.channels.wsChannel.messages.analysisCompletedAckMessage"
      ],
      "x-parser-unique-object-id": "acknowledgeAnalysisCompleted"
    }
  },
  "components": {
    "messages": {
      "RegistrationMessage": "$ref:$.channels.wsChannel.messages.registrationMessage",
      "StatusMessage": "$ref:$.channels.wsChannel.messages.statusMessage",
      "AnalysisStatusMessage": "$ref:$.channels.wsChannel.messages.analysisStatusMessage",
      "AnalysisMetricsMessage": "$ref:$.channels.wsChannel.messages.analysisMetricsMessage",
      "NodeMessage": "$ref:$.channels.wsChannel.messages.nodeMessage",
      "RelationMessage": "$ref:$.channels.wsChannel.messages.relationMessage",
      "NodesBatchMessage": "$ref:$.channels.wsChannel.messages.nodesBatchMessage",
      "RelationsBatchMessage": "$ref:$.channels.wsChannel.messages.relationsBatchMessage",
      "RunAnalysisAckMessage": "$ref:$.channels.wsChannel.messages.runAnalysisAckMessage",
      "ErrorMessage": "$ref:$.channels.wsChannel.messages.errorMessage",
      "RegistrationResultMessage": "$ref:$.channels.wsChannel.messages.registrationResultMessage",
      "RunAnalysisMessage": "$ref:$.channels.wsChannel.messages.runAnalysisMessage",
      "CancelAnalysisMessage": "$ref:$.channels.wsChannel.messages.cancelAnalysisMessage",
      "StatusAckMessage": "$ref:$.channels.wsChannel.messages.statusAckMessage",
      "BatchAckMessage": "$ref:$.channels.wsChannel.messages.batchAckMessage",
      "AnalysisCompletedAckMessage": "$ref:$.channels.wsChannel.messages.analysisCompletedAckMessage"
    },
    "schemas": {
      "RegistrationPayload": {
        "type": "object",
        "properties": {
          "serviceName": {
            "type": "string",
            "description": "Name of a service.",
            "x-parser-schema-id": "<anonymous-schema-81>"
          },
          "version": {
            "type": "string",
            "description": "Legimitive version of service.",
            "x-parser-schema-id": "<anonymous-schema-82>"
          },
          "capabilities": {
            "type": "array",
            "items": {
              "type": "string",
              "nullable": true,
              "x-parser-schema-id": "<anonymous-schema-84>"
            },
            "description": "Capabilities of a service",
            "x-parser-schema-id": "<anonymous-schema-83>"
          },
          "supportedLanguages": {
            "type": "array",
            "items": {
              "type": "string",
              "nullable": true,
              "x-parser-schema-id": "<anonymous-schema-86>"
            },
            "nullable": true,
            "description": "Supported languages for service. Applicable for analyzers.",
            "x-parser-schema-id": "<anonymous-schema-85>"
          },
          "serviceType": {
            "type": "integer",
            "description": "Type of a service.",
            "x-parser-schema-id": "<anonymous-schema-87>"
          },
          "poolId": {
            "type": "string",
            "nullable": true,
            "description": "Optional logical pool identifier grouping multiple worker instances.",
            "x-parser-schema-id": "<anonymous-schema-88>"
          },
          "serviceKey": {
            "type": "string",
            "nullable": true,
            "description": "Optional service pool authentication key passed in registration payload.",
            "x-parser-schema-id": "<anonymous-schema-89>"
          },
          "compilerEngine": {
            "type": "string",
            "nullable": true,
            "description": "Optional compiler engine declaration (e.g. \"roslyn\", \"tree-sitter\").",
            "x-parser-schema-id": "<anonymous-schema-90>"
          },
          "compilerVersion": {
            "type": "string",
            "nullable": true,
            "description": "Optional compiler engine version (e.g. \"4.8.0\", \"0.22.0\").",
            "x-parser-schema-id": "<anonymous-schema-91>"
          },
          "computeWeight": {
            "type": "string",
            "nullable": true,
            "description": "Optional compute weight capacity declaration (\"lightweight\" vs \"heavy_compute\").",
            "x-parser-schema-id": "<anonymous-schema-92>"
          },
          "protocolVersion": {
            "type": "string",
            "nullable": true,
            "description": "Negotiated protocol version supported by this service (e.g. \"1.0.0\", \"1.1.0\").",
            "x-parser-schema-id": "<anonymous-schema-93>"
          },
          "instanceId": {
            "type": "string",
            "description": "Instance id.",
            "x-parser-schema-id": "<anonymous-schema-94>"
          }
        },
        "required": [
          "serviceName",
          "version",
          "capabilities",
          "supportedLanguages",
          "serviceType",
          "instanceId"
        ],
        "description": "Payload for registering a service with the server.",
        "x-parser-schema-id": "RegistrationPayload"
      },
      "StatusPayload": {
        "type": "object",
        "properties": {
          "status": {
            "type": "integer",
            "description": "Actual service status. For more information about avaliable statuses see .",
            "x-parser-schema-id": "<anonymous-schema-95>"
          },
          "instanceId": {
            "type": "string",
            "description": "Instance id.",
            "x-parser-schema-id": "<anonymous-schema-96>"
          }
        },
        "required": [
          "status",
          "instanceId"
        ],
        "description": "Periodic status heartbeat payload.",
        "x-parser-schema-id": "StatusPayload"
      },
      "AnalysisStatusPayload": {
        "type": "object",
        "properties": {
          "message": {
            "type": "string",
            "description": "Textual message of the analysis status.",
            "x-parser-schema-id": "<anonymous-schema-97>"
          },
          "progress_percentage": {
            "type": "number",
            "nullable": true,
            "description": "Overall percentage of analysis completion (0.0 to 100.0).",
            "x-parser-schema-id": "<anonymous-schema-98>"
          },
          "processed_items": {
            "type": "integer",
            "nullable": true,
            "description": "Number of items processed so far (e.g. syntax trees, files, or nodes).",
            "x-parser-schema-id": "<anonymous-schema-99>"
          },
          "total_items": {
            "type": "integer",
            "nullable": true,
            "description": "Total number of items to process in the current phase or run.",
            "x-parser-schema-id": "<anonymous-schema-100>"
          },
          "current_file": {
            "type": "string",
            "nullable": true,
            "description": "Path or name of the file currently being processed.",
            "x-parser-schema-id": "<anonymous-schema-101>"
          },
          "run_id": {
            "type": "string",
            "nullable": true,
            "description": "Unique run identifier associated with this status update.",
            "x-parser-schema-id": "<anonymous-schema-102>"
          },
          "commit_hash": {
            "type": "string",
            "nullable": true,
            "description": "Commit hash associated with this analysis run.",
            "x-parser-schema-id": "<anonymous-schema-103>"
          },
          "branch": {
            "type": "string",
            "nullable": true,
            "description": "Git branch associated with this analysis run.",
            "x-parser-schema-id": "<anonymous-schema-104>"
          },
          "commit_message": {
            "type": "string",
            "nullable": true,
            "description": "Git commit message associated with this analysis run.",
            "x-parser-schema-id": "<anonymous-schema-105>"
          },
          "status": {
            "type": "integer",
            "description": "Actual service status. For more information about avaliable statuses see .",
            "x-parser-schema-id": "<anonymous-schema-106>"
          },
          "instanceId": {
            "type": "string",
            "description": "Instance id.",
            "x-parser-schema-id": "<anonymous-schema-107>"
          }
        },
        "required": [
          "message",
          "status",
          "instanceId"
        ],
        "description": "Analysis progress and lifecycle status payload.",
        "x-parser-schema-id": "AnalysisStatusPayload"
      },
      "AnalysisMetricsPayload": {
        "type": "object",
        "properties": {
          "run_id": {
            "type": "string",
            "description": "Gets or sets the unique identifier for the analysis run.",
            "x-parser-schema-id": "<anonymous-schema-108>"
          },
          "started_at": {
            "type": "string",
            "format": "date-time",
            "description": "Gets or sets the start time of the analysis run.",
            "x-parser-schema-id": "<anonymous-schema-109>"
          },
          "ended_at": {
            "type": "string",
            "format": "date-time",
            "description": "Gets or sets the end time of the analysis run.",
            "x-parser-schema-id": "<anonymous-schema-110>"
          },
          "duration_ms": {
            "type": "number",
            "description": "Gets or sets the duration of the analysis run in milliseconds.",
            "x-parser-schema-id": "<anonymous-schema-111>"
          },
          "generated_nodes": {
            "type": "integer",
            "description": "Gets or sets the number of nodes generated during the analysis.",
            "x-parser-schema-id": "<anonymous-schema-112>"
          },
          "generated_relations": {
            "type": "integer",
            "description": "Gets or sets the number of relations generated during the analysis.",
            "x-parser-schema-id": "<anonymous-schema-113>"
          },
          "commit_hash": {
            "type": "string",
            "nullable": true,
            "description": "Gets or sets the commit hash associated with the analysis run.",
            "x-parser-schema-id": "<anonymous-schema-114>"
          },
          "branch": {
            "type": "string",
            "nullable": true,
            "description": "Gets or sets the git branch associated with the analysis run.",
            "x-parser-schema-id": "<anonymous-schema-115>"
          },
          "commit_message": {
            "type": "string",
            "nullable": true,
            "description": "Gets or sets the git commit message associated with the analysis run.",
            "x-parser-schema-id": "<anonymous-schema-116>"
          },
          "instanceId": {
            "type": "string",
            "description": "Instance id.",
            "x-parser-schema-id": "<anonymous-schema-117>"
          }
        },
        "required": [
          "run_id",
          "started_at",
          "ended_at",
          "duration_ms",
          "generated_nodes",
          "generated_relations",
          "instanceId"
        ],
        "description": "Summary metrics emitted at the conclusion of an analysis run.",
        "x-parser-schema-id": "AnalysisMetricsPayload"
      },
      "RunAnalysisPayload": {
        "type": "object",
        "properties": {
          "run_id": {
            "type": "string",
            "description": "Unique execution run identifier.",
            "x-parser-schema-id": "<anonymous-schema-118>"
          },
          "code_context": {
            "type": "object",
            "properties": {
              "commit_hash": {
                "type": "string",
                "description": "Commit hash.",
                "x-parser-schema-id": "<anonymous-schema-120>"
              },
              "branch_name": {
                "type": "string",
                "description": "Branch name.",
                "x-parser-schema-id": "<anonymous-schema-121>"
              },
              "repository_url": {
                "type": "string",
                "description": "Repository URL.",
                "x-parser-schema-id": "<anonymous-schema-122>"
              },
              "provider": {
                "type": "string",
                "description": "Provider of AccesToken.",
                "x-parser-schema-id": "<anonymous-schema-123>"
              },
              "isPublic": {
                "type": "boolean",
                "description": "Gets or sets a value indicating whether the repository is publicly accessible.",
                "x-parser-schema-id": "<anonymous-schema-124>"
              },
              "credentials": {
                "type": "object",
                "properties": {
                  "access_token": {
                    "type": "string",
                    "description": "AccessToken.",
                    "x-parser-schema-id": "<anonymous-schema-126>"
                  }
                },
                "required": [
                  "access_token"
                ],
                "nullable": true,
                "description": "Optional credentials for Git access.",
                "x-parser-schema-id": "<anonymous-schema-125>"
              },
              "base_commit_hash": {
                "type": "string",
                "nullable": true,
                "description": "Optional base commit hash for diff / incremental CI/CD scans.",
                "x-parser-schema-id": "<anonymous-schema-127>"
              },
              "modified_files": {
                "type": "array",
                "items": {
                  "type": "string",
                  "nullable": true,
                  "x-parser-schema-id": "<anonymous-schema-129>"
                },
                "nullable": true,
                "description": "Optional collection of modified file paths between base commit and target commit.",
                "x-parser-schema-id": "<anonymous-schema-128>"
              }
            },
            "required": [
              "commit_hash",
              "branch_name",
              "repository_url",
              "provider"
            ],
            "description": "Target repository and branch/commit context.",
            "x-parser-schema-id": "<anonymous-schema-119>"
          },
          "requested_capabilities": {
            "type": "array",
            "items": {
              "type": "string",
              "nullable": true,
              "x-parser-schema-id": "<anonymous-schema-131>"
            },
            "nullable": true,
            "description": "Optional collection of specific capabilities requested for this execution run. When null or empty, the analyzer executes its default capabilities.",
            "x-parser-schema-id": "<anonymous-schema-130>"
          },
          "batching_config": {
            "type": "object",
            "properties": {
              "max_batch_size": {
                "type": "integer",
                "description": "Maximum number of items aggregated in a single micro-batch before flushing. Defaults to 100.",
                "x-parser-schema-id": "<anonymous-schema-133>"
              },
              "flush_interval_ms": {
                "type": "integer",
                "description": "Maximum elapsed milliseconds before flushing buffered items. Defaults to 100ms.",
                "x-parser-schema-id": "<anonymous-schema-134>"
              }
            },
            "nullable": true,
            "description": "Optional micro-batching configuration controlling maximum batch size and flush interval.",
            "x-parser-schema-id": "<anonymous-schema-132>"
          },
          "compute_weight": {
            "type": "string",
            "nullable": true,
            "description": "Optional compute weight requirement for this analysis run (\"lightweight\" vs \"heavy_compute\").",
            "x-parser-schema-id": "<anonymous-schema-135>"
          },
          "compiler_engine": {
            "type": "string",
            "nullable": true,
            "description": "Optional required compiler engine (e.g. \"roslyn\", \"tree-sitter\").",
            "x-parser-schema-id": "<anonymous-schema-136>"
          },
          "compiler_version": {
            "type": "string",
            "nullable": true,
            "description": "Optional required compiler engine version.",
            "x-parser-schema-id": "<anonymous-schema-137>"
          }
        },
        "required": [
          "run_id",
          "code_context"
        ],
        "description": "Task dispatch payload commanding an analyzer to analyze a code repository.",
        "x-parser-schema-id": "RunAnalysisPayload"
      },
      "RunAnalysisAckPayload": {
        "type": "object",
        "properties": {
          "run_id": {
            "type": "string",
            "description": "Gets or sets the analysis run identifier being acknowledged.",
            "x-parser-schema-id": "<anonymous-schema-138>"
          },
          "timestamp": {
            "type": "string",
            "format": "date-time",
            "description": "Gets or sets the UTC timestamp when the instruction was received.",
            "x-parser-schema-id": "<anonymous-schema-139>"
          },
          "instanceId": {
            "type": "string",
            "description": "Instance id.",
            "x-parser-schema-id": "<anonymous-schema-140>"
          }
        },
        "required": [
          "instanceId"
        ],
        "description": "Acknowledgment payload sent by worker upon accepting a run_analysis task.",
        "x-parser-schema-id": "RunAnalysisAckPayload"
      },
      "CancelAnalysisPayload": {
        "type": "object",
        "properties": {
          "run_id": {
            "type": "string",
            "description": "Unique identifier of the analysis run to cancel.",
            "x-parser-schema-id": "<anonymous-schema-141>"
          },
          "reason": {
            "type": "string",
            "nullable": true,
            "description": "Optional reason describing why the analysis was canceled.",
            "x-parser-schema-id": "<anonymous-schema-142>"
          }
        },
        "required": [
          "run_id"
        ],
        "description": "Payload for commanding an analyzer to cancel an active analysis task.",
        "x-parser-schema-id": "CancelAnalysisPayload"
      },
      "StatusAckPayload": {
        "type": "object",
        "properties": {
          "instance_id": {
            "type": "string",
            "description": "Gets or sets the instance identifier of the worker being acknowledged.",
            "x-parser-schema-id": "<anonymous-schema-143>"
          },
          "timestamp": {
            "type": "string",
            "format": "date-time",
            "description": "Gets or sets the UTC timestamp when the heartbeat was acknowledged.",
            "x-parser-schema-id": "<anonymous-schema-144>"
          }
        },
        "description": "Server heartbeat acknowledgment payload.",
        "x-parser-schema-id": "StatusAckPayload"
      },
      "BatchAckPayload": {
        "type": "object",
        "properties": {
          "batch_count": {
            "type": "integer",
            "description": "Gets or sets the count of items in the batch that was persisted.",
            "x-parser-schema-id": "<anonymous-schema-145>"
          },
          "run_id": {
            "type": "string",
            "format": "uuid",
            "nullable": true,
            "description": "Gets or sets the run identifier associated with the batch.",
            "x-parser-schema-id": "<anonymous-schema-146>"
          },
          "timestamp": {
            "type": "string",
            "format": "date-time",
            "description": "Gets or sets the UTC timestamp when the batch was acknowledged.",
            "x-parser-schema-id": "<anonymous-schema-147>"
          }
        },
        "description": "Server acknowledgment for a processed micro-batch of AST nodes or relations.",
        "x-parser-schema-id": "BatchAckPayload"
      },
      "AnalysisCompletedAckPayload": {
        "type": "object",
        "properties": {
          "run_id": {
            "type": "string",
            "description": "Gets or sets the analysis run identifier.",
            "x-parser-schema-id": "<anonymous-schema-148>"
          },
          "timestamp": {
            "type": "string",
            "format": "date-time",
            "description": "Gets or sets the UTC timestamp when the analysis completion was acknowledged.",
            "x-parser-schema-id": "<anonymous-schema-149>"
          }
        },
        "description": "Server acknowledgment confirming that all AST nodes and relations have been finalized in the database.",
        "x-parser-schema-id": "AnalysisCompletedAckPayload"
      },
      "NodesBatchPayload": {
        "type": "object",
        "properties": {
          "nodes": {
            "type": "array",
            "description": "Collection of AST nodes emitted in this micro-batch.",
            "x-parser-schema-id": "<anonymous-schema-150>"
          }
        },
        "required": [
          "nodes"
        ],
        "description": "Micro-batch array payload containing multiple AST nodes.",
        "x-parser-schema-id": "NodesBatchPayload"
      },
      "RelationsBatchPayload": {
        "type": "object",
        "properties": {
          "relations": {
            "type": "array",
            "items": {
              "type": "object",
              "properties": {
                "id": {
                  "type": "string",
                  "x-parser-schema-id": "<anonymous-schema-153>"
                },
                "from": {
                  "type": "string",
                  "x-parser-schema-id": "<anonymous-schema-154>"
                },
                "to": {
                  "type": "string",
                  "x-parser-schema-id": "<anonymous-schema-155>"
                },
                "relation_type": {
                  "type": "string",
                  "x-parser-schema-id": "<anonymous-schema-156>"
                },
                "is_pending": {
                  "type": "boolean",
                  "x-parser-schema-id": "<anonymous-schema-157>"
                },
                "run_id": {
                  "type": "string",
                  "nullable": true,
                  "x-parser-schema-id": "<anonymous-schema-158>"
                }
              },
              "nullable": true,
              "x-parser-schema-id": "<anonymous-schema-152>"
            },
            "description": "Collection of graph relations emitted in this micro-batch.",
            "x-parser-schema-id": "<anonymous-schema-151>"
          }
        },
        "required": [
          "relations"
        ],
        "description": "Micro-batch array payload containing multiple AST relation edges.",
        "x-parser-schema-id": "RelationsBatchPayload"
      },
      "ErrorMessagePayload": {
        "type": "object",
        "properties": {
          "code": {
            "type": "string",
            "description": "Machine-readable standardized error code. See .",
            "x-parser-schema-id": "<anonymous-schema-159>"
          },
          "message": {
            "type": "string",
            "description": "Human-readable descriptive error message.",
            "x-parser-schema-id": "<anonymous-schema-160>"
          },
          "run_id": {
            "type": "string",
            "nullable": true,
            "description": "Optional analysis run identifier correlated with the failure.",
            "x-parser-schema-id": "<anonymous-schema-161>"
          },
          "is_fatal": {
            "type": "boolean",
            "description": "Indicates whether the error was fatal and aborted the operation or worker process.",
            "x-parser-schema-id": "<anonymous-schema-162>"
          },
          "details": {
            "type": "string",
            "nullable": true,
            "description": "Optional additional technical details or stack trace for diagnostics.",
            "x-parser-schema-id": "<anonymous-schema-163>"
          },
          "timestamp": {
            "type": "string",
            "format": "date-time",
            "description": "UTC timestamp when the error was recorded.",
            "x-parser-schema-id": "<anonymous-schema-164>"
          }
        },
        "required": [
          "code",
          "message"
        ],
        "description": "Standardized error message payload for reporting failures.",
        "x-parser-schema-id": "ErrorMessagePayload"
      },
      "RegistrationStatusPayload": {
        "type": "object",
        "properties": {
          "status": {
            "type": "integer",
            "description": "Status of registration process can have value on of .",
            "x-parser-schema-id": "<anonymous-schema-165>"
          }
        },
        "required": [
          "status"
        ],
        "description": "Server registration response payload indicating whether registration was accepted.",
        "x-parser-schema-id": "RegistrationStatusPayload"
      },
      "NodeBase": {
        "type": "object",
        "description": "Base schema for all code AST entities.",
        "required": [
          "stable_id",
          "content_hash",
          "type"
        ],
        "properties": {
          "stable_id": {
            "type": "string",
            "description": "Deterministic ID based on structural language properties, invariant across builds.",
            "example": "MyApp.Services.UserService",
            "x-parser-schema-id": "<anonymous-schema-166>"
          },
          "content_hash": {
            "type": "string",
            "description": "Hash representing node content/definition state.",
            "example": "8f4b237c9a1e05d2",
            "x-parser-schema-id": "<anonymous-schema-167>"
          },
          "type": {
            "type": "string",
            "description": "AST entity type: classes, methods, fields, properties, interfaces, enums, records, structs, namespaces, parameters, delegates, events, attributes, etc.",
            "x-parser-schema-id": "<anonymous-schema-168>"
          },
          "parent_id": {
            "type": "string",
            "nullable": true,
            "description": "Identifier of parent container node.",
            "x-parser-schema-id": "<anonymous-schema-169>"
          },
          "language": {
            "type": "string",
            "nullable": true,
            "description": "Programming language (e.g. csharp, python).",
            "x-parser-schema-id": "<anonymous-schema-170>"
          }
        },
        "x-parser-schema-id": "NodeBase"
      },
      "ClassNode": {
        "type": "object",
        "properties": {
          "is_static": {
            "type": "boolean",
            "description": "Gets a value indicating whether the class is marked as static.",
            "x-parser-schema-id": "<anonymous-schema-171>"
          },
          "is_abstract": {
            "type": "boolean",
            "description": "Gets a value indicating whether the class is abstract.",
            "x-parser-schema-id": "<anonymous-schema-172>"
          },
          "is_sealed": {
            "type": "boolean",
            "description": "Gets a value indicating whether the class is sealed (cannot be inherited).",
            "x-parser-schema-id": "<anonymous-schema-173>"
          },
          "is_generic": {
            "type": "boolean",
            "description": "Gets a value indicating whether the class has generic parameters.",
            "x-parser-schema-id": "<anonymous-schema-174>"
          },
          "arity": {
            "type": "integer",
            "description": "Gets the number of generic type parameters (Arity) defined for this class.",
            "x-parser-schema-id": "<anonymous-schema-175>"
          },
          "cyclomatic_complexity": {
            "type": "integer",
            "description": "Gets the cyclomatic complexity of the class.",
            "x-parser-schema-id": "<anonymous-schema-176>"
          },
          "loc": {
            "type": "integer",
            "description": "Gets the lines of code (LOC) of the class.",
            "x-parser-schema-id": "<anonymous-schema-177>"
          },
          "locations": {
            "type": "array",
            "items": {
              "type": "object",
              "properties": {
                "file_path": {
                  "type": "string",
                  "description": "Gets the absolute or relative path to the source file.",
                  "x-parser-schema-id": "<anonymous-schema-180>"
                },
                "start_line": {
                  "type": "integer",
                  "description": "Gets the 1-based start line number.",
                  "x-parser-schema-id": "<anonymous-schema-181>"
                },
                "end_line": {
                  "type": "integer",
                  "description": "Gets the 1-based end line number.",
                  "x-parser-schema-id": "<anonymous-schema-182>"
                },
                "start_column": {
                  "type": "integer",
                  "description": "Gets the 1-based start column position.",
                  "x-parser-schema-id": "<anonymous-schema-183>"
                },
                "end_column": {
                  "type": "integer",
                  "description": "Gets the 1-based end column position.",
                  "x-parser-schema-id": "<anonymous-schema-184>"
                },
                "code_snippet": {
                  "type": "string",
                  "description": "Gets the source code snippet representing this location.",
                  "x-parser-schema-id": "<anonymous-schema-185>"
                }
              },
              "required": [
                "file_path",
                "start_line",
                "end_line",
                "start_column",
                "end_column",
                "code_snippet"
              ],
              "nullable": true,
              "x-parser-schema-id": "<anonymous-schema-179>"
            },
            "description": "Gets the collection of file paths where this class is declared. Relevant for partial classes spread across multiple files.",
            "x-parser-schema-id": "<anonymous-schema-178>"
          },
          "accessibility": {
            "type": "integer",
            "description": "Gets or sets the visibility of the class. Defaults to .",
            "x-parser-schema-id": "<anonymous-schema-186>"
          },
          "type": {
            "type": "string",
            "nullable": true,
            "description": "Tetermines the type of node.",
            "x-parser-schema-id": "<anonymous-schema-187>"
          },
          "identifier": {
            "type": "string",
            "description": "Gets or sets the human-readable identifier. Used for indexing and searchability within the graph database.",
            "x-parser-schema-id": "<anonymous-schema-188>"
          },
          "summary": {
            "type": "string",
            "nullable": true,
            "description": "Gets the XML documentation summary associated with the type.",
            "x-parser-schema-id": "<anonymous-schema-189>"
          },
          "stable_id": {
            "type": "string",
            "description": "Gets a deterministic identifier based on structural language properties. This ID remains stable across builds, allowing for persistent node mapping.",
            "x-parser-schema-id": "<anonymous-schema-190>"
          },
          "content_hash": {
            "type": "string",
            "description": "Gets a hash representing the current state of the node's content. For example, changes to a class body between commits will result in a different hash, signaling that the node's definition has evolved.",
            "x-parser-schema-id": "<anonymous-schema-191>"
          }
        },
        "required": [
          "is_static",
          "is_abstract",
          "is_sealed",
          "is_generic",
          "arity",
          "locations",
          "accessibility",
          "identifier",
          "summary",
          "stable_id",
          "content_hash"
        ],
        "description": "Represents a class definition in the AST graph.",
        "x-parser-schema-id": "ClassNode"
      },
      "InterfaceNode": {
        "type": "object",
        "properties": {
          "is_generic": {
            "type": "boolean",
            "description": "Gets a value indicating whether the interface has generic parameters (e.g., IReadOnlyList ).",
            "x-parser-schema-id": "<anonymous-schema-192>"
          },
          "arity": {
            "type": "integer",
            "description": "Gets the number of generic type parameters (Arity) defined for this interface.",
            "x-parser-schema-id": "<anonymous-schema-193>"
          },
          "cyclomatic_complexity": {
            "type": "integer",
            "description": "Gets the cyclomatic complexity of the interface.",
            "x-parser-schema-id": "<anonymous-schema-194>"
          },
          "loc": {
            "type": "integer",
            "description": "Gets the lines of code (LOC) of the interface.",
            "x-parser-schema-id": "<anonymous-schema-195>"
          },
          "accessibility": {
            "type": "integer",
            "description": "Gets or sets the visibility of the interface.",
            "x-parser-schema-id": "<anonymous-schema-196>"
          },
          "locations": {
            "type": "array",
            "items": {
              "type": "object",
              "properties": {
                "file_path": {
                  "type": "string",
                  "description": "Gets the absolute or relative path to the source file.",
                  "x-parser-schema-id": "<anonymous-schema-199>"
                },
                "start_line": {
                  "type": "integer",
                  "description": "Gets the 1-based start line number.",
                  "x-parser-schema-id": "<anonymous-schema-200>"
                },
                "end_line": {
                  "type": "integer",
                  "description": "Gets the 1-based end line number.",
                  "x-parser-schema-id": "<anonymous-schema-201>"
                },
                "start_column": {
                  "type": "integer",
                  "description": "Gets the 1-based start column position.",
                  "x-parser-schema-id": "<anonymous-schema-202>"
                },
                "end_column": {
                  "type": "integer",
                  "description": "Gets the 1-based end column position.",
                  "x-parser-schema-id": "<anonymous-schema-203>"
                },
                "code_snippet": {
                  "type": "string",
                  "description": "Gets the source code snippet representing this location.",
                  "x-parser-schema-id": "<anonymous-schema-204>"
                }
              },
              "required": [
                "file_path",
                "start_line",
                "end_line",
                "start_column",
                "end_column",
                "code_snippet"
              ],
              "nullable": true,
              "x-parser-schema-id": "<anonymous-schema-198>"
            },
            "description": "Gets the collection of file paths where this interface is declared. Supports partial interface definitions spread across multiple files.",
            "x-parser-schema-id": "<anonymous-schema-197>"
          },
          "type": {
            "type": "string",
            "nullable": true,
            "description": "Tetermines the type of node.",
            "x-parser-schema-id": "<anonymous-schema-205>"
          },
          "identifier": {
            "type": "string",
            "description": "Gets or sets the human-readable identifier. Used for indexing and searchability within the graph database.",
            "x-parser-schema-id": "<anonymous-schema-206>"
          },
          "summary": {
            "type": "string",
            "nullable": true,
            "description": "Gets the XML documentation summary associated with the type.",
            "x-parser-schema-id": "<anonymous-schema-207>"
          },
          "stable_id": {
            "type": "string",
            "description": "Gets a deterministic identifier based on structural language properties. This ID remains stable across builds, allowing for persistent node mapping.",
            "x-parser-schema-id": "<anonymous-schema-208>"
          },
          "content_hash": {
            "type": "string",
            "description": "Gets a hash representing the current state of the node's content. For example, changes to a class body between commits will result in a different hash, signaling that the node's definition has evolved.",
            "x-parser-schema-id": "<anonymous-schema-209>"
          }
        },
        "required": [
          "is_generic",
          "accessibility",
          "locations",
          "identifier",
          "summary",
          "stable_id",
          "content_hash"
        ],
        "description": "Represents an interface definition in the AST graph.",
        "x-parser-schema-id": "InterfaceNode"
      },
      "RecordNode": {
        "type": "object",
        "properties": {
          "accessibility": {
            "type": "integer",
            "description": "Gets or sets the visibility of the record.",
            "x-parser-schema-id": "<anonymous-schema-210>"
          },
          "is_record_struct": {
            "type": "boolean",
            "description": "Gets a value indicating whether this is a 'record struct'. If false, it represents a 'record class'.",
            "x-parser-schema-id": "<anonymous-schema-211>"
          },
          "is_read_only": {
            "type": "boolean",
            "description": "Gets a value indicating whether the record struct is marked as 'readonly'. (Only applicable when is true).",
            "x-parser-schema-id": "<anonymous-schema-212>"
          },
          "is_static": {
            "type": "boolean",
            "description": "Gets a value indicating whether the record is marked as static.",
            "x-parser-schema-id": "<anonymous-schema-213>"
          },
          "is_abstract": {
            "type": "boolean",
            "description": "Gets a value indicating whether the record is abstract.",
            "x-parser-schema-id": "<anonymous-schema-214>"
          },
          "is_sealed": {
            "type": "boolean",
            "description": "Gets a value indicating whether the record is sealed.",
            "x-parser-schema-id": "<anonymous-schema-215>"
          },
          "is_generic": {
            "type": "boolean",
            "description": "Gets a value indicating whether the record has generic parameters.",
            "x-parser-schema-id": "<anonymous-schema-216>"
          },
          "arity": {
            "type": "integer",
            "description": "Gets the number of generic type parameters (Arity) defined for this record.",
            "x-parser-schema-id": "<anonymous-schema-217>"
          },
          "cyclomatic_complexity": {
            "type": "integer",
            "description": "Gets the cyclomatic complexity of the record.",
            "x-parser-schema-id": "<anonymous-schema-218>"
          },
          "loc": {
            "type": "integer",
            "description": "Gets the lines of code (LOC) of the record.",
            "x-parser-schema-id": "<anonymous-schema-219>"
          },
          "locations": {
            "type": "array",
            "items": {
              "type": "object",
              "properties": {
                "file_path": {
                  "type": "string",
                  "description": "Gets the absolute or relative path to the source file.",
                  "x-parser-schema-id": "<anonymous-schema-222>"
                },
                "start_line": {
                  "type": "integer",
                  "description": "Gets the 1-based start line number.",
                  "x-parser-schema-id": "<anonymous-schema-223>"
                },
                "end_line": {
                  "type": "integer",
                  "description": "Gets the 1-based end line number.",
                  "x-parser-schema-id": "<anonymous-schema-224>"
                },
                "start_column": {
                  "type": "integer",
                  "description": "Gets the 1-based start column position.",
                  "x-parser-schema-id": "<anonymous-schema-225>"
                },
                "end_column": {
                  "type": "integer",
                  "description": "Gets the 1-based end column position.",
                  "x-parser-schema-id": "<anonymous-schema-226>"
                },
                "code_snippet": {
                  "type": "string",
                  "description": "Gets the source code snippet representing this location.",
                  "x-parser-schema-id": "<anonymous-schema-227>"
                }
              },
              "required": [
                "file_path",
                "start_line",
                "end_line",
                "start_column",
                "end_column",
                "code_snippet"
              ],
              "nullable": true,
              "x-parser-schema-id": "<anonymous-schema-221>"
            },
            "description": "Gets the collection of file paths where this record is declared. Records are frequently 'partial' when used with source generators or primary constructors.",
            "x-parser-schema-id": "<anonymous-schema-220>"
          },
          "type": {
            "type": "string",
            "nullable": true,
            "description": "Tetermines the type of node.",
            "x-parser-schema-id": "<anonymous-schema-228>"
          },
          "identifier": {
            "type": "string",
            "description": "Gets or sets the human-readable identifier. Used for indexing and searchability within the graph database.",
            "x-parser-schema-id": "<anonymous-schema-229>"
          },
          "summary": {
            "type": "string",
            "nullable": true,
            "description": "Gets the XML documentation summary associated with the type.",
            "x-parser-schema-id": "<anonymous-schema-230>"
          },
          "stable_id": {
            "type": "string",
            "description": "Gets a deterministic identifier based on structural language properties. This ID remains stable across builds, allowing for persistent node mapping.",
            "x-parser-schema-id": "<anonymous-schema-231>"
          },
          "content_hash": {
            "type": "string",
            "description": "Gets a hash representing the current state of the node's content. For example, changes to a class body between commits will result in a different hash, signaling that the node's definition has evolved.",
            "x-parser-schema-id": "<anonymous-schema-232>"
          }
        },
        "required": [
          "accessibility",
          "is_record_struct",
          "is_read_only",
          "is_static",
          "is_abstract",
          "is_sealed",
          "locations",
          "identifier",
          "summary",
          "stable_id",
          "content_hash"
        ],
        "description": "Represents a record (class or struct) definition in the AST graph.",
        "x-parser-schema-id": "RecordNode"
      },
      "StructNode": {
        "type": "object",
        "properties": {
          "is_read_only": {
            "type": "boolean",
            "description": "Gets a value indicating whether the struct is marked as 'readonly'. Readonly structs ensure that all fields are immutable and allow for performance optimizations.",
            "x-parser-schema-id": "<anonymous-schema-233>"
          },
          "is_ref_like": {
            "type": "boolean",
            "description": "Gets a value indicating whether this is a 'ref struct' (e.g., ). Ref-like structs are allocated on the stack and have strict usage constraints.",
            "x-parser-schema-id": "<anonymous-schema-234>"
          },
          "is_generic": {
            "type": "boolean",
            "description": "Gets a value indicating whether the struct has generic parameters.",
            "x-parser-schema-id": "<anonymous-schema-235>"
          },
          "arity": {
            "type": "integer",
            "description": "Gets the number of generic type parameters (Arity) defined for this struct.",
            "x-parser-schema-id": "<anonymous-schema-236>"
          },
          "cyclomatic_complexity": {
            "type": "integer",
            "description": "Gets the cyclomatic complexity of the struct.",
            "x-parser-schema-id": "<anonymous-schema-237>"
          },
          "loc": {
            "type": "integer",
            "description": "Gets the lines of code (LOC) of the struct.",
            "x-parser-schema-id": "<anonymous-schema-238>"
          },
          "accessibility": {
            "type": "integer",
            "description": "Gets or sets the visibility of the struct.",
            "x-parser-schema-id": "<anonymous-schema-239>"
          },
          "locations": {
            "type": "array",
            "items": {
              "type": "object",
              "properties": {
                "file_path": {
                  "type": "string",
                  "description": "Gets the absolute or relative path to the source file.",
                  "x-parser-schema-id": "<anonymous-schema-242>"
                },
                "start_line": {
                  "type": "integer",
                  "description": "Gets the 1-based start line number.",
                  "x-parser-schema-id": "<anonymous-schema-243>"
                },
                "end_line": {
                  "type": "integer",
                  "description": "Gets the 1-based end line number.",
                  "x-parser-schema-id": "<anonymous-schema-244>"
                },
                "start_column": {
                  "type": "integer",
                  "description": "Gets the 1-based start column position.",
                  "x-parser-schema-id": "<anonymous-schema-245>"
                },
                "end_column": {
                  "type": "integer",
                  "description": "Gets the 1-based end column position.",
                  "x-parser-schema-id": "<anonymous-schema-246>"
                },
                "code_snippet": {
                  "type": "string",
                  "description": "Gets the source code snippet representing this location.",
                  "x-parser-schema-id": "<anonymous-schema-247>"
                }
              },
              "required": [
                "file_path",
                "start_line",
                "end_line",
                "start_column",
                "end_column",
                "code_snippet"
              ],
              "nullable": true,
              "x-parser-schema-id": "<anonymous-schema-241>"
            },
            "description": "Gets the list of file paths where the struct is declared. Supports partial struct definitions across multiple files.",
            "x-parser-schema-id": "<anonymous-schema-240>"
          },
          "type": {
            "type": "string",
            "nullable": true,
            "description": "Tetermines the type of node.",
            "x-parser-schema-id": "<anonymous-schema-248>"
          },
          "identifier": {
            "type": "string",
            "description": "Gets or sets the human-readable identifier. Used for indexing and searchability within the graph database.",
            "x-parser-schema-id": "<anonymous-schema-249>"
          },
          "summary": {
            "type": "string",
            "nullable": true,
            "description": "Gets the XML documentation summary associated with the type.",
            "x-parser-schema-id": "<anonymous-schema-250>"
          },
          "stable_id": {
            "type": "string",
            "description": "Gets a deterministic identifier based on structural language properties. This ID remains stable across builds, allowing for persistent node mapping.",
            "x-parser-schema-id": "<anonymous-schema-251>"
          },
          "content_hash": {
            "type": "string",
            "description": "Gets a hash representing the current state of the node's content. For example, changes to a class body between commits will result in a different hash, signaling that the node's definition has evolved.",
            "x-parser-schema-id": "<anonymous-schema-252>"
          }
        },
        "required": [
          "is_read_only",
          "is_ref_like",
          "is_generic",
          "accessibility",
          "locations",
          "identifier",
          "summary",
          "stable_id",
          "content_hash"
        ],
        "description": "Represents a struct definition in the AST graph.",
        "x-parser-schema-id": "StructNode"
      },
      "EnumNode": {
        "type": "object",
        "properties": {
          "accessibility": {
            "type": "integer",
            "description": "Gets or sets the visibility of the enum.",
            "x-parser-schema-id": "<anonymous-schema-253>"
          },
          "location": {
            "type": "object",
            "properties": {
              "file_path": {
                "type": "string",
                "description": "Gets the absolute or relative path to the source file.",
                "x-parser-schema-id": "<anonymous-schema-255>"
              },
              "start_line": {
                "type": "integer",
                "description": "Gets the 1-based start line number.",
                "x-parser-schema-id": "<anonymous-schema-256>"
              },
              "end_line": {
                "type": "integer",
                "description": "Gets the 1-based end line number.",
                "x-parser-schema-id": "<anonymous-schema-257>"
              },
              "start_column": {
                "type": "integer",
                "description": "Gets the 1-based start column position.",
                "x-parser-schema-id": "<anonymous-schema-258>"
              },
              "end_column": {
                "type": "integer",
                "description": "Gets the 1-based end column position.",
                "x-parser-schema-id": "<anonymous-schema-259>"
              },
              "code_snippet": {
                "type": "string",
                "description": "Gets the source code snippet representing this location.",
                "x-parser-schema-id": "<anonymous-schema-260>"
              }
            },
            "required": [
              "file_path",
              "start_line",
              "end_line",
              "start_column",
              "end_column",
              "code_snippet"
            ],
            "nullable": true,
            "description": "Gets the source file path where the enum is defined.",
            "x-parser-schema-id": "<anonymous-schema-254>"
          },
          "type": {
            "type": "string",
            "nullable": true,
            "description": "Tetermines the type of node.",
            "x-parser-schema-id": "<anonymous-schema-261>"
          },
          "identifier": {
            "type": "string",
            "description": "Gets or sets the human-readable identifier. Used for indexing and searchability within the graph database.",
            "x-parser-schema-id": "<anonymous-schema-262>"
          },
          "summary": {
            "type": "string",
            "nullable": true,
            "description": "Gets the XML documentation summary associated with the type.",
            "x-parser-schema-id": "<anonymous-schema-263>"
          },
          "stable_id": {
            "type": "string",
            "description": "Gets a deterministic identifier based on structural language properties. This ID remains stable across builds, allowing for persistent node mapping.",
            "x-parser-schema-id": "<anonymous-schema-264>"
          },
          "content_hash": {
            "type": "string",
            "description": "Gets a hash representing the current state of the node's content. For example, changes to a class body between commits will result in a different hash, signaling that the node's definition has evolved.",
            "x-parser-schema-id": "<anonymous-schema-265>"
          }
        },
        "required": [
          "accessibility",
          "location",
          "identifier",
          "summary",
          "stable_id",
          "content_hash"
        ],
        "description": "Represents an enum definition in the AST graph.",
        "x-parser-schema-id": "EnumNode"
      },
      "MethodNode": {
        "type": "object",
        "properties": {
          "accessibility": {
            "type": "integer",
            "description": "Gets or sets the visibility of the method.",
            "x-parser-schema-id": "<anonymous-schema-266>"
          },
          "method_kind": {
            "type": "string",
            "description": "Gets the specific kind of the method (e.g., \"Ordinary\", \"Constructor\", \"UserDefinedOperator\").",
            "x-parser-schema-id": "<anonymous-schema-267>"
          },
          "is_static": {
            "type": "boolean",
            "description": "Gets a value indicating whether the method is shared across all instances.",
            "x-parser-schema-id": "<anonymous-schema-268>"
          },
          "is_abstract": {
            "type": "boolean",
            "description": "Gets a value indicating whether the method is declared without an implementation.",
            "x-parser-schema-id": "<anonymous-schema-269>"
          },
          "is_virtual": {
            "type": "boolean",
            "description": "Gets a value indicating whether the method can be overridden in derived classes.",
            "x-parser-schema-id": "<anonymous-schema-270>"
          },
          "is_override": {
            "type": "boolean",
            "description": "Gets a value indicating whether this method overrides a base implementation.",
            "x-parser-schema-id": "<anonymous-schema-271>"
          },
          "is_sealed": {
            "type": "boolean",
            "description": "Gets a value indicating whether this method cannot be further overridden.",
            "x-parser-schema-id": "<anonymous-schema-272>"
          },
          "is_async": {
            "type": "boolean",
            "description": "Gets a value indicating whether the method is marked with the 'async' modifier.",
            "x-parser-schema-id": "<anonymous-schema-273>"
          },
          "is_extension_method": {
            "type": "boolean",
            "description": "Gets a value indicating whether this is an extension method.",
            "x-parser-schema-id": "<anonymous-schema-274>"
          },
          "is_generic": {
            "type": "boolean",
            "description": "Gets a value indicating whether the method has type parameters.",
            "x-parser-schema-id": "<anonymous-schema-275>"
          },
          "arity": {
            "type": "integer",
            "description": "Gets the number of generic type parameters (Arity) of the method.",
            "x-parser-schema-id": "<anonymous-schema-276>"
          },
          "locations": {
            "type": "array",
            "items": {
              "type": "object",
              "properties": {
                "file_path": {
                  "type": "string",
                  "description": "Gets the absolute or relative path to the source file.",
                  "x-parser-schema-id": "<anonymous-schema-279>"
                },
                "start_line": {
                  "type": "integer",
                  "description": "Gets the 1-based start line number.",
                  "x-parser-schema-id": "<anonymous-schema-280>"
                },
                "end_line": {
                  "type": "integer",
                  "description": "Gets the 1-based end line number.",
                  "x-parser-schema-id": "<anonymous-schema-281>"
                },
                "start_column": {
                  "type": "integer",
                  "description": "Gets the 1-based start column position.",
                  "x-parser-schema-id": "<anonymous-schema-282>"
                },
                "end_column": {
                  "type": "integer",
                  "description": "Gets the 1-based end column position.",
                  "x-parser-schema-id": "<anonymous-schema-283>"
                },
                "code_snippet": {
                  "type": "string",
                  "description": "Gets the source code snippet representing this location.",
                  "x-parser-schema-id": "<anonymous-schema-284>"
                }
              },
              "required": [
                "file_path",
                "start_line",
                "end_line",
                "start_column",
                "end_column",
                "code_snippet"
              ],
              "nullable": true,
              "x-parser-schema-id": "<anonymous-schema-278>"
            },
            "description": "Gets the list of files where the method is defined. Usually contains one file, unless the method is a partial method with both definition and implementation.",
            "x-parser-schema-id": "<anonymous-schema-277>"
          },
          "complexity": {
            "type": "integer",
            "description": "Contains the results of the cyclometric analysis",
            "x-parser-schema-id": "<anonymous-schema-285>"
          },
          "type": {
            "type": "string",
            "nullable": true,
            "description": "Tetermines the type of node.",
            "x-parser-schema-id": "<anonymous-schema-286>"
          },
          "identifier": {
            "type": "string",
            "description": "Gets or sets the human-readable identifier. Used for indexing and searchability within the graph database.",
            "x-parser-schema-id": "<anonymous-schema-287>"
          },
          "summary": {
            "type": "string",
            "nullable": true,
            "description": "Gets the XML documentation summary associated with the type.",
            "x-parser-schema-id": "<anonymous-schema-288>"
          },
          "stable_id": {
            "type": "string",
            "description": "Gets a deterministic identifier based on structural language properties. This ID remains stable across builds, allowing for persistent node mapping.",
            "x-parser-schema-id": "<anonymous-schema-289>"
          },
          "content_hash": {
            "type": "string",
            "description": "Gets a hash representing the current state of the node's content. For example, changes to a class body between commits will result in a different hash, signaling that the node's definition has evolved.",
            "x-parser-schema-id": "<anonymous-schema-290>"
          }
        },
        "required": [
          "accessibility",
          "method_kind",
          "is_static",
          "is_abstract",
          "is_virtual",
          "is_override",
          "is_sealed",
          "is_async",
          "is_extension_method",
          "is_generic",
          "arity",
          "locations",
          "complexity",
          "identifier",
          "summary",
          "stable_id",
          "content_hash"
        ],
        "description": "Represents a method or constructor in the AST graph.",
        "x-parser-schema-id": "MethodNode"
      },
      "FieldNode": {
        "type": "object",
        "properties": {
          "accessibility": {
            "type": "integer",
            "description": "Gets or sets the visibility of the field.",
            "x-parser-schema-id": "<anonymous-schema-291>"
          },
          "is_const": {
            "type": "boolean",
            "description": "Gets a value indicating whether the field is a constant (evaluated at compile-time).",
            "x-parser-schema-id": "<anonymous-schema-292>"
          },
          "is_read_only": {
            "type": "boolean",
            "description": "Gets a value indicating whether the field is read-only (can only be set in a constructor).",
            "x-parser-schema-id": "<anonymous-schema-293>"
          },
          "is_static": {
            "type": "boolean",
            "description": "Gets a value indicating whether the field is shared across all instances.",
            "x-parser-schema-id": "<anonymous-schema-294>"
          },
          "is_volatile": {
            "type": "boolean",
            "description": "Gets a value indicating whether the field is volatile, signaling that it may be modified by multiple threads.",
            "x-parser-schema-id": "<anonymous-schema-295>"
          },
          "constant_value": {
            "type": "string",
            "nullable": true,
            "description": "Gets the string representation of the constant value, if is true.",
            "x-parser-schema-id": "<anonymous-schema-296>"
          },
          "location": {
            "type": "object",
            "properties": {
              "file_path": {
                "type": "string",
                "description": "Gets the absolute or relative path to the source file.",
                "x-parser-schema-id": "<anonymous-schema-298>"
              },
              "start_line": {
                "type": "integer",
                "description": "Gets the 1-based start line number.",
                "x-parser-schema-id": "<anonymous-schema-299>"
              },
              "end_line": {
                "type": "integer",
                "description": "Gets the 1-based end line number.",
                "x-parser-schema-id": "<anonymous-schema-300>"
              },
              "start_column": {
                "type": "integer",
                "description": "Gets the 1-based start column position.",
                "x-parser-schema-id": "<anonymous-schema-301>"
              },
              "end_column": {
                "type": "integer",
                "description": "Gets the 1-based end column position.",
                "x-parser-schema-id": "<anonymous-schema-302>"
              },
              "code_snippet": {
                "type": "string",
                "description": "Gets the source code snippet representing this location.",
                "x-parser-schema-id": "<anonymous-schema-303>"
              }
            },
            "required": [
              "file_path",
              "start_line",
              "end_line",
              "start_column",
              "end_column",
              "code_snippet"
            ],
            "nullable": true,
            "description": "Gets the source file path where the field is declared.",
            "x-parser-schema-id": "<anonymous-schema-297>"
          },
          "type": {
            "type": "string",
            "nullable": true,
            "description": "Tetermines the type of node.",
            "x-parser-schema-id": "<anonymous-schema-304>"
          },
          "identifier": {
            "type": "string",
            "description": "Gets or sets the human-readable identifier. Used for indexing and searchability within the graph database.",
            "x-parser-schema-id": "<anonymous-schema-305>"
          },
          "summary": {
            "type": "string",
            "nullable": true,
            "description": "Gets the XML documentation summary associated with the type.",
            "x-parser-schema-id": "<anonymous-schema-306>"
          },
          "stable_id": {
            "type": "string",
            "description": "Gets a deterministic identifier based on structural language properties. This ID remains stable across builds, allowing for persistent node mapping.",
            "x-parser-schema-id": "<anonymous-schema-307>"
          },
          "content_hash": {
            "type": "string",
            "description": "Gets a hash representing the current state of the node's content. For example, changes to a class body between commits will result in a different hash, signaling that the node's definition has evolved.",
            "x-parser-schema-id": "<anonymous-schema-308>"
          }
        },
        "required": [
          "accessibility",
          "is_const",
          "is_read_only",
          "is_static",
          "is_volatile",
          "constant_value",
          "location",
          "identifier",
          "summary",
          "stable_id",
          "content_hash"
        ],
        "description": "Represents a field in the AST graph.",
        "x-parser-schema-id": "FieldNode"
      },
      "PropertyNode": {
        "type": "object",
        "properties": {
          "accessibility": {
            "type": "integer",
            "description": "Gets or sets the visibility of the property.",
            "x-parser-schema-id": "<anonymous-schema-309>"
          },
          "is_indexer": {
            "type": "boolean",
            "description": "Gets a value indicating whether this property is an indexer (e.g., this[int index]).",
            "x-parser-schema-id": "<anonymous-schema-310>"
          },
          "is_static": {
            "type": "boolean",
            "description": "Gets a value indicating whether the property is shared across all instances.",
            "x-parser-schema-id": "<anonymous-schema-311>"
          },
          "is_abstract": {
            "type": "boolean",
            "description": "Gets a value indicating whether the property is declared without implementation.",
            "x-parser-schema-id": "<anonymous-schema-312>"
          },
          "is_virtual": {
            "type": "boolean",
            "description": "Gets a value indicating whether the property can be overridden in derived classes.",
            "x-parser-schema-id": "<anonymous-schema-313>"
          },
          "is_override": {
            "type": "boolean",
            "description": "Gets a value indicating whether this property overrides a base implementation.",
            "x-parser-schema-id": "<anonymous-schema-314>"
          },
          "is_sealed": {
            "type": "boolean",
            "description": "Gets a value indicating whether this property cannot be further overridden.",
            "x-parser-schema-id": "<anonymous-schema-315>"
          },
          "has_get": {
            "type": "boolean",
            "description": "Gets a value indicating whether the property has a 'get' accessor.",
            "x-parser-schema-id": "<anonymous-schema-316>"
          },
          "has_set": {
            "type": "boolean",
            "description": "Gets a value indicating whether the property has a 'set' or 'init' accessor.",
            "x-parser-schema-id": "<anonymous-schema-317>"
          },
          "is_init_only": {
            "type": "boolean",
            "description": "Gets a value indicating whether the 'set' accessor is 'init-only'. Relevant for C# 9.0+ immutable object initialization.",
            "x-parser-schema-id": "<anonymous-schema-318>"
          },
          "is_required": {
            "type": "boolean",
            "description": "Gets a value indicating whether the property is marked with the 'required' keyword (C# 11+).",
            "x-parser-schema-id": "<anonymous-schema-319>"
          },
          "locations": {
            "type": "array",
            "items": {
              "type": "object",
              "properties": {
                "file_path": {
                  "type": "string",
                  "description": "Gets the absolute or relative path to the source file.",
                  "x-parser-schema-id": "<anonymous-schema-322>"
                },
                "start_line": {
                  "type": "integer",
                  "description": "Gets the 1-based start line number.",
                  "x-parser-schema-id": "<anonymous-schema-323>"
                },
                "end_line": {
                  "type": "integer",
                  "description": "Gets the 1-based end line number.",
                  "x-parser-schema-id": "<anonymous-schema-324>"
                },
                "start_column": {
                  "type": "integer",
                  "description": "Gets the 1-based start column position.",
                  "x-parser-schema-id": "<anonymous-schema-325>"
                },
                "end_column": {
                  "type": "integer",
                  "description": "Gets the 1-based end column position.",
                  "x-parser-schema-id": "<anonymous-schema-326>"
                },
                "code_snippet": {
                  "type": "string",
                  "description": "Gets the source code snippet representing this location.",
                  "x-parser-schema-id": "<anonymous-schema-327>"
                }
              },
              "required": [
                "file_path",
                "start_line",
                "end_line",
                "start_column",
                "end_column",
                "code_snippet"
              ],
              "nullable": true,
              "x-parser-schema-id": "<anonymous-schema-321>"
            },
            "description": "Gets the collection of file paths where this property is declared. Supports partial types where properties might be defined in different locations.",
            "x-parser-schema-id": "<anonymous-schema-320>"
          },
          "type": {
            "type": "string",
            "nullable": true,
            "description": "Tetermines the type of node.",
            "x-parser-schema-id": "<anonymous-schema-328>"
          },
          "identifier": {
            "type": "string",
            "description": "Gets or sets the human-readable identifier. Used for indexing and searchability within the graph database.",
            "x-parser-schema-id": "<anonymous-schema-329>"
          },
          "summary": {
            "type": "string",
            "nullable": true,
            "description": "Gets the XML documentation summary associated with the type.",
            "x-parser-schema-id": "<anonymous-schema-330>"
          },
          "stable_id": {
            "type": "string",
            "description": "Gets a deterministic identifier based on structural language properties. This ID remains stable across builds, allowing for persistent node mapping.",
            "x-parser-schema-id": "<anonymous-schema-331>"
          },
          "content_hash": {
            "type": "string",
            "description": "Gets a hash representing the current state of the node's content. For example, changes to a class body between commits will result in a different hash, signaling that the node's definition has evolved.",
            "x-parser-schema-id": "<anonymous-schema-332>"
          }
        },
        "required": [
          "accessibility",
          "is_indexer",
          "is_static",
          "is_abstract",
          "is_virtual",
          "is_override",
          "is_sealed",
          "has_get",
          "has_set",
          "is_init_only",
          "is_required",
          "locations",
          "identifier",
          "summary",
          "stable_id",
          "content_hash"
        ],
        "description": "Represents a property in the AST graph.",
        "x-parser-schema-id": "PropertyNode"
      },
      "EventNode": {
        "type": "object",
        "properties": {
          "accessibility": {
            "type": "integer",
            "description": "Gets or sets the visibility of the event.",
            "x-parser-schema-id": "<anonymous-schema-333>"
          },
          "is_static": {
            "type": "boolean",
            "description": "Gets a value indicating whether the event is shared across all instances of the type.",
            "x-parser-schema-id": "<anonymous-schema-334>"
          },
          "is_abstract": {
            "type": "boolean",
            "description": "Gets a value indicating whether the event is declared without an implementation.",
            "x-parser-schema-id": "<anonymous-schema-335>"
          },
          "is_virtual": {
            "type": "boolean",
            "description": "Gets a value indicating whether the event can be overridden in derived classes.",
            "x-parser-schema-id": "<anonymous-schema-336>"
          },
          "is_override": {
            "type": "boolean",
            "description": "Gets a value indicating whether this event overrides a base class event.",
            "x-parser-schema-id": "<anonymous-schema-337>"
          },
          "is_sealed": {
            "type": "boolean",
            "description": "Gets a value indicating whether this event cannot be further overridden.",
            "x-parser-schema-id": "<anonymous-schema-338>"
          },
          "has_custom_accessors": {
            "type": "boolean",
            "description": "Gets a value indicating whether the event explicitly defines 'add' and 'remove' accessors. True if the event is not a simple field-like event.",
            "x-parser-schema-id": "<anonymous-schema-339>"
          },
          "location": {
            "type": "object",
            "properties": {
              "file_path": {
                "type": "string",
                "description": "Gets the absolute or relative path to the source file.",
                "x-parser-schema-id": "<anonymous-schema-341>"
              },
              "start_line": {
                "type": "integer",
                "description": "Gets the 1-based start line number.",
                "x-parser-schema-id": "<anonymous-schema-342>"
              },
              "end_line": {
                "type": "integer",
                "description": "Gets the 1-based end line number.",
                "x-parser-schema-id": "<anonymous-schema-343>"
              },
              "start_column": {
                "type": "integer",
                "description": "Gets the 1-based start column position.",
                "x-parser-schema-id": "<anonymous-schema-344>"
              },
              "end_column": {
                "type": "integer",
                "description": "Gets the 1-based end column position.",
                "x-parser-schema-id": "<anonymous-schema-345>"
              },
              "code_snippet": {
                "type": "string",
                "description": "Gets the source code snippet representing this location.",
                "x-parser-schema-id": "<anonymous-schema-346>"
              }
            },
            "required": [
              "file_path",
              "start_line",
              "end_line",
              "start_column",
              "end_column",
              "code_snippet"
            ],
            "nullable": true,
            "description": "Gets the source file path where the event is declared.",
            "x-parser-schema-id": "<anonymous-schema-340>"
          },
          "type": {
            "type": "string",
            "nullable": true,
            "description": "Tetermines the type of node.",
            "x-parser-schema-id": "<anonymous-schema-347>"
          },
          "identifier": {
            "type": "string",
            "description": "Gets or sets the human-readable identifier. Used for indexing and searchability within the graph database.",
            "x-parser-schema-id": "<anonymous-schema-348>"
          },
          "summary": {
            "type": "string",
            "nullable": true,
            "description": "Gets the XML documentation summary associated with the type.",
            "x-parser-schema-id": "<anonymous-schema-349>"
          },
          "stable_id": {
            "type": "string",
            "description": "Gets a deterministic identifier based on structural language properties. This ID remains stable across builds, allowing for persistent node mapping.",
            "x-parser-schema-id": "<anonymous-schema-350>"
          },
          "content_hash": {
            "type": "string",
            "description": "Gets a hash representing the current state of the node's content. For example, changes to a class body between commits will result in a different hash, signaling that the node's definition has evolved.",
            "x-parser-schema-id": "<anonymous-schema-351>"
          }
        },
        "required": [
          "accessibility",
          "is_static",
          "is_abstract",
          "is_virtual",
          "is_override",
          "is_sealed",
          "has_custom_accessors",
          "location",
          "identifier",
          "summary",
          "stable_id",
          "content_hash"
        ],
        "description": "Represents an event member in the AST graph.",
        "x-parser-schema-id": "EventNode"
      },
      "DelegateNode": {
        "type": "object",
        "properties": {
          "accessibility": {
            "type": "integer",
            "description": "Gets or sets the visibility of the delegate.",
            "x-parser-schema-id": "<anonymous-schema-352>"
          },
          "location": {
            "type": "object",
            "properties": {
              "file_path": {
                "type": "string",
                "description": "Gets the absolute or relative path to the source file.",
                "x-parser-schema-id": "<anonymous-schema-354>"
              },
              "start_line": {
                "type": "integer",
                "description": "Gets the 1-based start line number.",
                "x-parser-schema-id": "<anonymous-schema-355>"
              },
              "end_line": {
                "type": "integer",
                "description": "Gets the 1-based end line number.",
                "x-parser-schema-id": "<anonymous-schema-356>"
              },
              "start_column": {
                "type": "integer",
                "description": "Gets the 1-based start column position.",
                "x-parser-schema-id": "<anonymous-schema-357>"
              },
              "end_column": {
                "type": "integer",
                "description": "Gets the 1-based end column position.",
                "x-parser-schema-id": "<anonymous-schema-358>"
              },
              "code_snippet": {
                "type": "string",
                "description": "Gets the source code snippet representing this location.",
                "x-parser-schema-id": "<anonymous-schema-359>"
              }
            },
            "required": [
              "file_path",
              "start_line",
              "end_line",
              "start_column",
              "end_column",
              "code_snippet"
            ],
            "nullable": true,
            "description": "Gets the file path where the delegate is defined. Unlike classes, delegates cannot be partial, so they are typically associated with a single source location.",
            "x-parser-schema-id": "<anonymous-schema-353>"
          },
          "type": {
            "type": "string",
            "nullable": true,
            "description": "Tetermines the type of node.",
            "x-parser-schema-id": "<anonymous-schema-360>"
          },
          "identifier": {
            "type": "string",
            "description": "Gets or sets the human-readable identifier. Used for indexing and searchability within the graph database.",
            "x-parser-schema-id": "<anonymous-schema-361>"
          },
          "summary": {
            "type": "string",
            "nullable": true,
            "description": "Gets the XML documentation summary associated with the type.",
            "x-parser-schema-id": "<anonymous-schema-362>"
          },
          "stable_id": {
            "type": "string",
            "description": "Gets a deterministic identifier based on structural language properties. This ID remains stable across builds, allowing for persistent node mapping.",
            "x-parser-schema-id": "<anonymous-schema-363>"
          },
          "content_hash": {
            "type": "string",
            "description": "Gets a hash representing the current state of the node's content. For example, changes to a class body between commits will result in a different hash, signaling that the node's definition has evolved.",
            "x-parser-schema-id": "<anonymous-schema-364>"
          }
        },
        "required": [
          "accessibility",
          "location",
          "identifier",
          "summary",
          "stable_id",
          "content_hash"
        ],
        "description": "Represents a delegate definition in the AST graph.",
        "x-parser-schema-id": "DelegateNode"
      },
      "NamespaceNode": {
        "type": "object",
        "properties": {
          "identifier": {
            "type": "string",
            "description": "Gets or sets the fully qualified name of the namespace (e.g., \"System.Collections.Generic\"). Used as the primary label for grouping types within the graph.",
            "x-parser-schema-id": "<anonymous-schema-365>"
          },
          "type": {
            "type": "string",
            "nullable": true,
            "description": "Tetermines the type of node.",
            "x-parser-schema-id": "<anonymous-schema-366>"
          },
          "stable_id": {
            "type": "string",
            "description": "Gets a deterministic identifier based on structural language properties. This ID remains stable across builds, allowing for persistent node mapping.",
            "x-parser-schema-id": "<anonymous-schema-367>"
          },
          "content_hash": {
            "type": "string",
            "description": "Gets a hash representing the current state of the node's content. For example, changes to a class body between commits will result in a different hash, signaling that the node's definition has evolved.",
            "x-parser-schema-id": "<anonymous-schema-368>"
          }
        },
        "required": [
          "identifier",
          "stable_id",
          "content_hash"
        ],
        "description": "Represents a namespace container in the AST graph.",
        "x-parser-schema-id": "NamespaceNode"
      },
      "ParameterNode": {
        "type": "object",
        "properties": {
          "identifier": {
            "type": "string",
            "description": "Gets the name of the parameter.",
            "x-parser-schema-id": "<anonymous-schema-369>"
          },
          "ordinal": {
            "type": "integer",
            "description": "Gets the zero-based position of the parameter in the formal parameter list.",
            "x-parser-schema-id": "<anonymous-schema-370>"
          },
          "ref_kind": {
            "type": "string",
            "description": "Gets the ref kind of the parameter (e.g., \"None\", \"Ref\", \"Out\", \"In\").",
            "x-parser-schema-id": "<anonymous-schema-371>"
          },
          "is_params": {
            "type": "boolean",
            "description": "Gets a value indicating whether the parameter is a parameter array (marked with the 'params' keyword).",
            "x-parser-schema-id": "<anonymous-schema-372>"
          },
          "is_optional": {
            "type": "boolean",
            "description": "Gets a value indicating whether the parameter is optional (has a default value).",
            "x-parser-schema-id": "<anonymous-schema-373>"
          },
          "is_this": {
            "type": "boolean",
            "description": "Gets a value indicating whether this is the 'this' parameter of an extension method.",
            "x-parser-schema-id": "<anonymous-schema-374>"
          },
          "default_value": {
            "type": "string",
            "nullable": true,
            "description": "Gets the string representation of the default value, if is true.",
            "x-parser-schema-id": "<anonymous-schema-375>"
          },
          "type": {
            "type": "string",
            "nullable": true,
            "description": "Tetermines the type of node.",
            "x-parser-schema-id": "<anonymous-schema-376>"
          },
          "stable_id": {
            "type": "string",
            "description": "Gets a deterministic identifier based on structural language properties. This ID remains stable across builds, allowing for persistent node mapping.",
            "x-parser-schema-id": "<anonymous-schema-377>"
          },
          "content_hash": {
            "type": "string",
            "description": "Gets a hash representing the current state of the node's content. For example, changes to a class body between commits will result in a different hash, signaling that the node's definition has evolved.",
            "x-parser-schema-id": "<anonymous-schema-378>"
          }
        },
        "required": [
          "identifier",
          "ordinal",
          "ref_kind",
          "is_params",
          "is_optional",
          "is_this",
          "default_value",
          "stable_id",
          "content_hash"
        ],
        "description": "Represents a method parameter in the AST graph.",
        "x-parser-schema-id": "ParameterNode"
      },
      "AttributeNode": {
        "type": "object",
        "properties": {
          "identifier": {
            "type": "string",
            "description": "Gets or sets the human-readable identifier (e.g., MethodName or FullClassName). Used for indexing and searchability within the graph database.",
            "x-parser-schema-id": "<anonymous-schema-379>"
          },
          "arguments": {
            "type": "array",
            "items": {
              "type": "string",
              "nullable": true,
              "x-parser-schema-id": "<anonymous-schema-381>"
            },
            "description": "Gets or sets the list of arguments associated with the attribute.",
            "x-parser-schema-id": "<anonymous-schema-380>"
          },
          "type": {
            "type": "string",
            "nullable": true,
            "description": "Tetermines the type of node.",
            "x-parser-schema-id": "<anonymous-schema-382>"
          },
          "stable_id": {
            "type": "string",
            "description": "Gets a deterministic identifier based on structural language properties. This ID remains stable across builds, allowing for persistent node mapping.",
            "x-parser-schema-id": "<anonymous-schema-383>"
          },
          "content_hash": {
            "type": "string",
            "description": "Gets a hash representing the current state of the node's content. For example, changes to a class body between commits will result in a different hash, signaling that the node's definition has evolved.",
            "x-parser-schema-id": "<anonymous-schema-384>"
          }
        },
        "required": [
          "identifier",
          "arguments",
          "stable_id",
          "content_hash"
        ],
        "description": "Represents a code attribute/decorator in the AST graph.",
        "x-parser-schema-id": "AttributeNode"
      },
      "RelationNode": {
        "type": "object",
        "properties": {
          "id": {
            "type": "string",
            "description": "Unique deterministic identifier for the relation edge. Computed via : Direct: SHA256(\"{from}|{relation_type}|{to}\")Pending: SHA256(\"{from}|{relation_type}|{to}|pending\")",
            "x-parser-schema-id": "<anonymous-schema-385>"
          },
          "from": {
            "type": "string",
            "description": "The identifier of the source node (usually the caller or declaring symbol's content hash).",
            "x-parser-schema-id": "<anonymous-schema-386>"
          },
          "to": {
            "type": "string",
            "description": "The identifier of the target node. Contains the target node's ContentHash when direct ( is false), or the target symbol's StableId when pending resolution ( is true).",
            "x-parser-schema-id": "<anonymous-schema-387>"
          },
          "relation_type": {
            "type": "string",
            "description": "The semantic type of the relationship (e.g., \"CONTAINS\", \"IMPLEMENTS\", \"CALLS\", \"OF_TYPE\"). See .",
            "x-parser-schema-id": "<anonymous-schema-388>"
          },
          "is_pending": {
            "type": "boolean",
            "description": "Indicates whether the target node was not yet parsed or resolved during streaming analysis. When true, allows analyzers to emit edges without buffering entire syntax graphs in memory. The server resolves pending relations in a post-ingestion query before completion acknowledgement.",
            "x-parser-schema-id": "<anonymous-schema-389>"
          },
          "run_id": {
            "type": "string",
            "nullable": true,
            "description": "Identifier of the current analysis execution session correlated with this edge.",
            "x-parser-schema-id": "<anonymous-schema-390>"
          }
        },
        "description": "Represents a directed relationship edge between two AST entities.",
        "x-parser-schema-id": "RelationNode"
      },
      "CodeLocation": {
        "type": "object",
        "properties": {
          "file_path": {
            "type": "string",
            "description": "Gets the absolute or relative path to the source file.",
            "x-parser-schema-id": "<anonymous-schema-391>"
          },
          "start_line": {
            "type": "integer",
            "description": "Gets the 1-based start line number.",
            "x-parser-schema-id": "<anonymous-schema-392>"
          },
          "end_line": {
            "type": "integer",
            "description": "Gets the 1-based end line number.",
            "x-parser-schema-id": "<anonymous-schema-393>"
          },
          "start_column": {
            "type": "integer",
            "description": "Gets the 1-based start column position.",
            "x-parser-schema-id": "<anonymous-schema-394>"
          },
          "end_column": {
            "type": "integer",
            "description": "Gets the 1-based end column position.",
            "x-parser-schema-id": "<anonymous-schema-395>"
          },
          "code_snippet": {
            "type": "string",
            "description": "Gets the source code snippet representing this location.",
            "x-parser-schema-id": "<anonymous-schema-396>"
          }
        },
        "required": [
          "file_path",
          "start_line",
          "end_line",
          "start_column",
          "end_column",
          "code_snippet"
        ],
        "description": "Source code file location coordinates.",
        "x-parser-schema-id": "CodeLocation"
      }
    }
  },
  "x-parser-spec-parsed": true,
  "x-parser-api-version": 3,
  "x-parser-spec-stringified": true
};
    const config = {"show":{"sidebar":true},"sidebar":{"showOperations":"byDefault"}};
    const appRoot = document.getElementById('root');
    AsyncApiStandalone.render(
        { schema, config, }, appRoot
    );
  