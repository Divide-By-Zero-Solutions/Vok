# Vok.Maui

`Vok.Maui` is the Windows-focused .NET MAUI host and Blazor UI for Vok.

## Platform targets

- **Android:** `net10.0-android`, minimum API 30.
- **Windows:** `net10.0-windows10.0.19041.0`.

Android metadata is defined in `Platforms/Android/AndroidManifest.xml`. The manifest declares network, microphone, coarse/fine location, and notification permissions. Android still requires runtime approval for microphone and location; features should remain usable when optional permissions are denied.

## UI areas

- `/` — AAC tile selection, sentence construction, AI suggestions, and speech.
- `/manage` — vocabulary tile management.
- `/profiles` — profile selection and creation.
- `/settings` — downloads, scanning, haptics, and assets.
- `/caregiver` — suggestions, backups, and caregiver controls.

`MauiProgram.cs` composes the dependency injection graph and configures the BlazorWebView. Pages should depend on domain interfaces rather than concrete infrastructure classes.

See [architecture](../docs/architecture.md), [deployment](../docs/deployment.md), and [testing](../docs/testing.md).
