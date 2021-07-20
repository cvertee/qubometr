using System;
using UnityEngine;
using Zenject;

public class PickableItem : PickableItemBase
{
    public ItemSO itemObject;

    private AudioClip pickupSound;

    private GameManager gameManager;
    
    [Inject]
    private void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

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
        gameManager.AddItemById(
            itemObject.name,
            FindObjectOfType<Player>()
        );

        GameEvents.onAudioClipPlayRequested.Invoke(pickupSound);

        objectDestroyer.DestroyAndSave(gameObject);
    }
}