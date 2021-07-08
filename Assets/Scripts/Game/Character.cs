using System;
using UnityEngine;
using Zenject;

public class Character : MonoBehaviour, IInteractable, ICharacter
{
    public DialogueSO dialogue;
    public GameObject dialogueBox;

    private GameManager gameManager;

    [Inject]
    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

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
        gameManager.StopCurrentDialogue();
    }

    public void StartDialogue(DialogueSO dialogueSo)
    {
        gameManager.StartDialogue(dialogueSo, dialogueBox);
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
