using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction = Vector3.left;
    public float speed = 20.0f;

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var collisionGO = collision.gameObject;
        
        if (collisionGO.CompareTag("Player"))
        {
            var player = collisionGO.GetComponent<Player>();
            player.AddDamage(); // TODO: IDamageReceiver.SendDamage() ?
        }
        
        Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
            Die();
    }

    private void Die()
    {
        Instantiate(Resources.Load("Prefabs/ExplosionParticle"), transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
