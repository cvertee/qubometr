using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickableItemBase : DestroyerBase, IInteractable
{
    public UnityEvent onPickup;
    
    public void Interact()
    {
        if (onPickup != null)
            onPickup.Invoke();
        
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
