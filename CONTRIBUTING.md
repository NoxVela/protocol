# Contributing to NoxVela.Protocol

Thank you for your interest in contributing to **NoxVela.Protocol**!

`NoxVela.Protocol` is the wire protocol, data contract standard, and client serialization library connecting AST code analyzers and worker services to the NoxVela visualization platform. Maintaining consistency, determinism, and backward compatibility is critical for all components in the ecosystem.

This document outlines the guidelines and workflow for contributing to this project.

---

## Code of Conduct

By participating in this project, you agree to abide by our [Code of Conduct](./CODE_OF_CONDUCT.md). Please report unacceptable behavior through GitHub Security Advisories.

---

## How Can I Contribute?

### 1. Reporting Bugs
* Search the existing [GitHub Issues](https://github.com/NoxVela/protocol/issues) first to see if the issue has already been reported.
* If not, open a new issue using our **Bug Report** template.
* Include:
  * Clear description of the unexpected behavior.
  * Reproduction steps or sample message payload (JSON / MessagePack).
  * Expected vs actual behavior.
  * Target platform and runtime environment (.NET SDK version, OS).

### 2. Suggesting Protocol Enhancements (RFC)
Because `NoxVela.Protocol` defines wire contracts used by multiple languages and external workers, all schema or contract additions should follow an RFC-style discussion:
* Open an issue using the **Feature Request** template.
* Describe the motivation: which analyzer capability or server feature requires this contract?
* Provide sample payload structures (JSON schema / C# records).
* Address backward compatibility: how will existing analyzers and server handlers react to this message or property?

### 3. Submitting Pull Requests
* Always link your PR to an existing issue (`Fixes #123` or `Closes #123`).
* Keep PRs focused: avoid bundling unrelated contract additions, refactors, or documentation overhauls together.
* Ensure all tests pass and documentation is updated before requesting review.

---

## Protocol Design Principles

When modifying or introducing contracts in `NoxVela.Protocol`, adhere to these fundamental principles:

1. **Semantic Versioning (SemVer 2.0.0)**:
   * **PATCH**: Bug fixes, non-breaking comment updates, internal generator fixes.
   * **MINOR**: Additive, backward-compatible enhancements (new optional fields, new non-mandatory message types).
   * **MAJOR**: Breaking changes (renaming message types, changing required fields, removing constants). Major changes require coordination across the entire ecosystem.
2. **Wire Format Agnosticism**:
   * Every model must be serializable with both `System.Text.Json` (snake_case naming policy) and `MessagePack` (via explicit `[Key]` / `[MessagePackObject]` attributes).
3. **Deterministic Identity**:
   * AST code nodes must provide deterministic Stable IDs (`StableId`) and content hashes (`ContentHash`). Follow the hashing standard specified in [`INTEGRATION_GUIDE.md`](./INTEGRATION_GUIDE.md#5-canonical-hashing-standard-stableidfactory).
4. **Comprehensive Documentation**:
   * Every public C# class, record, property, and enum member **must** have clear XML doc comments (`/// <summary>`). These comments are extracted by the generator directly into the AsyncAPI specification and HTML documentation portal.

---

## Development Setup

### Prerequisites
* [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) (or latest compatible)
* [Node.js 24+](https://nodejs.org/) (for AsyncAPI CLI and documentation tooling)
* [@asyncapi/cli](https://www.npmjs.com/package/@asyncapi/cli):
  ```bash
  npm install -g @asyncapi/cli
  ```

### Building the Solution
```bash
# Clone the repository
git clone https://github.com/NoxVela/protocol.git
cd protocol

# Restore and build
dotnet restore NoxVela.sln
dotnet build NoxVela.sln --configuration Release
```

### Running Tests
All changes must be backed by automated tests:
```bash
dotnet test NoxVela.sln --configuration Release --verbosity normal
```

### Regenerating AsyncAPI Specification & Documentation
Whenever you modify C# models, message types, or XML doc comments, regenerate the AsyncAPI contract and HTML documentation:
```bash
# Regenerate spec/asyncapi.yaml and docs/
dotnet run --project tools/NoxVela.Protocol.Generator/NoxVela.Protocol.Generator.csproj --configuration Release -- --html .

# Validate generated AsyncAPI specification
asyncapi validate spec/asyncapi.yaml
```

---

## Git Workflow & Commit Guidelines

1. **Branch Naming**:
   * `feat/short-description` for new features or contracts.
   * `fix/short-description` for bug fixes.
   * `docs/short-description` for documentation or specification updates.
   * `chore/short-description` for tooling, CI, or dependency updates.
2. **Conventional Commits**:
   Commit messages must follow the [Conventional Commits](https://www.conventionalcommits.org/) format:
   * `feat(models): add arity and complexity metrics to MethodNode`
   * `fix(serialization): handle null values in NodeBaseConverter`
   * `docs(spec): regenerate AsyncAPI schema for v1.0.0`
   * `test(messages): add roundtrip serialization test for Wrapper`
3. **Clean Commit History**:
   Keep commit histories clean, atomic, and well-described. Rebasing onto `main` before opening a PR is recommended.

---

## Pull Request Checklist

Before submitting a Pull Request, verify:
- [ ] Code compiles without warnings (`dotnet build --configuration Release`).
- [ ] All unit and serialization tests pass (`dotnet test`).
- [ ] XML documentation comments (`/// <summary>`) added to all new models and properties.
- [ ] AsyncAPI specification regenerated (`dotnet run --project tools/... -- --html .`) and validated (`asyncapi validate spec/asyncapi.yaml`).
- [ ] Conventional Commit message format used.
- [ ] PR description clearly explains the changes and references corresponding issue(s).
