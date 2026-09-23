using Vok.Domain.Models;
using Vok.Infrastructure.Services;
using Moq;
using Vok.Domain.Interfaces;
using Xunit;

namespace Vok.Tests;

/// <summary>Verifies prediction cache key and lifecycle behavior.</summary>
public class PredictionCacheServiceTests {
    [Fact]
    public void SetAndGetCache_ShouldBeCaseInsensitive() {
        var cache = new PredictionCacheService();
        var predictions = new List<string> { "water", "please" };

        cache.SetCache("I Want", predictions);

        Assert.Same(predictions, cache.GetCache("i want"));
    }

    [Fact]
    public void Clear_ShouldRemoveCachedPredictions() {
        var cache = new PredictionCacheService();
        cache.SetCache("hello", new List<string> { "world" });

        cache.Clear();

        Assert.Null(cache.GetCache("hello"));
    }
}

/// <summary>Verifies vocabulary backup round trips.</summary>
public class BackupServiceTests {
    [Fact]
    public async Task ExportAndImportBackup_ShouldRoundTripCategories() {
        var sourceVocabulary = new Mock<IVocabularyService>();
        var categories = new List<AacCategory> {
            new() { Key = "needs", DisplayName = "Needs", Tiles = new() { new AacTile { Id = "water", Label = "Water" } } }
        };
        sourceVocabulary.SetupGet(vocabulary => vocabulary.Categories).Returns(categories);
        var backupPath = Path.Combine(Path.GetTempPath(), $"aac-backup-{Guid.NewGuid():N}.json");

        try {
            await new BackupService(sourceVocabulary.Object).ExportBackupAsync(backupPath);
            var destinationVocabulary = new Mock<IVocabularyService>();
            await new BackupService(destinationVocabulary.Object).ImportBackupAsync(backupPath);

            destinationVocabulary.Verify(vocabulary => vocabulary.AddLearnedCategoryAsync(
                It.Is<AacCategory>(category => category.Key == "needs" && category.Tiles.Count == 1)), Times.Once);
        } finally {
            if (File.Exists(backupPath)) File.Delete(backupPath);
        }
    }
}

/// <summary>Verifies AI settings persistence.</summary>
public class AiSettingsTests {
    [Fact]
    public void Properties_ShouldReadAndWriteConfigurationValues() {
        var configuration = new Mock<Vok.Infrastructure.Services.IAppConfig>();
        configuration.Setup(config => config.GetValue("OPENAI_API_KEY", "")).Returns("openai-key");
        configuration.Setup(config => config.GetValue("AI_PREFERRED_PROVIDER", "Local")).Returns("OpenAI");
        var settings = new AiSettings(configuration.Object);

        Assert.Equal("openai-key", settings.OpenAIKey);
        Assert.Equal("OpenAI", settings.PreferredProvider);
        settings.ClaudeKey = "claude-key";
        settings.GoogleKey = "google-key";

        configuration.Verify(config => config.SetValue("CLAUDE_API_KEY", "claude-key"), Times.Once);
        configuration.Verify(config => config.SetValue("GOOGLE_API_KEY", "google-key"), Times.Once);
    }
}

/// <summary>Verifies voice settings persistence.</summary>
public class VoiceSettingsTests {
    [Fact]
    public void Properties_ShouldUseConfiguredValuesAndPersistChanges() {
        var configuration = new Mock<Vok.Infrastructure.Services.IAppConfig>();
        configuration.Setup(config => config.GetValue("ELEVENLABS_API_KEY", "")).Returns("voice-key");
        configuration.Setup(config => config.GetValue("PREFERRED_VOICE_ID", "21m00TcmSsS7S6SiaS6A")).Returns("voice-id");
        configuration.Setup(config => config.GetValue("USE_CLOUD_VOICE", "false")).Returns("true");
        var settings = new VoiceSettings(configuration.Object);

        Assert.Equal("voice-key", settings.ElevenLabsApiKey);
        Assert.Equal("voice-id", settings.PreferredVoiceId);
        Assert.True(settings.UseCloudVoice);
        settings.UseCloudVoice = false;
        settings.Persist();

        configuration.Verify(config => config.SetValue("USE_CLOUD_VOICE", "false"), Times.Once);
        configuration.Verify(config => config.Save(), Times.Once);
    }
}

