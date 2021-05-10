using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    public string sceneName;
    public Key requiredKey; 
    
    public void Interact()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.Log($"Door: No scene name in {name}");
            return;
        }

        if (requiredKey != null)
        {
            if (GameData.Data.pickedUpKeys.Contains(requiredKey))
            {
                SceneManager.LoadScene(sceneName);
                return;
            }
            else
            {
                Debug.Log($"Door: No key! (requires {requiredKey.id})");
                return;
            }
        }

        SceneManager.LoadScene(sceneName);
    }

    public void StopInteract()
    {
        throw new System.NotImplementedException();
    }
}
