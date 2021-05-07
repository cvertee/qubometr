using Assets.Scripts.Game;
using Core;
using UnityEngine;

namespace Game
{
    public class Barrel : MonoBehaviour, ITakesDamage
    {
        [SerializeField] int coinAmount = 15;
        [SerializeField] float hp;
        
        public void TakeDamage(float damage)
        {
            hp -= damage;

            if (hp <= 0)
            {
                AudioManager.Instance.PlaySound("barrelBreak");
                for (var i = 0; i < coinAmount; i++)
                {
                    // TODO: USE POOL!!!!!!!!!!!
                    Instantiate(Resources.Load("Prefabs/Coin"), transform.position + new Vector3(UnityEngine.Random.Range(0, 7), 0), Quaternion.identity);
                }
                Destroy(gameObject);
                return;
            }
            
            AudioManager.Instance.PlaySound("barrelHit");
        }
    }
}