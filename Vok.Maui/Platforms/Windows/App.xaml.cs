using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace Vok.Maui.WinUI;

public partial class App : MauiWinUIApplication {
    protected override MauiApp CreateMauiApp() {
        return MauiProgram.CreateMauiApp();
    }
}
