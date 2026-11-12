## Description
<!-- Provide a clear and concise summary of the changes introduced in this PR. -->

## Related Issue
<!-- Link to the issue this PR resolves or addresses -->
Closes #

## Type of Change
<!-- Please mark the relevant options with an [x] -->
- [ ] **Bug fix** (non-breaking change fixing a serialization, validation, or protocol defect)
- [ ] **New contract / feature** (backward-compatible addition to models, messages, or constants)
- [ ] **Breaking change** (fix or feature modifying existing message shapes or required fields)
- [ ] **Documentation / Specification** (updates to README, INTEGRATION_GUIDE, AsyncAPI, or HTML docs)
- [ ] **Tooling / CI** (generator updates, workflow optimizations, dependency bumps)

## Protocol & Schema Impact
<!-- Detail any wire protocol or schema modifications -->
- **AsyncAPI regenerated**: `[ ] Yes / [ ] Not applicable`
- **Serialization support**: `[ ] JSON` and/or `[ ] MessagePack`
- **Backward compatible**: `[ ] Yes / [ ] Breaking (requires major version bump)`

## Verification & Testing
<!-- Describe the tests executed to verify your changes -->
- [ ] Solution compiles cleanly without warnings: `dotnet build --configuration Release`
- [ ] All unit and serialization tests pass: `dotnet test`
- [ ] AsyncAPI schema validated: `asyncapi validate spec/asyncapi.yaml`

## Checklist
- [ ] My code adheres to the coding style guidelines of this project (`.editorconfig`).
- [ ] I have added XML documentation comments (`/// <summary>`) to all public classes, properties, and enum members.
- [ ] I have added automated unit tests covering the new behavior or contracts.
- [ ] My commit messages follow the [Conventional Commits](https://www.conventionalcommits.org/) convention.
- [ ] I have reviewed the [CONTRIBUTING.md](./CONTRIBUTING.md) guidelines.
