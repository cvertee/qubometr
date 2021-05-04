using System.IO;
using Assets.Scripts.Game;
using Assets.Scripts.Save;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Save
{
    public static class SaveSystem
    {
        public static string SAVE_FILE = Application.persistentDataPath + "/save.sav"; // TODO: probably several profiles!
            
        public static void Save()
        {
            //var saveData = new SaveData
            //{
            //    sceneName = GameData.Instance.sceneName,
            //    hp = GameData.Instance.HP,
            //    coins = GameData.Instance.coins
            //};
            //saveData.InitPlayerPosition(GameData.Instance.GetPlayer().transform.position);

            var player = GameManager.Instance.GetPlayer();
            var playerPos = new SerializableVector3(player.transform.position);
            GameData.Data.playerPosition = playerPos;

            var json = JsonConvert.SerializeObject(GameData.Data);
            Debug.Log($"Saving json to {SAVE_FILE} | {json}");

            if (!File.Exists(SAVE_FILE))
            {
                var handle = File.Create(SAVE_FILE);
                handle.Close();
            }
            
            File.WriteAllText(SAVE_FILE, json);
        }

        public static void Load()
        {
            if (!File.Exists(SAVE_FILE))
                return;
            
            var json = File.ReadAllText(SAVE_FILE);
            var saveData = JsonConvert.DeserializeObject<SaveData>(json);

            GameData.Data = saveData;
            SceneManager.LoadScene(saveData.sceneName, LoadSceneMode.Single);
        }
    }
}