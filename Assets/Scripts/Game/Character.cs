using Assets.Scripts.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour, IInteractable, ICharacter
{
    public string dialogueId;
    public GameObject dialogueBox;

    public void GetName()
    {
        throw new System.NotImplementedException();
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
}
