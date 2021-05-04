using Assets.Scripts.Save;
using Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
