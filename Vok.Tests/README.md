# Vok.Tests

`Vok.Tests` contains xUnit service and integration tests for domain-facing infrastructure behavior.

## Coverage areas

- Vocabulary persistence and SQLite lifecycle.
- AI routing and voice fallback.
- Vector search and prediction caching.
- Backup/settings serialization.
- Switch scanning behavior and service events.

Tests should isolate filesystem and database state, avoid live network dependencies, and assert observable contract behavior. Coverage is configured by `coverage.runsettings` and is published by CI.

See [testing strategy](../docs/testing.md) and [test-flow diagrams](../docs/diagrams.md#test-and-documentation-flow).
