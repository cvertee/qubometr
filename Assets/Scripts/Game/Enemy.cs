using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Weapon")
            hp -= 10;

        if (hp <= 0)
            Die();
    }

    void Die()
    {
        Instantiate(Resources.Load("Prefabs/BloodParticle"), transform.position, Quaternion.identity);
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
