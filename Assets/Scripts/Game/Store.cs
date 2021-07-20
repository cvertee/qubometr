using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Store : MonoBehaviour, IInteractable
{
    public List<Item> items;

    public List<ItemSO> items_;

    private StoreUI storeUI;

    [Inject]
    public void Init(StoreUI storeUI)
    {
        this.storeUI = storeUI;
    }

    public void Interact()
    {
        storeUI.Show(items_);
    }

    public void StopInteract()
    {
        storeUI.Close();
    }
}