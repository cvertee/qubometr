using UnityEngine;

public class Barrel : DestroyerBase, ITakesDamage
{
    [SerializeField] private int coinAmount = 15;
    [SerializeField] private float hp;

    public void TakeDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            GameEvents.onAudioNamePlayRequested.Invoke(AudioResource.BarrelBreak);
            for (var i = 0; i < coinAmount; i++)
            {
                // TODO: USE POOL!!!!!!!!!!!
                Instantiate(Resources.Load("Prefabs/Coin"),
                    transform.position + new Vector3(Random.Range(0, 7), 0), Quaternion.identity);
            }

            var barrelParts = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/BarrelDestroyed"), transform.position, Quaternion.identity);
            barrelParts.transform.position = transform.position;
            barrelParts.transform.localScale = transform.localScale;
            DestroySave();
            return;
        }

        GameEvents.onAudioNamePlayRequested.Invoke(AudioResource.BarrelHit);
    }
}