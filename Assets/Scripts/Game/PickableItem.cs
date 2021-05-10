using System;
using UnityEngine;

public class PickableItem : PickableItemBase
{
    public string itemId;

    private AudioClip pickupSound;

    private void Start()
    {
        if (string.IsNullOrEmpty(itemId))
        {
            Debug.LogWarning($"No itemId for pickable item! | {transform.position} | {name}");
            Destroy(gameObject); // TODO: use placeholder?
        }

        var item = Resources.Load<Item>($"Prefabs/Items/{itemId}");
        if (item == null)
        {
            Debug.LogWarning($"Pickable item with {itemId} id does not exist or it's a bug");
            Destroy(gameObject);
        }

        GetComponent<SpriteRenderer>().sprite = item.icon;

        pickupSound = item.pickupSound;
    }

    protected override void OnPickup()
    {
        GameManager.Instance.AddItemById(itemId,
            FindObjectOfType<Player>()); // TODO: replace find object with somethign else
        AudioManager.Instance.PlayClip(pickupSound);
        // TODO: play pickup sound
        DestroySave();
    }
}