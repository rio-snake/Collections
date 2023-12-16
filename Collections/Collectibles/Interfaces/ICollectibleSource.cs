namespace Collections;

public interface ICollectibleSource
{
    public string GetName();
    public List<SourceCategory> GetSourceCategories();
    public bool GetIslocatable();
    public void DisplayLocation();
    public IDalamudTextureWrap GetIconLazy();
}