using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using Zenject;

public class DebugUi : MonoBehaviour
{
    private bool isShown = false;

    private AudioManager audioManager;
    private GameManager gameManager;
    private StoreUI storeUI;

    [Inject]
    public void Init(
        AudioManager audioManager,
        GameManager gameManager,
        StoreUI storeUI)
    {
        this.audioManager = audioManager;
        this.gameManager = gameManager;
        this.storeUI = storeUI;
    }

    public void OnDebugMenuToggle(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            isShown = !isShown;
    }

    private void OnGUI()
    {
#if DEBUG
        if (!isShown)
            return;

        if (GUILayout.Button("Try save"))
        {
            SaveSystem.Save();
        }

        if (GUILayout.Button("Try load"))
        {
            SaveSystem.Load();
        }

        if (GUILayout.Button("Add 100 coins"))
        {
            GameData.Data.coins += 100;
        }

        if (GUILayout.Button("Zero coins"))
        {
            GameData.Data.coins = 0;
        }

        if (GUILayout.Button("999 hp"))
        {
            GameData.IncreaseHealth(999.0f);
        }

        if (GUILayout.Button("-10 hp"))
        {
            GameData.DecreaseHealth(10f);
        }

        if (GUILayout.Button("Give knife"))
        {
            gameManager.AddItemById("Knife", FindObjectOfType<Player>());
        }
        if (GUILayout.Button("Give sword"))
        {
            gameManager.AddItemById("Sword", FindObjectOfType<Player>());
        }
        if (GUILayout.Button("Give shield"))
        {
            gameManager.AddItemById("ShieldPlaceholder", FindObjectOfType<Player>());
        }

        if (GUILayout.Button("Open example store"))
        {
            var item = gameManager.GetItemObjectById("HeavyArmor");
            storeUI.Show(new List<ItemSO> { item.data });
        }

        if (GUILayout.Button("Close store"))
        {
            storeUI.Close();
        }

        if (GUILayout.Button("Raise health restore event"))
        {
            GameEvents.onHealthRestored.Invoke();
        }

        if (GUILayout.Button("Raise health damage event"))
        {
            GameEvents.onPlayerReceivedDamage.Invoke();
        }

        if (GUILayout.Button("Raise coin pickup event"))
        {
            GameEvents.onCoinPickup.Invoke();
        }

        if (GUILayout.Button("GODMODE"))
        {
            GameEvents.onGameSettingChanged.Invoke("GodMode");
        }
        if (GUILayout.Button("EASYMODE"))
        {
            GameEvents.onGameSettingChanged.Invoke("EasyMode");
        }
        if (GUILayout.Button("DefaultMode"))
        {
            GameEvents.onGameSettingChanged.Invoke("DefaultSettings");
        }
        if (GUILayout.Button("TP to closest store"))
        {
            var player = FindObjectOfType<Player>();
            var stores = FindObjectsOfType<Store>();

            player.transform.position = stores[0].transform.position; // TODO: find closest
        }
        if (GUILayout.Button("Play sound delayed (0.5 sec)"))
        {
            GameEvents.onDelayedActionRequested.Invoke(0.5f, () => audioManager.PlaySound(AudioResource.StoreBuy));
        }
        if (GUILayout.Button("Test SaveSystem"))
        {
            SaveSystemTest.Start();
        }
#endif
    }
}