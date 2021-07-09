using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Store : MonoBehaviour, IInteractable
{
    public List<Item> items;

    private StoreUI storeUI;

    [Inject]
    public void Init(StoreUI storeUI)
    {
        this.storeUI = storeUI;
    }
    
    private void Start()
    {
        storeUI = FindObjectOfType<StoreUI>();
    }

    public void Interact()
    {
        storeUI.Show(items);
    }

    public void StopInteract()
    {
        storeUI.Close();
    }
}