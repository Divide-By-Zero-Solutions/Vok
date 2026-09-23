using System.Net.Http.Json;
using Vok.Domain.Interfaces;

namespace Vok.Infrastructure.Services;

public class NeuralVoiceService : IVoiceService {
    private readonly HttpClient _http;
    private readonly string _piperEndpoint = "http://localhost:10200"; // Standard Piper TTS port

    public NeuralVoiceService(HttpClient http) => _http = http;

    public async Task SpeakAsync(string text, float pitch, float rate, string emotion = "neutral") {
        // Modulate parameters based on emotion
        var finalPitch = pitch;
        var finalRate = rate;

        switch (emotion.ToLower()) {
            case "sad": finalPitch *= 0.8f; finalRate *= 0.7f; break;
            case "happy": finalPitch *= 1.2f; finalRate *= 1.1f; break;
            case "angry": finalPitch *= 0.9f; finalRate *= 1.3f; break;
        }

        try {
            // Send to local Piper TTS server
            var content = JsonContent.Create(new { 
                text = text, 
                voice = "en_US-lessac-medium", 
                length_scale = 1.0f / finalRate, 
                noise_scale = 0.667f 
            });
            await _http.PostAsync($"{_piperEndpoint}/api/tts", content);
        } catch {
            // Fallback to system TTS if Neural Server is offline
            await TextToSpeech.Default.SpeakAsync(text, new SpeechOptions { Pitch = finalPitch, Rate = finalRate });
        }
    }

    public List<string> GetAvailableVoices() => new() { "Neural (Piper)", "System Default" };
}
