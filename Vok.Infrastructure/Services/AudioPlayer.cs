using CommunityToolkit.Maui.Views;

namespace Vok.Infrastructure.Services;

/// <summary>Provides shared audio-file playback operations.</summary>
public static class AudioPlayer {
    public static async Task PlayAsync(string filePath) {
        var player = new MediaElement {
            Source = MediaSource.FromFile(filePath)
        };
        player.Play();
        await Task.CompletedTask;
    }
}
