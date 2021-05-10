using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyPickup : MonoBehaviour, IInteractable
{
    public Key key;

    private void Awake()
    {
        if (GameData.Data.pickedUpKeys.Any(x => x.id == key.id))
            Destroy(gameObject);
    }
    
    public void Interact()
    {
        GameData.AddKey(key);
        Destroy(gameObject);
    }

    public void StopInteract()
    {
        throw new System.NotImplementedException();
    }
}
