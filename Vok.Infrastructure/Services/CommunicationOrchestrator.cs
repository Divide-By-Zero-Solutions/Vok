using Vok.Domain.Interfaces;

namespace Vok.Infrastructure.Services;

/// <summary>Coordinates capture, verification, vocabulary, notifications, and session state.</summary>
public class CommunicationOrchestrator : ICommunicationOrchestrator {
    private readonly IVoiceCaptureService _capture;
    private readonly ISpeakerVerificationService _verification;
    private readonly IVocabularyService _vocab;
    private readonly INotificationService _notifications;
    private bool _isPatientActive;

    public bool IsPatientActive => _isPatientActive;

    public CommunicationOrchestrator(IVoiceCaptureService capture, ISpeakerVerificationService verification, IVocabularyService vocab, INotificationService notifications) {
        _capture = capture;
        _verification = verification;
        _vocab = vocab;
        _notifications = notifications;
    }

    public async Task BeginChatSessionAsync() {
        await _notifications.ShowNotificationAsync("AAC Active", "Listening for your voice...");
        await _capture.StartListeningAsync(async (text) => {
            var audio = new byte[1024]; 
            if (await _verification.VerifyPatientVoiceAsync(audio)) {
                _isPatientActive = true;
                await _notifications.ShowNotificationAsync("Voice Verified", $"Recognized: {text}");
            } else {
                _isPatientActive = false;
            }
        });
    }

    public async Task EndChatSessionAsync() {
        await _capture.StopListeningAsync();
        _isPatientActive = false;
        await _notifications.ShowNotificationAsync("AAC Paused", "Recording stopped.");
    }
}
