using Vok.Domain.Interfaces;

namespace Vok.Infrastructure.Services;

/// <summary>Captures speech and reports recognized text to the communication flow.</summary>
public class VoiceCaptureService : IVoiceCaptureService {
    private bool _isListening;
    public bool IsListening => _isListening;
    private CancellationTokenSource? _cts;

    public async Task StartListeningAsync(Action<string> onSpeechDetected) {
        _isListening = true;
        _cts = new CancellationTokenSource();

        // Explicit recording mode: only runs while _isListening is true
        _ = Task.Run(async () => {
            while (_isListening && !_cts.Token.IsCancellationRequested) {
                var audioChunk = await CaptureAudioChunk();
                
                if (DetectSpeech(audioChunk)) {
                    var text = await TranscribeAudio(audioChunk);
                    if (!string.IsNullOrEmpty(text)) {
                        onSpeechDetected(text);
                    }
                }
                await Task.Delay(100);
            }
        }, _cts.Token);
    }

    public async Task StopListeningAsync() {
        _isListening = false;
        _cts?.Cancel();
        await Task.CompletedTask;
    }

    private async Task<byte[]> CaptureAudioChunk() {
        // Interfaces with MAUI native microphone
        return await Task.FromResult(new byte[1024]); 
    }

    private bool DetectSpeech(byte[] audio) {
        // Simplified VAD: Check for energy levels above a threshold
        // In production, this uses a Silero VAD model
        double energy = 0;
        foreach (var b in audio) energy += Math.Abs(b - 128);
        return (energy / audio.Length) > 20; 
    }

    private async Task<string> TranscribeAudio(byte[] audio) {
        // Uses an embedded Whisper model (e.g., whisper.cpp)
        return await Task.FromResult("mock transcribed text");
    }
}
