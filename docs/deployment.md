# Operations and deployment

## Local development

1. Install the supported .NET SDKs and the .NET MAUI workload required by the target project.
2. Clone the parent repository and initialize the performance submodule with `git submodule update --init --recursive`.
3. Open `Vok.slnx` in Visual Studio or build with the appropriate target framework and platform.
4. Install the local Git hooks only when you want generated performance reports refreshed before commits.

## Runtime data

The application stores user data in platform application-data locations. SQLite, JSON configuration, audio assets, and backups are not source-controlled. Treat backup files and provider credentials as sensitive data.

## CI/CD

GitHub Actions restores dependencies, builds the solution, runs tests and coverage, validates documentation, and optionally executes performance workflows. The performance submodule is checked out as part of repository checkout. Release packaging is platform-specific and must be performed on a runner with the required MAUI workload and signing configuration.

## Deployment boundaries

- **Client:** Windows .NET MAUI application and embedded Blazor UI.
- **Local services:** SQLite, filesystem, preferences, audio, location, hardware feedback, and scanning adapters.
- **Remote services:** Optional cloud AI, speech, synchronization, download, and voice providers over HTTPS.
- **Build services:** GitHub Actions artifacts for test results, coverage, generated XML documentation, and benchmark reports.
