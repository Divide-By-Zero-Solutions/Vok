using System.Net.Http.Json;
using Vok.Domain.Interfaces;
using Vok.Domain.Models;

namespace Vok.Infrastructure.Services;

/// <summary>Routes AI requests to a remote provider with local fallback behavior.</summary>
public class AdvancedAiRouter : IAIService {
    private readonly HttpClient _http;
    private readonly IAIService _localAi;
    private readonly string _serverEndpoint = "http://localhost:11434/api/generate";
    
    public AdvancedAiRouter(HttpClient http, IAIService localAi) {
        _http = http;
        _localAi = localAi;
    }

    public async Task<List<string>> PredictNextWordsAsync(string currentPhrase, List<string> history) {
        // Use Local AI for speed in predictions
        return await _localAi.PredictNextWordsAsync(currentPhrase, history);
    }

    public async Task<string> PolishSentenceAsync(List<string> tokens) {
        // Use Server AI (Ollama) for high-quality polishing
        var prompt = $"Assemble these AAC tokens into a natural sentence: {string.Join(", ", tokens)}. Response only:";
        try {
            var result = await SendServerRequest(prompt);
            if (!string.IsNullOrEmpty(result)) return result;
        } catch { }
        
        // FALLBACK TO LOCAL AI
        return await _localAi.PolishSentenceAsync(tokens);
    }

    public async Task<string> AnalyzeSentimentAsync(string text) {
        // Simple sentiment analysis for voice modulation
        var prompt = $"Analyze sentiment of: '{text}'. Output ONE word: Happy, Sad, Angry, or Neutral.";
        return await SendServerRequest(prompt);
    }

    public async Task<AacCategory?> LearnCategoryAsync(string sentence, List<string> existingCategories) {
        var prompt = $"User spoke: {sentence}. Categories: {string.Join(",", existingCategories)}. Create new category JSON...";
        var json = await SendServerRequest(prompt);
        try { return System.Text.Json.JsonSerializer.Deserialize<AacCategory>(json); }
        catch { return null; }
    }

    public async Task LoadModelAsync(string path) => await _localAi.LoadModelAsync(path);

    private async Task<string> SendServerRequest(string prompt) {
        try {
            var content = JsonContent.Create(new { model = "qwen2.5:0.5b", prompt, stream = false });
            var res = await _http.PostAsync(_serverEndpoint, content);
            if (!res.IsSuccessStatusCode) return "";
            var data = await res.Content.ReadFromJsonAsync<OllamaResponse>();
            return data?.Response ?? "";
        } catch {
            return "";
        }
    }

    private record OllamaResponse(string Response);
}
