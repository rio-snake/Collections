using FFXIVClientStructs.FFXIV.Client.Game;

namespace Collections;

public class CurrencyDataGenerator
{
    public Dictionary<uint, SourceCategory> ItemIdToSourceCategory = new()
    {
        { 00001, SourceCategory.Gil }, // Gil
        { 00020, SourceCategory.CompanySeals }, // Storm Seal (designated company seals)
        { 00025, SourceCategory.PvP }, // Wolf Mark
        { 21067, SourceCategory.PvP }, // Wolf Collar
        { 36656, SourceCategory.PvP }, // Trophy Crystals
        { 10310, SourceCategory.Scrips }, // Blue gatherers scrip
        { 10311, SourceCategory.Scrips }, // Red gatherers scrip
        { 17834, SourceCategory.Scrips }, // Yellow gatherers scrip
        { 25200, SourceCategory.Scrips }, // White gatherers scrip
        { 33914, SourceCategory.Scrips }, // Purple gatherers scrip
        { 10308, SourceCategory.Scrips }, // Blue gatherers scrip
        { 10309, SourceCategory.Scrips }, // Red gatherers scrip
        { 17833, SourceCategory.Scrips }, // Yellow gatherers scrip
        { 25199, SourceCategory.Scrips }, // White gatherers scrip
        { 33913, SourceCategory.Scrips }, // Purple gatherers scrip
        { 00027, SourceCategory.TheHunt }, // Allied Seal
        { 10307, SourceCategory.TheHunt }, // Centurio Seal
        { 26533, SourceCategory.TheHunt }, // Sack of nuts
        { 00029, SourceCategory.MGP }, // MGP
        { 41629, SourceCategory.MGP}, // MGF (fall guys)
        { 37549, SourceCategory.IslandSanctuary }, // Seafarer's Cowrie
        { 37550, SourceCategory.IslandSanctuary }, // Islander's Cowrie
        { 28063, SourceCategory.RestorationZone}, // Skybuilder Scrips
        { 45690, SourceCategory.RestorationZone}, // Cosmocredit
        { 47343, SourceCategory.RestorationZone}, // Phaenna token booklet
        { 47594, SourceCategory.RestorationZone}, // Phaenna exploration token 
        { 48146, SourceCategory.RestorationZone}, // Phaenna credit
    };

    public CurrencyDataGenerator()
    {
        PopulateData();
    }

    private void PopulateData()
    {
        // Add Tomestones
        var TomestonesItemSheet = ExcelCache<TomestonesItem>.GetSheet();
        foreach (var tomestone in TomestonesItemSheet)
        {
            ItemIdToSourceCategory[tomestone.Item.RowId] = SourceCategory.Tomestones;
        }

        // Add Beast tribe currencies
        var beastTribeSheet = ExcelCache<BeastTribe>.GetSheet();
        foreach (var beastTribe in beastTribeSheet)
        {
            ItemIdToSourceCategory[beastTribe.CurrencyItem.RowId] = SourceCategory.BeastTribes;
        }

        // generate currency items where we know categories
        var ItemSheet = ExcelCache<ItemAdapter>.GetSheet();
        foreach (var item in ItemSheet)
        {
            if (item.ItemSortCategory.RowId == 41) // Deep Dungeon Currency items
            {
                ItemIdToSourceCategory[item.RowId] = SourceCategory.DeepDungeon;
            }
        }
    }
}
