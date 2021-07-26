using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public BossInfoSO bossInfo;

    private void Awake()
    {
        GetComponent<Enemy>().enemyInfo.hp = bossInfo.health;
    }
}
