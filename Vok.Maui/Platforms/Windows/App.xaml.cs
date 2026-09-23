using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace Vok.Maui.WinUI;

/// <summary>Provides the Windows-specific MAUI application bootstrap.</summary>
public partial class App : MauiWinUIApplication {
    /// <summary>Creates the shared MAUI application.</summary>
    protected override MauiApp CreateMauiApp() {
        return MauiProgram.CreateMauiApp();
    }
}
