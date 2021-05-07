using System.Collections;
using System.Collections.Generic;
using System.Text;
using Assets;
using Assets.Scripts.Core;
using Assets.Scripts.Game;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : Singleton<StatsUI>, IPopupUIElement
{
    public GameObject statsContainer;
    public Text statsText;
    
    // Start is called before the first frame update
    void Start()
    {
        Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        UIManager.Instance.RegisterPopup(this);
        statsContainer.SetActive(true);
        
        var data = GameData.Data;
        var sb = new StringBuilder();

        sb.AppendLine($"Потрачено монет: {data.totalWastedCoins}");
        sb.AppendLine($"Собрано монет: {data.totalCollectedCoins}");
        sb.AppendLine($"Всего урона получено: {data.totalDamageReceived}");
        sb.AppendLine($"Игровое время: {data.gameTimeMs / 1000} секунд");
        sb.AppendLine($"Количество уничтоженных противников: {data.killedEnemiesCount}");
        sb.AppendLine($"Количество выпитых целебных зелий: {data.healthKitsUsed}");

        statsText.text = sb.ToString();
    }
    
    public void Close()
    {
        statsContainer.SetActive(false);
    }
}
