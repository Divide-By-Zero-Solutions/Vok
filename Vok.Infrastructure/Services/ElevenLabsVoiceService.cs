using System.Net.Http.Json;
using Vok.Domain.Interfaces;

namespace Vok.Infrastructure.Services;

/// <summary>Generates speech through the ElevenLabs HTTP API.</summary>
public class ElevenLabsVoiceService : IVoiceService {
    private readonly HttpClient _http;
    private readonly IVoiceSettings _settings;

    public ElevenLabsVoiceService(HttpClient http, IVoiceSettings settings) {
        _http = http;
        _settings = settings;
    }

    public async Task SpeakAsync(string text, float pitch, float rate, string emotion = "neutral") {
        if (string.IsNullOrEmpty(_settings.ElevenLabsApiKey)) throw new Exception("API Key missing");

        var request = new {
            text = text,
            model_id = "eleven_multilingual_v2",
            voice_settings = new {
                stability = 0.5f,
                similarity_boost = 0.75f,
                style = 0.0f,
                use_speaker_boost = true
            }
        };

        var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"https://api.elevenlabs.io/v1/text-to-speech/{_settings.PreferredVoiceId}");
        httpRequest.Headers.Add("xi-api-key", _settings.ElevenLabsApiKey);
        httpRequest.Content = JsonContent.Create(request);

        var response = await _http.SendAsync(httpRequest);
        
        if (response.IsSuccessStatusCode) {
            var audioBytes = await response.Content.ReadAsByteArrayAsync();
            var path = Path.Combine(FileSystem.CacheDirectory, "speech.mp3");
            await File.WriteAllBytesAsync(path, audioBytes);
            
            // FIX: Implementation of actual playback using MAUI's MediaElement or similar
            // For this scaffold, we use a simplified AudioPlayer helper
            await AudioPlayer.PlayAsync(path);
        } else {
            throw new Exception($"ElevenLabs Error: {response.StatusCode}");
        }
    }

    public List<string> GetAvailableVoices() => new() { "ElevenLabs Neural" };
}
