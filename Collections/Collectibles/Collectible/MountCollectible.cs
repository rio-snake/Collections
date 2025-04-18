using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.Game.UI;

namespace Collections;

public class MountCollectible : Collectible<ItemAdapter>, ICreateable<MountCollectible, ItemAdapter>
{
    public new static string CollectionName => "Mounts";

    public MountCollectible(ItemAdapter excelRow) : base(excelRow)
    {
    }

    public static MountCollectible Create(ItemAdapter excelRow)
    {
        return new(excelRow);
    }

    protected override string GetCollectionName()
    {
        return CollectionName;
    }

    protected override string GetName()
    {
        return GetMountFromUnlock().Singular.ToString();
    }

    protected override uint GetId()
    {
        return ExcelRow.RowId;
    }

    protected override string GetDescription()
    {
        return ExcelCache<MountTransient>.GetSheet().GetRow(GetMountFromUnlock().RowId)?.Description.ToString() ?? "";
    }

    protected override HintModule GetPrimaryHint()
    {
        return new HintModule($"Mount ID: {ExcelRow.RowId}", FontAwesomeIcon.PeopleGroup);
    }

    protected override HintModule GetSecondaryHint()
    {
        return new HintModule(GetPatchAdded(), null);
    }

    public override unsafe void UpdateObtainedState()
    {
        isObtained = PlayerState.Instance()->IsMountUnlocked(GetMountFromUnlock().RowId);
    }

    protected override int GetIconId()
    {
        return GetMountFromUnlock().Icon;
    }

    public override unsafe void Interact()
    {
        if (isObtained)
            ActionManager.Instance()->UseAction(ActionType.Mount, GetMountFromUnlock().RowId);
    }

    public override void OpenGamerEscape()
    {
        WikiOpener.OpenGamerEscape(GetDisplayName() + "_(Mount)");
    }

    private Mount GetMountFromUnlock()
    {
        return ExcelCache<Mount>.GetSheet().GetRow(ExcelRow.ItemAction.Value.Data.ElementAt(0)).Value;
    }
}
