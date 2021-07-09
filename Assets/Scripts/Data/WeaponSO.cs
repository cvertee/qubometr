using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemName", menuName = "Items/Weapon", order = 0)]
public class WeaponSO : ItemSO
{
    public float damage;
    public List<string> attackNames = new List<string>() { "Attack" };
}