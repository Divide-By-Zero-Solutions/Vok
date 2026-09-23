using Vok.Domain.Interfaces;

namespace Vok.Infrastructure.Services;

public interface IContextService {
    string GetSuggestedCategory();
}

public class ContextService : IContextService {
    public string GetSuggestedCategory() {
        var hour = DateTime.Now.Hour;
        if (hour >= 5 && hour < 11) return "breakfast";
        if (hour >= 11 && hour < 15) return "lunch";
        if (hour >= 15 && hour < 21) return "dinner";
        return "needs";
    }
}
