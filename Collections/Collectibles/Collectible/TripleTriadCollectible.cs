using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.Game.UI;

namespace Collections;

public class TripleTriadCollectible : Collectible<TripleTriadCard>, ICreateable<TripleTriadCollectible, TripleTriadCard>
{
    public static string CollectionName => "Triple Triad";

    public TripleTriadCollectible(TripleTriadCard excelRow) : base(excelRow)
    {
    }

    public static TripleTriadCollectible Create(TripleTriadCard excelRow)
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
        return ExcelRow.Description.ToString();
    }

    protected override HintModule GetSecondaryHint()
    {
        TripleTriadCardResident? temp = ExcelCache<TripleTriadCardResident>.GetSheet().GetRow(ExcelRow.RowId);
        return new HintModule($"Card {(temp?.UIPriority > 0 ? "Ex" : "No")}. {temp?.Order ?? 0}", null);
    }


    private static uint CardNumIconId = 230119;
    private static uint CardStarIconId = 230110;

    public override void DrawAdditionalTooltip()
    {
        TripleTriadCardResident? temp = ExcelCache<TripleTriadCardResident>.GetSheet().GetRow(ExcelRow.RowId);
        var cursor = ImGui.GetCursorPos();
        // triple triad card icon start
        // draw card
        var size = GetIconLazy().GetWrapOrEmpty().Size;
        var iconSize = new Vector2(40, 40);
        ImGui.Image(GetIconLazy().GetWrapOrEmpty().Handle, size);
        // add values
        ImGui.SetCursorPos(cursor + ((size - iconSize) * new Vector2(0.5f, 0.75f)));
        ImGui.Image(GetGameIcon(temp.Value.Top + CardNumIconId).GetWrapOrEmpty().Handle, iconSize);
        ImGui.SetCursorPos(cursor + ((size - iconSize) * new Vector2(0.3f, 0.825f)));
        ImGui.Image(GetGameIcon(temp.Value.Left + CardNumIconId).GetWrapOrEmpty().Handle, iconSize);
        ImGui.SetCursorPos(cursor + ((size - iconSize) * new Vector2(0.7f, 0.825f)));
        ImGui.Image(GetGameIcon(temp.Value.Right + CardNumIconId).GetWrapOrEmpty().Handle, iconSize);
        ImGui.SetCursorPos(cursor + ((size - iconSize) * new Vector2(0.5f, 0.9f)));
        ImGui.Image(GetGameIcon(temp.Value.Bottom + CardNumIconId).GetWrapOrEmpty().Handle, iconSize);
        // Star
        ImGui.SetCursorPos(cursor + size * new Vector2(0.1f, 0.05f));
        ImGui.Image(GetGameIcon(temp.Value.TripleTriadCardRarity.RowId + CardStarIconId).GetWrapOrEmpty().Handle, iconSize);

        // reset back to where card would have been drawn
        ImGui.SetCursorPos(cursor + new Vector2(0, size.Y));

    }

    private ISharedImmediateTexture GetGameIcon(uint iconId)
    {
        GameIconLookup lookup = new GameIconLookup(iconId: iconId, itemHq: false);
        return Services.TextureProvider.GetFromGameIcon(lookup);
    }

    public override unsafe void UpdateObtainedState()
    {
        isObtained = UIState.Instance()->IsTripleTriadCardUnlocked((ushort)ExcelRow.RowId);
    }

    protected override int GetIconId()
    {
        return (int)ExcelRow.RowId + 87000;
    }

    public override unsafe void Interact()
    {
    }

    public override void OpenGamerEscape()
    {
        WikiOpener.OpenGamerEscape(GetDisplayName() + "_Card");
    }
}
