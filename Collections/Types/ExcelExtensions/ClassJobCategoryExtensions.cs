using System.Reflection;
using Collections;

// Used to add a helper function to the ClassJobCategory Struct
public static class ClassJobCategoryExtensions
{
    public static List<ClassJob> GetJobs(this ClassJobCategory category)
    {
        // using reflection here to iterate over category properties
        PropertyInfo[] props = category.GetType().GetProperties();
        return ExcelCache<ClassJob>.GetSheet().Where(job =>
        {
            // if square ever goofs and adds a job that doesn't have a column in ClassJobCategory, this will catch that.
            // + 2 to skip RowId and Name properties
            // Can't use Job.Abbreviation to check since in other language versions the abbreviation changes.
            if (job.RowId + 2 >= props.Count()) return false;
            return props[job.RowId + 2]?.GetValue(category) as bool? ?? false;
        }).ToList();
    }
}