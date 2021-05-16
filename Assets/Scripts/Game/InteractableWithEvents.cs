using UnityEngine;
using UnityEngine.Events;

public class InteractableWithEvents : MonoBehaviour, IInteractable
{
    public UnityEvent onInteraction;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
    
    public void Interact()
    {
        onInteraction.Invoke();
    }

    public void StopInteract()
    {

    }
}
