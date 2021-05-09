using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour, IInteractable
{
    public List<Item> items;

    private StoreUI storeUI;

    private void Start()
    {
        storeUI = FindObjectOfType<StoreUI>();
    }

    public void Interact()
    {
        StoreUI.Instance.Show(items);
    }

    public void StopInteract()
    {
        StoreUI.Instance.Close();
    }
}