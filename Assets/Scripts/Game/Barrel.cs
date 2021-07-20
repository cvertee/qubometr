using System;
using Game;
using UnityEngine;
using Zenject;

public class Barrel : MonoBehaviour, ITakesDamage
{
    [SerializeField] private int coinAmount = 15;
    [SerializeField] private float hp;
    private DestroyerBase destroyer;

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

    private void Awake()
    {
        destroyer = GetComponent<DestroyerBase>();
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

            var barrelParts = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Props/BarrelDestroyed"), transform.position, Quaternion.identity);
            barrelParts.transform.position = transform.position;
            barrelParts.transform.localScale = transform.localScale;
            destroyer.DestroySave();
            return;
        }

        audioManager.PlaySound(AudioResource.BarrelHit);
    }
}