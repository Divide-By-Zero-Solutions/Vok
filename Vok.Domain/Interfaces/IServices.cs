namespace Vok.Domain.Interfaces;
using Vok.Domain.Models;

/// <summary>Provides discovery and local management of downloadable assets.</summary>
public interface IDownloadService {
    /// <summary>Gets the assets available from the configured source.</summary>
    Task<IEnumerable<DownloadableAsset>> GetAvailableAssetsAsync();
    /// <summary>Downloads an asset and reports progress from zero to one.</summary>
    /// <param name="assetId">The asset identifier.</param><param name="progressCallback">The progress callback.</param>
    Task DownloadAssetAsync(string assetId, Action<double> progressCallback);
    /// <summary>Determines whether an asset is already available locally.</summary>
    /// <param name="assetId">The asset identifier.</param>
    bool IsAssetDownloaded(string assetId);
}

/// <summary>Describes a downloadable application asset.</summary>
public record DownloadableAsset(string Id, string Name, string Url, string Category, long SizeBytes);

/// <summary>Controls the strategy used to scan selectable tiles.</summary>
public enum ScanningMode {
    /// <summary>Visits tiles one at a time.</summary>
    Sequential,
    /// <summary>Scans rows and then columns.</summary>
    RowColumn
}

/// <summary>Controls switch-access scanning and selection events.</summary>
public interface ISwitchControlService {
    /// <summary>Gets or sets whether scanning is active.</summary>
    bool IsScanningEnabled { get; set; }
    /// <summary>Gets or sets the active scanning mode.</summary>
    ScanningMode Mode { get; set; }
    /// <summary>Starts the scanning timer and highlight sequence.</summary>
    void StartScanning();
    /// <summary>Stops scanning and clears active state.</summary>
    void StopScanning();
    /// <summary>Selects the currently highlighted tile.</summary>
    void TriggerSelect();
    /// <summary>Raised when scanning selects a tile.</summary>
    event Action<AacTile> OnTileSelected;
    /// <summary>Raised when the highlighted index or row/column changes.</summary>
    event Action<int, int?> OnScanHighlight; // (index, column_or_row)
}

/// <summary>Provides tactile feedback for interaction outcomes.</summary>
public interface IHardwareFeedbackService {
    /// <summary>Signals a successful operation.</summary>
    void TriggerSuccessHaptic();
    /// <summary>Signals an error.</summary>
    void TriggerErrorHaptic();
    /// <summary>Signals a warning.</summary>
    void TriggerWarningHaptic();
}

/// <summary>Provides location-derived context for adaptive vocabulary.</summary>
public interface ILocationService {
    /// <summary>Gets the current context category.</summary>
    Task<string> GetCurrentContextCategoryAsync();
    /// <summary>Requests platform location permissions.</summary>
    void RequestPermissions();
}

/// <summary>Stores and searches vectorized communication memories.</summary>
public interface IVectorStore {
    /// <summary>Adds a text and its embedding to the memory store.</summary>
    Task AddMemoryAsync(string text, float[] embedding);
    /// <summary>Finds the most relevant memories for a query.</summary>
    /// <param name="query">The query text.</param><param name="topK">The maximum result count.</param>
    Task<List<string>> QueryMemoriesAsync(string query, int topK = 3);
}

/// <summary>Synchronizes application data with a configured remote service.</summary>
public interface ISyncService {
    /// <summary>Synchronizes local and remote data.</summary>
    Task SyncDataAsync();
    /// <summary>Exports local data to the remote service.</summary>
    Task ExportToCloudAsync();
    /// <summary>Imports remote data into the local store.</summary>
    Task ImportFromCloudAsync();
}

/// <summary>Displays user notifications through the platform.</summary>
public interface INotificationService {
    /// <summary>Shows a notification.</summary>
    Task ShowNotificationAsync(string title, string message);
    /// <summary>Configures a platform notification channel.</summary>
    void SetNotificationChannel(string channelId, string name, string description);
}

/// <summary>Captures speech and reports recognized text.</summary>
public interface IVoiceCaptureService {
    /// <summary>Starts listening and invokes the callback when speech is detected.</summary>
    Task StartListeningAsync(Action<string> onSpeechDetected);
    /// <summary>Stops the active listening session.</summary>
    Task StopListeningAsync();
    /// <summary>Gets whether speech capture is active.</summary>
    bool IsListening { get; }
}

