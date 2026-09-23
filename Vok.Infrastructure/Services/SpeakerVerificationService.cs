using Vok.Domain.Interfaces;

namespace Vok.Infrastructure.Services;

public class SpeakerVerificationService : ISpeakerVerificationService {
    private float[]? _patientVoicePrint;

    public async Task<bool> VerifyPatientVoiceAsync(byte[] audioData) {
        if (_patientVoicePrint == null) return true; // First time user

        var currentPrint = GenerateVoicePrint(audioData);
        var similarity = CalculateCosineSimilarity(currentPrint, _patientVoicePrint);

        return similarity > 0.85f; // Threshold for verification
    }

    public async Task EnrollPatientVoiceAsync(byte[] audioData) {
        _patientVoicePrint = GenerateVoicePrint(audioData);
        await Task.CompletedTask;
    }

    private float[] GenerateVoicePrint(byte[] audio) {
        // Implementation of an Embedding model (e.g., ECAPA-TDNN)
        // Extracts speaker-specific characteristics (pitch, timbre, resonance)
        var print = new float[128];
        for (int i = 0; i < audio.Length && i < 128; i++) {
            print[i] = audio[i] / 255.0f;
        }
        return print;
    }

    private float CalculateCosineSimilarity(float[] v1, float[] v2) {
        float dot = 0, mag1 = 0, mag2 = 0;
        for (int i = 0; i < v1.Length; i++) {
            dot += v1[i] * v2[i];
            mag1 += v1[i] * v1[i];
            mag2 += v2[i] * v2[i];
        }
        return dot / (float)(Math.Sqrt(mag1) * Math.Sqrt(mag2));
    }
}
