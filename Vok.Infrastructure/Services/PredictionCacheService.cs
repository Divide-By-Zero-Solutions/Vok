using Vok.Domain.Interfaces;

namespace Vok.Infrastructure.Services;

/// <summary>Caches prediction lists by phrase key.</summary>
public class PredictionCacheService : IPredictionCache {
    private readonly Dictionary<string, List<string>> _cache = new();

    public void SetCache(string phrase, List<string> predictions) {
        _cache[phrase.ToLower()] = predictions;
    }

    public List<string>? GetCache(string phrase) {
        return _cache.TryGetValue(phrase.ToLower(), out var result) ? result : null;
    }

    public void Clear() => _cache.Clear();
}
