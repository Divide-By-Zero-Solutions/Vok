using Vok.Domain.Interfaces;

namespace Vok.Infrastructure.Services;

/// <summary>Provides local embedded voice behavior.</summary>
public class EmbeddedVoiceService : IVoiceService {
    public async Task SpeakAsync(string text, float pitch, float rate, string emotion = "neutral") {
        var finalPitch = pitch;
        var finalRate = rate;

        switch (emotion.ToLower()) {
            case "sad": finalPitch *= 0.8f; finalRate *= 0.7f; break;
            case "happy": finalPitch *= 1.2f; finalRate *= 1.1f; break;
            case "angry": finalPitch *= 0.9f; finalRate *= 1.3f; break;
        }

        await TextToSpeech.Default.SpeakAsync(text, new SpeechOptions { 
            Pitch = finalPitch, 
            Rate = finalRate 
        });
    }

    public List<string> GetAvailableVoices() => new() { "Embedded Native Voice" };
}
