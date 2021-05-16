using System;
using UnityEngine;

public class Character : MonoBehaviour, IInteractable, ICharacter
{
    public DialogueSO dialogue;
    public GameObject dialogueBox;

    private void Start()
    {
        if (dialogue == null)
            Debug.Log($"Character: no dialogue", this);
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

    public void SetDialogue(DialogueSO dialogueSo)
    {
        dialogue = dialogueSo;
    }

    public void AddItem(Item item)
    {
        throw new NotImplementedException();
    }
}
