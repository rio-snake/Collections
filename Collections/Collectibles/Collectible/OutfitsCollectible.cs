using FFXIVClientStructs.FFXIV.Component.Excel;

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
        return "";
    }

    protected override HintModule GetPrimaryHint()
    {
        return new HintModule($"Patch {GetPatchAdded()}", null);
    }

    protected override HintModule GetSecondaryHint()
    {
        return new HintModule($"{ExcelRow.ClassJobCategory.Value.Name}", null);
    }

    public override void UpdateObtainedState()
    {
        //Dev.Log($"Trying to obtain status of {GetName()}\n  ItemId: {ExcelRow.RowId}\n  IsInDresser: {Services.ItemFinder.IsItemInDresser(ExcelRow.RowId)}");
        
        isObtained = Services.ItemFinder.IsItemInInventory(ExcelRow.RowId)
                    || Services.ItemFinder.IsItemInDresser(ExcelRow.RowId)
                    || Services.ItemFinder.IsItemInArmoireCache(ExcelRow.RowId);
    }

    protected override int GetIconId()
    {
        return ExcelRow.Icon;
    }

    public override void Interact()
    {
        // Do nothing - taken care of by event service (should probably unify this in some way)
    }
}
