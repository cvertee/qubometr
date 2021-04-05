using Assets.Scripts.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class DebugUi : MonoBehaviour
    {
        private void OnGUI()
        {
            if (GUILayout.Button("Print save json"))
            {
                var json = JsonUtility.ToJson(GameData.Instance);
                Debug.Log(json);
            }
        }
    }
}
