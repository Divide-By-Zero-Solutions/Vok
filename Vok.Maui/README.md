# Vok.Maui

`Vok.Maui` is the Windows-focused .NET MAUI host and Blazor UI for Vok.

## UI areas

- `/` — AAC tile selection, sentence construction, AI suggestions, and speech.
- `/manage` — vocabulary tile management.
- `/profiles` — profile selection and creation.
- `/settings` — downloads, scanning, haptics, and assets.
- `/caregiver` — suggestions, backups, and caregiver controls.

`MauiProgram.cs` composes the dependency injection graph and configures the BlazorWebView. Pages should depend on domain interfaces rather than concrete infrastructure classes.

See [architecture](../docs/architecture.md), [deployment](../docs/deployment.md), and [testing](../docs/testing.md).
