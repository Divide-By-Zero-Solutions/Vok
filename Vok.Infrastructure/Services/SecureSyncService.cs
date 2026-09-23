using System.Net.Http.Json;
using Vok.Domain.Interfaces;
using Vok.Domain.Models;

namespace Vok.Infrastructure.Services;

public class SecureSyncService : ISyncService {
    private readonly IVocabularyService _vocab;
    private readonly HttpClient _http;
    private readonly string _syncEndpoint = "https://api.Vok.cloud/sync";

    public SecureSyncService(IVocabularyService vocab, HttpClient http) {
        _vocab = vocab;
        _http = http;
    }

    public async Task SyncDataAsync() {
        try {
            var response = await _http.PostAsJsonAsync(_syncEndpoint, new { deviceId = "deviceId123" });
            if (response.IsSuccessStatusCode) {
                var cloudData = await response.Content.ReadFromJsonAsync<List<AacCategory>>();
                if (cloudData != null) {
                    foreach(var cat in cloudData) await _vocab.AddLearnedCategoryAsync(cat);
                }
            }
        } catch {
            // Silent fail: app continues to work offline
        }
    }

    public async Task ExportToCloudAsync() {
        try {
            await _http.PostAsJsonAsync(_syncEndpoint + "/upload", _vocab.Categories);
        } catch { }
    }

    public async Task ImportFromCloudAsync() {
        try {
            var data = await _http.GetFromJsonAsync<List<AacCategory>>(_syncEndpoint + "/download");
            if (data != null) {
                foreach(var cat in data) await _vocab.AddLearnedCategoryAsync(cat);
            }
        } catch { }
    }
}
