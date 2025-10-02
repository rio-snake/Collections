using FFXIVClientStructs.FFXIV.Client.UI.Agent;

namespace Collections;

public class AchievementOpener 
{
    public static unsafe void OpenAchievementByAchievementId(uint achievementId)
    {
        AgentAchievement.Instance()->Show();
        AgentAchievement.Instance()->OpenById(achievementId);
    }
}