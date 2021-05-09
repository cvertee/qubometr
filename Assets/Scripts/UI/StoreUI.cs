using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreUI : Singleton<StoreUI>, IPopupUIElement
{
    public GameObject itemsContainer;

    private List<StoreItemInfo> storeItemInfoComponents = new List<StoreItemInfo>();

    private void Awake()
    {
        Close();
    }

    public void Show(List<Item> items)
    {
        UIManager.Instance.RegisterPopup(this);
        itemsContainer.SetActive(true);

        foreach (var item in items)
        {
            var itemInfoComponent = Resources.Load<StoreItemInfo>("Prefabs/UI/Store/ItemInfo");
            var itemInfo = Instantiate<StoreItemInfo>(itemInfoComponent, itemsContainer.transform);
            itemInfo.Initialize(item);
            storeItemInfoComponents.Add(itemInfo);
        }
    }

    public void Close()
    {
        // Clear list of displayed items
        for (var i = 0; i < storeItemInfoComponents.Count; i++) // FIX?: Foreach won't work
        {
            var storeItemInfo = storeItemInfoComponents[i];
            Destroy(storeItemInfo.gameObject);
            storeItemInfoComponents.Remove(storeItemInfo);
        }

        itemsContainer.SetActive(false);
    }
}