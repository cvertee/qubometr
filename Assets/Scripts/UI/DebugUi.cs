using Assets.Scripts.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Save;
using UnityEngine;
using UnityEngine.SceneManagement;

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
#endif
        }
    }
}
