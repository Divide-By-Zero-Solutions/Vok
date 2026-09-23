namespace Vok.Maui;

/// <summary>Defines the MAUI application root.</summary>
public partial class App : Application {
    /// <summary>Initializes the application root.</summary>
    public App() {
        InitializeComponent();
    }

    /// <summary>Creates the application shell window.</summary>
    protected override Window CreateWindow(IActivationState? activationState) {
        return new Window(new AppShell());
    }
}
