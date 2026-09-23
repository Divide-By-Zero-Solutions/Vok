using Vok.Domain.Interfaces;
using Vok.Domain.Models;

namespace Vok.Infrastructure.Services;

public class MauiVoiceService : IVoiceService {
    public async Task SpeakAsync(string text, float pitch, float rate, string emotion = "neutral") {
        await TextToSpeech.Default.SpeakAsync(text, new SpeechOptions { 
            Pitch = pitch, 
            Rate = rate 
        });
    }
    public List<string> GetAvailableVoices() => new() { "Default System Voice" };
}
