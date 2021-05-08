using System;
using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int amount = 1;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameData.Data.coins += amount;
            GameData.Data.totalCollectedCoins += amount;
            GameEvents.onCoinPickup.Invoke();
            AudioManager.Instance.PlaySound("coin_pickup");
            Destroy(gameObject);
        }
    }
}
