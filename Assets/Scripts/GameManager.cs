using Assets.Scripts.Core;
using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets;

public class GameManager : Singleton<GameManager>
{
    // Start is called before the first frame update
    void Start()
    {
        GameData.Data.sceneName = SceneManager.GetActiveScene().name;
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!UIManager.Instance.CloseLatestPopup())
            {
                // TODO: open main menu
            }
        }
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

    public void StartVideoScene(string videoName)
    {
        GameData.Data.videoToLoad = videoName;
        SceneManager.LoadScene("vid");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void AddItemById(string id, Player character)
    {
         character.AddItem(GetItemObjectById(id));
    }

    public GameObject GetItemObjectById(string id) // TODO: wtf fix name
    {
        return Resources.Load<GameObject>($"Prefabs/Items/{id}");
    }

    public Player GetPlayer()
    {
        return FindObjectOfType<Player>();
    }
}
