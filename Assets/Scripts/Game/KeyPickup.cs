using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class KeyPickup : PickableItemBase
{
    public Key key;

    private AudioManager audioManager;

    [Inject]
    public void Init(AudioManager audioManager)
    {
        this.audioManager = audioManager;
    }
    
    protected override void OnPickup()
    {
        audioManager.PlaySound(AudioResource.KeyPickup);
        GameData.AddKey(key);
        destroyer.DestroySave();
    }
}
