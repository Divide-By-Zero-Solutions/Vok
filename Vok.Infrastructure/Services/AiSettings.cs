using Vok.Domain.Interfaces;

namespace Vok.Infrastructure.Services;

public class AiSettings : IAiSettings {
    private readonly IAppConfig _config;
    public AiSettings(IAppConfig config) => _config = config;

    public string OpenAIKey { 
        get => _config.GetValue("OPENAI_API_KEY", ""); 
        set => _config.SetValue("OPENAI_API_KEY", value); 
    }
    public string ClaudeKey { 
        get => _config.GetValue("CLAUDE_API_KEY", ""); 
        set => _config.SetValue("CLAUDE_API_KEY", value); 
    }
    public string GoogleKey { 
        get => _config.GetValue("GOOGLE_API_KEY", ""); 
        set => _config.SetValue("GOOGLE_API_KEY", value); 
    }
    public string PreferredProvider { 
        get => _config.GetValue("AI_PREFERRED_PROVIDER", "Local"); 
        set => _config.SetValue("AI_PREFERRED_PROVIDER", value); 
    }
}
