using Vok.Domain.Interfaces;
using Vok.Domain.Models;

namespace Vok.Infrastructure.Services;

/// <summary>Maps feedback outcomes to MAUI device haptics.</summary>
public class MauiHardwareFeedbackService : IHardwareFeedbackService {
    public void TriggerSuccessHaptic() => HapticFeedback.Default.Perform(HapticFeedbackType.Click);
    public void TriggerErrorHaptic() => HapticFeedback.Default.Perform(HapticFeedbackType.LongPress);
    public void TriggerWarningHaptic() => HapticFeedback.Default.Perform(HapticFeedbackType.Click);
}
