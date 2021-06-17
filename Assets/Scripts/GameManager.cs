using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private void Awake()
    {
        GameEvents.onHealthKitUseStart.AddListener(() =>
        {
            Sound.Play(AudioResource.HealthKitUse);
        });
        GameEvents.onHealthKitUseEnd.AddListener(() =>
        {
            GameData.IncreaseHealth(10f * GameSettings.GlobalHealthKitMultiplier);
            GameData.Data.healthKitsUsed += 1;
        });
        GameEvents.onAudioClipPlayRequested.AddListener(clip => 
        {
            //Debug.Log($"Playing raw audio clip `{clip.name}`");

            AudioManager.Instance.PlayClip(clip);
        });
        GameEvents.onAudioNamePlayRequested.AddListener(audioName => 
        {
            //Debug.Log($"Playing audio clip by name `{audioName}`");

            AudioManager.Instance.PlaySound(audioName);
        });
        GameEvents.onDelayedActionRequested.AddListener((time, action) => 
        {
            StartDelayedAction(time, action);
        });
    }

    // Start is called before the first frame update
    private void Start()
    {
        GameData.Data.sceneName = SceneManager.GetActiveScene().name;
        //Application.targetFrameRate = Screen.currentResolution.refreshRate;

        GameEvents.onEnemyAlert.AddListener(() => Sound.Play(AudioResource.EnemyAlert));
        GameEvents.onPlayerDeath.AddListener(() => SaveSystem.Load());

        SceneManager.sceneLoaded += (scene, mode) => GameEvents.onLocationStart.Invoke();
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
        GameData.Data.playerPosition = null;
        SceneManager.LoadScene(sceneName);
    }

    public void AddItem(Item item, ICharacter character)
    {
        character.AddItem(item);
    }

    public void AddItemById(string id, ICharacter character)
    {
        var item = GetItemObjectById(id);
        AddItem(item, character);
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
        
        AudioManager.Instance.PlayMusic(bossInfo.music, loop: true);
    }

    public void StopBossFight()
    {
        var bossUI = FindObjectOfType<BossUI>();
        bossUI.Disable();

        AudioManager.Instance.StopMusic();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void AddAchievement(AchievementSO achievement)
    {
        Debug.Log($"Got achievement {achievement.displayName}");
    }

    private void StartDelayedAction(float time, Action action) 
    {
        Debug.Log($"Started delayed action - {time} seconds");

        StartCoroutine(DelayedActionCoroutine(time, action));
    }
    private IEnumerator DelayedActionCoroutine(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action.Invoke();
        Debug.Log($"Delayed action complete");
    }
}
