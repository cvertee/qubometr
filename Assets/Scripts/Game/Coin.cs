using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameData.Data.coins += 15;
            GameData.Data.totalCollectedCoins += 15;
            AudioManager.Instance.PlaySound("coin_pickup");
            Destroy(gameObject);
        }
    }
}
