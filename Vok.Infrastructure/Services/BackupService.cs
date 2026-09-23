using System.Text.Json;
using Vok.Domain.Interfaces;
using Vok.Domain.Models;

namespace Vok.Infrastructure.Services;

/// <summary>Exports and imports vocabulary backups.</summary>
public interface IBackupService {
    Task ExportBackupAsync(string filePath);
    Task ImportBackupAsync(string filePath);
}

/// <summary>Serializes vocabulary data to and from JSON backup files.</summary>
public class BackupService : IBackupService {
    private readonly IVocabularyService _vocab;

    public BackupService(IVocabularyService vocab) => _vocab = vocab;

    public async Task ExportBackupAsync(string filePath) {
        var data = _vocab.Categories;
        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(filePath, json);
    }

    public async Task ImportBackupAsync(string filePath) {
        var json = await File.ReadAllTextAsync(filePath);
        var data = JsonSerializer.Deserialize<List<AacCategory>>(json);
        if (data != null) {
            foreach(var cat in data) {
                await _vocab.AddLearnedCategoryAsync(cat);
            }
        }
    }
}
