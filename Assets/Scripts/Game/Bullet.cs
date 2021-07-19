using System.Collections;
using UnityEngine;
using Zenject;

public class Bullet : MonoBehaviour, ITakesDamage
{
    public Vector3 direction = Vector3.left;
    public float damage = 10.0f;
    public float speed = 20.0f;
    public GameObject owner;

    protected float lifeTime = 2.5f;

    private GameSettingsSO gameSettings;

    [Inject]
    public void Init(GameSettingsSO gameSettings)
    {
        this.gameSettings = gameSettings;
    }

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
        var damageable = collision.gameObject.GetComponent<ITakesDamage>();
        if (damageable != null && owner != collision.gameObject)
        {
            damageable.TakeDamage(damage * gameSettings.damageMultiplierToEnemies);
            Die();
        }
        else if (!collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
        
        
        //Die();
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
        //Instantiate(Resources.Load("Prefabs/ExplosionParticle"), transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
