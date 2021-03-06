using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Zenject;

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

    private AudioManager audioManager;

    [Inject]
    public void Init(AudioManager audioManager)
    {
        this.audioManager = audioManager;
    }
    
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }


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
            if (!GameData.Data.pickedUpKeys.Any(x => x == requiredKey))
            {
                Debug.Log($"Door: No key! (requires {requiredKey.name})");
                audioManager.PlaySound(AudioResource.DoorLocked);
                return;
            }
            else
            {
                audioManager.PlaySound("doorUnlocked", shouldPlayInAudioSource: true);
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
                GameEvents.onLocationChangeRequested.Invoke();
                GameData.Data.playerPosition = null;
                StartCoroutine(LoadSceneTimeout());
                break;
            
            case DoorType.None:
                Debug.Log("Door: DoorType.None so nothing will happen");
                break;
            
            default:
                break;
        }
    }

    private IEnumerator LoadSceneTimeout()
    {
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene(sceneName);
    }

    public void StopInteract()
    {
        throw new System.NotImplementedException();
    }
}
