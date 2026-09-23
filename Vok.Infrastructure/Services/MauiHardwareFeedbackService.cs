using Vok.Domain.Interfaces;
using Vok.Domain.Models;

namespace Vok.Infrastructure.Services;

public class MauiHardwareFeedbackService : IHardwareFeedbackService {
    public void TriggerSuccessHaptic() => HapticFeedback.Default.Perform(HapticFeedbackType.Click);
    public void TriggerErrorHaptic() => HapticFeedback.Default.Perform(HapticFeedbackType.LongPress);
    public void TriggerWarningHaptic() => HapticFeedback.Default.Perform(HapticFeedbackType.Click);
}
