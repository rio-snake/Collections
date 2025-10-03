namespace Collections;

public class SettingsTab : IDrawable
{
    private List<string> collectionNames = new();
    public SettingsTab()
    {
        autoOpenInstanceTab = Services.Configuration.AutoOpenInstanceTab;
        onlyOpenIfUncollected = Services.Configuration.OnlyOpenIfUncollected;
        autoHideObtainedFromInstanceTab = Services.Configuration.AutoHideObtainedFromInstanceTab;
        excludedCollectionsFromInstanceTab = Services.Configuration.ExcludedCollectionsFromInstanceTab;
        collectionNames = Services.DataProvider.GetCollections().AsParallel().Select(col => col.Key).OrderBy((key) => key != GlamourCollectible.CollectionName).ThenBy(k => k).ToList();
    }

    private bool autoOpenInstanceTab;
    private bool onlyOpenIfUncollected;
    private bool autoHideObtainedFromInstanceTab;
    private List<string> excludedCollectionsFromInstanceTab;
    public void Draw()
    {
        if (ImGui.Checkbox("Auto open Instance tab when entering an instance", ref autoOpenInstanceTab))
        {
            Services.Configuration.AutoOpenInstanceTab = autoOpenInstanceTab;
            Services.Configuration.Save();
        }
        // spacing to signify this is a sub-option for auto-open
        ImGui.InvisibleButton("padding", new Vector2(15, 0));
        ImGui.SameLine();
        if (ImGui.Checkbox("Only open Instance tab if there are uncollected items ", ref onlyOpenIfUncollected))
        {
            Services.Configuration.OnlyOpenIfUncollected = onlyOpenIfUncollected;
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
