using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

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
    public LocalizedString displayName;

    public bool isBeingUsed = false;
    public float protectionMultiplier = 0.0f;
}
