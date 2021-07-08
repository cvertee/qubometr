using UnityEngine;
using Zenject;

public class Coin : MonoBehaviour
{
    public int amount = 1;

    private AudioManager audioManager;

    [Inject]
    public void Init(AudioManager audioManager)
    {
        this.audioManager = audioManager;
    }
    
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
            audioManager.PlaySound(AudioResource.CoinPickup);
            Destroy(gameObject);
        }
    }

    public class Factory : PlaceholderFactory<AudioManager, Coin>
    {
    }
}
