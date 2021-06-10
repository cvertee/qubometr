using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : Singleton<GameSettings>
{
    // TODO: this shit will make me insane
    // TOOD: then fix it
    public static float GlobalAiTickTime => Instance.gameSettings.aiTickTime;
    public static float GlobalDamageToEnemiesMutliplier => Instance.gameSettings.damageMultiplierToEnemies;
    public static float GlobalDamageReceiveMultiplier => Instance.gameSettings.damageReceiveMultiplier;
    public static float GlobalShieldImperviousTimeMultiplier => Instance.gameSettings.imperviousToDamageTimeMultiplier;
    public static float GlobalDialogueTime => Instance.gameSettings.dialogueTimeSeconds;
    public static float GlobalPriceMultiplier => Instance.gameSettings.priceMultiplier;
    public static float GlobalHealthKitMultiplier => Instance.gameSettings.healthKitMultiplier;

    GameSettingsSO gameSettings;

    private void Awake()
    {
        GameEvents.onGameSettingChanged.AddListener((setting) =>
        {
            Debug.Log($"[GameSettings] Changing setting to {setting}");
            gameSettings = LoadSetting(setting);
        });

        GameEvents.onGameSettingChanged.Invoke("DefaultSettings");
    }

    private GameSettingsSO LoadSetting(string settingName) => Resources.Load<GameSettingsSO>($"GameSettings/{settingName}");
}
