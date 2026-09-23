namespace Vok.Domain.Models;

public class UserProfile {
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = "Default User";
    public string Theme { get; set; } = "Dark";
    public int FontSize { get; set; } = 16;
    public bool HighContrast { get; set; } = false;
}

public enum TileType {
    Generic,
    Person,
    Action,
    Emotion,
    Need
}

public class AacTile {
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Label { get; set; } = string.Empty;
    public string Icon { get; set; } = "💬";
    public string? ImagePath { get; set; }
    public string? TargetCategory { get; set; }
    public bool IsLearned { get; set; }
    public bool IsQuickPhrase { get; set; } = false;
    public int UsageCount { get; set; } = 0;
    public TileType Type { get; set; } = TileType.Generic;
    public string? ContactInfo { get; set; }
}




public class AacCategory {
    public string Key { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public List<AacTile> Tiles { get; set; } = new();
}
