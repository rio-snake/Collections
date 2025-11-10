namespace Collections;

public class OutfitItemChangeEventArgs : EventArgs
{
    public OutfitsCollectible Collectible { get; init; }
    public OutfitItemChangeEventArgs(OutfitsCollectible collectible)
    {
        Collectible = collectible;
    }
}

public class OutfitItemChangeEvent : Event<OutfitItemChangeEventArgs>
{
}


