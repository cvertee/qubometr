using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItemBase : DestroyerBase, IInteractable
{
    public void Interact()
    {
        OnPickup();
    }

    protected virtual void OnPickup()
    {
        
    }

    public void StopInteract()
    {
        throw new System.NotImplementedException();
    }
}
