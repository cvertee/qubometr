using System;
using UnityEngine;

public class Character : MonoBehaviour, IInteractable, ICharacter
{
    public DialogueSO dialogue;
    public GameObject dialogueBox;

    public void GetName()
    {
        throw new NotImplementedException();
    }

    public void Interact()
    {
        StartDialogue(dialogue);
    }

    public void StopInteract()
    {
        GameManager.Instance.StopCurrentDialogue();
    }

    public void StartDialogue(DialogueSO dialogueSo)
    {
        GameManager.Instance.StartDialogue(dialogueSo, dialogueBox);
    }

    public void AddItem(Item item)
    {
        throw new NotImplementedException();
    }
}
