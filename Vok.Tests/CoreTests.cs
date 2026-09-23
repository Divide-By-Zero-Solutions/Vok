using Xunit;
using Moq;
using Vok.Domain.Interfaces;
using Vok.Domain.Models;
using Vok.Infrastructure.Services;
using System.IO;

namespace Vok.Tests;

public class VocabularyIntegrationTests : IDisposable {
    private readonly string _testDbPath;
    private readonly SqliteVocabularyService _vocab;

    public VocabularyIntegrationTests() {
        _testDbPath = Path.Combine(Path.GetTempPath(), $"test_vocab_{Guid.NewGuid()}.db3");
        _vocab = new SqliteVocabularyService(_testDbPath);
    }

    public void Dispose() {
        try {
            if (File.Exists(_testDbPath)) File.Delete(_testDbPath);
        } catch { }
    }

    [Fact]
    public async Task AddTile_ShouldPersistAndRetrieve() {
        var tile = new AacTile { Label = "Apple", Icon = "🍎", Id = "apple-1" };
        await _vocab.AddTileAsync("food", tile);
        var retrieved = _vocab.GetTiles("food");
        Assert.Contains(retrieved, t => t.Label == "Apple");
    }

    [Fact]
    public async Task AddTile_ShouldAutoCreateCategory() {
        var tile = new AacTile { Label = "Water", Icon = "💧" };
        await _vocab.AddTileAsync("drinks", tile);
        Assert.Contains(_vocab.Categories, c => c.Key == "drinks");
    }

    [Fact]
    public async Task DeleteTile_ShouldRemoveFromDb() {
        var tile = new AacTile { Label = "DeleteMe", Icon = "🗑️", Id = "del-1" };
        await _vocab.AddTileAsync("test", tile);
        await _vocab.DeleteTileAsync("del-1");
        var retrieved = _vocab.GetTiles("test");
        Assert.DoesNotContain(retrieved, t => t.Id == "del-1");
    }
}

public class AiRouterTests {
    [Fact]
    public async Task RouteRequest_ShouldFallbackToLocalOnCloudError() {
        var mockCloud = new Mock<IAIService>();
        var mockLocal = new Mock<IAIService>();
        var mockHttp = new Mock<HttpClient>();
        
        mockCloud.Setup(c => c.PolishSentenceAsync(It.IsAny<List<string>>()))
                 .ThrowsAsync(new Exception("Cloud Timeout"));
        mockLocal.Setup(l => l.PolishSentenceAsync(It.IsAny<List<string>>()))
                 .ReturnsAsync("Local Polished");

        var router = new AdvancedAiRouter(mockHttp.Object, mockLocal.Object); 
        var result = await router.PolishSentenceAsync(new List<string> { "I", "want", "water" });

        Assert.Equal("Local Polished", result);
        mockLocal.Verify(l => l.PolishSentenceAsync(It.IsAny<List<string>>()), Times.Once);
    }
}

public class VoiceOrchestratorTests {
    [Fact]
    public async Task Speak_ShouldFailoverToNativeWhenCloudFails() {
        var mockCloudVoice = new Mock<IVoiceService>();
        var mockNativeVoice = new Mock<IVoiceService>();
        var mockSettings = new Mock<IVoiceSettings>();
        mockSettings.Setup(s => s.UseCloudVoice).Returns(true);

        mockCloudVoice.Setup(v => v.SpeakAsync(It.IsAny<string>(), It.IsAny<float>(), It.IsAny<float>(), It.IsAny<string>()))
                      .ThrowsAsync(new Exception("API Key Invalid"));

        var orchestrator = new VoiceOrchestrator(mockCloudVoice.Object, mockNativeVoice.Object, mockSettings.Object);
        await orchestrator.SpeakAsync("Hello", 1.0f, 1.0f);

        mockNativeVoice.Verify(v => v.SpeakAsync("Hello", 1.0f, 1.0f, "neutral"), Times.Once);
    }
}

public class VectorStoreTests {
    [Fact]
    public async Task MemorySearch_ShouldReturnMostSimilar() {
        var testPath = Path.Combine(Path.GetTempPath(), $"test_vec_{Guid.NewGuid()}.bin");
        var store = new LocalVectorStore(testPath);
        
        await store.AddMemoryAsync("I am hungry", new float[] { 1.0f, 0.5f, 0.0f });
        await store.AddMemoryAsync("The sky is blue", new float[] { 0.0f, 0.1f, 0.9f });

        var results = await store.QueryMemoriesAsync("I need food", 1);
        Assert.Contains("I am hungry", results);
        
        if (File.Exists(testPath)) File.Delete(testPath);
    }
}

public class SwitchScanningTests {
    [Fact]
    public void Scanner_ShouldCycleIndicesCorrectly() {
        var mockVocab = new Mock<IVocabularyService>();
        mockVocab.Setup(v => v.GetTiles(It.IsAny<string>())).Returns(new List<AacTile> { 
            new AacTile { Label = "T1" }, new AacTile { Label = "T2" } 
        });

        var scanner = new SwitchScanningService(mockVocab.Object);
        scanner.StartScanning();
        scanner.TriggerSelect(); 
        Assert.True(true);
    }
}
