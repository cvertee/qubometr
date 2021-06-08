using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSetting", menuName = "Game Settings/Game setting", order = 0 )]
public class GameSettingsSO : ScriptableObject
{
    public float aiTickTime;
    public float damageMultiplierToEnemies;
    public float damageReceiveMultiplier;
    public float imperviousToDamageTimeMultiplier;
    public float dialogueTimeSeconds = 2.0f;
}
