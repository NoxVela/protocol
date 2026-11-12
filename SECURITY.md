# Security Policy

The NoxVela team takes the security and integrity of our communication protocol, data contracts, and client libraries seriously. We appreciate the responsible disclosure of any vulnerabilities found in **NoxVela.Protocol**.

---

## Supported Versions

Only the latest active major and minor releases receive security updates and patches:

| Version | Supported          | Status |
| :--- | :--- | :--- |
| **1.0.x** | :white_check_mark: | Current Stable Release |
| < 1.0.0 | :x:                | Deprecated / Pre-release |

---

## Reporting a Vulnerability

> [!IMPORTANT]
> **Please do not report security vulnerabilities through public GitHub issues, pull requests, or discussions.**

If you discover a security vulnerability, issue, or potential flaw within `NoxVela.Protocol`, report it privately using **GitHub Private Vulnerability Reporting**:

1. Navigate to the [Security Advisories](https://github.com/NoxVela/protocol/security/advisories) tab of this repository.
2. Click **Report a vulnerability** to submit a private security advisory report directly to the repository maintainers.

This ensures your advisory is handled in a secure, encrypted private workspace where maintainers can collaborate with you on a patch before public disclosure.

---

## What to Include in Your Report

To help us triage and resolve the issue quickly, include as much detail as possible:
* **Type of issue**: (e.g., deserialization vulnerability, buffer exhaustion / DoS via micro-batches, authentication bypass in handshake headers, ID collision exploit).
* **Affected component(s)**: Specific classes, serialization converters, or message handlers.
* **Reproduction steps**: Clear step-by-step instructions or a minimal Proof of Concept (PoC) payload.
* **Impact assessment**: Potential impact on analyzer workers, the core server, or client connection stability.
* **Mitigation / Suggested fix** (if known).

---

## Our Response Process

1. **Acknowledgment**: We aim to acknowledge receipt of your vulnerability report within **48 hours**.
2. **Investigation & Triage**: Within **7 days**, we will confirm the issue, determine its severity, and keep you informed of our progress.
3. **Patch & Verification**: We will develop a fix and verify it against our automated test safety net.
4. **Coordinated Disclosure**: We will publish a patched release (e.g., v1.0.1) and coordinate a public security advisory credits disclosure with you.
