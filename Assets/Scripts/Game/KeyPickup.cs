using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour, IInteractable
{
    public Key key;
    
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
