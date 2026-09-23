using Vok.Domain.Interfaces;
using Vok.Domain.Models;
using System.Text.Json;

namespace Vok.Infrastructure.Services;

/// <summary>Provides profile selection and persistence operations.</summary>
public interface IProfileService {
    UserProfile CurrentProfile { get; }
    void SetProfile(UserProfile profile);
    List<UserProfile> GetAllProfiles();
    void CreateProfile(string name);
}

/// <summary>Persists user profiles as local application data.</summary>
public class LocalProfileService : IProfileService {
    private readonly string _path = Path.Combine(FileSystem.AppDataDirectory, "profiles.json");
    private List<UserProfile> _profiles = new();
    public UserProfile CurrentProfile { get; private set; } = new();

    public LocalProfileService() {
        if (File.Exists(_path)) {
            _profiles = JsonSerializer.Deserialize<List<UserProfile>>(File.ReadAllText(_path)) ?? new();
            CurrentProfile = _profiles.FirstOrDefault() ?? new UserProfile();
        } else {
            var defaultProfile = new UserProfile { Name = "Default User" };
            _profiles.Add(defaultProfile);
            CurrentProfile = defaultProfile;
            Save();
        }
    }

    public void SetProfile(UserProfile profile) {
        CurrentProfile = profile;
        Save();
    }

    public List<UserProfile> GetAllProfiles() => _profiles;

    public void CreateProfile(string name) {
        var profile = new UserProfile { Name = name };
        _profiles.Add(profile);
        CurrentProfile = profile;
        Save();
    }

    private void Save() => File.WriteAllText(_path, JsonSerializer.Serialize(_profiles));
}
