using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour, ITakesDamage
{
    public Vector3 direction = Vector3.left;
    public float speed = 20.0f;

    protected float lifeTime = 2.5f;

    private void Start()
    {
        StartCoroutine(LifetimeCoroutine());
    }
    
    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Die();
    }
    

    public void TakeDamage(float damage)
    {
        Die(); // Explode on any damage
    }

    private IEnumerator LifetimeCoroutine()
    {
        yield return new WaitForSeconds(lifeTime);
        Die();
    }

    private void Die()
    {
        Instantiate(Resources.Load("Prefabs/ExplosionParticle"), transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
