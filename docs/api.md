# API and service contracts

`Vok.Domain` defines the contracts consumed by the MAUI application and implemented by `Vok.Infrastructure`. The domain layer contains no MAUI or provider-specific implementation details.

## Contract groups

- **Vocabulary:** `IVocabularyService`, `IPredictionCache`, `IVectorStore`.
- **Communication:** `ICommunicationOrchestrator`, `IAIService`, `IVoiceService`, `IVoiceCaptureService`.
- **Device integration:** `IHardwareFeedbackService`, `ILocationService`, `ISwitchControlService`.
- **Configuration:** `IAiSettings`, `IVoiceSettings`, `IAppConfig`.
- **Persistence and integration:** `ISyncService`, `INotificationService`, `IDownloadService`, `ISpeakerVerificationService`.

## Boundary rules

1. UI components depend on interfaces, not concrete infrastructure classes.
2. Infrastructure adapters own HTTP, SQLite, preferences, filesystem, audio, and platform APIs.
3. Domain models are serializable data structures and should remain provider-neutral.
4. External failures are handled at adapter/orchestration boundaries and should not leak provider-specific response types into the UI.

The generated XML documentation files are produced in each project's output directory during a documentation build. Source-level symbol comments remain the authoritative explanation of method behavior and constraints.
