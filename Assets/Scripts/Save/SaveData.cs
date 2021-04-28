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
    public float playerX;
    public float playerY;
    public float playerZ;

    public void InitPlayerPosition(Vector3 pos)
    {
        playerX = pos.x;
        playerY = pos.y;
        playerZ = pos.z;
    }

    public Vector3 GetPlayerPosition()
    {
        return new Vector3(playerX, playerY, playerZ);
    }
}
