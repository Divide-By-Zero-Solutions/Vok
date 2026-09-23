using Vok.Domain.Interfaces;

namespace Vok.Infrastructure.Services;

/// <summary>Provides context-derived vocabulary suggestions.</summary>
public interface IContextService {
    string GetSuggestedCategory();
}

/// <summary>Provides the current application context category.</summary>
public class ContextService : IContextService {
    public string GetSuggestedCategory() {
        var hour = DateTime.Now.Hour;
        if (hour >= 5 && hour < 11) return "breakfast";
        if (hour >= 11 && hour < 15) return "lunch";
        if (hour >= 15 && hour < 21) return "dinner";
        return "needs";
    }
}