/// <summary>Enrolls and verifies the patient's voice.</summary>
public interface ISpeakerVerificationService {
    /// <summary>Verifies captured audio against the enrolled voice.</summary>
    Task<bool> VerifyPatientVoiceAsync(byte[] audioData);
    /// <summary>Enrolls captured audio as the patient's voice.</summary>
    Task EnrollPatientVoiceAsync(byte[] audioData);
}

/// <summary>Coordinates communication sessions and patient state.</summary>
public interface ICommunicationOrchestrator {
    /// <summary>Starts a communication session.</summary>
    Task BeginChatSessionAsync();
    /// <summary>Ends the current communication session.</summary>
    Task EndChatSessionAsync();
    /// <summary>Gets whether a patient is currently active.</summary>
    bool IsPatientActive { get; }
}

/// <summary>Converts text to spoken output and exposes available voices.</summary>
public interface IVoiceService {
    /// <summary>Speaks text using the requested prosody and emotion.</summary>
    Task SpeakAsync(string text, float pitch, float rate, string emotion = "neutral");
    /// <summary>Gets the voices available from the active provider.</summary>
    List<string> GetAvailableVoices();
}

/// <summary>Provides language-model predictions and analysis.</summary>
public interface IAIService {
    /// <summary>Predicts likely next words for a phrase and history.</summary>
    Task<List<string>> PredictNextWordsAsync(string currentPhrase, List<string> history);
    /// <summary>Polishes tokens into a sentence.</summary>
    Task<string> PolishSentenceAsync(List<string> tokens);
    /// <summary>Extracts a learned category from a sentence.</summary>
    Task<AacCategory?> LearnCategoryAsync(string sentence, List<string> existingCategories);
    /// <summary>Analyzes the sentiment of text.</summary>
    Task<string> AnalyzeSentimentAsync(string text); // Added for Emotional Voice
    /// <summary>Loads a local model from the supplied path.</summary>
    Task LoadModelAsync(string modelPath);
}

/// <summary>Provides access to persistent AAC vocabulary.</summary>
public interface IVocabularyService {
    /// <summary>Gets the currently loaded categories.</summary>
    List<AacCategory> Categories { get; }
    /// <summary>Gets tiles for a category key.</summary>
    List<AacTile> GetTiles(string categoryKey);
    /// <summary>Adds a learned category and persists it.</summary>
    Task AddLearnedCategoryAsync(AacCategory category);
    /// <summary>Adds a tile to a category and persists it.</summary>
    Task AddTileAsync(string categoryKey, AacTile tile);
    /// <summary>Deletes a tile by identifier.</summary>
    Task DeleteTileAsync(string tileId);
    /// <summary>Persists pending vocabulary changes.</summary>
    void Save();
}

/// <summary>Caches predictions by normalized phrase.</summary>
public interface IPredictionCache {
    /// <summary>Stores predictions for a phrase.</summary>
    void SetCache(string phrase, List<string> predictions);
    /// <summary>Gets cached predictions, or null when absent.</summary>
    List<string>? GetCache(string phrase);
    /// <summary>Clears all cached predictions.</summary>
    void Clear();
}

/// <summary>Stores AI provider credentials and selection.</summary>
public interface IAiSettings {
    /// <summary>Gets or sets the OpenAI key.</summary>
    string OpenAIKey { get; set; }
    /// <summary>Gets or sets the Claude key.</summary>
    string ClaudeKey { get; set; }
    /// <summary>Gets or sets the Google key.</summary>
    string GoogleKey { get; set; }
    /// <summary>Gets or sets the preferred provider.</summary>
    string PreferredProvider { get; set; }
}

/// <summary>Stores voice provider credentials and preferences.</summary>
public interface IVoiceSettings {
    /// <summary>Gets or sets the ElevenLabs key.</summary>
    string ElevenLabsApiKey { get; set; }
    /// <summary>Gets or sets the preferred voice identifier.</summary>
    string PreferredVoiceId { get; set; }
    /// <summary>Gets or sets whether cloud voice is enabled.</summary>
    bool UseCloudVoice { get; set; }
}

/// <summary>Provides key/value application configuration.</summary>
public interface IAppConfig {
    /// <summary>Gets a value or the supplied default when no value exists.</summary>
    string GetValue(string key, string defaultValue);
    /// <summary>Stores a key/value configuration entry.</summary>
    void SetValue(string key, string value);
}

