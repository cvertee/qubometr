using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyPickup : PickableItemBase
{
    public Key key;

    // private void Awake()
    // {
    //     if (GameData.Data.pickedUpKeys.Any(x => x.id == key.id))
    //         Destroy(gameObject);
    // }
    
    protected override void OnPickup()
    {
        GameData.AddKey(key);
        DestroySave();
    }
}
