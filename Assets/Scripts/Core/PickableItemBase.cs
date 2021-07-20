using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class PickableItemBase : MonoBehaviour, IInteractable
{
    public UnityEvent onPickup;

    protected IObjectDestroyer objectDestroyer;

    [Inject]
    public void Init(IObjectDestroyer objectDestroyer)
    {
        this.objectDestroyer = objectDestroyer;
    }
    
    private void Start()
    {
        objectDestroyer.CheckIfDestroyed(gameObject);
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
