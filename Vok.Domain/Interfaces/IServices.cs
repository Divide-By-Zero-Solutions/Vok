namespace Vok.Domain.Interfaces;
using Vok.Domain.Models;

public interface IDownloadService {
    Task<IEnumerable<DownloadableAsset>> GetAvailableAssetsAsync();
    Task DownloadAssetAsync(string assetId, Action<double> progressCallback);
    bool IsAssetDownloaded(string assetId);
}

public record DownloadableAsset(string Id, string Name, string Url, string Category, long SizeBytes);

public enum ScanningMode {
    Sequential,
    RowColumn
}

public interface ISwitchControlService {
    bool IsScanningEnabled { get; set; }
    ScanningMode Mode { get; set; }
    void StartScanning();
    void StopScanning();
    void TriggerSelect();
    event Action<AacTile> OnTileSelected;
    event Action<int, int?> OnScanHighlight; // (index, column_or_row)
}

public interface IHardwareFeedbackService {
    void TriggerSuccessHaptic();
    void TriggerErrorHaptic();
    void TriggerWarningHaptic();
}

public interface ILocationService {
    Task<string> GetCurrentContextCategoryAsync();
    void RequestPermissions();
}

public interface IVectorStore {
    Task AddMemoryAsync(string text, float[] embedding);
    Task<List<string>> QueryMemoriesAsync(string query, int topK = 3);
}

public interface ISyncService {
    Task SyncDataAsync();
    Task ExportToCloudAsync();
    Task ImportFromCloudAsync();
}

public interface INotificationService {
    Task ShowNotificationAsync(string title, string message);
    void SetNotificationChannel(string channelId, string name, string description);
}

public interface IVoiceCaptureService {
    Task StartListeningAsync(Action<string> onSpeechDetected);
    Task StopListeningAsync();
    bool IsListening { get; }
}

public interface ISpeakerVerificationService {
    Task<bool> VerifyPatientVoiceAsync(byte[] audioData);
    Task EnrollPatientVoiceAsync(byte[] audioData);
}

public interface ICommunicationOrchestrator {
    Task BeginChatSessionAsync();
    Task EndChatSessionAsync();
    bool IsPatientActive { get; }
}

public interface IVoiceService {
    Task SpeakAsync(string text, float pitch, float rate, string emotion = "neutral");
    List<string> GetAvailableVoices();
}

public interface IAIService {
    Task<List<string>> PredictNextWordsAsync(string currentPhrase, List<string> history);
    Task<string> PolishSentenceAsync(List<string> tokens);
    Task<AacCategory?> LearnCategoryAsync(string sentence, List<string> existingCategories);
    Task<string> AnalyzeSentimentAsync(string text); // Added for Emotional Voice
    Task LoadModelAsync(string modelPath);
}

public interface IVocabularyService {
    List<AacCategory> Categories { get; }
    List<AacTile> GetTiles(string categoryKey);
    Task AddLearnedCategoryAsync(AacCategory category);
    Task AddTileAsync(string categoryKey, AacTile tile);
    Task DeleteTileAsync(string tileId);
    void Save();
}

public interface IPredictionCache {
    void SetCache(string phrase, List<string> predictions);
    List<string>? GetCache(string phrase);
    void Clear();
}

public interface IAiSettings {
    string OpenAIKey { get; set; }
    string ClaudeKey { get; set; }
    string GoogleKey { get; set; }
    string PreferredProvider { get; set; }
}

public interface IVoiceSettings {
    string ElevenLabsApiKey { get; set; }
    string PreferredVoiceId { get; set; }
    bool UseCloudVoice { get; set; }
}

public interface IAppConfig {
    string GetValue(string key, string defaultValue);
    void SetValue(string key, string value);
}

