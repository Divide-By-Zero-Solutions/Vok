using Vok.Domain.Interfaces;
using Vok.Domain.Models;
using System.Text.Json;

namespace Vok.Infrastructure.Services;

public class LocalVocabularyService : IVocabularyService {
    public List<AacCategory> Categories { get; private set; } = new();

    public LocalVocabularyService() {
        Load();
    }

    public List<AacTile> GetTiles(string categoryKey) => 
        Categories.FirstOrDefault(c => c.Key == categoryKey)?.Tiles ?? new List<AacTile>();

    public async Task AddLearnedCategoryAsync(AacCategory category) {
        Categories.RemoveAll(c => c.Key == category.Key);
        Categories.Add(category);
        Save();
        await Task.CompletedTask;
    }

    public async Task AddTileAsync(string categoryKey, AacTile tile) {
        var cat = Categories.FirstOrDefault(c => c.Key == categoryKey);
        if (cat == null) {
            cat = new AacCategory { Key = categoryKey, DisplayName = categoryKey };
            Categories.Add(cat);
        }
        cat.Tiles.Add(tile);
        Save();
        await Task.CompletedTask;
    }

    public async Task DeleteTileAsync(string tileId) {
        foreach(var cat in Categories) cat.Tiles.RemoveAll(t => t.Id == tileId);
        Save();
        await Task.CompletedTask;
    }

    public void Save() => 
        Preferences.Default.Set("aac_vocab", JsonSerializer.Serialize(Categories));

    private void Load() {
        var json = Preferences.Default.Get("aac_vocab", string.Empty);
        Categories = string.IsNullOrEmpty(json) ? GetDefaults() : JsonSerializer.Deserialize<List<AacCategory>>(json)!;
    }

    private List<AacCategory> GetDefaults() => new() {
        new AacCategory { Key = "needs", DisplayName = "Needs", Tiles = new() { new AacTile { Label = "Water", Icon = "💧" } } }
    };
}
