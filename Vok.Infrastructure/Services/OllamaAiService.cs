using System.Net.Http.Json;
using Vok.Domain.Interfaces;
using Vok.Domain.Models;

namespace Vok.Infrastructure.Services;

public class OllamaAiService : IAIService {
    private readonly HttpClient _http;
    private readonly string _endpoint = "http://localhost:11434/api/generate";
    private readonly string _model = "qwen2.5:0.5b";

    public OllamaAiService(HttpClient http) => _http = http;

    public async Task<List<string>> PredictNextWordsAsync(string currentPhrase, List<string> history) {
        var prompt = $"AAC Assistant. History: {string.Join("|", history)}. Current: {currentPhrase}. Output 3 next words as CSV.";
        var response = await SendRequest(prompt);
        return response.Split(',').Select(s => s.Trim()).ToList();
    }

    public async Task<string> PolishSentenceAsync(List<string> tokens) {
        var prompt = $"Clean these AAC tokens into a sentence: {string.Join(", ", tokens)}. Respond ONLY with the sentence.";
        return await SendRequest(prompt);
    }

    public async Task<AacCategory?> LearnCategoryAsync(string sentence, List<string> existingCategories) {
        var prompt = $"User spoke: {sentence}. Categories: {string.Join(",", existingCategories)}. Create new category JSON if needed...";
        var json = await SendRequest(prompt);
        try { return System.Text.Json.JsonSerializer.Deserialize<AacCategory>(json); }
        catch { return null; }
    }

    public async Task<string> AnalyzeSentimentAsync(string text) {
        var prompt = $"Analyze sentiment of: {text}. Respond only with one word: positive, negative, or neutral.";
        return await SendRequest(prompt);
    }

    public async Task LoadModelAsync(string modelPath) {
        await Task.CompletedTask; // Ollama handles model loading internally
    }

    private async Task<string> SendRequest(string prompt) {
        var content = JsonContent.Create(new { model = _model, prompt, stream = false });
        var res = await _http.PostAsync(_endpoint, content);
        var data = await res.Content.ReadFromJsonAsync<OllamaResponse>();
        return data?.Response ?? "";
    }

    private record OllamaResponse(string Response);
}
