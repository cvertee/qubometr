using Assets.Scripts.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Save;
using UnityEngine;
using UnityEngine.SceneManagement;
using Game;

namespace Assets.Scripts.UI
{
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

            if (GUILayout.Button("Open example store"))
            {
                var item = GameManager.Instance.GetItemObjectById("HeavyArmor");
                StoreUI.Instance.Show(new List<Item> { item.GetComponent<Item>() });
            }
            if (GUILayout.Button("Close store"))
            {
                StoreUI.Instance.Close();
            }
#endif
        }
    }
}
