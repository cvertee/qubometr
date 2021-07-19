using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DialogueUi : MonoBehaviour
{
    private CommandExecutor commandExecutor;
    private GameSettingsSO gameSettings;

    public void Init(
        CommandExecutor commandExecutor,
        GameSettingsSO gameSettings)
    {
        this.commandExecutor = commandExecutor;
        this.gameSettings = gameSettings;
    }
    
    public void ShowDialogueTexts(string[] texts, GameObject container)
    {
        var dialogueTextGameObject = (GameObject)Instantiate(Resources.Load("Prefabs/UI/DialogueText"), container.transform.position, Quaternion.identity);
        dialogueTextGameObject.transform.SetParent(transform);
        StartCoroutine(DialogueCoroutine(texts, dialogueTextGameObject.GetComponent<Text>()));
    }

    private IEnumerator DialogueCoroutine(string[] texts, Text textObject)
    {
        foreach(var text in texts)
        {
            if (text.StartsWith("!"))
            {
                commandExecutor.ExecuteRawCommand(text);
                continue;
            }
            
            textObject.text = text;
            yield return new WaitForSeconds(gameSettings.dialogueTimeSeconds);
        }
        yield return new WaitForSeconds(1.0f);
        Destroy(textObject.gameObject);
    }
}
