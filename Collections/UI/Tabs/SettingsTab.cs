namespace Collections;

public class SettingsTab : IDrawable
{
    private List<string> collectionNames = new();
    public SettingsTab()
    {
        autoOpenInstanceTab = Services.Configuration.AutoOpenInstanceTab;
        autoHideObtainedFromInstanceTab = Services.Configuration.AutoHideObtainedFromInstanceTab;
        excludedCollectionsFromInstanceTab = Services.Configuration.ExcludedCollectionsFromInstanceTab;
        collectionNames = Services.DataProvider.GetCollections().AsParallel().Select(col => col.Key).OrderBy((key) => key != GlamourCollectible.CollectionName).ThenBy(k => k).ToList();
    }

    private bool autoOpenInstanceTab;
    private bool autoHideObtainedFromInstanceTab;
    private List<string> excludedCollectionsFromInstanceTab;
    public void Draw()
    {
        if (ImGui.Checkbox("Auto open Instance tab when entering an instance", ref autoOpenInstanceTab))
        {
            Services.Configuration.AutoOpenInstanceTab = autoOpenInstanceTab;
            Services.Configuration.Save();
        }
        if (ImGui.Checkbox("Auto hide obtained items from Instance tab", ref autoHideObtainedFromInstanceTab))
        {
            Services.Configuration.AutoHideObtainedFromInstanceTab = autoHideObtainedFromInstanceTab;
            Services.Configuration.Save();
        }

        ImGui.Text("Exclude these collections from the Instance tab");
        ImGui.BeginListBox("##collection-box", new Vector2(300, 200));
        foreach (var collection in collectionNames)
        {
            bool isExcluded = excludedCollectionsFromInstanceTab.Contains(collection);
            if (ImGui.Checkbox($"{collection}", ref isExcluded))
            {
                if (isExcluded)
                    excludedCollectionsFromInstanceTab.Add(collection);
                else
                    excludedCollectionsFromInstanceTab.Remove(collection);
            }
        }
        ImGui.EndListBox();

    }

    public void OnOpen()
    {
        Dev.Log();
    }

    public void OnClose()
    {
    }

    public void Dispose()
    {
    }
}
