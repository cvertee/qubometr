using UnityEngine;
using UnityEngine.Events;

public class InteractableWithEvents : MonoBehaviour, IInteractable
{
    public UnityEvent onInteraction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
