using Vok.Domain.Interfaces;
using System.Numerics;

namespace Vok.Infrastructure.Services;

public class LocalVectorStore : IVectorStore {
    private readonly string _storagePath;
    private List<MemoryEntry> _memories = new();

    public LocalVectorStore(string? storagePath = null) {
        _storagePath = storagePath ?? Path.Combine(FileSystem.AppDataDirectory, "memories.json");
        if (File.Exists(_storagePath)) {
            var json = File.ReadAllText(_storagePath);
            _memories = System.Text.Json.JsonSerializer.Deserialize<List<MemoryEntry>>(json) ?? new();
        }
    }

    public async Task AddMemoryAsync(string text, float[] embedding) {
        _memories.Add(new MemoryEntry { Text = text, Embedding = embedding });
        await File.WriteAllTextAsync(_storagePath, System.Text.Json.JsonSerializer.Serialize(_memories));
    }

    public async Task<List<string>> QueryMemoriesAsync(string query, int topK = 3) {
        float[] queryEmbedding = GenerateEmbedding(query);

        var results = _memories
            .Select(m => new { m.Text, Score = CosineSimilarity(queryEmbedding, m.Embedding) })
            .OrderByDescending(r => r.Score)
            .Take(topK)
            .Select(r => r.Text)
            .ToList();

        return await Task.FromResult(results);
    }

    private float CosineSimilarity(float[] V1, float[] V2) {
        if (V1.Length != V2.Length) return 0;
        float dot = 0, mag1 = 0, mag2 = 0;
        for (int i = 0; i < V1.Length; i++) {
            dot += V1[i] * V2[i];
            mag1 += V1[i] * V1[i];
            mag2 += V2[i] * V2[i];
        }
        return dot / (float)(Math.Sqrt(mag1) * Math.Sqrt(mag2));
    }

    private float[] GenerateEmbedding(string text) {
        float[] vector = new float[128];
        for (int i = 0; i < text.Length; i++) {
            int index = (text[i] * i) % 128;
            vector[index] += 1.0f;
        }
        
        float mag = (float)Math.Sqrt(vector.Sum(x => x * x));
        if (mag > 0) {
            for (int i = 0; i < 128; i++) vector[i] /= mag;
        }
        return vector;
    }

    private class MemoryEntry {
        public string Text { get; set; } = "";
        public float[] Embedding { get; set; } = Array.Empty<float>();
    }
}
