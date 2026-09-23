using SQLite;
using Vok.Domain.Interfaces;
using Vok.Domain.Models;

namespace Vok.Infrastructure.Services;

/// <summary>Persists AAC categories and tiles in a local SQLite database.</summary>
public class SqliteVocabularyService : IVocabularyService {
    private SQLiteAsyncConnection _db;
    private List<AacCategory> _cachedCategories = new();

    public List<AacCategory> Categories => _cachedCategories;

    public SqliteVocabularyService(string? dbPath = null) {
        dbPath ??= Path.Combine(FileSystem.AppDataDirectory, "aac_vocab.db3");
        _db = new SQLiteAsyncConnection(dbPath);
        InitializeDb().Wait();
    }

    private async Task InitializeDb() {
        await _db.CreateTableAsync<DbTile>();
        await _db.CreateTableAsync<DbCategory>();
        await LoadCategories();
    }

    private async Task LoadCategories() {
        var categories = await _db.Table<DbCategory>().ToListAsync();
        var tiles = await _db.Table<DbTile>().ToListAsync();

        _cachedCategories = categories.Select(c => new AacCategory {
            Key = c.Key,
            DisplayName = c.DisplayName,
            Tiles = tiles.Where(t => t.CategoryKey == c.Key).Select(t => new AacTile {
                Id = t.Id,
                Label = t.Label,
                Icon = t.Icon,
                TargetCategory = t.TargetCategory,
                IsLearned = t.IsLearned
            }).ToList()
        }).ToList();
    }

    public List<AacTile> GetTiles(string categoryKey) => 
        _cachedCategories.FirstOrDefault(c => c.Key == categoryKey)?.Tiles ?? new List<AacTile>();

    public async Task AddLearnedCategoryAsync(AacCategory category) {
        await _db.InsertAsync(new DbCategory { Key = category.Key, DisplayName = category.DisplayName });
        foreach(var tile in category.Tiles) {
            await _db.InsertAsync(new DbTile { 
                Id = tile.Id, Label = tile.Label, Icon = tile.Icon, 
                ImagePath = tile.ImagePath,
                CategoryKey = category.Key, TargetCategory = tile.TargetCategory, IsLearned = tile.IsLearned 
            });
        }
        await LoadCategories();
    }

    public async Task AddTileAsync(string categoryKey, AacTile tile) {
        var cat = await _db.Table<DbCategory>().Where(c => c.Key == categoryKey).FirstOrDefaultAsync();
        if (cat == null) {
            await _db.InsertAsync(new DbCategory { Key = categoryKey, DisplayName = categoryKey });
        }
        await _db.InsertAsync(new DbTile { 
            Id = tile.Id, Label = tile.Label, Icon = tile.Icon, 
            ImagePath = tile.ImagePath,
            CategoryKey = categoryKey, TargetCategory = tile.TargetCategory, IsLearned = tile.IsLearned 
        });
        await LoadCategories();
    }

    public async Task DeleteTileAsync(string tileId) {
        await _db.DeleteAsync<DbTile>(tileId);
        await LoadCategories();
    }

    public void Save() { /* SQLite is auto-committing */ }

    private class DbCategory {
        [PrimaryKey] public string Key { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
    }

    private class DbTile {
        [PrimaryKey] public string Id { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string? ImagePath { get; set; }
        public string CategoryKey { get; set; } = string.Empty;
        public string? TargetCategory { get; set; }
        public bool IsLearned { get; set; }
        public int UsageCount { get; set; }
    }

}
