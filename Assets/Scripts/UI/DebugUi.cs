using System.Collections.Generic;
using UnityEngine;

public class DebugUi : MonoBehaviour
{
    private bool isShown = false;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
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
            GameData.Data.hp = 999;
        }

        if (GUILayout.Button("-10 hp"))
        {
            GameData.Data.hp -= 10;
        }

        if (GUILayout.Button("Give knife"))
        {
            GameManager.Instance.AddItemById("Knife", FindObjectOfType<Player>());
        }
        if (GUILayout.Button("Give sword"))
        {
            GameManager.Instance.AddItemById("Sword", FindObjectOfType<Player>());
        }
        if (GUILayout.Button("Give shield"))
        {
            GameManager.Instance.AddItemById("ShieldPlaceholder", FindObjectOfType<Player>());
        }

        if (GUILayout.Button("Open example store"))
        {
            var item = GameManager.Instance.GetItemObjectById("HeavyArmor");
            StoreUI.Instance.Show(new List<Item> {item.GetComponent<Item>()});
        }

        if (GUILayout.Button("Close store"))
        {
            StoreUI.Instance.Close();
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
#endif
    }
}