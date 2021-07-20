namespace Core
{
    public interface IItemDatabase
    {
        ItemSO GetItemDataByName(string itemName);
    }
}