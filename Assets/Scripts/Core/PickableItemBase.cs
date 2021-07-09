using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickableItemBase : MonoBehaviour, IInteractable
{
    public UnityEvent onPickup;

    protected DestroyerBase destroyer;

    private void Awake()
    {
        destroyer = GetComponent<DestroyerBase>();
    }
    
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
