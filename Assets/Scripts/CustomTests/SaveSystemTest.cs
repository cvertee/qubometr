using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public static class SaveSystemTest
{
    public static void Start()
    {
        const string TEST_PROFILE_NAME = "TEST";
        var SavePath = $"{Application.persistentDataPath}/{TEST_PROFILE_NAME}_save.sav";
        var player = GameManager.Instance.GetPlayer();
        var sceneName = SceneManager.GetActiveScene().name;
        var coinsCount = GameData.Data.coins;

        SaveSystem.SetCurrentProfile(TEST_PROFILE_NAME);
        Assert.IsTrue(SaveSystem.GetCurrentProfile() == TEST_PROFILE_NAME);

        SaveSystem.Save();
        Assert.IsTrue(File.Exists(SavePath), $"There should be save with {TEST_PROFILE_NAME} profile");
        SaveSystem.Remove();
        Assert.IsFalse(File.Exists(SavePath), $"There should be NO save with {TEST_PROFILE_NAME} profile");
        
        SaveSystem.Save();

        // Load current save from file and start comparing fields
        var save = SaveSystem.GetSaveData();
        var currentPosition = new SerializableVector3(player.transform.position);
        // Assert.IsTrue(save.playerPosition == currentPosition, "Wrong saved player location"); // TODO: float == fix
        Assert.IsTrue(save.sceneName == sceneName, "Wrong saved scene");
        Assert.IsTrue(save.coins == coinsCount, "Wrong saved coins");
        
        SaveSystem.Remove();

        Debug.Log($"{nameof(SaveSystemTest)}: OK.");
    }
}
