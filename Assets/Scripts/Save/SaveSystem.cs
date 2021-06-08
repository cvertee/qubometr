using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveSystem
{
    private static string currentProfile;
    public static string GetCurrentProfile() => currentProfile;
    public static void SetCurrentProfile(string value)
    {
        Debug.Log($"Setting current profile to {value} (previous {currentProfile})");
        currentProfile = value;
    }

    private static string fullPath => FullProfilePath(currentProfile);
    private static string FullProfilePath(string profile) 
        => $"{Application.persistentDataPath}/{profile}_save.sav";

    public static void EmptyAllData()
    {
        GameData.Data = null;
        currentProfile = null;
    }
    
    public static void Save()
    {
        var player = GameManager.Instance.GetPlayer();
        var playerPos = new SerializableVector3(player.transform.position);
        GameData.Data.playerPosition = playerPos;
        GameData.Data.profile = currentProfile;
        
        WriteSave(GameData.Data);
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

    public static void RemoveProfile(string profile)
    {
        File.Delete(FullProfilePath(profile));
    }
    
    public static SaveData GetSaveData()
    {
        if (!File.Exists(fullPath))
            return null;
        
        return LoadSaveAtPath(fullPath);
    }

    public static List<SaveData> GetAllSaves()
    {
        var result = new List<SaveData>();
        var savePaths = Directory.GetFiles(Application.persistentDataPath).Where(x => x.Contains(".sav")).ToList();

        foreach (var savePath in savePaths)
        {
            var saveData = LoadSaveAtPath(savePath);
            if (saveData != null)
                result.Add(saveData);
        }

        return result;
    }

    private static SaveData LoadSaveAtPath(string fullPathToSave)
    {
        try
        {
            var json = File.ReadAllText(fullPathToSave);
            return JsonUtility.FromJson<SaveData>(json);
        }
        catch
        {
            return null;
        }
    }

    private static void WriteSave(SaveData saveData)
    {
        var json = JsonUtility.ToJson(saveData);
        Debug.Log($"Saving json to {fullPath} | {json}");

        if (!File.Exists(fullPath))
        {
            var handle = File.Create(fullPath);
            handle.Close();
        }
        
        File.WriteAllText(fullPath, json);
    }
}