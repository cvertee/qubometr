using System;
using UnityEngine;

public class PickableItem : PickableItemBase
{
    public Item itemObject;

    private AudioClip pickupSound;

    private void Start()
    {
        if (itemObject == null)
        {
            Debug.LogWarning($"Pickable item is not here or it's a bug", this);
            Destroy(gameObject);
        }

        GetComponent<SpriteRenderer>().sprite = itemObject.icon;

        pickupSound = itemObject.pickupSound;
    }

    protected override void OnPickup()
    {
        GameManager.Instance.AddItem(
            itemObject,
            FindObjectOfType<Player>()
        );
        
        GameEvents.onAudioClipPlayRequested.Invoke(pickupSound);
        
        DestroySave();
    }
}