# Vok.Domain

`Vok.Domain` is the framework-neutral contract and model layer for the AAC application. It contains vocabulary/profile models and interfaces used by the MAUI application and infrastructure adapters.

## Boundaries

- Must not depend on MAUI, SQLite, HTTP clients, or provider SDKs.
- Defines service contracts consumed through dependency injection.
- Keeps models serializable and provider-neutral.

## Key files

- `Models/AacTile.cs` — vocabulary, category, and profile models.
- `Interfaces/IServices.cs` — application service contracts and shared enums.

See [API contracts](../docs/api.md), [architecture](../docs/architecture.md), and [coding standards](../docs/standards.md).
