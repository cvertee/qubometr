using System;
using UnityEngine;

public class Character : MonoBehaviour, IInteractable, ICharacter
{
    public string dialogueId;
    public GameObject dialogueBox;

    public void GetName()
    {
        throw new NotImplementedException();
    }

    public void Interact()
    {
        StartDialogue(dialogueId);
    }

    public void StopInteract()
    {
        GameManager.Instance.StopCurrentDialogue();
    }

    public void StartDialogue(string id)
    {
        if (!string.IsNullOrEmpty(id))
        {
            GameManager.Instance.StartDialogue(id, dialogueBox);
        }
    }

    public void AddItem(Item item)
    {
        throw new NotImplementedException();
    }
}
