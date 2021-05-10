using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public float hp;
    public int coins;
    public string sceneName;
    public string videoToLoad;
    public List<string> killedEnemies = new List<string>();
    public List<string> playerItemIds = new List<string>();
    public SerializableVector3 playerPosition;
    public List<Key> pickedUpKeys = new List<Key>();
    
    // 
    // STATS
    //
    public int killedEnemiesCount;
    public float gameTimeMs; // How many milliseconds player wasted
    public int totalCollectedCoins;
    public int totalWastedCoins;
    public float totalDamageReceived;
    public int healthKitsUsed;
}
