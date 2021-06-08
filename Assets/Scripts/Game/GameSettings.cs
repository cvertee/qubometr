using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : Singleton<GameSettings>
{
    public static float GlobalAiTickTime => Instance.gameSettings.aiTickTime;
    public static float GlobalDamageToEnemiesMutliplier => Instance.gameSettings.damageMultiplierToEnemies;
    public static float GlobalDamageReceiveMultiplier => Instance.gameSettings.damageReceiveMultiplier;
    public static float GlobalShieldImperviousTimeMultiplier => Instance.gameSettings.imperviousToDamageTimeMultiplier;
    public static float GlobalDialogueTime => Instance.gameSettings.dialogueTimeSeconds;

    GameSettingsSO gameSettings;

    private void Awake() 
    {
        gameSettings = Resources.Load<GameSettingsSO>("GameSettings/EasyMode");
    }
}
