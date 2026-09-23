using CommunityToolkit.Maui.Views;

namespace Vok.Infrastructure.Services;

public static class AudioPlayer {
    public static async Task PlayAsync(string filePath) {
        var player = new MediaElement {
            Source = MediaSource.FromFile(filePath)
        };
        player.Play();
        await Task.CompletedTask;
    }
}
