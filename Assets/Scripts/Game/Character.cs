using Assets.Scripts.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour, IInteractable, ICharacter
{
    public string dialogueId;

    public void GetName()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        if (!string.IsNullOrEmpty(dialogueId))
        {
            GameManager.Instance.StartDialogue(dialogueId);
        }
    }

    public void StopInteract()
    {
        GameManager.Instance.StopCurrentDialogue();
    }
}
