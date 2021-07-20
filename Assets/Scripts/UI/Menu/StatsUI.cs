using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;
using UnityEngine.UI;

public class StatsUI : Singleton<StatsUI>, IPopupUIElement
{
    public GameObject statsContainer;
    public Text statsText;
    
    // Start is called before the first frame update
    private void Start()
    {
        Close();
    }

    public void Show()
    {
        UIManager.Instance.RegisterPopup(this);
        statsContainer.SetActive(true);
        
        var data = GameData.Data;
        var sb = new StringBuilder();

        // sb.AppendLine($"{LocalizationUtil.IdToLocalized("#StatMoneyWaste")}: {data.totalWastedCoins}");
        // sb.AppendLine($"{LocalizationUtil.IdToLocalized("#StatMoneyCollected")}: {data.totalCollectedCoins}");
        // sb.AppendLine($"{LocalizationUtil.IdToLocalized("#StatDamageReceieved")}: {data.totalDamageReceived}");
        // //sb.AppendLine($"Игровое время: {data.gameTimeMs / 1000} секунд");
        // sb.AppendLine($"{LocalizationUtil.IdToLocalized("#StatKilledEnemyCount")}: {data.killedEnemiesCount}");
        // sb.AppendLine($"{LocalizationUtil.IdToLocalized("#StatHealthKitsUsed")}: {data.healthKitsUsed}");

        statsText.text = sb.ToString();
    }
    
    public void Close()
    {
        statsContainer.SetActive(false);
    }
}
