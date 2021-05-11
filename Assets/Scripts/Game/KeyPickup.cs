using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyPickup : PickableItemBase
{
    public Key key;

    protected override void OnPickup()
    {
        AudioManager.Instance.PlaySound("keyPickup");
        GameData.AddKey(key);
        DestroySave();
    }
}
