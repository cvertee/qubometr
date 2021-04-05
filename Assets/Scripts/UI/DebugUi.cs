using Assets.Scripts.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class DebugUi : MonoBehaviour
    {
        private void OnGUI()
        {
            if (GUILayout.Button("Print save json"))
            {
                var json = JsonUtility.ToJson(GameData.Instance.GetSaveData());
                Debug.Log(json);
            }

            if (GUILayout.Button("Try load from json"))
            {
                var json = "{\"hp\":50,\"coins\":50,\"sceneName\":\"Dev\"}";
                Debug.Log(json);
                var saveData = JsonUtility.FromJson<SaveData>(json);
                GameData.Instance.InitFromSaveData(saveData);
                SceneManager.LoadScene(saveData.sceneName);
            }
        }
    }
}
