using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Shield,
    Armor,
    Unknown
}

[CreateAssetMenu(fileName = "ItemName", menuName = "Items/Item", order = 0 )]
public class ItemSO : ScriptableObject
{
    public ItemType type;
    public Sprite icon;
    public RuntimeAnimatorController animatorController;
    public int price;
    public AudioClip pickupSound;
    public string displayName = "NO_NAME";

    public bool isBeingUsed = false;
    public float protectionMultiplier = 0.0f;
}
