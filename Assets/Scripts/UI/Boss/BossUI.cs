using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    public Slider bossHp;
    public Text bossName;

    private Enemy boss;

    private bool isEnabled = false;

    public void Initialize(BossInfoSO bossInfo, Enemy boss)
    {
        bossHp.gameObject.SetActive(true);
        bossName.gameObject.SetActive(true);
        
        bossHp.maxValue = bossInfo.health;
        bossName.text = bossInfo.displayName;

        this.boss = boss;

        isEnabled = true;
    }

    public void Disable()
    {
        bossHp.gameObject.SetActive(false);
        bossName.gameObject.SetActive(false);

        isEnabled = false;
    }

    private void Update()
    {
        if (!isEnabled)
            return;
        
        bossHp.value = boss.hp;
    }
}
