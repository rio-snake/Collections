using FFXIVClientStructs.FFXIV.Client.Game;

namespace Collections;

public class QuestExecutor
{
    // wrapper for QuestManager
    public static unsafe bool IsQuestComplete(uint questId)
    {
        return QuestManager.IsQuestComplete(questId);
    }
}