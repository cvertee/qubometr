using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // Start is called before the first frame update
    private void Start()
    {
        GameData.Data.sceneName = SceneManager.GetActiveScene().name;
        //Application.targetFrameRate = Screen.currentResolution.refreshRate;

        GameEvents.onEnemyAlert.AddListener(() => AudioManager.Instance.PlaySound("alert"));
        GameEvents.onPlayerDeath.AddListener(() => SaveSystem.Load());
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!UIManager.Instance.CloseLatestPopup())
            {
                MenuUI.Instance.Show();
            }
        }
    }

    public void StartDialogue(DialogueSO dialogueSo, GameObject container)
    {
        Debug.Log($"Starting dialogue [{dialogueSo.name}]");

        FindObjectOfType<DialogueUi>().ShowDialogueTexts(dialogueSo.lines, container);
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

    public void AddItemById(string id, ICharacter character)
    {
        var item = GetItemObjectById(id);
        character.AddItem(item);
    }

    public Item GetItemObjectById(string id)
    {
        return Resources.Load<Item>($"Prefabs/Items/{id}");
    }

    public Player GetPlayer()
    {
        return FindObjectOfType<Player>();
    }

    public void StartBossFight(Enemy boss)
    {
        var bossUI = FindObjectOfType<BossUI>();
        var bossInfo = boss.GetComponent<Boss>().bossInfo;
        bossUI.Initialize(bossInfo, boss);
        
        AudioManager.Instance.PlayMusic(bossInfo.music);
    }
}
