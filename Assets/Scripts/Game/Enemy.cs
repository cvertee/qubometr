using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Weapon")
        {
            AudioManager.Instance.PlaySound("slashkut");
            hp -= 10;
        }

        if (hp <= 0)
            Die();
    }

    void Die()
    {
        Instantiate(Resources.Load("Prefabs/BloodParticle"), transform.position, Quaternion.identity);
        Instantiate(Resources.Load("Prefabs/Coin"), transform.position, Quaternion.identity);
        Destroy(gameObject);
        //GetComponent<BoxCollider2D>().enabled = false;
        //StartCoroutine(DieCoroutine());
    }
    IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(1.25f);
        Destroy(gameObject);
    }
}
