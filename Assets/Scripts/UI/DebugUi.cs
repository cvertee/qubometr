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
        private void OnGUI()
        {
            if (GUILayout.Button("Try save"))
            {
                SaveSystem.Save();
            }

            if (GUILayout.Button("Try load"))
            {
                SaveSystem.Load();
            }
        }
    }
}
