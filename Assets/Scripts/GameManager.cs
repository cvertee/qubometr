using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Zenject;

public class GameManager : MonoBehaviour
{
    private AudioManager audioManager;
    private Item.Factory itemFactory;
    private GameSettingsSO gameSettings;
    private IItemDatabase itemDatabase;

    [Inject]
    private void Init(
        AudioManager audioManager,
        Item.Factory itemFactory,
        GameSettingsSO gameSettings,
        IItemDatabase itemDatabase)
    {
        this.audioManager = audioManager;
        this.gameSettings = gameSettings;
        this.itemFactory = itemFactory;
        this.itemDatabase = itemDatabase;
    }
    
    private void Awake()
    {
        Debug.Log("Gamemanager awake", this);

        GameEvents.onHealthKitUseStart.AddListener(() =>
        {
            audioManager.PlaySound(AudioResource.HealthKitUse);
        });
        GameEvents.onHealthKitUseEnd.AddListener(() =>
        {
            GameData.IncreaseHealth(10f * gameSettings.healthKitMultiplier);
            GameData.Data.healthKitsUsed += 1;
        });
        GameEvents.onAudioClipPlayRequested.AddListener(clip => 
        {
            Debug.Log($"Playing raw audio clip `{clip.name}`");

            audioManager.PlayClip(clip);
        });
        GameEvents.onAudioNamePlayRequested.AddListener(audioName => 
        {
            Debug.Log($"Playing audio clip by name `{audioName}`");

            audioManager.PlaySound(audioName);
        });
        GameEvents.onDelayedActionRequested.AddListener((time, action) => 
        {
            StartDelayedAction(time, action);
        });
    }

    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("Gamemanager start", this);
        GameData.Data.sceneName = SceneManager.GetActiveScene().name;
        //Application.targetFrameRate = Screen.currentResolution.refreshRate;

        GameEvents.onEnemyAlert.AddListener(() => audioManager.PlaySound(AudioResource.EnemyAlert));
        GameEvents.onPlayerDeath.AddListener(() => SaveSystem.Load());

        SceneManager.sceneLoaded += (scene, mode) => GameEvents.onLocationStart.Invoke();
    }

    public void OnMenu(InputAction.CallbackContext ctx)
    {
        if (!ctx.started) 
            return;
        
        if (!UIManager.Instance.HasPopupElements)
            MenuUI.Instance.Show();
    }

    public void OnGoBack(InputAction.CallbackContext ctx)
    {
        if (!ctx.started)
            return;
        
        if (UIManager.Instance.HasPopupElements)
        {
            UIManager.Instance.CloseLatestPopup();
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
        var itemData = itemDatabase.GetItemDataByName(id);
        var item = itemFactory.Create(gameSettings);
        item.Initialize(itemData);
        
        return item;
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
        
        audioManager.PlayMusic(bossInfo.music, loop: true);
    }

    public void StopBossFight()
    {
        var bossUI = FindObjectOfType<BossUI>();
        bossUI.Disable();

        audioManager.StopMusic();
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
