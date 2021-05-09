using UnityEngine;
using UnityEngine.Events;

public class InteractableWithEvents : MonoBehaviour, IInteractable
{
    public UnityEvent onInteraction;

    public void Interact()
    {
        onInteraction.Invoke();
    }

    public void StopInteract()
    {

    }
}
