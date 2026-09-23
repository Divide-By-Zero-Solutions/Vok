# Operations and deployment

## Local development

1. Install the supported .NET SDKs and the .NET MAUI workload required by the target project.
2. Clone the parent repository and initialize the performance submodule with `git submodule update --init --recursive`.
3. Open `Vok.slnx` in Visual Studio or build with the appropriate target framework and platform.
4. Install the local Git hooks only when you want generated performance reports refreshed before commits.

## Android development and deployment

The MAUI application targets `net10.0-android` with Android API 30 as the minimum supported platform. Build or deploy the Android target from Visual Studio with an Android emulator/device selected, or use the Android workload tooling from the command line.

The Android manifest requests:

- `INTERNET` and `ACCESS_NETWORK_STATE` for configured AI, voice, download, and synchronization providers.
- `RECORD_AUDIO` for speech capture and speaker verification.
- `ACCESS_COARSE_LOCATION` and `ACCESS_FINE_LOCATION` for context-aware location services.
- `POST_NOTIFICATIONS` for notification delivery on Android versions that enforce it.

Microphone, location, and notification access are runtime permissions on applicable Android versions. Production UI flows must handle denial and provide a degraded offline experience where possible. No cleartext HTTP traffic is permitted by the application metadata.

## Runtime data

The application stores user data in platform application-data locations. SQLite, JSON configuration, audio assets, and backups are not source-controlled. Treat backup files and provider credentials as sensitive data.

## CI/CD

GitHub Actions restores dependencies, builds the solution, runs tests and coverage, validates documentation, and optionally executes performance workflows. The performance submodule is checked out as part of repository checkout. Release packaging is platform-specific and must be performed on a runner with the required MAUI workload and signing configuration.

## Deployment boundaries

- **Client:** Windows .NET MAUI application and embedded Blazor UI.
- **Local services:** SQLite, filesystem, preferences, audio, location, hardware feedback, and scanning adapters.
- **Remote services:** Optional cloud AI, speech, synchronization, download, and voice providers over HTTPS.
- **Build services:** GitHub Actions artifacts for test results, coverage, generated XML documentation, and benchmark reports.
