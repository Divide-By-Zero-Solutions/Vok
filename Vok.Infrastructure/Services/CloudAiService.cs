using System.Net.Http.Json;
using Vok.Domain.Interfaces;

namespace Vok.Infrastructure.Services;

/// <summary>Executes HTTP requests against the configured cloud AI provider.</summary>
public class CloudAiService {
    private readonly HttpClient _http;
    private readonly IAiSettings _settings;

    public CloudAiService(HttpClient http, IAiSettings settings) {
        _http = http;
        _settings = settings;
    }

    public async Task<string> QueryProviderAsync(string prompt) {
        return _settings.PreferredProvider switch {
            "OpenAI" => await QueryOpenAI(prompt),
            "Claude" => await QueryClaude(prompt),
            "Google" => await QueryGoogle(prompt),
            _ => throw new NotSupportedException("Provider not supported or set to Local")
        };
    }

    private async Task<string> QueryOpenAI(string prompt) {
        var request = new { model = "gpt-3.5-turbo", messages = new[] { new { role = "user", content = prompt } } };
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
        httpRequest.Headers.Add("Authorization", $"Bearer {_settings.OpenAIKey}");
        httpRequest.Content = JsonContent.Create(request);
        var res = await _http.SendAsync(httpRequest);
        var data = await res.Content.ReadFromJsonAsync<dynamic>(); 
        return data?.choices[0].message.content ?? "";
    }

    private async Task<string> QueryClaude(string prompt) {
        var request = new { model = "claude-3-haiku-20240307", max_tokens = 100, messages = new[] { new { role = "user", content = prompt } } };
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://api.anthropic.com/v1/messages");
        httpRequest.Headers.Add("x-api-key", _settings.ClaudeKey);
        httpRequest.Headers.Add("anthropic-version", "2023-06-01");
        httpRequest.Content = JsonContent.Create(request);
        var res = await _http.SendAsync(httpRequest);
        var data = await res.Content.ReadFromJsonAsync<dynamic>();
        return data?.content[0].text ?? "";
    }

    private async Task<string> QueryGoogle(string prompt) {
        var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent?key={_settings.GoogleKey}";
        var request = new { contents = new[] { new { parts = new[] { new { text = prompt } } } } };
        var res = await _http.PostAsJsonAsync(url, request);
        var data = await res.Content.ReadFromJsonAsync<dynamic>();
        return data?.candidates[0].content.parts[0].text ?? "";
    }
}
