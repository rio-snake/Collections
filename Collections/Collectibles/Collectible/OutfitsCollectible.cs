namespace Collections;

public class OutfitsCollectible : Collectible<ItemAdapter>, ICreateable<OutfitsCollectible, ItemAdapter>
{
    public new static string CollectionName => "Outfits";

    public OutfitsCollectible(ItemAdapter excelRow) : base(excelRow)
    {
    }

    public static OutfitsCollectible Create(ItemAdapter excelRow)
    {
        return new(excelRow);
    }

    protected override string GetCollectionName()
    {
        return CollectionName;
    }

    protected override string GetName()
    {
        return ExcelRow.Name.ToString();
    }

    protected override uint GetId()
    {
        return ExcelRow.RowId;
    }

    protected override string GetDescription()
    {
        var items = Services.ItemFinder.ItemsInOutfit(ExcelRow.RowId);
        return items.Select(i => i.Name).Aggregate((full, item) => full + "\n" + item).ToString();
    }

    protected override HintModule GetSecondaryHint()
    {
        if (this.CollectibleKey == null) return base.GetSecondaryHint();
        return new HintModule($"{(this.CollectibleKey as OutfitKey).FirstItem.ClassJobCategory.Value.Name}, Lv. {(this.CollectibleKey as OutfitKey).FirstItem.LevelEquip}", null);
    }

    public override void UpdateObtainedState()
    {
        isObtained = Services.ItemFinder.IsItemInDresser(ExcelRow.RowId);
    }

    protected override int GetIconId()
    {
        return ExcelRow.Icon;
    }

    public int GetNumberOfDyeSlots()
    {
        if (this.CollectibleKey == null) return 0;
        return (CollectibleKey as OutfitKey).FirstItem.DyeCount;
    }

    public override void Interact()
    {
        // Do nothing - taken care of by event service (should probably unify this in some way)
    }
}
