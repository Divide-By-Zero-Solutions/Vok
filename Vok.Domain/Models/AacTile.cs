namespace Vok.Domain.Models;

/// <summary>Represents user-specific presentation and accessibility preferences.</summary>
public class UserProfile {
    /// <summary>Gets or sets the stable profile identifier.</summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();
    /// <summary>Gets or sets the profile display name.</summary>
    public string Name { get; set; } = "Default User";
    /// <summary>Gets or sets the selected visual theme.</summary>
    public string Theme { get; set; } = "Dark";
    /// <summary>Gets or sets the preferred text size.</summary>
    public int FontSize { get; set; } = 16;
    /// <summary>Gets or sets whether high-contrast presentation is enabled.</summary>
    public bool HighContrast { get; set; } = false;
}

/// <summary>Classifies the communication intent of an AAC tile.</summary>
public enum TileType {
    /// <summary>A general-purpose tile.</summary>
    Generic,
    /// <summary>A person or contact tile.</summary>
    Person,
    /// <summary>An action tile.</summary>
    Action,
    /// <summary>An emotion tile.</summary>
    Emotion,
    /// <summary>A need or request tile.</summary>
    Need
}

/// <summary>Represents one selectable AAC communication tile.</summary>
public class AacTile {
    /// <summary>Gets or sets the stable tile identifier.</summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();
    /// <summary>Gets or sets the text displayed and spoken for the tile.</summary>
    public string Label { get; set; } = string.Empty;
    /// <summary>Gets or sets the fallback icon displayed for the tile.</summary>
    public string Icon { get; set; } = "💬";
    /// <summary>Gets or sets an optional image path.</summary>
    public string? ImagePath { get; set; }
    /// <summary>Gets or sets the category opened when this tile is selected.</summary>
    public string? TargetCategory { get; set; }
    /// <summary>Gets or sets whether the tile has been learned by the user.</summary>
    public bool IsLearned { get; set; }
    /// <summary>Gets or sets whether the tile belongs to the quick-phrase set.</summary>
    public bool IsQuickPhrase { get; set; } = false;
    /// <summary>Gets or sets the number of observed uses.</summary>
    public int UsageCount { get; set; } = 0;
    /// <summary>Gets or sets the semantic tile classification.</summary>
    public TileType Type { get; set; } = TileType.Generic;
    /// <summary>Gets or sets optional contact metadata for person tiles.</summary>
    public string? ContactInfo { get; set; }
}




/// <summary>Groups related AAC tiles under a stable category key.</summary>
public class AacCategory {
    /// <summary>Gets or sets the stable category key.</summary>
    public string Key { get; set; } = string.Empty;
    /// <summary>Gets or sets the localized or user-facing category name.</summary>
    public string DisplayName { get; set; } = string.Empty;
    /// <summary>Gets or sets the tiles contained in the category.</summary>
    public List<AacTile> Tiles { get; set; } = new();
}
