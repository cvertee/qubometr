using Assets.Scripts.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue(string dialogueId)
    {
        Debug.Log($"Starting dialogue [{dialogueId}]");
    }

    public void StopCurrentDialogue()
    {
        Debug.Log("Stopped current dialogue");
    }
}
