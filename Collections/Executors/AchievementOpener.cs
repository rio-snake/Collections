using FFXIVClientStructs.FFXIV.Client.UI.Agent;

namespace Collections;

public class AchievementOpener 
{
    public static unsafe void OpenAchievementByAchievementId(uint achievementId)
    {
        AgentAchievement.Instance()->Show();
        AgentAchievement.Instance()->OpenById(achievementId);
    }

    // Helper to 
    public static unsafe bool IsComplete(int achievementId)
    {

        var ach = FFXIVClientStructs.FFXIV.Client.Game.UI.Achievement.Instance();
        if (!ach->IsLoaded()) ach->RequestAchievementProgress((uint)achievementId);
        return ach->IsComplete(achievementId);
    }
}