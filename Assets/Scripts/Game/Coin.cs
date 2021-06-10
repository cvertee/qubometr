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
            GameEvents.onAudioNamePlayRequested.Invoke("coin_pickup");
            Destroy(gameObject);
        }
    }
}
