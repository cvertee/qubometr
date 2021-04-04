using Assets.Scripts.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public void StartDialogue(string dialogueId, GameObject container)
    {
        Debug.Log($"Starting dialogue [{dialogueId}]");
        var lines = Resources.Load<TextAsset>($"Dialogues/{dialogueId}")
            .text
            .Split('\n');

        FindObjectOfType<DialogueUi>().ShowDialogueTexts(lines, container);
    }

    public void StopCurrentDialogue()
    {
        Debug.Log("Stopped current dialogue");
    }
}
