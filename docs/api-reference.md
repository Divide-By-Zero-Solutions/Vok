# Maintained API reference

The domain interfaces are the source-of-truth API contract. Infrastructure implementations should use `<inheritdoc />` or an implementation-specific XML comment when a method changes observable behavior. The following table maps the maintained implementation surface to its contract and responsibility.

| Implementation | Contract or role | Main methods and responsibility |
| --- | --- | --- |
| `SqliteVocabularyService` | `IVocabularyService` | Load categories, read tiles, add categories/tiles, delete tiles, persist database state. |
| `LocalVocabularyService` | `IVocabularyService` | Serialize and restore vocabulary through MAUI Preferences. |
| `PredictionCacheService` | `IPredictionCache` | Set, retrieve case-insensitive predictions, and clear cache entries. |
| `LocalVectorStore` | `IVectorStore` | Add embedded memories and rank query results by similarity. |
| `AdvancedAiRouter` | `IAIService` | Route predictions, polishing, sentiment, and category learning with local fallback. |
| `EmbeddedAiService` | `IAIService` | Provide local model loading and deterministic local AI behavior. |
| `OllamaAiService` | `IAIService` | Send AI requests to a local Ollama HTTP endpoint. |
| `VoiceOrchestrator` | `IVoiceService` | Select cloud or native voice and fail over on provider errors. |
| `MauiVoiceService` | `IVoiceService` | Use the native MAUI speech API. |
| `ElevenLabsVoiceService` | `IVoiceService` | Generate and play remote ElevenLabs audio. |
| `EmbeddedVoiceService` | `IVoiceService` | Provide local voice output. |
| `VoiceCaptureService` | `IVoiceCaptureService` | Start/stop speech capture and deliver recognized text. |
| `CommunicationOrchestrator` | `ICommunicationOrchestrator` | Coordinate capture, verification, vocabulary, notifications, and session state. |
| `SwitchScanningService` | `ISwitchControlService` | Cycle highlights and raise tile-selection events. |
| `DownloadService` | `IDownloadService` | List, download, and locate local assets. |
| `SecureSyncService` | `ISyncService` | Synchronize local data with a secure remote endpoint. |
| `BackupService` | Backup role | Export and import JSON vocabulary backups. |
| `LocalProfileService` | Profile role | Persist and select user profiles. |
| `AppConfig`, `AiSettings`, `VoiceSettings` | Configuration contracts | Read, write, and persist application/provider settings. |
| `SpeakerVerificationService` | `ISpeakerVerificationService` | Enroll samples and compare voice prints. |
| `MauiHardwareFeedbackService`, `MauiLocationService`, `LocalNotificationService` | Device contracts | Adapt haptics, location, and notifications to platform facilities. |

For full signatures and parameter/return documentation, use the XML documentation files generated under each project's `bin` directory or the source contracts in `Vok.Domain/Interfaces/IServices.cs`.
