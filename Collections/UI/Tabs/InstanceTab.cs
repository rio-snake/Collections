namespace Collections;

public class InstanceTab : IDrawable
{
    private Dictionary<string, List<ICollectible>> collections = new();
    private uint collectiblesLoadedInstanceId = 0;

    private EventService EventService { get; init; }
    private CollectionWidget CollectionWidget { get; init; }
    public InstanceTab()
    {
        EventService = new EventService();
        CollectionWidget = new CollectionWidget(EventService, false);
        Services.DutyState.DutyStarted += OnDutyStarted;
    }

    public void OnDutyStarted(object sender, ushort arg)
    {
        Dev.Log("Received DutyStarted event");
        if (Services.Configuration.AutoOpenInstanceTab)
        {
            Services.WindowsInitializer.MainWindow.OpenTab("Instance");
        }
    }

    public void Draw()
    {
        // Not in valid instance
        if (!InValidInstance())
        {
            ImGui.Text("Can only be viewed in an Instance");
            collectiblesLoadedInstanceId = 0;
            return;
        }

        // Load collectibles if not done already
        if (GetCurrentInstance() != collectiblesLoadedInstanceId)
        {
            LoadCollectibles();
        }

        // Draw collections
        DrawCollections();
    }

    public void DrawCollections()
    {
        foreach (var (name, collection) in collections)
        {
            ImGui.Text(name);
            CollectionWidget.Draw(collection, false, false);
        }
    }

    private void LoadCollectibles()
    {
        collectiblesLoadedInstanceId = GetCurrentInstance();
        var currentDutyItemIds = CurrentDutyItemIds(collectiblesLoadedInstanceId);

        collections = Services.DataProvider.GetCollections();
        foreach (var (name, collection) in collections)
        {
            collections[name] = collection
                .Where(c => c.CollectibleKey is not null && currentDutyItemIds.Contains(c.CollectibleKey.item.RowId))
                .ToList();
        }
    }

    private static uint GetCurrentInstance()
    {
        var territoryType = ExcelCache<TerritoryType>.GetSheet().GetRow(Services.ClientState.TerritoryType);
        return territoryType.ContentFinderCondition.Row;
    }

    private static bool InValidInstance()
    {
        var instanceId = GetCurrentInstance();
        var inInstance = instanceId != 0;
        var collectionExists = Services.DataGenerator.InstancesDataGenerator.contentFinderConditionToItems.ContainsKey(instanceId);
        return inInstance && collectionExists;
    }

    private List<uint>? CurrentDutyItemIds(uint instanceId)
    {

        if (Services.DataGenerator.InstancesDataGenerator.contentFinderConditionToItems.ContainsKey(instanceId))
        {
            return Services.DataGenerator.InstancesDataGenerator.contentFinderConditionToItems[instanceId];
        }
        Dev.Log($"Unable to find items obtained by the current duty id: {instanceId}");
        return new List<uint>();
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
        Services.DutyState.DutyStarted -= OnDutyStarted;
    }
}
