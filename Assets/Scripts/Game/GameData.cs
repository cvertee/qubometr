using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameData
{
    private static SaveData data;

    public static SaveData Data
    {
        get
        {
            if (data == null)
            {
                data = new SaveData
                {
                    coins = 0,
                    hp = 50,
                    sceneName = SceneManager.GetActiveScene().name
                };
            }

            return data;
        }

        set { data = value; }
    }

    public static void AddKey(Key key)
    {
        Debug.Log($"Key {key.name} was added");
        Data.pickedUpKeys.Add(key);
    }

    public static float GetHealth() => Data.hp;

    public static float IncreaseHealth(float health)
    {
        data.hp += health;

        GameEvents.onPlayerHealthModified.Invoke(data.hp);
        GameEvents.onHealthRestored.Invoke();
        
        return data.hp;
    }

    public static float DecreaseHealth(float health)
    {
        data.hp -= health;
        Data.totalDamageReceived += health;

        GameEvents.onPlayerHealthModified.Invoke(data.hp);
        GameEvents.onPlayerReceivedDamage.Invoke();

        return data.hp;
    }
}