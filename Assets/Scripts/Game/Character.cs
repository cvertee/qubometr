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
        if (!string.IsNullOrEmpty(dialogueId))
        {
            GameManager.Instance.StartDialogue(dialogueId, dialogueBox);
        }
    }

    public void StopInteract()
    {
        GameManager.Instance.StopCurrentDialogue();
    }
}
