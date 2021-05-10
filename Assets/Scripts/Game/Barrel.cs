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
            AudioManager.Instance.PlaySound("barrelBreak");
            for (var i = 0; i < coinAmount; i++)
            {
                // TODO: USE POOL!!!!!!!!!!!
                Instantiate(Resources.Load("Prefabs/Coin"),
                    transform.position + new Vector3(Random.Range(0, 7), 0), Quaternion.identity);
            }

            DestroySave();
            return;
        }

        AudioManager.Instance.PlaySound("barrelHit");
    }
}