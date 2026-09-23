using System.Net.Http;
using Vok.Domain.Interfaces;

namespace Vok.Infrastructure.Services;

/// <summary>Downloads and tracks local application assets.</summary>
public class DownloadService : IDownloadService {
    private readonly HttpClient _http;
    private readonly string _storagePath = FileSystem.AppDataDirectory;

    public DownloadService(HttpClient http) => _http = http;

    public async Task<IEnumerable<DownloadableAsset>> GetAvailableAssetsAsync() {
        // In a production app, this would fetch from a JSON manifest on a server.
        return new List<DownloadableAsset> {
            new DownloadableAsset("phi3-mini", "Phi-3 Mini (Fast)", "https://huggingface.co/models/phi3.gguf", "AI Model", 2300000000),
            new DownloadableAsset("tinyllama", "TinyLlama (Ultra Fast)", "https://huggingface.co/models/tinyllama.gguf", "AI Model", 600000000),
            new DownloadableAsset("voice-en-us-1", "Neural Voice: Sarah", "https://voices.aac/sarah.onnx", "Voice", 50000000),
            new DownloadableAsset("voice-en-gb-1", "Neural Voice: James", "https://voices.aac/james.onnx", "Voice", 50000000)
        };
    }

    public async Task DownloadAssetAsync(string assetId, Action<double> progressCallback) {
        var assets = await GetAvailableAssetsAsync();
        var asset = assets.FirstOrDefault(a => a.Id == assetId);
        if (asset == null) throw new Exception("Asset not found");

        var filePath = Path.Combine(_storagePath, $"{assetId}.bin");
        using var response = await _http.GetAsync(asset.Url, HttpCompletionOption.ResponseHeadersRead);
        var totalBytes = response.Content.Headers.ContentLength ?? asset.SizeBytes;

        using var stream = await response.Content.ReadAsStreamAsync();
        using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);

        var buffer = new byte[8192];
        long totalRead = 0;
        int read;

        while ((read = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0) {
            await fileStream.WriteAsync(buffer, 0, read);
            totalRead += read;
            progressCallback((double)totalRead / totalBytes);
        }
    }

    public bool IsAssetDownloaded(string assetId) {
        return File.Exists(Path.Combine(_storagePath, $"{assetId}.bin"));
    }
}
