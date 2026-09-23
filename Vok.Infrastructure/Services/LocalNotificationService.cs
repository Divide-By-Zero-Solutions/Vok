using Vok.Domain.Interfaces;

namespace Vok.Infrastructure.Services;

/// <summary>Implements notifications using local platform facilities.</summary>
public class LocalNotificationService : INotificationService {
    public async Task ShowNotificationAsync(string title, string message) {
        // In a real MAUI app, this uses Plugin.LocalNotification or native APIs
        // Example: LocalNotificationCenter.Current.Show(new NotificationRequest { ... });
        Console.WriteLine($"[NOTIFICATION] {title}: {message}");
        await Task.CompletedTask;
    }

    public void SetNotificationChannel(string channelId, string name, string description) {
        // Android specific channel setup
        Console.WriteLine($"Setting up channel {channelId}: {name}");
    }
}
