using Collections;

// Used to add a helper function to the ClassJob struct
public static class ClassJobExtensions
{
    public static ISharedImmediateTexture GetIcon(this ClassJob job)
    {
        int baseIcon = 62300;
        if (job.JobIndex > 0)
        {
            baseIcon += 100 + job.JobIndex;
        }
        // icons for DoH/DoL start at 62310, and we can use UI Priority to calculate the correct spot
        else if (job.DohDolJobIndex >= 0)
        {
            baseIcon += 10 + job.DohDolJobIndex + job.UIPriority / 200 * 8;
        }
        return IconHandler.GetIcon(baseIcon);
    }
}