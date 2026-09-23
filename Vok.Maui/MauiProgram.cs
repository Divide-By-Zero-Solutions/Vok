using Microsoft.Extensions.Logging;
using Vok.Domain.Interfaces;
using Vok.Infrastructure.Services;
using CommunityToolkit.Maui;
using Microsoft.AspNetCore.Components.WebView.Maui;

namespace Vok.Maui;

/// <summary>Builds the MAUI application and registers runtime services.</summary>
public static class MauiProgram {
    /// <summary>Creates and configures the MAUI application.</summary>
    public static MauiApp CreateMauiApp() {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiCommunityToolkitMediaElement(false);

        builder.Services.AddMauiBlazorWebView();
        
        builder.ConfigureFonts(fonts => {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });

        // SOLID: Dependency Inversion Registrations
        builder.Services.AddSingleton<IVocabularyService, SqliteVocabularyService>();
        builder.Services.AddSingleton<IContextService, ContextService>();
        builder.Services.AddSingleton<ILocationService, MauiLocationService>();
        builder.Services.AddSingleton<IVectorStore, LocalVectorStore>();
        builder.Services.AddSingleton<ISyncService, SecureSyncService>();
        builder.Services.AddSingleton<IPredictionCache, PredictionCacheService>();
        builder.Services.AddSingleton<IDownloadService, DownloadService>();
        builder.Services.AddSingleton<IBackupService, BackupService>();
        builder.Services.AddSingleton<IHardwareFeedbackService, MauiHardwareFeedbackService>();
        builder.Services.AddSingleton<IVoiceCaptureService, VoiceCaptureService>();
        builder.Services.AddSingleton<ISwitchControlService, SwitchScanningService>();
        builder.Services.AddSingleton<IProfileService, LocalProfileService>();
        builder.Services.AddSingleton<IVoiceSettings, VoiceSettings>();
        
        builder.Services.AddSingleton<EmbeddedAiService>();
        builder.Services.AddSingleton<EmbeddedVoiceService>();
        builder.Services.AddHttpClient<ElevenLabsVoiceService>();
        builder.Services.AddSingleton<IVoiceService, VoiceOrchestrator>();
        builder.Services.AddSingleton<IAIService, AdvancedAiRouter>();

        return builder.Build();
    }
}
