using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        var saveData = GetSaveData();

        if (saveData == null)
        {
            Debug.LogWarning("SaveSystem.Load: No save file");
            return;
        }

        GameData.Data = saveData;
        SceneManager.LoadScene(saveData.sceneName, LoadSceneMode.Single);
    }

    public static SaveData GetSaveData()
    {
        if (!File.Exists(SAVE_FILE))
            return null;

        var json = File.ReadAllText(SAVE_FILE);
        return JsonConvert.DeserializeObject<SaveData>(json);
    }
}