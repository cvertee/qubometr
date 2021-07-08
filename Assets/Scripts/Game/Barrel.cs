using Game;
using UnityEngine;
using Zenject;

public class Barrel : DestroyerBase, ITakesDamage
{
    [SerializeField] private int coinAmount = 15;
    [SerializeField] private float hp;

    private AudioManager audioManager;
    private CoinSpawner coinSpawner;
    
    [Inject]
    public void Init(
        AudioManager audioManager, 
        CoinSpawner coinSpawner)
    {
        this.audioManager = audioManager;
        this.coinSpawner = coinSpawner;
    }
    
    public void TakeDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            audioManager.PlaySound(AudioResource.BarrelBreak);
            for (var i = 0; i < coinAmount; i++)
            {
                coinSpawner.Spawn(transform.position);
            }

            var barrelParts = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/BarrelDestroyed"), transform.position, Quaternion.identity);
            barrelParts.transform.position = transform.position;
            barrelParts.transform.localScale = transform.localScale;
            DestroySave();
            return;
        }

        audioManager.PlaySound(AudioResource.BarrelHit);
    }
}