using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    public enum DoorType
    {
        LoadsScene,
        Opens,
        None
    }
    
    public string sceneName;
    public Key requiredKey;
    public DoorType interactionType = DoorType.None;
    
    public void Interact()
    {
        if (string.IsNullOrEmpty(sceneName) && interactionType == DoorType.LoadsScene)
        {
            Debug.Log($"Door: No scene name in {name}");
            return;
        }

        if (requiredKey != null)
        {
            // If player doesn't have required key
            if (!GameData.Data.pickedUpKeys.Any(x => x.name == requiredKey.name))
            {
                Debug.Log($"Door: No key! (requires {requiredKey.name})");
                AudioManager.Instance.PlaySound("doorLocked");
                return;
            }
            else
            {
                AudioManager.Instance.PlaySound("doorUnlocked", shouldPlayInAudioSource: true);
            }
        }

        Use();
    }

    public void Use()
    {
        switch (interactionType)
        {
            case DoorType.Opens:
                Destroy(gameObject);
                break;
            
            case DoorType.LoadsScene:
                SceneManager.LoadScene(sceneName);
                GameData.Data.playerPosition = null;
                break;
            
            case DoorType.None:
                Debug.Log("Door: DoorType.None so nothing will happen");
                break;
            
            default:
                break;
        }
    }

    public void StopInteract()
    {
        throw new System.NotImplementedException();
    }
}
