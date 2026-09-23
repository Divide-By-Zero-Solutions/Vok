# Vok.Infrastructure

`Vok.Infrastructure` implements `Vok.Domain` contracts and isolates platform, persistence, HTTP, AI, voice, and device integrations from the UI.

## Service groups

- Persistence: SQLite, preferences, JSON files, backups, and vector storage.
- AI and voice: cloud, embedded, Ollama, neural voice, native MAUI voice, and fallback orchestration.
- Device adapters: audio, location, notifications, hardware feedback, scanning, and capture.
- Integration: downloads, synchronization, speaker verification, and application configuration.

## Design rules

Implementations should be registered in `Vok.Maui/MauiProgram.cs`, expose domain interfaces, isolate external failures at boundaries, and keep provider-specific models out of `Vok.Domain`.

See [architecture](../docs/architecture.md), [data model](../docs/data-model.md), and [API contracts](../docs/api.md).
