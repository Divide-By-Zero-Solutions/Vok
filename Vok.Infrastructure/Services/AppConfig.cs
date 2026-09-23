using System.Text.Json;

namespace Vok.Infrastructure.Services;

public interface IAppConfig {
    string GetValue(string key, string defaultValue = "");
    void SetValue(string key, string value);
    void Save();
}

public class AppConfig : IAppConfig {
    private readonly string _path = Path.Combine(FileSystem.AppDataDirectory, "config.json");
    private Dictionary<string, string> _settings = new();

    public AppConfig() {
        if (File.Exists(_path)) {
            try {
                var json = File.ReadAllText(_path);
                _settings = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new();
            } catch {
                _settings = new();
            }
        }
    }

    public string GetValue(string key, string defaultValue = "") {
        // Check environment variables first (useful for opencode/CLI)
        var envVal = Environment.GetEnvironmentVariable(key.ToUpper());
        if (!string.IsNullOrEmpty(envVal)) return envVal;

        return _settings.TryGetValue(key, out var value) ? value : defaultValue;
    }

    public void SetValue(string key, string value) {
        _settings[key] = value;
    }

    public void Save() {
        var json = JsonSerializer.Serialize(_settings, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_path, json);
    }
}
