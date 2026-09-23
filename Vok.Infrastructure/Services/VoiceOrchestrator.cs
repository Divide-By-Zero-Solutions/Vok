using Vok.Domain.Interfaces;

namespace Vok.Infrastructure.Services;

/// <summary>Selects cloud or local voice output according to persisted settings.</summary>
public class VoiceOrchestrator : IVoiceService {
    private readonly IVoiceService _cloudVoice;
    private readonly IVoiceService _localVoice;
    private readonly IVoiceSettings _settings;

    public VoiceOrchestrator(IVoiceService cloud, IVoiceService local, IVoiceSettings settings) {
        _cloudVoice = cloud;
        _localVoice = local;
        _settings = settings;
    }

    public async Task SpeakAsync(string text, float pitch, float rate, string emotion = "neutral") {
        if (_settings.UseCloudVoice) {
            try {
                await _cloudVoice.SpeakAsync(text, pitch, rate, emotion);
                return;
            } catch (Exception ex) {
                Console.WriteLine($"Cloud voice failed: {ex.Message}. Falling back to local.");
            }
        }
        // Fallback to local project
        await _localVoice.SpeakAsync(text, pitch, rate, emotion);
    }

    public List<string> GetAvailableVoices() {
        var voices = _localVoice.GetAvailableVoices();
        if (_settings.UseCloudVoice) voices.AddRange(_cloudVoice.GetAvailableVoices());
        return voices;
    }
}

/// <summary>Persists voice provider credentials and preferences.</summary>
public class VoiceSettings : IVoiceSettings {
    private readonly IAppConfig _config;
    public VoiceSettings(IAppConfig config) => _config = config;

    public string ElevenLabsApiKey { 
        get => _config.GetValue("ELEVENLABS_API_KEY", ""); 
        set => _config.SetValue("ELEVENLABS_API_KEY", value); 
    }
    public string PreferredVoiceId { 
        get => _config.GetValue("PREFERRED_VOICE_ID", "21m00TcmSsS7S6SiaS6A"); 
        set => _config.SetValue("PREFERRED_VOICE_ID", value); 
    }
    public bool UseCloudVoice { 
        get => _config.GetValue("USE_CLOUD_VOICE", "false") == "true"; 
        set => _config.SetValue("USE_CLOUD_VOICE", value.ToString().ToLower()); 
    }
    
    public void Persist() => _config.Save();
}
