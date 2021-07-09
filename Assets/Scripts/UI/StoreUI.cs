using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StoreUI : MonoBehaviour, IPopupUIElement
{
    public GameObject itemsContainer;

    private List<StoreItemInfo> storeItemInfoComponents = new List<StoreItemInfo>();

    private StoreManager storeManager;
    private StoreItemInfo.Factory storeItemInfoFactory;

    [Inject]
    public void Init(
        StoreManager storeManager, 
        StoreItemInfo.Factory storeItemInfoFactory)
    {
        this.storeManager = storeManager;
        this.storeItemInfoFactory = storeItemInfoFactory;
    }
    
    private void Awake()
    {
        Close();
    }

    public void Show(List<Item> items)
    {
        UIManager.Instance.RegisterPopup(this);
        itemsContainer.SetActive(true);
        
        // Destroy all children in items container to prevent duplications
        foreach(Transform child in itemsContainer.transform)
            Destroy(child.gameObject);

        foreach (var item in items)
        {
            var itemInfoComponent = Resources.Load<StoreItemInfo>("Prefabs/UI/Store/ItemInfo");
            var itemInfo = storeItemInfoFactory.Create(storeManager);
            itemInfo.transform.SetParent(itemsContainer.transform);
            itemInfo.Initialize(item);
            
            storeItemInfoComponents.Add(itemInfo);
        }
    }

    public void Close()
    {
        storeItemInfoComponents.Clear();
        itemsContainer.SetActive(false);
    }
}