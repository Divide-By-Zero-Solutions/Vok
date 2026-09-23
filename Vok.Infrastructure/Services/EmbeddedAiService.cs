using LLama.Common;
using LLama;
using Vok.Domain.Interfaces;
using Vok.Domain.Models;

namespace Vok.Infrastructure.Services;

/// <summary>Provides local embedded AI behavior without a remote provider.</summary>
public class EmbeddedAiService : IAIService {
    private LLamaContext? _context;
    private InteractiveExecutor? _executor;
    private readonly string _modelPath = "Models/tinyllama-1.1b-chat-v1.0.Q4_K_M.gguf";

    public async Task LoadModelAsync(string modelPath) {
        var path = string.IsNullOrEmpty(modelPath) ? _modelPath : modelPath;
        var parameters = new ModelParams(path) {
            ContextSize = 1024,
            GpuLayerCount = 20
        };
        using var weights = LLamaWeights.LoadFromFile(parameters);
        _context = weights.CreateContext(parameters);
        _executor = new InteractiveExecutor(_context);
    }

    public async Task<List<string>> PredictNextWordsAsync(string currentPhrase, List<string> history) {
        if (_executor == null) return new List<string>();
        var prompt = $"AAC. History: {string.Join("|", history)}. Current: {currentPhrase}. Next 3 words CSV:";
        string response = "";
        await foreach (var token in _executor.InferAsync(prompt, new InferenceParams { MaxTokens = 20, AntiPrompts = new[] { "\n" } })) {
            response += token;
        }
        return response.Split(',').Select(s => s.Trim()).ToList();
    }

    public async Task<string> PolishSentenceAsync(List<string> tokens) {
        if (_executor == null) return string.Join(" ", tokens);
        var prompt = $"Polish AAC tokens: {string.Join(", ", tokens)}. Response only:";
        string response = "";
        await foreach (var token in _executor.InferAsync(prompt, new InferenceParams { MaxTokens = 50 })) {
            response += token;
        }
        return response.Trim();
    }

    public async Task<AacCategory?> LearnCategoryAsync(string sentence, List<string> existingCategories) {
        if (_executor == null) return null;
        var prompt = $"Analyze sentence: {sentence}. Suggest a category key from {string.Join(",", existingCategories)} or a new one in JSON.";
        string response = "";
        await foreach (var token in _executor.InferAsync(prompt, new InferenceParams { MaxTokens = 100 })) {
            response += token;
        }
        try { return System.Text.Json.JsonSerializer.Deserialize<AacCategory>(response); }
        catch { return null; }
    }

    public async Task<string> AnalyzeSentimentAsync(string text) {
        if (_executor == null) return "neutral";
        var prompt = $"Sentiment of {text}? (positive/negative/neutral):";
        string response = "";
        await foreach (var token in _executor.InferAsync(prompt, new InferenceParams { MaxTokens = 10 })) {
            response += token;
        }
        return response.Trim().ToLower();
    }
}
