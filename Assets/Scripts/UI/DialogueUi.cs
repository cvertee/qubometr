using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUi : MonoBehaviour
{
    public void ShowDialogueTexts(string[] texts, GameObject container)
    {
        var dialogueTextGameObject = (GameObject)Instantiate(Resources.Load("Prefabs/UI/DialogueText"), container.transform.position, Quaternion.identity);
        dialogueTextGameObject.transform.parent = transform;
        StartCoroutine(DialogueCoroutine(texts, dialogueTextGameObject.GetComponent<Text>()));
    }

    private IEnumerator DialogueCoroutine(string[] texts, Text textObject)
    {
        foreach(var text in texts)
        {
            if (text.StartsWith("!"))
            {
                CommandExecutor.Instance.ExecuteRawCommand(text);
                continue;
            }
            
            textObject.text = text;
            yield return new WaitForSeconds(2.0f);
        }
        yield return new WaitForSeconds(1.0f);
        Destroy(textObject.gameObject);
    }
}
